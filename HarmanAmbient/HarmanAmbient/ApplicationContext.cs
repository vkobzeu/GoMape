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
        private PulseHandlerInterfaceImpl _pulseInterfaceImpl1;
        private PulseHandlerInterfaceImpl _pulseInterfaceImpl2;

        private HarmanAmbientForm harmanForm = new HarmanAmbientForm();
        private NotifyIcon notifyIcon;
        private Thread captureThread;
        private bool _done;

        
        public ApplicationContext()
        {
            _pulseInterfaceImpl1 = new PulseHandlerInterfaceImpl("JBL Pulse Right");
            _pulseInterfaceImpl2 = new PulseHandlerInterfaceImpl("JBL Pulse Left");
            if (_pulseInterfaceImpl1.ConnectMasterDevice() == false || _pulseInterfaceImpl2.ConnectMasterDevice() == false)
            {
                Application.Exit();
            };

            _harmanManager = new HarmanManager(_pulseInterfaceImpl1);
            _harmanManager2 = new HarmanManager(_pulseInterfaceImpl2);

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
                        PulseColor c2;
                        _harmanManager.SetImage(scaledImage, out c);
                        _harmanManager2.SetImage(scaledImage, out c2);

                        SetBitmapDelegate d = harmanForm.SetBitmap;
                        harmanForm.Invoke(d, scaledImage, c);
                    }

                }
                Thread.Sleep(10);
            }
        }

    }
}
