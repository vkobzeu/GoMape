using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harman.Flowthings
{
    public class HarmanFlowthingsServiceMock : IHarmanFlowthingsService
    {
        public float GetSensorLuminance(string sensorId)
        {
            return 500;
        }
    }
}
