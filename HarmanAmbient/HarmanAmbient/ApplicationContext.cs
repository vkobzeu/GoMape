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
        private PulseHandlerInterfaceImpl _pulseInterfaceImpl;

        private HarmanAmbientForm harmanForm = new HarmanAmbientForm();
        private NotifyIcon notifyIcon;
        private Thread captureThread;
        private bool _done;

        
        public ApplicationContext()
        {
            _pulseInterfaceImpl = new PulseHandlerInterfaceImpl();
            if (_pulseInterfaceImpl.ConnectMasterDevice(null) == false)
            {
                Application.Exit();
            };

            _harmanManager = new HarmanManager(_pulseInterfaceImpl);

            MenuItem configMenuItem = new MenuItem("Configuration", new EventHandler(ShowConfig));
            MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));

            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Resource.MainIcon;
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[]
                { configMenuItem, exitMenuItem });
            notifyIcon.Visible = true;
            
            MainForm = harmanForm;

            captureThread = new Thread(ScreenCapture);
            captureThread.Start();
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
                    using (Bitmap scaledImage = CaptureScreen.ScaleImage(11, 9, image))
                    {
                        PulseColor c;
                        _harmanManager.SetImage(scaledImage, out c);
                        
                        SetBitmapDelegate d = harmanForm.SetBitmap;
                        harmanForm.Invoke(d, scaledImage, c);
                    }

                }
                Thread.Sleep(10);
            }
        }

    }
}
