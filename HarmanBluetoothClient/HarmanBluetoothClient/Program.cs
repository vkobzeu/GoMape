using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;

namespace HarmanBluetoothClient
{
    class Program
    {
        static void Main(string[] args)
        {
            const string device_name = "JBL Pulse 2";

            BluetoothClient bluetoothClient = new BluetoothClient();

            IEnumerable<BluetoothDeviceInfo> targetDevices;

            Console.WriteLine("Discovering...");
            while (true)
            {
                var devices = bluetoothClient.DiscoverDevices();

                targetDevices = devices.Where(d => d.DeviceName == device_name);

                if (!targetDevices.Any())
                {
                    Console.WriteLine("No device by name JBL Pulse 2 found! Retrying...");
                }
                else break;
            }
            if(targetDevices.Count() > 1)
            {
                Console.WriteLine("More than one Pulse 2 found! Picking one...");
            }
            BluetoothDeviceInfo pulse2 = targetDevices.First();

            Console.WriteLine(String.Format("Pulse 2 discovered, address is: {0}", pulse2.DeviceAddress));
            var targetAddress = pulse2.DeviceAddress;

            //new Guid("00001101-0000-1000-8000-00805F9B34FB")));
            bluetoothClient.Connect(targetAddress, BluetoothService.SerialPort);

            if(bluetoothClient.Connected)
            {
                Console.WriteLine("Connected to device's Serial Port");
            }

            while(true)
            {
                Console.WriteLine("Input color in sbyte");
                string s = Console.ReadLine();
                int val = int.Parse(s);
                //set bg color to smth
                sbyte[] cmd = new sbyte[] { -86, 88, 2, (sbyte)val, (sbyte)(0) };
                bluetoothClient.Client.Send(cmd.Select(b => (byte)b).ToArray());
            }
            

            bluetoothClient.Close();
            Console.WriteLine("Closing the client...");
            Console.ReadLine();
        }
    }
}
