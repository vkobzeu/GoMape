using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;

using flowthings.Util;


namespace flowthings.Services
{
    public class DropService : BaseService
    {

        protected string flowId;

        /// <summary>
        /// Constructs the service. Should only be used on its own if you want to
        /// make raw calls to the API.
        /// </summary>
        /// <param name="creds">The credentials token</param>
        /// <param name="secure">True if this should be a secure connection</param>
        /// <param name="host">The API host</param>
        /// <param name="version">The API version</param>
        public DropService(Token creds, bool secure, string host, string version, string flowId)
            : base(creds, secure, host, version)
        {
            this.canRead = true;
            this.canUpdate = true;
            this.canDelete = true;
            this.canCreate = true;

            this.flowId = flowId;

            this.basePath = "/drop/" + flowId;
        }

        #region Findable Service


        /// <summary>
        /// Find one or multiple drops that match filter
        /// </summary>
        /// <typeparam name="T">The type of the object to find</typeparam>
        /// <param name="filter">The filter string</param>
        /// <param name="encoder">An encode that handles T</param>
        /// <param name="parms">Additional parameters to be passed to the platform</param>
        /// <returns>A list of objects of type T that satisfy filter</returns>
        public async Task<List<T>> Find<T>(string filter, IJsonEncoder<T> encoder,
             Dictionary<string, string> parms = null)
        {
            if (!this.canRead) throw new FlowThingsNotImplementedException();

            if (parms == null) parms = new Dictionary<string, string>();
            parms.Add("filter", filter);

            string url = this.MakeURL("", parms);
            JToken jt = await this.RequestAsync("GET", null, url);

            List<T> l = new List<T>();

            foreach (JToken t in (JArray)jt["body"])
            {
                l.Add(encoder.Decode(t));
            }

            return l;
        }


        /// <summary>
        /// Find one or multiple drops that match filter
        /// </summary>
        /// <param name="filter">The filter string</param>
        /// <param name="parms">Additional parameters to be passed to the platform</param>
        /// <returns>A list of dynamics that satisfy filter</returns>
        public async Task<List<dynamic>> Find(string filter, Dictionary<string, string> parms = null)
        {
            if (!this.canRead) throw new FlowThingsNotImplementedException();

            if (parms == null) parms = new Dictionary<string, string>();
            parms.Add("filter", filter);

            string url = this.MakeURL("", parms);
            JToken jt = await this.RequestAsync("GET", null, url);

            List<dynamic> l = new List<dynamic>();

            foreach (JToken t in (JArray)jt["body"])
            {
                l.Add(t);
            }

            return l;
        }


        /// <summary>
        /// Returns multiple items based on the IDs passed
        /// </summary>
        /// <typeparam name="T">The type of the item expected as return</typeparam>
        /// <param name="targets">An array of the form accepted by the platform</param>
        /// <param name="encoder">An encoder that can parse T from a JToken</param>
        /// <param name="parms">The parameters to pass to the platform</param>
        /// <returns>An item of type T from the platform</returns>
        public async Task<Dictionary<string, List<T>>> FindMany<T>(dynamic targets, IJsonEncoder<T> encoder)
        {
            if (!this.canRead) throw new FlowThingsNotImplementedException();

            string url = (this.secure ? "https:" : "http:") + "//" + this.host + "/v" + this.version + "/" +
                this.creds.account + "/drop";

            JToken jt = await this.RequestAsync("MGET", JObject.FromObject(targets), url);

            Dictionary<string, List<T>> items = new Dictionary<string, List<T>>();

            foreach (KeyValuePair<string, JToken> kvp in (JObject)jt["body"])
            {
                string flowId = kvp.Key;
                JArray dropList = (JArray)kvp.Value;

                List<T> l = new List<T>();
                foreach (JToken t in dropList)
                {
                    T obj = encoder.Decode(t);
                    l.Add(obj);
                }

                items.Add(flowId, l);
            }

            return items;
        }


        /// <summary>
        /// Returns multiple items based on the IDs passed
        /// </summary>
        /// <typeparam name="T">The type of the item expected as return</typeparam>
        /// <param name="targets">An array of the form accepted by the platform. Must
        /// be an array or an exception will be thrown</param>
        /// <param name="encoder">An encoder that can parse T from a JToken</param>
        /// <param name="parms">The parameters to pass to the platform</param>
        /// <returns>An item of type T from the platform</returns>
        public async Task<dynamic> FindMany(dynamic targets)
        {
            if (!this.canRead) throw new FlowThingsNotImplementedException();


            string url = (this.secure ? "https:" : "http:") + "//" + this.host + "/v" + this.version + "/" +
                this.creds.account + "/drop"; 
            
            dynamic jt = await this.RequestAsync("MGET", JArray.FromObject(targets), url);
            return jt.body;
        }
        
        #endregion
    
    }
}
