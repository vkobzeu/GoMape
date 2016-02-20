using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Harman.Pulse.Stubs;

/*     */

namespace Harman.Pulse
{
    /*     */
    /*     */
    public class ImplementPulseHandler : PulseHandlerInterface
    /*     */
    /*     */
    {
        private bool InstanceFieldsInitialized = false;

        private void InitializeInstanceFields()
        {

        }

        /*     */
        private Activity mActivity;
        /*  24 */
        private PulseNotifiedListener pulseNotifiedListener = null;
        /*  25 */
        private bool? bConnectMasterDevice = Convert.ToBoolean(false);
        /*  26 */
        private bool? bGetDeviceInfo = Convert.ToBoolean(false);
        /*  27 */
        private BluetoothSocket mSocket = null;
        /*  28 */
        private System.IO.Stream @is = null;
        /*  29 */
        private System.IO.Stream os = null;
        /*  30 */
        private BluetoothDevice bluetoothDevice = null;
        ///*  31 */
        //private Lock @lock = new ReentrantLock();
        ///*  32 */
        //private Condition conditionA;
        ///*  33 */
        //private Lock lockSetDev = new ReentrantLock();
        ///*  34 */
        //private Condition condSetDev;
        /*  35 */
        private DeviceModel[] device = new DeviceModel[2];
        /*     */
        /*  37 */
        private PulseThemePattern LEDPattern = PulseThemePattern.PulseTheme_Firework;
        /*  38 */
        private int SetDevInfoACK = 0;
        /*  39 */
        private PulseColor captureColor = new PulseColor();
        /*     */
        /*     */

        public ImplementPulseHandler()
        {
            if (!InstanceFieldsInitialized)
            {
                InitializeInstanceFields();
                InstanceFieldsInitialized = true;
            }
            /*  42 */
            this.device[0] = new DeviceModel();
            /*  43 */
            this.device[1] = new DeviceModel();
            /*     */
        } /*     */
        /*  46 */

        public virtual void registerPulseNotifiedListener(PulseNotifiedListener listener)
        {
            this.pulseNotifiedListener = listener;
        }

        /*     */
        /*     */

        public virtual bool? IsConnectMasterDevice
        {
            get
            {
                /*  49 */
                return this.bConnectMasterDevice;
                /*     */
            }
        } /*     */
        /*     */

        public virtual bool? ConnectMasterDevice(Activity activity)
        {
            /*  53 */
            this.mActivity = activity;
            /*  54 */
            bool bBluetoothEnabled = BluetoothHelper.BluetoothEnabled;
            /*  55 */
            if (!bBluetoothEnabled)
            {
                /*  56 */
                Log.d("hello", "!bBluetoothEnabled");
                /*  57 */
                return Convert.ToBoolean(false);
                /*     */
            } /*     */
            /*  60 */
            if (this.bConnectMasterDevice.Value)
            {
                /*  61 */
                return Convert.ToBoolean(true);
                /*     */
            } /*     */
            /*  64 */
            BluetoothHelper.getA2DPProfileProxy(activity, new ServiceListenerAnonymousInnerClassHelper(this));
            /* 106 */
            return Convert.ToBoolean(true);
            /*     */
        }

        private class ServiceListenerAnonymousInnerClassHelper : BluetoothProfile.ServiceListener
        {
            private readonly ImplementPulseHandler outerInstance;

            public ServiceListenerAnonymousInnerClassHelper(ImplementPulseHandler outerInstance)
            {
                this.outerInstance = outerInstance;
            }

            /*     */ /*     */

            public virtual void onServiceConnected(int profile, BluetoothProfile proxy)
            {
                /*  67 */
                IList<BluetoothDevice> deviceList = proxy.ConnectedDevices;
                /*  68 */
                if (deviceList.Count == 0)
                {
                    /*  69 */
                    Log.d("hello", "deviceList.size()==0");
                    /*  70 */
                }
                else if (deviceList.Count == 1)
                {
                    /*  71 */
                    outerInstance.bluetoothDevice = ((BluetoothDevice)deviceList[0]);
                    /*     */
                    /*     */
                    /*     */
                    /*     */
                    /*     */
                    /*  77 */
                    outerInstance.mSocket = BluetoothHelper.connect(outerInstance.bluetoothDevice);
                    /*  78 */
                    if ((outerInstance.mSocket != null) && (outerInstance.mSocket.Connected))
                    {
                        /*     */
                        try
                        {
                            /*  80 */
                            outerInstance.@is = outerInstance.mSocket.InputStream;
                            /*  81 */
                            outerInstance.os = outerInstance.mSocket.OutputStream;
                            /*     */
                        }
                        catch (Exception e)
                        {
                            /*  83 */
                            Console.WriteLine(e.ToString());
                            Console.Write(e.StackTrace);
                            /*     */
                        } /*  85 */
                        Task.Run(() =>
                        {
                            new MyMessageThread(outerInstance).run();
                        });
                        /*     */
                        /*  87 */
                        SppCmdHelper.init(outerInstance.os);
                        /*  88 */
                        SppCmdHelper.reqDeviceSoftwareVersion();
                        /*     */
                        /*  90 */
                        outerInstance.bConnectMasterDevice = Convert.ToBoolean(true);
                        /*  91 */
                        outerInstance.pulseNotifiedListener.onConnectMasterDevice();
                        /*     */
                    } /*     */
                } /*  94 */
                else if (deviceList.Count > 1)
                {
                    /*  95 */
                    Log.d("hello", "device:" + deviceList.Count);
                    /*     */
                } /*     */
            } /*     */
            /*     */

            public virtual void onServiceDisconnected(int profile)
            /*     */
            {
                /* 101 */
                Log.d("hello", "a2dp onServiceDisconnected");
                /* 102 */
                outerInstance.bConnectMasterDevice = Convert.ToBoolean(false);
                /* 103 */
                outerInstance.pulseNotifiedListener.onDisconnectMasterDevice();
                /*     */
            } /* 105 */
        } /*     */
        /*     */

        public virtual bool? RequestDeviceInfo()
        {
            /* 110 */
            if (!this.bConnectMasterDevice.Value)
            {
                /* 111 */
                return Convert.ToBoolean(false);
                /*     */
            } /* 113 */
            return Convert.ToBoolean(true);
            /*     */
        } /*     */
        /*     */

        public virtual bool? GetLEDPattern()
        {
            /* 117 */
            if (!this.bConnectMasterDevice.Value)
            {
                /* 118 */
                return Convert.ToBoolean(false);
                /*     */
            } /* 120 */
            SppCmdHelper.reqLedPatternInfo();
            /* 121 */
            return Convert.ToBoolean(true);
            /*     */
        } /*     */
        /*     */

        public virtual bool? SetDeviceName(string devName, int devIndex)
        {
            /* 125 */
            if (!this.bConnectMasterDevice.Value)
            {
                /* 126 */
                return Convert.ToBoolean(false);
                /*     */
            } /* 128 */
            SppCmdHelper.setDeviceName(devName, 0);
            /* 129 */
            this.device[devIndex].DeviceName = devName;
            /* 130 */
            return Convert.ToBoolean(true);
            /*     */
        } /*     */
        /* 133 */

        public virtual bool? SetDeviceChannel(int devIndex, int channel)
        {
            if (!this.bConnectMasterDevice.Value)
            {
                /* 134 */
                return Convert.ToBoolean(false);
                /*     */
            } /* 136 */
            SppCmdHelper.setDeviceChannel(devIndex, channel);
            /* 137 */
            this.device[devIndex].ActiveChannel = channel;
            /* 138 */
            return Convert.ToBoolean(true);
            /*     */
        } /*     */
        /* 141 */

        public virtual bool? SetLEDPattern(PulseThemePattern pattern)
        {
            if (!this.bConnectMasterDevice.Value)
            {
                /* 142 */
                return Convert.ToBoolean(false);
                /*     */
            } /* 144 */
            SppCmdHelper.LedPattern = pattern.ordinal();
            /* 145 */
            this.LEDPattern = pattern;
            /* 146 */
            return Convert.ToBoolean(true);
            /*     */
        } /*     */
        /* 149 */

        public virtual bool? SetBrightness(int brightness)
        {
            if (!this.bConnectMasterDevice.Value)
            {
                /* 150 */
                return Convert.ToBoolean(false);
                /*     */
            } /* 152 */
            SppCmdHelper.SetBrightness(brightness);
            /* 153 */
            return Convert.ToBoolean(true);
            /*     */
        } /*     */
        /* 156 */

        public virtual bool? SetBackgroundColor(PulseColor color, bool inlcudeSlave)
        {
            if (!this.bConnectMasterDevice.Value)
            {
                /* 157 */
                return Convert.ToBoolean(false);
                /*     */
            } /* 159 */
            int idx = WebColorHelper.RGBToWeb216Index(color);
            /* 160 */
            SppCmdHelper.SetBackgroundColor(idx, inlcudeSlave);
            /* 161 */
            return Convert.ToBoolean(true);
            /*     */
        } /*     */
        /* 164 */

        public virtual bool? SetColorImage(PulseColor[] bitmap)
        {
            if (!this.bConnectMasterDevice.Value)
            {
                /* 165 */
                return Convert.ToBoolean(false);
                /*     */
            } /* 167 */
            int[] idxPixel = new int[99];
            /* 168 */
            for (int i = 0; i < 99; i++)
            {
                /* 169 */
                idxPixel[i] = WebColorHelper.RGBToWeb216Index(bitmap[i]);
                /*     */
            } /* 171 */
            SppCmdHelper.ColorImage = idxPixel;
            /* 172 */
            return Convert.ToBoolean(true);
            /*     */
        } /*     */
        /* 175 */

        public virtual bool? SetCharacterPattern(char character, PulseColor foreground, PulseColor background,
            bool inlcudeSlave)
        {
            if (!this.bConnectMasterDevice.Value)
            {
                /* 176 */
                return Convert.ToBoolean(false);
                /*     */
            } /* 178 */
            int foregroundColor = WebColorHelper.RGBToWeb216Index(foreground);
            /* 179 */
            int backgroundColor = WebColorHelper.RGBToWeb216Index(background);
            /* 180 */
            SppCmdHelper.SetCharacterPattern(character, foregroundColor, backgroundColor, inlcudeSlave);
            /* 181 */
            return Convert.ToBoolean(true);
            /*     */
        } /*     */
        /*     */

        public virtual bool? CaptureColorFromColorPicker()
        {
            /* 185 */
            if (!this.bConnectMasterDevice.Value)
            {
                /* 186 */
                return Convert.ToBoolean(false);
                /*     */
            } /* 188 */
            SppCmdHelper.reqColorFromColorPicker();
            /* 189 */
            return Convert.ToBoolean(true);
            /*     */
        } /*     */
        /*     */

        public virtual bool? PropagateCurrentLedPattern()
        {
            /* 193 */
            if (!this.bConnectMasterDevice.Value)
            {
                /* 194 */
                return Convert.ToBoolean(false);
                /*     */
            } /* 196 */
            SppCmdHelper.PropagateLedPattern();
            /* 197 */
            return Convert.ToBoolean(true);
            /*     */
        } /*     */
        /*     */

        public virtual void GetMicrophoneSoundLevel()
        {
            /* 201 */
            if (!this.bConnectMasterDevice.Value)
            {
                /* 202 */
                return;
                /*     */
            } /* 204 */
            SppCmdHelper.GetMicrophoneSoundLevel();
            /*     */
        } /*     */
        /*     */

        public virtual void SetLEDAndSoundFeedback(int devIndex)
        {
            /* 208 */
            SppCmdHelper.reqLEDAndSoundFeedback(devIndex);
            /*     */
        } /*     */
        /*     */

        public virtual bool? GetBrightness()
        {
            /* 212 */
            if (!this.bConnectMasterDevice.Value)
            {
                /* 213 */
                return Convert.ToBoolean(false);
                /*     */
            } /* 215 */
            SppCmdHelper.cmd_getBrightness();
            /* 216 */
            return Convert.ToBoolean(true);
            /*     */
        } /*     */
        /*     */

        private class MyMessageThread
        {
            private readonly ImplementPulseHandler outerInstance;

            /*     */

            public bool interrupted { get; set; }

            internal MyMessageThread(ImplementPulseHandler outerInstance)
            {
                this.outerInstance = outerInstance;
            }

            /*     */
            /* 222 */

            public virtual void run()
            {
                /* 223 */
                while (!interrupted)
                {
                    /* 224 */
                    if (outerInstance.@is == null)
                    {
                        return;
                    }
                    /* 225 */
                    byte[] msg = new byte['Ѐ'];
                    /*     */
                    try
                    {
                        /* 227 */
                        int readed = outerInstance.@is.Read(msg, 0, msg.Length);
                        /* 228 */
                        sbyte[] buffer = new sbyte[readed];
                        /* 229 */
                        for (int i = 0; i < readed; i++)
                        {
                            /* 230 */
                            buffer[i] = (sbyte)msg[i];
                            /*     */
                        } /* 232 */
                        outerInstance.processMessage(buffer);
                        /*     */
                    }
                    catch (IOException e)
                    {
                        /* 234 */
                        Console.WriteLine(e.ToString());
                        Console.Write(e.StackTrace);
                        /*     */
                    } /*     */
                } /*     */
            } /*     */
        } /*     */
        /*     */

        private void processMessage(sbyte[] buffer)
        {
            /* 241 */
            string result = HexHelper.encodeHexStr(buffer);
            /* 242 */
            Log.d("my_msg", result);
            /*     */
            /* 244 */
            if ((buffer[0] == SppConstant.RET_CMD_ACK[0]) && (buffer[1] == SppConstant.RET_CMD_ACK[1]))
            {
                /* 245 */
                if (buffer[3] == 21)
                {
                    /* 246 */
                    this.SetDevInfoACK = buffer[4];
                    /* 247 */
                }
                else if (buffer[3] == 83)
                {
                    /* 248 */
                    bool ret = buffer[4] == 0;
                    /* 249 */
                    this.pulseNotifiedListener.onRetSetLEDPattern(ret);
                    /*     */
                } /* 251 */
            }
            else if ((buffer[0] == SppConstant.RET_CMD_DEV_INFO[0]) && (buffer[1] == SppConstant.RET_CMD_DEV_INFO[1]))
            {
                /* 252 */
                parseDevInfo(buffer);
                /* 253 */
            }
            else if ((buffer[0] == SppConstant.RET_LED_PATTERN_CHANGE[0]) &&
                     (buffer[1] == SppConstant.RET_LED_PATTERN_CHANGE[1]))
            {
                /* 254 */
                if (buffer[2] <= PulseThemePattern.PulseTheme_Ripple.ordinal())
                {
                    /* 255 */
                    this.LEDPattern = PulseThemePattern.values()[buffer[2]];
                    /* 256 */
                    this.pulseNotifiedListener.onLEDPatternChanged(this.LEDPattern);
                    /*     */
                }
                else
                {
                    /* 258 */
                    this.pulseNotifiedListener.onLEDPatternChanged(null);
                    /*     */
                } /* 260 */
            }
            else if ((buffer[0] == SppConstant.RET_SOUND_EVENT[0]) && (buffer[1] == SppConstant.RET_SOUND_EVENT[1]))
            {
                /* 261 */
                this.pulseNotifiedListener.onSoundEvent(buffer[3]);
                /* 262 */
            }
            else if ((buffer[0] == SppConstant.RET_LED_PATTERN[0]) && (buffer[1] == SppConstant.RET_LED_PATTERN[1]))
            {
                /* 263 */
                if (buffer[3] <= PulseThemePattern.PulseTheme_Ripple.ordinal())
                {
                    /* 264 */
                    this.LEDPattern = PulseThemePattern.values()[buffer[3]];
                    /* 265 */
                    this.pulseNotifiedListener.onRetGetLEDPattern(this.LEDPattern);
                    /*     */
                }
                else
                {
                    /* 267 */
                    this.LEDPattern = PulseThemePattern.PulseTheme_Canvas;
                    /* 268 */
                    this.pulseNotifiedListener.onLEDPatternChanged(this.LEDPattern);
                    /*     */
                } /* 270 */
            }
            else if ((buffer[0] == SppConstant.RET_COLOR_PICKER[0]) && (buffer[1] == SppConstant.RET_COLOR_PICKER[1]))
            {
                /* 271 */
                this.captureColor.red = buffer[3];
                /* 272 */
                this.captureColor.green = buffer[4];
                /* 273 */
                this.captureColor.blue = buffer[5];
                /* 274 */
                this.pulseNotifiedListener.onRetCaptureColor(this.captureColor);
                /* 275 */
                this.pulseNotifiedListener.onRetCaptureColor(buffer[3], buffer[4], buffer[5]);
                /* 276 */
            }
            else if ((buffer[0] == SppConstant.RET_BRIGHTNESS[0]) && (buffer[1] == SppConstant.RET_BRIGHTNESS[1]))
            {
                /* 277 */
                this.pulseNotifiedListener.onRetBrightness(buffer[3]);
                /*     */
            } /*     */
        } /*     */
        /*     */

        private void parseDevInfo(sbyte[] buffer)
        {
            /* 282 */
            int pos = 2;
            /* 283 */
            int msgLen = buffer[pos];
            /* 284 */
            pos++;
            /* 285 */
            int devIndex = buffer[pos];
            /* 286 */
            this.device[devIndex].deviceIndex = devIndex;
            /* 287 */
            pos++;
            /*     */
            /* 289 */
            if (buffer[pos] == -63)
            {
                /* 290 */
                pos++;
                /* 291 */
                int len = buffer[pos];
                /* 292 */
                pos++;
                /* 293 */
                char[] nameChar = new char[len];
                /* 294 */
                for (int i = 0; i < len; i++)
                {
                    /* 295 */
                    nameChar[i] = ((char)buffer[(pos + i)]);
                    /*     */
                } /* 297 */
                pos += len;
                /* 298 */
                this.device[devIndex].DeviceName = new string(nameChar);
                /*     */
            } /* 300 */
            if (pos >= msgLen)
            {
                /* 301 */
                return;
                /*     */
            } /*     */
            /* 304 */
            if (buffer[pos] == 66)
            {
                /* 305 */
                pos++;
                /* 306 */
                sbyte[] pidbyte = new sbyte[2];
                /* 307 */
                for (int i = 0; i < 2; i++)
                {
                    /* 308 */
                    pidbyte[i] = buffer[(pos + i)];
                    /*     */
                } /* 310 */
                this.device[devIndex].product = getPID(pidbyte);
                /* 311 */
                pos += 2;
                /*     */
            } /* 313 */
            if (pos >= msgLen)
            {
                /* 314 */
                return;
                /*     */
            } /*     */
            /* 317 */
            if (buffer[pos] == 67)
            {
                /* 318 */
                pos++;
                /* 319 */
                this.device[devIndex].model = getMID(buffer[pos]);
                /* 320 */
                pos++;
                /*     */
            } /* 322 */
            if (pos >= msgLen)
            {
                /* 323 */
                return;
                /*     */
            } /*     */
            /* 326 */
            if (buffer[pos] == 68)
            {
                /* 327 */
                pos++;
                /* 328 */
                this.device[devIndex].BatteryPower = buffer[pos];
                /* 329 */
                pos++;
                /* 330 */
                int state = this.device[devIndex].BatteryPower >> 7;
                /* 331 */
                if (state == 1)
                {
                    /* 332 */
                    Log.d("hello", "charging: " + this.device[devIndex].BatteryPower);
                    /*     */
                }
                else
                {
                    /* 334 */
                    Log.d("hello", "not charging: " + this.device[devIndex].BatteryPower);
                    /*     */
                } /*     */
            } /* 337 */
            if (pos >= msgLen)
            {
                /* 338 */
                return;
                /*     */
            } /*     */
            /* 341 */
            if (buffer[pos] == 69)
            {
                /* 342 */
                pos++;
                /* 343 */
                this.device[devIndex].LinkedDeviceCount = buffer[pos];
                /* 344 */
                pos++;
                /*     */
            } /* 346 */
            if (pos >= msgLen)
            {
                /* 347 */
                return;
                /*     */
            } /*     */
            /* 350 */
            if (buffer[pos] == 70)
            {
                /* 351 */
                pos++;
                /* 352 */
                this.device[devIndex].ActiveChannel = buffer[pos];
                /* 353 */
                pos++;
                /*     */
            } /* 355 */
            if (pos >= msgLen)
            {
                /* 356 */
                return;
                /*     */
            } /*     */
            /* 359 */
            if (buffer[pos] == 71)
            {
                /* 360 */
                pos++;
                /* 361 */
                this.device[devIndex].AudioSource = buffer[pos];
                /* 362 */
                pos++;
                /*     */
            } /* 364 */
            if (pos >= msgLen)
            {
                /* 365 */
                return;
                /*     */
            } /*     */
            /* 368 */
            if (buffer[pos] == 72)
            {
                /* 369 */
                pos++;
                /* 370 */
                sbyte[] macbyte = new sbyte[6];
                /* 371 */
                for (int i = 0; i < 6; i++)
                {
                    /* 372 */
                    macbyte[i] = buffer[(pos + i)];
                    /*     */
                } /* 374 */
                string tmp = HexHelper.encodeHexStr(macbyte).ToUpper();
                /* 375 */
                this.device[devIndex].Mac = "";
                /* 376 */
                for (int j = 0; j < tmp.Length; j += 2)
                {
                    /* 377 */
                    this.device[devIndex].Mac += tmp[j];
                    /* 378 */
                    this.device[devIndex].Mac += tmp[j + 1];
                    /* 379 */
                    if (j != tmp.Length - 2)
                    {
                        /* 380 */
                        this.device[devIndex].Mac += ":";
                        /*     */
                    } /*     */
                } /*     */
            } /*     */
            /* 385 */
            Log.d("hello", "name : " + this.device[devIndex].DeviceName);
            /* 386 */
            Log.d("hello", "PID : " + this.device[devIndex].product);
            /* 387 */
            Log.d("hello", "MID : " + this.device[devIndex].model);
            /* 388 */
            Log.d("hello", "battery: " + this.device[devIndex].BatteryPower);
            /* 389 */
            Log.d("hello", "get linked device count: " + this.device[devIndex].LinkedDeviceCount);
            /* 390 */
            Log.d("hello", "get active channel : " + this.device[devIndex].ActiveChannel);
            /* 391 */
            Log.d("hello", "get audio source : " + this.device[devIndex].AudioSource);
            /* 392 */
            Log.d("hello", "mac : " + this.device[devIndex].Mac);
            /*     */
            /* 394 */
            this.bGetDeviceInfo = Convert.ToBoolean(true);
            /*     */
        } /*     */
        /*     */ /* Error */
        /*     */

        private void getDeviceInfo()
        /*     */
        {
            /*     */ // Byte code:
            /*     */ //   0: aload_0
            /*     */
            //   1: getfield 13	com/harman/pulsesdk/ImplementPulseHandler:lock	Ljava/util/concurrent/locks/Lock;
            /*     */ //   4: invokeinterface 108 1 0
            /*     */ //   9: invokestatic 36	com/harman/pulsesdk/SppCmdHelper:reqLedPatternInfo	()V
            /*     */ //   12: invokestatic 109	com/harman/pulsesdk/SppCmdHelper:reqDevInfo	()V
            /*     */ //   15: aload_0
            /*     */
            //   16: getfield 15	com/harman/pulsesdk/ImplementPulseHandler:conditionA	Ljava/util/concurrent/locks/Condition;
            /*     */ //   19: ldc2_w 110
            /*     */ //   22: invokeinterface 112 3 0
            /*     */ //   27: pop2
            /*     */ //   28: goto +8 -> 36
            /*     */ //   31: astore_1
            /*     */ //   32: aload_1
            /*     */ //   33: invokevirtual 114	java/lang/InterruptedException:printStackTrace	()V
            /*     */ //   36: getstatic 115	java/lang/System:out	Ljava/io/PrintStream;
            /*     */ //   39: new 84	java/lang/StringBuilder
            /*     */ //   42: dup
            /*     */ //   43: invokespecial 85	java/lang/StringBuilder:<init>	()V
            /*     */ //   46: invokestatic 116	java/lang/Thread:currentThread	()Ljava/lang/Thread;
            /*     */ //   49: invokevirtual 117	java/lang/Thread:getName	()Ljava/lang/String;
            /*     */
            //   52: invokevirtual 87	java/lang/StringBuilder:append	(Ljava/lang/String;)Ljava/lang/StringBuilder;
            /*     */ //   55: ldc 118
            /*     */
            //   57: invokevirtual 87	java/lang/StringBuilder:append	(Ljava/lang/String;)Ljava/lang/StringBuilder;
            /*     */ //   60: invokevirtual 89	java/lang/StringBuilder:toString	()Ljava/lang/String;
            /*     */ //   63: invokevirtual 119	java/io/PrintStream:println	(Ljava/lang/String;)V
            /*     */ //   66: aload_0
            /*     */
            //   67: getfield 13	com/harman/pulsesdk/ImplementPulseHandler:lock	Ljava/util/concurrent/locks/Lock;
            /*     */ //   70: invokeinterface 120 1 0
            /*     */ //   75: goto +15 -> 90
            /*     */ //   78: astore_2
            /*     */ //   79: aload_0
            /*     */
            //   80: getfield 13	com/harman/pulsesdk/ImplementPulseHandler:lock	Ljava/util/concurrent/locks/Lock;
            /*     */ //   83: invokeinterface 120 1 0
            /*     */ //   88: aload_2
            /*     */ //   89: athrow
            /*     */ //   90: return
            /*     */ // Line number table:
            /*     */ //   Java source line #399	-> byte code offset #0
            /*     */ //   Java source line #402	-> byte code offset #9
            /*     */ //   Java source line #403	-> byte code offset #12
            /*     */ //   Java source line #404	-> byte code offset #15
            /*     */ //   Java source line #407	-> byte code offset #28
            /*     */ //   Java source line #405	-> byte code offset #31
            /*     */ //   Java source line #406	-> byte code offset #32
            /*     */ //   Java source line #408	-> byte code offset #36
            /*     */ //   Java source line #410	-> byte code offset #66
            /*     */ //   Java source line #411	-> byte code offset #75
            /*     */ //   Java source line #410	-> byte code offset #78
            /*     */ //   Java source line #412	-> byte code offset #90
            /*     */ // Local variable table:
            /*     */ //   start	length	slot	name	signature
            /*     */ //   0	91	0	this	ImplementPulseHandler
            /*     */ //   31	2	1	e	InterruptedException
            /*     */ //   78	11	2	localObject	Object
            /*     */ // Exception table:
            /*     */ //   from	to	target	type
            /*     */ //   9	28	31	java/lang/InterruptedException
            /*     */ //   9	66	78	finally
            /*     */
        } /*     */
        /*     */ /* Error */
        /*     */

        private void retGetDeviceInfo()
        /*     */
        {
            /*     */ // Byte code:
            /*     */ //   0: aload_0
            /*     */
            //   1: getfield 13	com/harman/pulsesdk/ImplementPulseHandler:lock	Ljava/util/concurrent/locks/Lock;
            /*     */ //   4: invokeinterface 108 1 0
            /*     */ //   9: getstatic 115	java/lang/System:out	Ljava/io/PrintStream;
            /*     */ //   12: new 84	java/lang/StringBuilder
            /*     */ //   15: dup
            /*     */ //   16: invokespecial 85	java/lang/StringBuilder:<init>	()V
            /*     */ //   19: invokestatic 116	java/lang/Thread:currentThread	()Ljava/lang/Thread;
            /*     */ //   22: invokevirtual 117	java/lang/Thread:getName	()Ljava/lang/String;
            /*     */
            //   25: invokevirtual 87	java/lang/StringBuilder:append	(Ljava/lang/String;)Ljava/lang/StringBuilder;
            /*     */ //   28: ldc 121
            /*     */
            //   30: invokevirtual 87	java/lang/StringBuilder:append	(Ljava/lang/String;)Ljava/lang/StringBuilder;
            /*     */ //   33: invokevirtual 89	java/lang/StringBuilder:toString	()Ljava/lang/String;
            /*     */ //   36: invokevirtual 119	java/io/PrintStream:println	(Ljava/lang/String;)V
            /*     */ //   39: aload_0
            /*     */
            //   40: getfield 15	com/harman/pulsesdk/ImplementPulseHandler:conditionA	Ljava/util/concurrent/locks/Condition;
            /*     */ //   43: invokeinterface 122 1 0
            /*     */ //   48: aload_0
            /*     */
            //   49: getfield 13	com/harman/pulsesdk/ImplementPulseHandler:lock	Ljava/util/concurrent/locks/Lock;
            /*     */ //   52: invokeinterface 120 1 0
            /*     */ //   57: goto +15 -> 72
            /*     */ //   60: astore_1
            /*     */ //   61: aload_0
            /*     */
            //   62: getfield 13	com/harman/pulsesdk/ImplementPulseHandler:lock	Ljava/util/concurrent/locks/Lock;
            /*     */ //   65: invokeinterface 120 1 0
            /*     */ //   70: aload_1
            /*     */ //   71: athrow
            /*     */ //   72: return
            /*     */ // Line number table:
            /*     */ //   Java source line #415	-> byte code offset #0
            /*     */ //   Java source line #417	-> byte code offset #9
            /*     */ //   Java source line #418	-> byte code offset #39
            /*     */ //   Java source line #420	-> byte code offset #48
            /*     */ //   Java source line #421	-> byte code offset #57
            /*     */ //   Java source line #420	-> byte code offset #60
            /*     */ //   Java source line #422	-> byte code offset #72
            /*     */ // Local variable table:
            /*     */ //   start	length	slot	name	signature
            /*     */ //   0	73	0	this	ImplementPulseHandler
            /*     */ //   60	11	1	localObject	Object
            /*     */ // Exception table:
            /*     */ //   from	to	target	type
            /*     */ //   9	48	60	finally
            /*     */
        } /*     */
        /*     */ /* Error */
        /*     */

        private void setDeviceInfo()
        /*     */
        {
            /*     */ // Byte code:
            /*     */ //   0: aload_0
            /*     */
            //   1: getfield 16	com/harman/pulsesdk/ImplementPulseHandler:lockSetDev	Ljava/util/concurrent/locks/Lock;
            /*     */ //   4: invokeinterface 108 1 0
            /*     */ //   9: aload_0
            /*     */
            //   10: getfield 17	com/harman/pulsesdk/ImplementPulseHandler:condSetDev	Ljava/util/concurrent/locks/Condition;
            /*     */ //   13: ldc2_w 110
            /*     */ //   16: invokeinterface 112 3 0
            /*     */ //   21: pop2
            /*     */ //   22: goto +8 -> 30
            /*     */ //   25: astore_1
            /*     */ //   26: aload_1
            /*     */ //   27: invokevirtual 114	java/lang/InterruptedException:printStackTrace	()V
            /*     */ //   30: getstatic 115	java/lang/System:out	Ljava/io/PrintStream;
            /*     */ //   33: new 84	java/lang/StringBuilder
            /*     */ //   36: dup
            /*     */ //   37: invokespecial 85	java/lang/StringBuilder:<init>	()V
            /*     */ //   40: invokestatic 116	java/lang/Thread:currentThread	()Ljava/lang/Thread;
            /*     */ //   43: invokevirtual 117	java/lang/Thread:getName	()Ljava/lang/String;
            /*     */
            //   46: invokevirtual 87	java/lang/StringBuilder:append	(Ljava/lang/String;)Ljava/lang/StringBuilder;
            /*     */ //   49: ldc 123
            /*     */
            //   51: invokevirtual 87	java/lang/StringBuilder:append	(Ljava/lang/String;)Ljava/lang/StringBuilder;
            /*     */ //   54: invokevirtual 89	java/lang/StringBuilder:toString	()Ljava/lang/String;
            /*     */ //   57: invokevirtual 119	java/io/PrintStream:println	(Ljava/lang/String;)V
            /*     */ //   60: aload_0
            /*     */
            //   61: getfield 16	com/harman/pulsesdk/ImplementPulseHandler:lockSetDev	Ljava/util/concurrent/locks/Lock;
            /*     */ //   64: invokeinterface 120 1 0
            /*     */ //   69: goto +15 -> 84
            /*     */ //   72: astore_2
            /*     */ //   73: aload_0
            /*     */
            //   74: getfield 16	com/harman/pulsesdk/ImplementPulseHandler:lockSetDev	Ljava/util/concurrent/locks/Lock;
            /*     */ //   77: invokeinterface 120 1 0
            /*     */ //   82: aload_2
            /*     */ //   83: athrow
            /*     */ //   84: return
            /*     */ // Line number table:
            /*     */ //   Java source line #425	-> byte code offset #0
            /*     */ //   Java source line #428	-> byte code offset #9
            /*     */ //   Java source line #431	-> byte code offset #22
            /*     */ //   Java source line #429	-> byte code offset #25
            /*     */ //   Java source line #430	-> byte code offset #26
            /*     */ //   Java source line #432	-> byte code offset #30
            /*     */ //   Java source line #434	-> byte code offset #60
            /*     */ //   Java source line #435	-> byte code offset #69
            /*     */ //   Java source line #434	-> byte code offset #72
            /*     */ //   Java source line #436	-> byte code offset #84
            /*     */ // Local variable table:
            /*     */ //   start	length	slot	name	signature
            /*     */ //   0	85	0	this	ImplementPulseHandler
            /*     */ //   25	2	1	e	InterruptedException
            /*     */ //   72	11	2	localObject	Object
            /*     */ // Exception table:
            /*     */ //   from	to	target	type
            /*     */ //   9	22	25	java/lang/InterruptedException
            /*     */ //   9	60	72	finally
            /*     */
        } /*     */
        /*     */ /* Error */
        /*     */

        private void retSetDeviceInfo()
        /*     */
        {
            /*     */ // Byte code:
            /*     */ //   0: aload_0
            /*     */
            //   1: getfield 16	com/harman/pulsesdk/ImplementPulseHandler:lockSetDev	Ljava/util/concurrent/locks/Lock;
            /*     */ //   4: invokeinterface 108 1 0
            /*     */ //   9: getstatic 115	java/lang/System:out	Ljava/io/PrintStream;
            /*     */ //   12: new 84	java/lang/StringBuilder
            /*     */ //   15: dup
            /*     */ //   16: invokespecial 85	java/lang/StringBuilder:<init>	()V
            /*     */ //   19: invokestatic 116	java/lang/Thread:currentThread	()Ljava/lang/Thread;
            /*     */ //   22: invokevirtual 117	java/lang/Thread:getName	()Ljava/lang/String;
            /*     */
            //   25: invokevirtual 87	java/lang/StringBuilder:append	(Ljava/lang/String;)Ljava/lang/StringBuilder;
            /*     */ //   28: ldc 124
            /*     */
            //   30: invokevirtual 87	java/lang/StringBuilder:append	(Ljava/lang/String;)Ljava/lang/StringBuilder;
            /*     */ //   33: invokevirtual 89	java/lang/StringBuilder:toString	()Ljava/lang/String;
            /*     */ //   36: invokevirtual 119	java/io/PrintStream:println	(Ljava/lang/String;)V
            /*     */ //   39: aload_0
            /*     */
            //   40: getfield 17	com/harman/pulsesdk/ImplementPulseHandler:condSetDev	Ljava/util/concurrent/locks/Condition;
            /*     */ //   43: invokeinterface 122 1 0
            /*     */ //   48: aload_0
            /*     */
            //   49: getfield 16	com/harman/pulsesdk/ImplementPulseHandler:lockSetDev	Ljava/util/concurrent/locks/Lock;
            /*     */ //   52: invokeinterface 120 1 0
            /*     */ //   57: goto +15 -> 72
            /*     */ //   60: astore_1
            /*     */ //   61: aload_0
            /*     */
            //   62: getfield 16	com/harman/pulsesdk/ImplementPulseHandler:lockSetDev	Ljava/util/concurrent/locks/Lock;
            /*     */ //   65: invokeinterface 120 1 0
            /*     */ //   70: aload_1
            /*     */ //   71: athrow
            /*     */ //   72: return
            /*     */ // Line number table:
            /*     */ //   Java source line #439	-> byte code offset #0
            /*     */ //   Java source line #441	-> byte code offset #9
            /*     */ //   Java source line #442	-> byte code offset #39
            /*     */ //   Java source line #444	-> byte code offset #48
            /*     */ //   Java source line #445	-> byte code offset #57
            /*     */ //   Java source line #444	-> byte code offset #60
            /*     */ //   Java source line #446	-> byte code offset #72
            /*     */ // Local variable table:
            /*     */ //   start	length	slot	name	signature
            /*     */ //   0	73	0	this	ImplementPulseHandler
            /*     */ //   60	11	1	localObject	Object
            /*     */ // Exception table:
            /*     */ //   from	to	target	type
            /*     */ //   9	48	60	finally
            /*     */
        } /*     */
        /*     */

        private string getPID(sbyte[] pid)
        /*     */
        {
            /* 449 */
            if ((pid[0] == 0) && (pid[1] == 38))
            {
                /* 450 */
                return "JBL Pulse 2";
                /*     */
            } /* 452 */
            return "";
            /*     */
        } /*     */
        /*     */

        private string getMID(sbyte mid)
        /*     */
        {
            /* 457 */
            if (mid == 1)
            /* 458 */
            {
                return "black";
            }
            /* 459 */
            if (mid == 2)
            {
                /* 460 */
                return "white";
                /*     */
            } /* 462 */
            return "";
            /*     */
        } /*     */
    }

    /* Location:              D:\Hack\classes.jar!\com\harman\pulsesdk\ImplementPulseHandler.class
	 * Java compiler version: 7 (51.0)
	 * JD-Core Version:       0.7.1
	 */
}