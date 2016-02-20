using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HarmanAmbient.ScreenCapturer;

namespace HarmanAmbient
{
    public partial class HarmanAmbientForm : Form
    {
        private Bitmap _bitmap;

        public HarmanAmbientForm()
        {
            InitializeComponent();
        }

        public void SetBitmap(Bitmap bmp)
        {
            _bitmap?.Dispose();
            
            Rectangle cloneRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.PixelFormat format = bmp.PixelFormat;
            _bitmap = bmp.Clone(cloneRect, format);

            this.Refresh();
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            if (_bitmap != null)
            {
                e.Graphics.DrawImage(_bitmap, 0, 0);
            }
            
            base.OnPaint(e);
        }

      
        /*
        private void btnCapture_Click(object sender, EventArgs e)
        {
            var bitmap = CaptureScreen.GetDesktopImage();
            bitmap.Save("C:\\dkomin\\research\\StamfordHackaton2016\\HarmanAmbient\\test.jpg", ImageFormat.Jpeg);
            
        }*/

    }
}
