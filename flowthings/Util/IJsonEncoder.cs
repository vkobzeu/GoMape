using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace flowthings.Util
{
    public interface IJsonEncoder<T>
    {

        /// <summary>
        /// Encode the object for sending to the API
        /// </summary>
        /// <param name="o">The object to encode</param>
        /// <returns></returns>
        JToken Encode(T o);

        
        /// <summary>
        /// Decode the string to an object
        /// </summary>
        /// <param name="jo">The JObject to decode to whatever object</param>
        /// <returns></returns>
        T Decode(JToken jt);

    }
}
