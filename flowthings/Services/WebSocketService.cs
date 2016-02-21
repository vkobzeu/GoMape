using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Diagnostics;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using WebSocketSharp;

using flowthings.Util;

namespace flowthings.Services
{
    public class WebSocketService
    {
        private Token creds;
        private bool secure;
        private string host;
        
        private WebSocket ws;
        private Dictionary<int, OnResponse> replyCallbacks;
        private int replyId;

        private readonly object _syncRID = new Object();

        /// <summary>
        /// True if the websocket is connected
        /// </summary>
        public bool connected { get; private set; }
        

        /// <summary>
        /// Create the service
        /// </summary>
        /// <param name="creds"></param>
        /// <param name="secure"></param>
        /// <param name="host"></param>
        public WebSocketService(Token creds, bool secure, string host)
        {
            this.creds = creds;
            this.secure = secure;
            this.host = host;

            this.ws = null;
            this.replyCallbacks = new Dictionary<int, OnResponse>();
            this.replyId = 0;
            this.connected = false;
        }


        /// <summary>
        /// Handles the Websocket open event
        /// </summary>
        public delegate void OnOpenHandler();

        /// <summary>
        /// Fired when the Websocket is opened
        /// </summary>
        public event OnOpenHandler OnOpen;


        /// <summary>
        /// Handles the Websocket close event
        /// </summary>
        /// <param name="reason">A message indicating the reason for the close</param>
        /// <param name="wasClean">True if this was a clean (error-free) close,
        /// otherwise false</param>
        public delegate void OnCloseHandler(string reason, bool wasClean);


        /// <summary>
        /// Fired when a Websocket is closed (either on purpose or because of an error)
        /// </summary>
        public event OnCloseHandler OnClose;


        /// <summary>
        /// Handles a Websocket error. 
        /// </summary>
        /// <param name="message">The error message</param>
        public delegate void OnErrorHandler(string message);


        /// <summary>
        /// Fires when there is an error with the Websocket
        /// </summary>
        public event OnErrorHandler OnError;


        /// <summary>
        /// Handles a message over WS.  The value comes from the platform and depends
        /// on the resource type (flow, drop, track)
        /// </summary>
        /// <param name="resource">The resource</param>
        /// <param name="value">The object from the platform</param>
        public delegate void OnMessageHandler(string resource, dynamic value);


        /// <summary>
        /// Fired when a message is received
        /// </summary>
        public event OnMessageHandler OnMessage;


        /// <summary>
        /// Called when there is a response to a message sent to the platform
        /// </summary>
        /// <param name="body">The response body from the platform</param>
        public delegate void OnResponse(dynamic body);


        /// <summary>
        /// Gets a sessionId from the server and returns it
        /// </summary>
        protected async Task<string> GetSessionId()
        {
            HttpWebRequest req = WebRequest.Create(this.MakeHTTPURL()) as HttpWebRequest;

            req.Method = "POST";
            this.SetHeaders(req);

            using (var res = await req.GetResponseAsync().ConfigureAwait(false) as HttpWebResponse)
            {
                if ((int)res.StatusCode >= 200 && (int)res.StatusCode < 400)
                {
                    using (var sr = new StreamReader(res.GetResponseStream()))
                    {
                        try
                        {
                            string json = await sr.ReadToEndAsync().ConfigureAwait(false);
                            JObject jo = JObject.Parse(json);

                            string sessionId = (string)jo["body"]["id"];
                            return sessionId;
                        }
                        catch
                        {
                            throw new FlowThingsException();
                        }
                    }
                }
                else
                {
                    JObject jo = null;

                    try
                    {
                        using (var sr = new StreamReader(res.GetResponseStream()))
                        {
                            string json = await sr.ReadToEndAsync().ConfigureAwait(false);
                            jo = JObject.Parse(json);
                        }
                    }
                    catch { }

                    switch ((int)res.StatusCode)
                    {
                        case 400: throw new FlowThingsBadRequestException(jo);
                        case 403: throw new FlowThingsForbiddenException(jo);
                        case 404: throw new FlowThingsNotFoundException(jo);
                        case 500: throw new FlowThingsServerErrorException(jo);
                        default: throw new FlowThingsException(jo);
                    }
                }
            }

        }


