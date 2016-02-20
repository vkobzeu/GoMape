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

        public Bitmap SetImage(Bitmap destImage, out PulseColor c)
        {
            BitmapData bData = destImage.LockBits(new Rectangle(0, 0, destImage.Width, destImage.Height), ImageLockMode.ReadWrite, destImage.PixelFormat);

            var bitsPerPixel = 24;

            /*the size of the image in bytes */
            int size = bData.Stride * bData.Height;

            /*Allocate buffer for image*/
            byte[] data = new byte[size];

            /*This overload copies data of /size/ into /data/ from location specified (/Scan0/)*/
            System.Runtime.InteropServices.Marshal.Copy(bData.Scan0, data, 0, size);
            
            PulseColor[] pulseColors = new PulseColor[destImage.Width * destImage.Height];
            for (int i = 0; i < destImage.Height; i++)
            {
                for (int j = 0; j < destImage.Width; j++)
                {
                    var index = i*destImage.Width + j;
                    pulseColors[index] = new PulseColor();
                    var pulseColor = pulseColors[index];

                    var index2 = (i * bData.Stride) / 3 + j;
                    pulseColor.blue = (sbyte)data[index2 * 3];
                    pulseColor.green = (sbyte)data[index2 * 3 + 1];
                    pulseColor.red = (sbyte)data[index2 * 3 + 2];
                }
            }
            
            /* This override copies the data back into the location specified */
            System.Runtime.InteropServices.Marshal.Copy(data, 0, bData.Scan0, data.Length);

            destImage.UnlockBits(bData);

           

            var c2 = pulseColors[destImage.Width*destImage.Height / 2];
            c = new PulseColor(c2.red, c2.green, c2.blue);

            //_pulseHandlerInterface.SetBackgroundColor(c, true);
            _pulseHandlerInterface.SetColorImage(pulseColors);

            return destImage;
        }
    }
}
