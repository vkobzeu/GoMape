using System;
using System.Runtime.Remoting.Contexts;
using Harman.Pulse.Stubs;

/*    */

namespace Harman.Pulse
{
    /*    */
    /*    */
    //using BluetoothAdapter = android.bluetooth.BluetoothAdapter;
    ///*    */
    //using BluetoothDevice = android.bluetooth.BluetoothDevice;
    ///*    */
    ///*    */
    //using BluetoothSocket = android.bluetooth.BluetoothSocket;
    ///*    */
    //using Context = android.content.Context;

    /*    */ /*    */
    /*    */

    public class BluetoothHelper
        /*    */
    {
        /* 12 */
        public static string SPP_UUID = "00001101-0000-1000-8000-00805F9B34FB";
        /*    */
        public const int REQUEST_ENABLE = 1000;
        /*    */
        /*    */

        public static bool BluetoothSupported
        {
            get
            {
                throw new NotImplementedException();
                /* 16 */
                //BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
                ///* 17 */
                //return adapter != null;
                /*    */
            }
        } /*    */
        /*    */

        public static bool BluetoothEnabled
        {
            get
                /*    */
            {
                throw new NotImplementedException();
                /* 22 */
                //BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
                ///* 23 */
                //return adapter.Enabled;
                /*    */
            }
        } /*    */
        /*    */

        public static bool isA2DPDeviceConnected(Context context)
            /*    */
        {
                throw new NotImplementedException();
            /* 28 */
            //BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            ///* 29 */
            //int state = adapter.getProfileConnectionState(2);
            ///* 30 */
            //return state == 2;
            /*    */
        } /*    */
        /*    */

        public static bool getA2DPProfileProxy(Context context, BluetoothProfile.ServiceListener listener)
            /*    */
        {
                throw new NotImplementedException();
            /* 35 */
            //BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            ///* 36 */
            //bool success = adapter.getProfileProxy(context, listener, 2);
            ///* 37 */
            //return success;
            /*    */
        } /*    */
        /*    */

        public static BluetoothSocket createBluetoothSocket(BluetoothDevice device)
        {
                throw new NotImplementedException();
            /* 41 */
            //BluetoothSocket socket = null;
            ///* 42 */
            //UUID uuid = UUID.fromString(SPP_UUID);
            ///*    */
            ///*    */
            //try
            //    /*    */
            //{
            //    /* 46 */
            //    socket = device.createRfcommSocketToServiceRecord(uuid);
            //    /*    */
            //} /*    */
            //catch (Exception)
            //{
            //    /*    */
            //    try
            //    {
            //        /* 50 */
            //        socket = device.createInsecureRfcommSocketToServiceRecord(uuid);
            //        /*    */
            //    } /*    */
            //    catch (Exception)
            //        /*    */
            //    {
            //        /* 54 */
            //        socket = null;
            //        /*    */
            //    } /*    */
            //} /* 57 */
            //return socket;
            /*    */
        } /*    */
        /*    */

        public static BluetoothSocket connect(BluetoothDevice device)
        {
            /* 61 */
            BluetoothSocket socket = null;
            /*    */
            try
            {
                /* 63 */
                socket = createBluetoothSocket(device);
                /* 64 */
                if (socket == null)
                {
                    return null;
                }
                /* 65 */
                socket.connect();
                /*    */
            }
            catch (Exception e)
            {
                /* 67 */
                socket = null;
                /* 68 */
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                /*    */
            } /* 70 */
            return socket;
            /*    */
        } /*    */
    }

    /* Location:              D:\Hack\classes.jar!\com\harman\pulsesdk\BluetoothHelper.class
	 * Java compiler version: 7 (51.0)
	 * JD-Core Version:       0.7.1
	 */
}