        /// <summary>
        /// Connects the WebSocket to the platform.
        /// </summary>
        public async void Connect()
        {
            string sessionId = await this.GetSessionId().ConfigureAwait(false);
            string wsURL = this.MakeWSURL(sessionId);

            this.ws = new WebSocket(wsURL);
            this.ws.OnOpen += ws_OnOpen;
            this.ws.OnMessage += ws_OnMessage;
            this.ws.OnClose += ws_OnClose;
            this.ws.OnError += ws_OnError;

            this.ws.Connect();
        }


        /// <summary>
        /// Disconnects the service
        /// </summary>
        public void Disconnect()
        {
            try
            {
                this.ws.Close();
                this.ws.Dispose();
                this.ws = null;
            }
            catch { }
        }


        /// <summary>
        /// Disposes of the service
        /// </summary>
        public void Dispose()
        {
            this.Disconnect();
        }


        /// <summary>
        /// Sends the OnError message when an error occurs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ws_OnError(object sender, ErrorEventArgs e)
        {
            if (OnError != null) OnError(e.Message);
        }


        /// <summary>
        /// Sends the OnClose message when the socket is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ws_OnClose(object sender, CloseEventArgs e)
        {
            this.connected = false;
            if (OnClose != null) OnClose(e.Reason, e.WasClean);
        }


        /// <summary>
        /// Either sends an OnMessage event or calls a subscription callback, depending on
        /// the kind of message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ws_OnMessage(object sender, MessageEventArgs e)
        {
            JObject data = JObject.Parse(e.Data);
            OnResponse callback;

            if ((string)data["type"] == "message")
            {
                if (OnMessage != null) OnMessage((string)data["resource"], ((dynamic)data).value);
            }

            else if (!Utilities.JTokenIsNullOrEmpty(data["head"]) && 
                !Utilities.JTokenIsNullOrEmpty(data["head"]["msgId"]))
            {
                int rid = (int)data["head"]["msgId"];
                if (this.replyCallbacks.TryGetValue(rid, out callback))
                {
                    callback(data["body"]);
                    this.replyCallbacks.Remove(rid);
                }

            }
        }


        /// <summary>
        /// Calls the onOpen delegate when the socket is opened
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ws_OnOpen(object sender, EventArgs e)
        {
            this.connected = true;
            if (this.OnOpen != null) OnOpen();
        }


        /// <summary>
        /// Subscribe to receive websocket updates from the resource.
        /// </summary>
        /// <param name="resourceId">The resource ID</param>
        /// <param name="callback">A callback to call when subscribed, or null</param>
        public void Subscribe(string resourceId, OnResponse callback = null)
        {
            this.Send("subscribe", "drop", new JProperty[] { new JProperty("flowId", resourceId) }, callback);
        }


        /// <summary>
        /// Unsubscribe from websocket updates from the resource.
        /// </summary>
        /// <param name="resourceId">The resource ID</param>
        /// <param name="callback">A callback to call when subscribed, or null</param>
        public void Unsubscribe(string resourceId, OnResponse callback = null)
        {
            this.Send("unsubscribe", "drop", new JProperty[] { new JProperty("flowId", resourceId) }, callback);
        }


        /// <summary>
        /// Create a new object via websockets.
        /// </summary>
        /// <param name="obj">The object type (flow, track) -- for drops use CreateDrop</param>
        /// <param name="value">The value object</param>
        /// <param name="callback">A callback to be called when a response is received</param>
        public void Create(string obj, dynamic value, OnResponse callback = null)
        {
            if (obj == "flow" || obj == "track")
                this.Send("create", obj, new JProperty[] { new JProperty("value", JToken.FromObject(value)) }, callback);
            else if (obj == "drop")
                throw new ArgumentException("Drops require a flowId, use the CreateDrop function instead");
            else
                throw new FlowThingsNotImplementedException();
        }


