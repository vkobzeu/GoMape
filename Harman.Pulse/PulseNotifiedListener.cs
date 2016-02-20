namespace Harman.Pulse
{
    public interface PulseNotifiedListener
    {
        void onConnectMasterDevice();

        void onDisconnectMasterDevice();

        void onRetBrightness(int paramInt);

        void onLEDPatternChanged(PulseThemePattern paramPulseThemePattern);

        void onSoundEvent(int paramInt);

        void onRetCaptureColor(PulseColor paramPulseColor);

        void onRetCaptureColor(sbyte paramByte1, sbyte paramByte2, sbyte paramByte3);

        void onRetSetDeviceInfo(bool paramBoolean);

        void onRetGetLEDPattern(PulseThemePattern paramPulseThemePattern);

        void onRetRequestDeviceInfo(DeviceModel[] paramArrayOfDeviceModel);

        void onRetSetLEDPattern(bool paramBoolean);
    }


    /* Location:              D:\Hack\classes.jar!\com\harman\pulsesdk\PulseNotifiedListener.class
	 * Java compiler version: 7 (51.0)
	 * JD-Core Version:       0.7.1
	 */
}