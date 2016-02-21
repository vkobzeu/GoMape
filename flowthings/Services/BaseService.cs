using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;

using flowthings.Util;

namespace flowthings.Services
{
    public class BaseService
    {
        protected Token creds;
        protected bool secure;
        
        protected string host;
        protected string version;

        protected bool canRead = false;
        protected bool canCreate = false;
        protected bool canUpdate = false;
        protected bool canDelete = false;

        public string basePath { get; set; }


        /// <summary>
        /// Constructs the service. Should only be used on its own if you want to
        /// make raw calls to the API.
        /// </summary>
        /// <param name="creds"></param>
        /// <param name="secure"></param>
        /// <param name="host"></param>
        /// <param name="version"></param>
        public BaseService(Token creds, bool secure, string host, string version)
        {
            this.creds = creds;
            this.secure = secure;
            this.host = host;
            this.version = version;

            this.basePath = "";
        }


        /// <summary>
        /// Constructs the service. This is an internal constructor.
        /// </summary>
        /// <param name="creds">The credentials token</param>
        /// <param name="secure">True if this should be a secure connection</param>
        /// <param name="host">The API host</param>
        /// <param name="version">The API version</param>
        /// <param name="canRead">True if the obj can be read</param>
        /// <param name="canCreate">True if the obj can be created</param>
        /// <param name="canUpdate">True if the obj can be updated</param>
        /// <param name="canDelete">True if the obj can be deleted</param>
        /// <param name="basePath">The base path</param>
        internal BaseService(Token creds, bool secure, string host, string version,
            bool canRead, bool canCreate, bool canUpdate, bool canDelete, string basePath)
            : this(creds, secure, host, version)
        {
            this.canRead = canRead;
            this.canCreate = canCreate;
            this.canUpdate = canUpdate;
            this.canDelete = canDelete;
            this.basePath = basePath;
        }


        #region API Requests


