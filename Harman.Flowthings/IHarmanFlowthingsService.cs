using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harman.Flowthings
{
    public interface IHarmanFlowthingsService
    {
        float GetSensorLuminance(string sensorId);
    }
}
