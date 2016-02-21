using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace flowthings.Util
{
    public class DefaultJsonEncoder<T> : IJsonEncoder<T>
    {
        public JToken Encode(T o)
        {
            return JObject.FromObject(o);
        }

        public T Decode(JToken s)
        {
            return s.ToObject<T>();
        }
    }
}