        /// <summary>
        /// Create a new object via websockets.
        /// </summary>
        /// <param name="flowId">The ID of the flow to create the drop in</param>
        /// <param name="value">The value object</param>
        /// <param name="callback">A callback to be called when a response is received</param>
        public void CreateDrop(string flowId, dynamic value, OnResponse callback = null)
        {
            JObject jo =
                new JObject(
                    new JProperty("elems", JToken.FromObject(value)));

            this.Send("create", "drop", new JProperty[] { new JProperty("flowId", flowId), new JProperty("value", jo) }, callback);
        }


        /// <summary>
        /// Update an object via websockets.
        /// </summary>
        /// <param name="obj">The object type (flow, track, or drop)</param>
        /// <param name="value">The value object</param>
        /// <param name="callback">A callback to be called when a response is received</param>
        public void Update(string obj, dynamic value, OnResponse callback = null)
        {
            if (obj == "drop" || obj == "flow" || obj == "track")
                this.Send("update", obj, new JProperty[] { new JProperty("value", JToken.FromObject(value)) }, callback);
            else
                throw new FlowThingsNotImplementedException();
        }


        /// <summary>
        /// Delete an object via websockets.
        /// </summary>
        /// <param name="obj">The object type (flow, track, or drop)</param>
        /// <param name="ids">The array of ids</param>
        /// <param name="callback">A callback to be called when a response is received</param>
        public void Delete(string obj, string[] ids, OnResponse callback = null)
        {
            if (obj == "drop" || obj == "flow" || obj == "track")
                this.Send("delete", obj, new JProperty[] { new JProperty("id", JArray.FromObject(ids)) }, callback);
            else
                throw new FlowThingsNotImplementedException();
        }


        /// <summary>
        /// Read an object via websockets.
        /// </summary>
        /// <param name="obj">The object type (flow, track, or drop)</param>
        /// <param name="ids">The array of ids</param>
        /// <param name="callback">A callback to be called when a response is received</param>
        public void Read(string obj, string[] ids, OnResponse callback = null)
        {
            if (obj == "drop" || obj == "flow" || obj == "track")
                this.Send("find", obj, new JProperty[] { new JProperty("id", JArray.FromObject(ids)) }, callback);
            else
                throw new FlowThingsNotImplementedException();
        }


        /// <summary>
        /// Send a heartbeat to the websocket server, should be done at a regular interval on a timer.
        /// </summary>
        public void SendHeartbeat()
        {
            this.Send("heartbeat", "", new JProperty[] { });
        }


        /// <summary>
        /// Send a message to the WS server.
        /// </summary>
        /// <param name="type">The type field, (create, update, delete, find)</param>
        /// <param name="obj">The object field (flow, drop, track)</param>
        /// <param name="props">Properties to be added to the message to be sent to the server</param>
        /// <param name="callback">A callback called when a response is received</param>
        protected void Send(string type, string obj, JProperty[] props, OnResponse callback = null)
        {
            if (this.ws == null || !this.connected)
                throw new FlowThingsNotConnectedException();

            JObject data =
                new JObject(
                    new JProperty("type", type),
                    new JProperty("object", obj));

            foreach (JProperty prop in props)
                data.Add(prop);

            if (callback != null)
            {
                lock (_syncRID)
                {
                    int rid = this.replyId;
                    data["id"] = rid;
                    this.replyCallbacks[rid] = callback;
                    this.replyId++;
                }
            }

            ws.Send(data.ToString());
        }


        /// <summary>
        /// Makes a WS URL
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        protected string MakeWSURL(string sessionId)
        {
            string s = (this.secure ? "wss:" : "ws:") + "//" + this.host + "/session/" +
                sessionId + "/ws";

            return s;
        }


        /// <summary>
        /// Makes an HTTP url to get the session ID
        /// </summary>
        /// <returns></returns>
        protected string MakeHTTPURL()
        {
            string s = (this.secure ? "https:" : "http:") + "//" + this.host + "/session";

            return s;
        }


        /// <summary>
        /// Sets the headers for an API request before it is sent.
        /// </summary>
        /// <param name="req"></param>
        protected void SetHeaders(HttpWebRequest req)
        {
            req.ContentType = "application/json";
            req.Headers["x-auth-token"] = creds.token;
            req.Headers["x-auth-account"] = creds.account;
        }
    }
}
