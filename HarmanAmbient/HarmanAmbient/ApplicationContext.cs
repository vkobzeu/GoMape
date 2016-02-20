using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HarmanAmbient.ScreenCapturer;

namespace HarmanAmbient
{
    public class ApplicationContext : System.Windows.Forms.ApplicationContext
    {
        private HarmanAmbientForm harmanForm = new HarmanAmbientForm();
        private NotifyIcon notifyIcon;
        private Thread captureThread;
        private bool _done;



        public ApplicationContext()
        {
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

        delegate void SetBitmapDelegate(Bitmap x);

        private void ScreenCapture()
        {
            while (!_done)
            {
                using (Bitmap bitmap = CaptureScreen.GetDesktopImage())
                {
                    SetBitmapDelegate d = harmanForm.SetBitmap;
                    harmanForm.Invoke(d, bitmap);
                }
                Thread.Sleep(10);
            }
        }
    }
}
