using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Harman.Pulse;
using HarmanAmbient.Harman;
using HarmanAmbient.ScreenCapturer;
using HarmanBluetoothClient;

namespace HarmanAmbient
{
    public class ApplicationContext : System.Windows.Forms.ApplicationContext
    {
        private HarmanManager _harmanManager;
        private HarmanManager _harmanManager2;
        private PulseHandlerImpl _pulseImpl1;
        private PulseHandlerImpl _pulseImpl2;

        private HarmanAmbientForm harmanForm = new HarmanAmbientForm();
        private NotifyIcon notifyIcon;
        private Thread captureThread;

        private bool _done;
        private bool _issplit;

        public ApplicationContext()
        {
            _pulseImpl1 = new PulseHandlerImpl("JBL Pulse Left");
            _pulseImpl2 = new PulseHandlerImpl("JBL Pulse Right");
            if (_pulseImpl1.ConnectMasterDevice() == false || _pulseImpl2.ConnectMasterDevice() == false)
            {
                Application.Exit();
            };

            _harmanManager = new HarmanManager(_pulseImpl1);
            _harmanManager2 = new HarmanManager(_pulseImpl2);

            MenuItem configMenuItem = new MenuItem("Configuration", new EventHandler(ShowConfig));
            MenuItem splitModeMenuItem = new MenuItem("Split mode", new EventHandler(SetSplitMode));
            MenuItem unifiedModeMenuItem = new MenuItem("Unified mode", new EventHandler(SetUnifiedMode));
            MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));

            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Resource.MainIcon;
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[]
                { configMenuItem, splitModeMenuItem, unifiedModeMenuItem, exitMenuItem });
            notifyIcon.Visible = true;
            
            MainForm = harmanForm;

            captureThread = new Thread(ScreenCapture);
            captureThread.Start();
        }

        void SetSplitMode(object sender, EventArgs e)
        {
            _issplit = true;
        }

        void SetUnifiedMode(object sender, EventArgs e)
        {
            _issplit = false;
        }

        void ShowConfig(object sender, EventArgs e)
        {
            if (harmanForm.Visible)
            {
                harmanForm.Activate();
            }
            else
            {
                harmanForm.ShowDialog();
            }
        }

        void Exit(object sender, EventArgs e)
        {
            _done = true;
            captureThread.Join();
            Application.Exit();
        }

        delegate void SetBitmapDelegate(Bitmap x, PulseColor c);
        
        private void ScreenCapture()
        {
            while (!_done)
            {
                using (Bitmap image = CaptureScreen.GetDesktopImage())
                {
                    if (!_issplit)
                    {
                        ProcessUnifiedImage(image);
                    }
                    else
                    {
                        ProcessSplitImage(image);
                    }
                }
                Thread.Sleep(10);
            }
        }

        private void ProcessUnifiedImage(Bitmap image)
        {
            using (Bitmap scaledImage = CaptureScreen.ScaleImage(11, 9, image))
            {
                PulseColor c;
                PulseColor c2;
                _harmanManager.SetImage(scaledImage, out c);
                _harmanManager2.SetImage(scaledImage, out c2);

                SetBitmapDelegate d = harmanForm.SetBitmap;
                harmanForm.Invoke(d, scaledImage, c);
            }
        }

        private void ProcessSplitImage(Bitmap image)
        {
            System.Drawing.Imaging.PixelFormat format = image.PixelFormat;

            Rectangle cloneRect = new Rectangle(0, 0, image.Width / 2, image.Height);
            Bitmap left = image.Clone(cloneRect, format);
            Rectangle cloneRect2 = new Rectangle(image.Width / 2, 0, image.Width / 2, image.Height);
            Bitmap right = image.Clone(cloneRect2, format);


            using (Bitmap scaledImage = CaptureScreen.ScaleImage(11, 9, left))
            {
                PulseColor c;
                _harmanManager.SetImage(scaledImage, out c);
                
                SetBitmapDelegate d = harmanForm.SetBitmap;
                harmanForm.Invoke(d, scaledImage, c);
            }

            using (Bitmap scaledImage = CaptureScreen.ScaleImage(11, 9, right))
            {
                PulseColor c;
                _harmanManager2.SetImage(scaledImage, out c);
                
                //SetBitmapDelegate d = harmanForm.SetBitmap;
                //harmanForm.Invoke(d, scaledImage, c);
            }
            
            left.Dispose();
            right.Dispose();
        }
    }
}
