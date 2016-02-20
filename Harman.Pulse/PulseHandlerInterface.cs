using Harman.Pulse.Stubs;

namespace Harman.Pulse
{
    public interface PulseHandlerInterface
    {
        void registerPulseNotifiedListener(PulseNotifiedListener paramPulseNotifiedListener);

        bool? ConnectMasterDevice(Activity paramActivity);

        bool? IsConnectMasterDevice { get; }

        bool? RequestDeviceInfo();

        bool? SetDeviceName(string paramString, int paramInt);

        bool? SetDeviceChannel(int paramInt1, int paramInt2);

        bool? SetLEDPattern(PulseThemePattern paramPulseThemePattern);

        bool? GetLEDPattern();

        bool? SetBrightness(int paramInt);

        bool? GetBrightness();

        bool? SetBackgroundColor(PulseColor paramPulseColor, bool paramBoolean);

        bool? SetColorImage(PulseColor[] paramArrayOfPulseColor);

        bool? SetCharacterPattern(char paramChar, PulseColor paramPulseColor1, PulseColor paramPulseColor2,
            bool paramBoolean);

        bool? CaptureColorFromColorPicker();

        bool? PropagateCurrentLedPattern();

        void GetMicrophoneSoundLevel();

        void SetLEDAndSoundFeedback(int paramInt);
    }


    /* Location:              D:\Hack\classes.jar!\com\harman\pulsesdk\PulseHandlerInterface.class
	 * Java compiler version: 7 (51.0)
	 * JD-Core Version:       0.7.1
	 */
}