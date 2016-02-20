using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harman.Pulse;

namespace HarmanAmbient.Harman
{
    public class HarmanManager
    {
        private PulseHandlerInterface _pulseHandlerInterface;

        public HarmanManager(PulseHandlerInterface pulseHandlerInterface)
        {
            _pulseHandlerInterface = pulseHandlerInterface;
        }

        public Bitmap SetImage(Bitmap destImage)
        {
            BitmapData bData = destImage.LockBits(new Rectangle(0, 0, destImage.Width, destImage.Height), ImageLockMode.ReadWrite, destImage.PixelFormat);

            var bitsPerPixel = 24;

            /*the size of the image in bytes */
            int size = bData.Stride * bData.Height;

            /*Allocate buffer for image*/
            byte[] data = new byte[size];

            /*This overload copies data of /size/ into /data/ from location specified (/Scan0/)*/
            System.Runtime.InteropServices.Marshal.Copy(bData.Scan0, data, 0, size);

            var size2 = destImage.Width * destImage.Height;
            PulseColor[] pulseColors = new PulseColor[size2];
            for (int i = 0; i < size2; i ++)
            {
                pulseColors[i] = new PulseColor();

                var pulseColor = pulseColors[i];
                pulseColor.red = (sbyte)data[i*3];
                pulseColor.green = (sbyte)data[i * 3 + 1];
                pulseColor.blue = (sbyte)data[i * 3 + 2];
            }

            /* This override copies the data back into the location specified */
            System.Runtime.InteropServices.Marshal.Copy(data, 0, bData.Scan0, data.Length);

            destImage.UnlockBits(bData);

            //_pulseHandlerInterface.SetBackgroundColor(pulseColors[0], true);

            _pulseHandlerInterface.SetColorImage(pulseColors);

            return destImage;
        }
    }
}
