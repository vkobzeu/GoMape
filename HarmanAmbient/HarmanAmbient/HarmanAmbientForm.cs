using System;
using System.Drawing;
using System.Windows.Forms;
using Harman.Pulse;

namespace HarmanAmbient
{
    public partial class HarmanAmbientForm : Form
    {
        private Bitmap _bitmap;
        private SolidBrush _b = new SolidBrush(Color.AliceBlue);

        public event EventHandler SingleModeEnabled;
        public event EventHandler SplitModeEnabled;
        public event EventHandler BrightnessDecreased;
        public event EventHandler BrightnessIncreased;
        public event EventHandler ManualBrightnessMode;
        public event EventHandler AutomaticBrightnessMode;

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
            
            Refresh();
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
                switch (m.WParam.ToInt32())
                {
                    case HotKeys.SingleMode:
                        OnSingleModeEnabled();
                        break;
                    case HotKeys.SplitMode:
                        OnSplitModeEnabled();
                        break;
                    case HotKeys.DecreaseBrightness:
                        OnBrightnessDecreased();
                        break;
                    case HotKeys.IncreaseBrightness:
                        OnBrightnessIncreased();
                        break;
                    case HotKeys.ManualBrightnessMode:
                        OnManualBrightnessMode();
                        break;
                    case HotKeys.AutomaticBrightnessMode:
                        OnAutomaticBrightnessMode();
                        break;
                }
            }
            base.WndProc(ref m);
        }

        protected virtual void OnSingleModeEnabled()
        {
            SingleModeEnabled?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnSplitModeEnabled()
        {
            SplitModeEnabled?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnBrightnessDecreased()
        {
            BrightnessDecreased?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnBrightnessIncreased()
        {
            BrightnessIncreased?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnManualBrightnessMode()
        {
            ManualBrightnessMode?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnAutomaticBrightnessMode()
        {
            AutomaticBrightnessMode?.Invoke(this, EventArgs.Empty);
        }
    }
}
