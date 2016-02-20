using System.IO;

namespace Harman.Pulse.Stubs
{
    public class BluetoothSocket
    {
        public void connect()
        {
            
        }

        public bool Connected { get; set; }

        public Stream InputStream { get; set; }

        public Stream OutputStream { get; set; }
    }
}