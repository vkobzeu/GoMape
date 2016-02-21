using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Harman.Flowthings;
using Harman.Pulse;
using HarmanAmbient.Harman;
using HarmanAmbient.ScreenCapturer;
using HarmanBluetoothClient;
using HarmanAmbient.ImageBlurFilter;

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
        private bool _issplit = false;

        private bool _isflowthings = false;
        private string _sensorId = "ms2";
        private IHarmanFlowthingsService _flowthingsService = new HarmanFlowthingsServiceImpl();

        private int _brightness = 255;

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
            harmanForm.SingleModeEnabled += SetUnifiedMode;
            harmanForm.SplitModeEnabled += SetSplitMode;
            harmanForm.BrightnessDecreased += HarmanFormOnBrightnessDecreased;
            harmanForm.BrightnessIncreased += HarmanFormOnBrightnessIncreased;
            harmanForm.FlowthingsEnabled += HarmanForm_FlowthingsEnabled;
            harmanForm.FlowthingsSensorChanged += HarmanForm_FlowthingsSensorChanged;
            harmanForm.ManualBrightnessMode += HarmanFormOnManualBrightnessMode;
            harmanForm.AutomaticBrightnessMode += HarmanFormOnAutomaticBrightnessMode;

            HotKeys.RegisterHotKey(harmanForm.Handle, HotKeys.SingleMode, 3 /* ctrl+alt */, (int) Keys.D1);
            HotKeys.RegisterHotKey(harmanForm.Handle, HotKeys.SplitMode, 3 /* ctrl+alt */, (int) Keys.D2);
            HotKeys.RegisterHotKey(harmanForm.Handle, HotKeys.DecreaseBrightness, 3 /* ctrl+alt */, (int) Keys.OemMinus);
            HotKeys.RegisterHotKey(harmanForm.Handle, HotKeys.IncreaseBrightness, 3 /* ctrl+alt */, (int) Keys.Oemplus);
            HotKeys.RegisterHotKey(harmanForm.Handle, HotKeys.ManualBrightnessMode, 3 /* ctrl+alt */, (int) Keys.D9);
            HotKeys.RegisterHotKey(harmanForm.Handle, HotKeys.AutomaticBrightnessMode, 3 /* ctrl+alt */, (int) Keys.D0);

            captureThread = new Thread(ScreenCapture);
            captureThread.Start();
        }

        private void HarmanFormOnAutomaticBrightnessMode(object sender, EventArgs eventArgs)
        {
            Debug.WriteLine("Automatic Brightness");
        }

        private void HarmanFormOnManualBrightnessMode(object sender, EventArgs eventArgs)
        {
            Debug.WriteLine("Manual Brightness");
        }

        private void HarmanFormOnBrightnessDecreased(object sender, EventArgs e)
        {
            Debug.WriteLine("Decrease Brightness");
            var br = _brightness - 40;
            if (br < 0)
            {
                br = 0;
        }
            _brightness = br;
        }

        private void HarmanFormOnBrightnessIncreased(object sender, EventArgs e)
        {
            Debug.WriteLine("Increase Brightness");
            var br = _brightness + 40;
            if (br > 255)
            {
                br = 255;
        }
            _brightness = br;
        }

        void SetSplitMode(object sender, EventArgs e)
        {
            _issplit = true;
            Debug.WriteLine("Split mode: On");
        }

        void SetUnifiedMode(object sender, EventArgs e)
        {
            _issplit = false;
            Debug.WriteLine("Split mode: Off");
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
            HotKeys.UnregisterHotKey(harmanForm.Handle, HotKeys.SingleMode);
            HotKeys.UnregisterHotKey(harmanForm.Handle, HotKeys.SplitMode);
            HotKeys.UnregisterHotKey(harmanForm.Handle, HotKeys.DecreaseBrightness);
            HotKeys.UnregisterHotKey(harmanForm.Handle, HotKeys.IncreaseBrightness);
            HotKeys.UnregisterHotKey(harmanForm.Handle, HotKeys.ManualBrightnessMode);
            HotKeys.UnregisterHotKey(harmanForm.Handle, HotKeys.AutomaticBrightnessMode);
            Application.Exit();
        }

        private void ScreenCapture()
        {
            while (!_done)
            {
                if (_isflowthings)
                {
                    var luminance = _flowthingsService.GetSensorLuminance(_sensorId);
                    _brightness = HarmanManager.GetBrightnesForLuminosity(luminance);
                }

                using (Bitmap image = CaptureScreen.GetDesktopImage())
                {
                    //using (var gaussed = image.ImageBlurFilter(ExtBitmap.BlurType.GaussianBlur3x3))
                    //{
                        if (!_issplit)
                        {
                            ProcessUnifiedImage(image);
                        }
                        else
                        {
                            ProcessSplitImage(image);
                    }
                    //}
                }
                Thread.Sleep(1);
            }
        }

        private void ProcessUnifiedImage(Bitmap image)
        {
            using (Bitmap scaledImage = CaptureScreen.ScaleImage(11, 9, image))
            {
                PulseColor c;
                PulseColor c2;

                _harmanManager.SetImage(scaledImage, _brightness);
                _harmanManager2.SetImage(scaledImage, _brightness);

                //SetBitmapDelegate d = harmanForm.SetBitmap;
                //harmanForm.Invoke(d, scaledImage, c);
            }
        }

        private void ProcessSplitImage(Bitmap image)
        {
            using (Bitmap scaledImage = CaptureScreen.ScaleImage(22, 9, image))
            {
                System.Drawing.Imaging.PixelFormat format = scaledImage.PixelFormat;

                Rectangle cloneRect = new Rectangle(0, 0, scaledImage.Width / 2, scaledImage.Height);
                Bitmap left = scaledImage.Clone(cloneRect, format);
                Rectangle cloneRect2 = new Rectangle(scaledImage.Width / 2, 0, scaledImage.Width / 2, scaledImage.Height);
                Bitmap right = scaledImage.Clone(cloneRect2, format);


                _harmanManager.SetImage(left, _brightness);
                
                //SetBitmapDelegate d = harmanForm.SetBitmap;
                //harmanForm.Invoke(d, scaledImage, c);

                _harmanManager2.SetImage(right, _brightness);
            
            left.Dispose();
            right.Dispose();
        }
    }
        
        private void HarmanForm_FlowthingsSensorChanged(object sender, FlowthingsSensorChangedEventArgs e)
        {
            if (e != null)
            {
                _sensorId = e.Sensor;
            }
        }

        private void HarmanForm_FlowthingsEnabled(object sender, FlowthingsEnabledEventArgs e)
        {
            if (e != null)
            {
                _isflowthings = e.Enabled;
                if (!e.Enabled)
                {
                    _brightness = 255;
                }
            }
        }
}
}
