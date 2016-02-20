using Harman.Pulse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harman.Pulse.Stubs;

namespace HarmanBluetoothClient
{
    public class PulseHandlerInterfaceImpl : PulseHandlerInterface
    {
        public bool? IsConnectMasterDevice
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool? CaptureColorFromColorPicker()
        {
            throw new NotImplementedException();
        }

        public bool? ConnectMasterDevice(Activity paramActivity)
        {
            throw new NotImplementedException();
        }

        public bool? GetBrightness()
        {
            throw new NotImplementedException();
        }

        public bool? GetLEDPattern()
        {
            throw new NotImplementedException();
        }

        public void GetMicrophoneSoundLevel()
        {
            throw new NotImplementedException();
        }

        public bool? PropagateCurrentLedPattern()
        {
            throw new NotImplementedException();
        }

        public void registerPulseNotifiedListener(PulseNotifiedListener paramPulseNotifiedListener)
        {
            throw new NotImplementedException();
        }

        public bool? RequestDeviceInfo()
        {
            throw new NotImplementedException();
        }

        public bool? SetBackgroundColor(PulseColor paramPulseColor, bool paramBoolean)
        {
            throw new NotImplementedException();
        }

        public bool? SetBrightness(int paramInt)
        {
            throw new NotImplementedException();
        }

        public bool? SetCharacterPattern(char paramChar, PulseColor paramPulseColor1, PulseColor paramPulseColor2, bool paramBoolean)
        {
            throw new NotImplementedException();
        }

        public bool? SetColorImage(PulseColor[] paramArrayOfPulseColor)
        {
            throw new NotImplementedException();
        }

        public bool? SetDeviceChannel(int paramInt1, int paramInt2)
        {
            throw new NotImplementedException();
        }

        public bool? SetDeviceName(string paramString, int paramInt)
        {
            throw new NotImplementedException();
        }

        public void SetLEDAndSoundFeedback(int paramInt)
        {
            throw new NotImplementedException();
        }

        public bool? SetLEDPattern(PulseThemePattern paramPulseThemePattern)
        {
            throw new NotImplementedException();
        }
    }
}
