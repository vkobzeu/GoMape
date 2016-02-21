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
using Harman.Pulse;
using HarmanAmbient.ScreenCapturer;

namespace HarmanAmbient
{
    public partial class HarmanAmbientForm : Form
    {
        private Bitmap _bitmap;
        private SolidBrush _b = new SolidBrush(Color.AliceBlue);

        public event EventHandler CtrlAlt1Pressed;
        public event EventHandler CtrlAlt2Pressed;

        public HarmanAmbientForm()
        {
            InitializeComponent();
        }

        public void SetBitmap(Bitmap bmp, PulseColor c)
        {
            _bitmap?.Dispose();
            
            Rectangle cloneRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.PixelFormat format = bmp.PixelFormat;
            _bitmap = bmp.Clone(cloneRect, format);

            _b.Color = Color.FromArgb((byte) c.red, (byte) c.green, (byte) c.blue);
            
            this.Refresh();
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            if (_bitmap != null)
            {
                e.Graphics.DrawImage(_bitmap, 0, 0, 110, 90);

                e.Graphics.FillEllipse(_b, 50, 50, 20, 20);
            }

            base.OnPaint(e);
        }

      
        /*
        private void btnCapture_Click(object sender, EventArgs e)
        {
            var bitmap = CaptureScreen.GetDesktopImage();
            bitmap.Save("C:\\dkomin\\research\\StamfordHackaton2016\\HarmanAmbient\\test.jpg", ImageFormat.Jpeg);
            
        }*/


        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                if (m.WParam.ToInt32() == 1)
                {
                    OnCtrlAlt1Pressed();
                }
                else if (m.WParam.ToInt32() == 2)
                {
                    OnCtrlAlt2Pressed();
                }
            }
            base.WndProc(ref m);
        }

        protected virtual void OnCtrlAlt1Pressed()
        {
            CtrlAlt1Pressed?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnCtrlAlt2Pressed()
        {
            CtrlAlt2Pressed?.Invoke(this, EventArgs.Empty);
        }
    }
}
