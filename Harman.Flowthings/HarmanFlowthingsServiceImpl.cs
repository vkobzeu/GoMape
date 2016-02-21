using System.Collections.Generic;
using System.Threading.Tasks;
using flowthings;
using flowthings.Util;
using Newtonsoft.Json.Linq;

namespace Harman.Flowthings
{
    class HarmanFlowthingsServiceImpl : IHarmanFlowthingsService
    {
        const string REST_HOST = "api.flowthings.io";
        const string WS_HOST = "ws.flowthings.io";
        const string VER = "0.1";
        const bool SECURE = false;

        Token MY_TOKEN = new Token("rpresnakov", "kyr3FQxXGR11ZCXvXl2DFFzVkN23");

        const string TEST_FLOW_ID_MS1 = "f56c9ed4b5bb70955e3ad896f";
        const string TEST_FLOW_ID_MS2 = "f56c9ed605bb70955e3ad89c7";
        const string TEST_FLOW_ID_MS3 = "f56c9ed715bb70955e3ad8a0b";

        public float GetSensorLuminance(string sensorId)
        {
            string flowid = TEST_FLOW_ID_MS2;
            switch (sensorId)
            {
                case "ms1" :
                    flowid = TEST_FLOW_ID_MS1;
                    break;
                case "ms2" :
                    flowid = TEST_FLOW_ID_MS2;
                    break;
                case "ms3" :
                    flowid = TEST_FLOW_ID_MS3;
                    break;
            }

            API api = new API(MY_TOKEN, REST_HOST, WS_HOST, SECURE);
            LuminEncoder be = new LuminEncoder();

            Task<List<LuminData>> t = api.drop(flowid).Find<LuminData>("", be);
            Task.WaitAll(t);
            List<LuminData> d3 = t.Result;

            return d3[0].lux;
        }

        public static void Main()
        {
            IHarmanFlowthingsService service = new HarmanFlowthingsServiceImpl();
            float lux = service.GetSensorLuminance("ms3");
            System.Console.WriteLine(lux);
        }

    }

            public class LuminData
            {
                public string id;
                public int lux;
            }

            public class LuminEncoder : IJsonEncoder<LuminData>
            {

                public JToken Encode(LuminData o)
                {
                    return null;
                }

                public LuminData Decode(JToken jt)
                {
                    LuminData ldata = new LuminData();

                    ldata.id = (string)jt["id"];
                    ldata.lux = (int)jt["elems"]["luminiscence_lux_0"]["value"]["value"]["value"];

                    return ldata;
                }
            }


}
