using Harman.Pulse;
using System;
using System.Collections.Generic;
using System.Linq;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;

namespace HarmanBluetoothClient
{
    public class PulseHandlerInterfaceImpl : PulseHandlerInterface
    {
        const string device_name = "JBL Pulse 2";

        private BluetoothClient _bluetoothClient;

        public bool? IsConnectMasterDevice
        {
            get; set;
        }

        public bool? CaptureColorFromColorPicker()
        {
            throw new NotImplementedException();
        }

        public bool? ConnectMasterDevice()
        {
            _bluetoothClient = new BluetoothClient();

            IEnumerable<BluetoothDeviceInfo> targetDevices = null;

            Console.WriteLine("Discovering...");
            int att = 5;
            while (att > 0)
            {
                att--;
                var devices = _bluetoothClient.DiscoverDevices();

                targetDevices = devices.Where(d => d.DeviceName == device_name);

                if (!targetDevices.Any())
                {
                    Console.WriteLine("No device by name JBL Pulse 2 found! Retrying...");
                }
                else break;
            }
            if (targetDevices.Count() > 1)
            {
                Console.WriteLine("More than one Pulse 2 found! Picking one...");
            }
            BluetoothDeviceInfo pulse2 = targetDevices.First();

            Console.WriteLine(String.Format("Pulse 2 discovered, address is: {0}", pulse2.DeviceAddress));
            var targetAddress = pulse2.DeviceAddress;

            //new Guid("00001101-0000-1000-8000-00805F9B34FB")));
            _bluetoothClient.Connect(targetAddress, BluetoothService.SerialPort);

            if (_bluetoothClient.Connected)
            {
                Console.WriteLine("Connected to device's Serial Port");
                IsConnectMasterDevice = true;
            }
            return true;
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
            int colorIdx = WebColorHelper.RGBToWeb216Index(paramPulseColor);

            sbyte[] cmd = new sbyte[] { -86, 88, 2, (sbyte)colorIdx, (sbyte)(paramBoolean ? 1 : 0) };
            _bluetoothClient.Client.Send(cmd.Select(b => (byte)b).ToArray());

            return true;
        }

        public bool? SetBrightness(int brightness)
        {
            if (!IsConnectMasterDevice ?? false)
            {
                return false;
            }
            sbyte[] cmd = { -86, 86, 1, (sbyte)brightness };
            _bluetoothClient.Client.Send(cmd.Select(b => (byte)b).ToArray());
            return true;
        }

        public bool? SetCharacterPattern(char character, PulseColor foreground, PulseColor background, bool inlcudeSlave)
        {
            if (!IsConnectMasterDevice ?? false)
            {
                return false;
            }
            int foregroundColor = WebColorHelper.RGBToWeb216Index(foreground);
            int backgroundColor = WebColorHelper.RGBToWeb216Index(background);
            SppCmdHelper.SetCharacterPattern(character, foregroundColor, backgroundColor, inlcudeSlave);
            return Convert.ToBoolean(true);
        }

        public bool? SetColorImage(PulseColor[] paramArrayOfPulseColor)
        {
            if (!IsConnectMasterDevice ?? false)
            {
                return false;
            }
            sbyte[] cmd = new sbyte[102];
            cmd[0] = -86;
            cmd[1] = 89;
            cmd[2] = 99;
            for (int i = 0; i < 99; i++)
            {
                cmd[i + 3] = (sbyte)WebColorHelper.RGBToWeb216Index(paramArrayOfPulseColor[i]);
            }
            _bluetoothClient.Client.Send(cmd.Select(b => (byte)b).ToArray());
            return true;
        }

        public bool? SetDeviceChannel(int paramInt1, int paramInt2)
        {
            throw new NotImplementedException();
        }

        public bool? SetDeviceName(string paramString, int paramInt)
        {
            throw new NotImplementedException();
        }

        public void SetLEDAndSoundFeedback(int devIndex)
        {
            SppCmdHelper.reqLEDAndSoundFeedback(devIndex);
        }

        public bool? SetLEDPattern(PulseThemePattern pattern)
        {
            if (!IsConnectMasterDevice ?? false)
            {
                return Convert.ToBoolean(false);
            }
            SppCmdHelper.LedPattern = pattern.ordinal();
            return Convert.ToBoolean(true);
        }
    }
}