        /// <summary>
        /// Make an asynchronous request to the flowthings api.
        /// </summary>
        /// <param name="method">The method (GET, POST, DELETE, MGET, etc)</param>
        /// <param name="data">The post data, which must be encodable by the encoder of this service</param>
        /// <param name="url">The URL</param>
        /// <returns></returns>
        internal async Task<dynamic> RequestAsync(string method, JToken data, string url) 
        {
            HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;

            req.Method = method;
            this.SetHeaders(req);

            if (data != null)
            {
                string json = data.ToString();

                using (var sw = new StreamWriter(await req.GetRequestStreamAsync()))
                {
                    await sw.WriteAsync(json);
                }
            }

            try
            {
                using (var res = await req.GetResponseAsync() as HttpWebResponse)
                {
                    if ((int)res.StatusCode >= 200 && (int)res.StatusCode < 400)
                    {
                        using (var sr = new StreamReader(res.GetResponseStream()))
                        {
                            string json = await sr.ReadToEndAsync();
                            return JObject.Parse(json);
                        }
                    }
                    else
                    {
                        JObject jo = null;

                        using (var sr = new StreamReader(res.GetResponseStream()))
                        {
                            string json = await sr.ReadToEndAsync();
                            jo = JObject.Parse(json);
                        }

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
            catch (WebException e)
            {
                if ((e.Response as HttpWebResponse) != null)
                {
                    var response = e.Response as HttpWebResponse;
                    if (response != null)
                    {
                        switch ((int)response.StatusCode)
                        {
                            case 400: throw new FlowThingsBadRequestException(null);
                            case 403: throw new FlowThingsForbiddenException(null);
                            case 404: throw new FlowThingsNotFoundException(null);
                            case 500: throw new FlowThingsServerErrorException(null);
                            default: throw new FlowThingsException();
                        }
                    }
                    else
                    {
                        throw new FlowThingsException();
                    }
                }
                else
                {
                    throw new FlowThingsException();
                }
            }
        }

            
        /// <summary>
        /// Make an asynchronous request to the flowthings api.
        /// </summary>
        /// <param name="method">The method (GET, POST, DELETE, MGET, etc)</param>
        /// <param name="path">The path, which will be concatenated with the base path for the account</param>
        /// <param name="data">The post data, which must be encodable by the encoder of this service</param>
        /// <param name="parms">The request URL parameters</param>
        /// <returns>An object that was the result of deencoding the return JSON</returns>
        public async Task<dynamic> RequestAsync(string method, string path, JToken data = null, Dictionary<string, string> parms = null)
        {
            string url = this.MakeURL(path, parms);
            return await this.RequestAsync(method, data, url);
        }
        

        /// <summary>
        /// Creates a URL to be used with the Request function.
        /// </summary>
        /// <param name="path">the flowthings path to connect to</param>
        /// <param name="parms">the URL parameters</param>
        /// <returns>a flowthings URL as a string</returns>
        protected string MakeURL(string path, Dictionary<string, string> parms = null)
        {
            string s = (this.secure ? "https:" : "http:") + "//" + this.host + "/v" + this.version + "/" +
                this.creds.account + this.basePath + path;

            if (parms != null)
            {
                s += "?";

                foreach (KeyValuePair<string, string> kvp in parms)
                {
                    s += WebUtility.UrlEncode(kvp.Key) + "=" + WebUtility.UrlEncode(kvp.Value) + "&";
                }
            }

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

        #endregion

        #region Findable Service

        /// <summary>
        /// Reads an item from the server by ID and decodes it with the encoder.
        /// </summary>
        /// <typeparam name="T">The type of the item expected as return</typeparam>
        /// <param name="id">The id of the item</param>
        /// <param name="encoder">An encoder that can parse T from a JToken</param>
        /// <param name="parms">The parameters to pass to the platform</param>
        /// <returns>An item of type T from the platform</returns>
        public async Task<T> Read<T>(string id, IJsonEncoder<T> encoder, 
            Dictionary<string, string> parms = null)
        {
            if (!this.canRead) throw new FlowThingsNotImplementedException();

            JToken jt = await this.RequestAsync("GET", "/" + id, null, parms);
            return encoder.Decode(jt["body"]);
        }


        /// <summary>
        /// Returns an object with the passed ID as a dynamic object, using the default
        /// decoder. Parameters can be accessed as string s = item.param, etc.
        /// </summary>
        /// <param name="id">The ID of the item</param>
        /// <param name="parms">Parameters passed to the platform</param>
        /// <returns></returns>
        public async Task<dynamic> Read(string id, Dictionary<string, string> parms = null)
        {
            if (!this.canRead) throw new FlowThingsNotImplementedException();

            dynamic jt = await this.RequestAsync("GET", "/" + id, null, parms);
            return jt.body;
        }

        #endregion

        #region Createable Service

        /// <summary>
        /// Creates an item of type T on the platform
        /// </summary>
        /// <typeparam name="T">The type of the item to be sent to the platform</typeparam>
        /// <param name="item">The item to be created</param>
        /// <param name="encoder">An encoder that understands T</param>
        /// <param name="parms">Parameters passed to the platform</param>
        /// <returns>The created object, after decoding with the passed encoder</returns>
        public async Task<T> Create<T>(T item, IJsonEncoder<T> encoder,
            Dictionary<string, string> parms = null)
        {
            if (!this.canCreate) throw new FlowThingsNotImplementedException();

            JToken jt = await this.RequestAsync("POST", "", encoder.Encode(item), parms);
            return encoder.Decode(jt["body"]);            
        }


        /// <summary>
        /// Creates an item on the platform
        /// </summary>
        /// <param name="item">The item to be created as a dynamic; this must be an
        /// object, not a non-associative array (which will throw an exception) -- 
        /// it will be encoded with the default encoder</param>
        /// <param name="parms">Parameters passed to the platform</param>
        /// <returns>The created object, as a dynamic</returns>
        public async Task<dynamic> Create(dynamic item, Dictionary<string, string> parms = null)
        {
            if (!this.canCreate) throw new FlowThingsNotImplementedException();

            dynamic jt = await this.RequestAsync("POST", "", JObject.FromObject(item), parms);
            return jt.body;
        }

        #endregion

        #region Updateable Service

        /// <summary>
        /// Updates the item with the passed ID on the platform.
        /// </summary>
        /// <typeparam name="T">The type of the item to be updated</typeparam>
        /// <param name="id">The ID of the item to be updated</param>
        /// <param name="item">The contents of the item -- all fields produced by Encode(item) will 
        /// be updated on the platform; to update only certain fields use Update with dynamics
        /// below</param>
        /// <param name="encoder">The encoder that can encode/decode T to a JToken</param>
        /// <param name="parms">Additional parameters passed to the platform</param>
        /// <returns>The updated item as an object of type T</returns>
        public async Task<T> Update<T>(string id, T item, IJsonEncoder<T> encoder,
            Dictionary<string, string> parms = null)
        {
            if (!this.canUpdate) throw new FlowThingsNotImplementedException();

            JToken jt = await this.RequestAsync("PUT", "/" + id, encoder.Encode(item), parms);
            return encoder.Decode(jt["body"]);
        }


        /// <summary>
        /// Updates the item with the passed ID on the platform.
        /// </summary>
        /// <param name="id">The ID of the item to be updated</param>
        /// <param name="item">The fields to update as an item.fieldname = fieldval,
        /// will only update passed fields; a non-associative array will throw an 
        /// exception</param>
        /// <param name="parms">Additional parameters passed to the platform</param>
        /// <returns>The updated item as a dynamic</returns>
        public async Task<dynamic> Update(string id, dynamic fields,
            Dictionary<string, string> parms = null)
        {
            if (!this.canUpdate) throw new FlowThingsNotImplementedException();

            dynamic jt = await this.RequestAsync("PUT", "/" + id, JObject.FromObject(fields), parms);
            return jt.body;
        }


        /// <summary>
        /// Updates multiple items
        /// </summary>
        /// <typeparam name="T">The type of the item to be updated</typeparam>
        /// <param name="items">A dictionary of id => item where item can be encoded
        /// with encoder</param>
        /// <param name="encoder">The encoder that can handle T</param>
        /// <param name="parms">Additional params for the platform</param>
        /// <returns>A dictionary of id => item</returns>
        public async Task<Dictionary<string, T>> UpdateMany<T>(Dictionary<string, T> items,
            IJsonEncoder<T> encoder, Dictionary<string, string> parms = null)
        {
            if (!this.canUpdate) throw new FlowThingsNotImplementedException();

            JObject jo = new JObject();
            foreach (KeyValuePair<string, T> kvp in items)
            {
                jo.Add(new JProperty(kvp.Key, encoder.Encode(kvp.Value)));
            }

            JToken jt = await this.RequestAsync("MPUT", "", jo, parms);

            Dictionary<string, T> d = new Dictionary<string, T>();

            foreach (KeyValuePair<string, JToken> kvp in (JObject)jt["body"])
            {
                string id = kvp.Key;
                T item = encoder.Decode(kvp.Value);

                d.Add(id, item);
            }

            return d;
        }

        /// <summary>
        /// Updates multiple items
        /// </summary>
        /// <param name="items">A dictionary of id => item where item can be encoded
        /// with encoder</param>
        /// <param name="parms">Additional params for the platform</param>
        /// <returns>The turned object from the platform</returns>
        public async Task<dynamic> UpdateMany(dynamic items, Dictionary<string, string> parms = null)
        {
            if (!this.canUpdate) throw new FlowThingsNotImplementedException();

            dynamic jt = await this.RequestAsync("MPUT", "", JToken.FromObject(items), parms);
            return jt;
        }

        #endregion

        #region Destroyable Service


        /// <summary>
        /// Deletes the object with the specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public async Task Delete(string id, Dictionary<string, string> parms = null)
        {
            if (!this.canDelete) throw new FlowThingsNotImplementedException();

            JToken jt = await this.RequestAsync("DELETE", "/" + id, null, parms);
        }

        #endregion
    }
}
