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

        public void SetImage(Bitmap image)
        {
            var width = 11;
            var height = 9;

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);

        
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            BitmapData bData = destImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, destImage.PixelFormat);

            var bitsPerPixel = 24;

            /*the size of the image in bytes */
            int size = bData.Stride * bData.Height;

            /*Allocate buffer for image*/
            byte[] data = new byte[size];

            /*This overload copies data of /size/ into /data/ from location specified (/Scan0/)*/
            System.Runtime.InteropServices.Marshal.Copy(bData.Scan0, data, 0, size);

            var size2 = width*height;

            PulseColor[] pulseColors = new PulseColor[size2];
            for (int i = 0; i < size2; i ++)
            {
                var pulseColor = pulseColors[i];
                pulseColor.red = (sbyte)data[i*3];
                pulseColor.green = (sbyte)data[i * 3 + 1];
                pulseColor.blue = (sbyte)data[i * 3 + 2];
            }

            /* This override copies the data back into the location specified */
            System.Runtime.InteropServices.Marshal.Copy(data, 0, bData.Scan0, data.Length);

            destImage.UnlockBits(bData);

            _pulseHandlerInterface.SetBackgroundColor(pulseColors[0], true);

            //_pulseHandlerInterface.SetColorImage(pulseColors);
        }
    }
}
