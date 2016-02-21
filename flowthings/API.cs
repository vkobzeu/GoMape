using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using flowthings.Services;

namespace flowthings
{
    public sealed class API
    {
        const string VERSION = "0.1";
        const string REST_HOST = "api.flowthings.io";
        const string WS_HOST = "ws.flowthings.io";

        private Token creds;
        private string rest_host, ws_host;
        private bool secure;

        public BaseService flow { get; private set; }
        public BaseService identity { get; private set; }
        public BaseService track { get; private set; }
        public BaseService share { get; private set; }
        public BaseService group { get; private set; }
        public BaseService api_task { get; private set; }
        public BaseService mqtt_task { get; private set; }
        public BaseService token { get; private set; }
       // public WebSocketService websocket { get; private set; }

        public API(Token creds, string rest_host = REST_HOST, string ws_host = WS_HOST, 
            bool secure = true)
        {
            this.creds = creds;
            this.rest_host = rest_host;
            this.ws_host = ws_host;
            this.secure = secure;

            this.flow = new BaseService(creds, secure, rest_host, VERSION, 
                true, true, true, true, "/flow");

            this.identity = new BaseService(creds, secure, rest_host, VERSION,
                true, false, true, false, "/identity");

            this.group = new BaseService(creds, secure, rest_host, VERSION,
                true, true, true, true, "/group");

            this.track = new BaseService(creds, secure, rest_host, VERSION,
                true, true, true, true, "/track");

            this.api_task = new BaseService(creds, secure, rest_host, VERSION,
                true, true, true, true, "/api-task");

            this.mqtt_task = new BaseService(creds, secure, rest_host, VERSION,
                true, true, true, true, "/mqtt");

            this.token = new BaseService(creds, secure, rest_host, VERSION,
                true, true, false, true, "/token");

            this.share = new BaseService(creds, secure, rest_host, VERSION,
                true, true, false, true, "/share");

            //this.websocket = new WebSocketService(creds, secure, ws_host);
        }


        /// <summary>
        /// Creates a drop service from a FlowID.  This is a special case to mimic the python and
        /// node apis.
        /// </summary>
        /// <param name="flowId">The flow ID</param>
        /// <returns>The service</returns>
        public DropService drop(string flowId)
        {
            return new DropService(creds, this.secure, this.rest_host, VERSION, flowId);
        }

    }
}
