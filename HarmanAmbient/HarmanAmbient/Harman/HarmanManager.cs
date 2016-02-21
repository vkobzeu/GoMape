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
        private IPulseHandler _pulseHandler;

        public HarmanManager(IPulseHandler pulseHandler)
        {
            _pulseHandler = pulseHandler;
        }

        public Bitmap SetImage(Bitmap destImage, int brightness)
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

            if (brightness < 0)
            {
                brightness = 0;
            }
            if (brightness > 255)
            {
                brightness = 255;
            }
           
            //_pulseHandler.SetBackgroundColor(c, true);
            _pulseHandler.SetColorImage(pulseColors);

            _pulseHandler.SetBrightness(brightness);

            return destImage;
        }

        public static int GetBrightnesForLuminosity(float luminance)
        {
            if (luminance > 70)
            {
                luminance = 70;
            }

            float brightnessFactor = (255.0f * (luminance / 70.0f));
            if (brightnessFactor < 30)
            {
                brightnessFactor = 30;
            }
            return (int)brightnessFactor;
            /*
            if (luminance > 70)
            {
                brightnessFactor = 1.0f;
            }
            else if (luminance > 60)
            {
                brightnessFactor = .9f;
            }
            else if (luminance > 50)
            {
                brightnessFactor = .8f;
            }
            else if (luminance > 40)
            {
                brightnessFactor = .7f;
            }
            else if (luminance > 30)
            {
                brightnessFactor = .6f;
            }
            else if (luminance > 20)
            {
                brightnessFactor = .55f;
            }
            else if (luminance > 10)
            {
                brightnessFactor = .50f;
            }
            else { 
                brightnessFactor = .2f;
            }

            return (int)(255.0f * brightnessFactor);
            */
        }
    }
}
