using System;
using System.Collections.Generic;
using System.Diagnostics;

/*     */

namespace Harman.Pulse
{
    /*     */
    /*     */
    /*     */ /*     */ /*     */
    /*     */

    public class SppCmdHelper
        /*     */
    {
        /*     */

        public sealed class SppCmdType
            /*     */
        {
            /*  11 */
            public static readonly SppCmdType cmd_none = new SppCmdType("cmd_none", InnerEnum.cmd_none);
            /*  12 */
            public static readonly SppCmdType cmd_get_pid = new SppCmdType("cmd_get_pid", InnerEnum.cmd_get_pid);
            /*  13 */
            public static readonly SppCmdType cmd_get_mid = new SppCmdType("cmd_get_mid", InnerEnum.cmd_get_mid);
            /*  14 */

            public static readonly SppCmdType cmd_get_battery_status = new SppCmdType("cmd_get_battery_status",
                InnerEnum.cmd_get_battery_status);

            /*  15 */

            public static readonly SppCmdType cmd_set_left_channel = new SppCmdType("cmd_set_left_channel",
                InnerEnum.cmd_set_left_channel);

            /*  16 */

            public static readonly SppCmdType cmd_set_right_channel = new SppCmdType("cmd_set_right_channel",
                InnerEnum.cmd_set_right_channel);

            /*  17 */

            public static readonly SppCmdType cmd_set_stereo_channel = new SppCmdType("cmd_set_stereo_channel",
                InnerEnum.cmd_set_stereo_channel);

            /*  18 */

            public static readonly SppCmdType cmd_set_device_name = new SppCmdType("cmd_set_device_name",
                InnerEnum.cmd_set_device_name);

            /*  19 */

            public static readonly SppCmdType cmd_get_linked_device_count = new SppCmdType(
                "cmd_get_linked_device_count", InnerEnum.cmd_get_linked_device_count);

            /*  20 */

            public static readonly SppCmdType cmd_get_active_channel = new SppCmdType("cmd_get_active_channel",
                InnerEnum.cmd_get_active_channel);

            /*  21 */

            public static readonly SppCmdType cmd_get_audio_source = new SppCmdType("cmd_get_audio_source",
                InnerEnum.cmd_get_audio_source);

            /*  22 */

            public static readonly SppCmdType cmd_req_link_dev = new SppCmdType("cmd_req_link_dev",
                InnerEnum.cmd_req_link_dev);

            /*  23 */

            public static readonly SppCmdType cmd_req_drop_link_dev = new SppCmdType("cmd_req_drop_link_dev",
                InnerEnum.cmd_req_drop_link_dev);

            /*  24 */

            public static readonly SppCmdType cmd_req_start_linking = new SppCmdType("cmd_req_start_linking",
                InnerEnum.cmd_req_start_linking);

            /*  25 */

            public static readonly SppCmdType cmd_req_led_and_sound_feedback =
                new SppCmdType("cmd_req_led_and_sound_feedback", InnerEnum.cmd_req_led_and_sound_feedback);

            /*  26 */

            public static readonly SppCmdType cmd_req_device_software_version =
                new SppCmdType("cmd_req_device_software_version", InnerEnum.cmd_req_device_software_version);

            /*  27 */

            public static readonly SppCmdType cmd_req_dfu_start = new SppCmdType("cmd_req_dfu_start",
                InnerEnum.cmd_req_dfu_start);

            /*  28 */

            public static readonly SppCmdType cmd_req_dfu_start_with_sec_id =
                new SppCmdType("cmd_req_dfu_start_with_sec_id", InnerEnum.cmd_req_dfu_start_with_sec_id);

            /*  29 */

            public static readonly SppCmdType cmd_req_color_from_color_picker =
                new SppCmdType("cmd_req_color_from_color_picker", InnerEnum.cmd_req_color_from_color_picker);

            /*  30 */

            public static readonly SppCmdType cmd_notify_dfu_tart = new SppCmdType("cmd_notify_dfu_tart",
                InnerEnum.cmd_notify_dfu_tart);

            /*  31 */

            public static readonly SppCmdType cmd_set_dfu_data = new SppCmdType("cmd_set_dfu_data",
                InnerEnum.cmd_set_dfu_data);

            /*  32 */

            public static readonly SppCmdType cmd_notify_Sec_Start = new SppCmdType("cmd_notify_Sec_Start",
                InnerEnum.cmd_notify_Sec_Start);

            /*  33 */

            public static readonly SppCmdType cmd_reqLedPatternInfo = new SppCmdType("cmd_reqLedPatternInfo",
                InnerEnum.cmd_reqLedPatternInfo);

            /*  34 */

            public static readonly SppCmdType cmd_setLedPattern = new SppCmdType("cmd_setLedPattern",
                InnerEnum.cmd_setLedPattern);

            /*  35 */

            public static readonly SppCmdType cmd_setBrightness = new SppCmdType("cmd_setBrightness",
                InnerEnum.cmd_setBrightness);

            /*  36 */

            public static readonly SppCmdType cmd_setBackgroundColor = new SppCmdType("cmd_setBackgroundColor",
                InnerEnum.cmd_setBackgroundColor);

            /*  37 */

            public static readonly SppCmdType cmd_setColorImage = new SppCmdType("cmd_setColorImage",
                InnerEnum.cmd_setColorImage);

            /*  38 */

            public static readonly SppCmdType cmd_setCharacterPattern = new SppCmdType("cmd_setCharacterPattern",
                InnerEnum.cmd_setCharacterPattern);

            /*  39 */

            public static readonly SppCmdType cmd_propagateLedPattern = new SppCmdType("cmd_propagateLedPattern",
                InnerEnum.cmd_propagateLedPattern);

            /*  40 */

            public static readonly SppCmdType cmd_getMicrophoneSoundLevel = new SppCmdType(
                "cmd_getMicrophoneSoundLevel", InnerEnum.cmd_getMicrophoneSoundLevel);

            /*  41 */

            public static readonly SppCmdType cmd_getBrightness = new SppCmdType("cmd_getBrightness",
                InnerEnum.cmd_getBrightness);

            private static readonly IList<SppCmdType> valueList = new List<SppCmdType>();

            static SppCmdType()
            {
                valueList.Add(cmd_none);
                valueList.Add(cmd_get_pid);
                valueList.Add(cmd_get_mid);
                valueList.Add(cmd_get_battery_status);
                valueList.Add(cmd_set_left_channel);
                valueList.Add(cmd_set_right_channel);
                valueList.Add(cmd_set_stereo_channel);
                valueList.Add(cmd_set_device_name);
                valueList.Add(cmd_get_linked_device_count);
                valueList.Add(cmd_get_active_channel);
                valueList.Add(cmd_get_audio_source);
                valueList.Add(cmd_req_link_dev);
                valueList.Add(cmd_req_drop_link_dev);
                valueList.Add(cmd_req_start_linking);
                valueList.Add(cmd_req_led_and_sound_feedback);
                valueList.Add(cmd_req_device_software_version);
                valueList.Add(cmd_req_dfu_start);
                valueList.Add(cmd_req_dfu_start_with_sec_id);
                valueList.Add(cmd_req_color_from_color_picker);
                valueList.Add(cmd_notify_dfu_tart);
                valueList.Add(cmd_set_dfu_data);
                valueList.Add(cmd_notify_Sec_Start);
                valueList.Add(cmd_reqLedPatternInfo);
                valueList.Add(cmd_setLedPattern);
                valueList.Add(cmd_setBrightness);
                valueList.Add(cmd_setBackgroundColor);
                valueList.Add(cmd_setColorImage);
                valueList.Add(cmd_setCharacterPattern);
                valueList.Add(cmd_propagateLedPattern);
                valueList.Add(cmd_getMicrophoneSoundLevel);
                valueList.Add(cmd_getBrightness);
            }

            public enum InnerEnum
            {
                cmd_none,
                cmd_get_pid,
                cmd_get_mid,
                cmd_get_battery_status,
                cmd_set_left_channel,
                cmd_set_right_channel,
                cmd_set_stereo_channel,
                cmd_set_device_name,
                cmd_get_linked_device_count,
                cmd_get_active_channel,
                cmd_get_audio_source,
                cmd_req_link_dev,
                cmd_req_drop_link_dev,
                cmd_req_start_linking,
                cmd_req_led_and_sound_feedback,
                cmd_req_device_software_version,
                cmd_req_dfu_start,
                cmd_req_dfu_start_with_sec_id,
                cmd_req_color_from_color_picker,
                cmd_notify_dfu_tart,
                cmd_set_dfu_data,
                cmd_notify_Sec_Start,
                cmd_reqLedPatternInfo,
                cmd_setLedPattern,
                cmd_setBrightness,
                cmd_setBackgroundColor,
                cmd_setColorImage,
                cmd_setCharacterPattern,
                cmd_propagateLedPattern,
                cmd_getMicrophoneSoundLevel,
                cmd_getBrightness
            }

            private readonly string nameValue;
            private readonly int ordinalValue;
            private readonly InnerEnum innerEnumValue;
            private static int nextOrdinal = 0;

            private SppCmdType(string name, InnerEnum innerEnum)
            {
                nameValue = name;
                ordinalValue = nextOrdinal++;
                innerEnumValue = innerEnum;
            }

            /*     */

            public static IList<SppCmdType> values()
            {
                return valueList;
            }

            public InnerEnum InnerEnumValue()
            {
                return innerEnumValue;
            }

            public int ordinal()
            {
                return ordinalValue;
            }

            public override string ToString()
            {
                return nameValue;
            }

            public static SppCmdType valueOf(string name)
            {
                foreach (SppCmdType enumInstance in SppCmdType.values())
                {
                    if (enumInstance.nameValue == name)
                    {
                        return enumInstance;
                    }
                }
                throw new System.ArgumentException(name);
            }
        }

        /*     */
        /*  46 */

        public sealed class ByeByeReason
        {
            public static readonly ByeByeReason unknown = new ByeByeReason("unknown", InnerEnum.unknown);
            /*  47 */

            public static readonly ByeByeReason device_power_off = new ByeByeReason("device_power_off",
                InnerEnum.device_power_off);

            /*  48 */

            public static readonly ByeByeReason kick_by_other_app = new ByeByeReason("kick_by_other_app",
                InnerEnum.kick_by_other_app);

            private static readonly IList<ByeByeReason> valueList = new List<ByeByeReason>();

            static ByeByeReason()
            {
                valueList.Add(unknown);
                valueList.Add(device_power_off);
                valueList.Add(kick_by_other_app);
            }

            public enum InnerEnum
            {
                unknown,
                device_power_off,
                kick_by_other_app
            }

            private readonly string nameValue;
            private readonly int ordinalValue;
            private readonly InnerEnum innerEnumValue;
            private static int nextOrdinal = 0;

            private ByeByeReason(string name, InnerEnum innerEnum)
            {
                nameValue = name;
                ordinalValue = nextOrdinal++;
                innerEnumValue = innerEnum;
            }

            /*     */

            public static IList<ByeByeReason> values()
            {
                return valueList;
            }

            public InnerEnum InnerEnumValue()
            {
                return innerEnumValue;
            }

            public int ordinal()
            {
                return ordinalValue;
            }

            public override string ToString()
            {
                return nameValue;
            }

            public static ByeByeReason valueOf(string name)
            {
                foreach (ByeByeReason enumInstance in ByeByeReason.values())
                {
                    if (enumInstance.nameValue == name)
                    {
                        return enumInstance;
                    }
                }
                throw new System.ArgumentException(name);
            }
        }

        /*     */
        /*  53 */

        public sealed class UpgradeSectionId
        {
            public static readonly UpgradeSectionId MCU = new UpgradeSectionId("MCU", InnerEnum.MCU);
            /*  54 */
            public static readonly UpgradeSectionId BT = new UpgradeSectionId("BT", InnerEnum.BT);
            /*  55 */

            public static readonly UpgradeSectionId TRADITIONAL = new UpgradeSectionId("TRADITIONAL",
                InnerEnum.TRADITIONAL);

            /*  56 */
            public static readonly UpgradeSectionId None = new UpgradeSectionId("None", InnerEnum.None);

            private static readonly IList<UpgradeSectionId> valueList = new List<UpgradeSectionId>();

            static UpgradeSectionId()
            {
                valueList.Add(MCU);
                valueList.Add(BT);
                valueList.Add(TRADITIONAL);
                valueList.Add(None);
            }

            public enum InnerEnum
            {
                MCU,
                BT,
                TRADITIONAL,
                None
            }

            private readonly string nameValue;
            private readonly int ordinalValue;
            private readonly InnerEnum innerEnumValue;
            private static int nextOrdinal = 0;

            private UpgradeSectionId(string name, InnerEnum innerEnum)
            {
                nameValue = name;
                ordinalValue = nextOrdinal++;
                innerEnumValue = innerEnum;
            }

            /*     */
            /*     */

            public static IList<UpgradeSectionId> values()
            {
                return valueList;
            }

            public InnerEnum InnerEnumValue()
            {
                return innerEnumValue;
            }

            public int ordinal()
            {
                return ordinalValue;
            }

            public override string ToString()
            {
                return nameValue;
            }

            public static UpgradeSectionId valueOf(string name)
            {
                foreach (UpgradeSectionId enumInstance in UpgradeSectionId.values())
                {
                    if (enumInstance.nameValue == name)
                    {
                        return enumInstance;
                    }
                }
                throw new System.ArgumentException(name);
            }
        }

        /*  59 */
        public static SppCmdType bleCmdType = SppCmdType.cmd_none;
        /*     */
        /*     */
        internal static System.IO.Stream os;
        /*     */
        /*     */
        /*  64 */

        public static void init(System.IO.Stream stream)
        {
            os = stream;
        }

        /*     */
        /*     */

        public static sbyte[] getDeviceCmd(string deviceName, int devIndex)
        {
            /*  67 */
            if (deviceName.Length > 16)
                /*     */
            {
                /*  69 */
                deviceName = deviceName.Substring(0, 16);
                /*     */
            } /*     */
            /*  72 */
            string hexDeviceName = HexHelper.encodeHexStr(deviceName.GetBytes());
            /*  73 */
            sbyte[] data = HexHelper.decodeHex(hexDeviceName.ToCharArray());
            /*     */
            /*  75 */
            sbyte deviceNameLen = (sbyte) data.Length;
            /*  76 */
            sbyte payloadLen = (sbyte) (deviceNameLen + 3);
            /*  77 */
            sbyte[] cmd_set_device_name = new sbyte[] {-86, 21, payloadLen, (sbyte) devIndex, -63, deviceNameLen};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*  87 */
            List<sbyte?> cmd = new List<sbyte?>();
            /*     */
            /*  89 */
            for (int i = 0; i < cmd_set_device_name.Length; i++)
                /*     */
            {
                /*  91 */
                cmd.Add(Convert.ToSByte(cmd_set_device_name[i]));
                /*     */
            } /*     */
            /*  94 */
            for (int i = 0; i < data.Length; i++)
                /*     */
            {
                /*  96 */
                cmd.Add(Convert.ToSByte(data[i]));
                /*     */
            } /*     */
            /*  99 */
            sbyte[] cmdFull = new sbyte[cmd.Count];
            /* 100 */
            for (int i = 0; i < cmd.Count; i++)
                /*     */
            {
                /* 102 */
                cmdFull[i] = ((sbyte?) cmd[i]).Value;
                /*     */
            } /*     */
            /* 105 */
            return cmdFull;
            /*     */
        } /*     */
        /*     */

        public static sbyte[] getReqLinkDevCmd(string mac)
            /*     */
        {
            /* 110 */
            string hexMac = mac;
            /* 111 */
            sbyte[] data = HexHelper.decodeHex(hexMac.ToCharArray());
            /*     */
            /* 113 */
            sbyte[] CMD_REQ_LINK_DEV = new sbyte[] {-86, 33, 6, 0, 0, 0, 0, 0, 0};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 125 */
            for (int i = 0; i < 6; i++)
                /*     */
            {
                /* 127 */
                CMD_REQ_LINK_DEV[(i + 3)] = data[i];
                /*     */
            } /* 129 */
            return CMD_REQ_LINK_DEV;
            /*     */
        } /*     */
        /*     */

        public static void sendCmd(sbyte[] value)
            /*     */
        {
            /* 134 */
            string result = HexHelper.encodeHexStr(value);
            /* 135 */
            Debug.WriteLine("sendCmd", result);
            /*     */
            /* 137 */
            if (os == null)
            {
                return;
            }
            /*     */
            try
            {
                /* 139 */
                os.Write(ByteHelper.ToByteArray(value), 0, value.Length);
                /* 140 */
                os.Flush();
                /*     */
            }
            catch (Exception e)
            {
                /* 142 */
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                /*     */
            } /*     */
        } /*     */
        /*     */

        public static void setDeviceName(string deviceName, int devIndex)
            /*     */
        {
            /* 148 */
            bleCmdType = SppCmdType.cmd_set_device_name;
            /* 149 */
            sbyte[] cmd = getDeviceCmd(deviceName, devIndex);
            /* 150 */
            sendCmd(cmd);
            /*     */
        } /*     */
        /*     */

        public static void getPID(int devIndex)
            /*     */
        {
            /* 155 */
            bleCmdType = SppCmdType.cmd_get_pid;
            /* 156 */
            sbyte[] CMD_GET_PID = new sbyte[] {-86, 19, 2, (sbyte) devIndex, 66};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 164 */
            sendCmd(CMD_GET_PID);
            /*     */
        } /*     */
        /*     */

        public static void getMID(int devIndex)
            /*     */
        {
            /* 169 */
            bleCmdType = SppCmdType.cmd_get_mid;
            /* 170 */
            sbyte[] CMD_GET_MID = new sbyte[] {-86, 19, 2, (sbyte) devIndex, 67};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 178 */
            sendCmd(CMD_GET_MID);
            /*     */
        } /*     */
        /*     */

        public static void setMID(int devIndex, int mid)
            /*     */
        {
            /* 183 */
            bleCmdType = SppCmdType.cmd_get_mid;
            /* 184 */
            sbyte[] CMD_GET_MID = new sbyte[] {-86, 21, 2, (sbyte) devIndex, 67, (sbyte) mid};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 193 */
            sendCmd(CMD_GET_MID);
            /*     */
        } /*     */
        /*     */

        public static void getBatteryStatus(int devIndex)
            /*     */
        {
            /* 198 */
            bleCmdType = SppCmdType.cmd_get_battery_status;
            /* 199 */
            sbyte[] CMD_GET_BATTERY_STATUS = new sbyte[] {-86, 19, 2, (sbyte) devIndex, 68};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 207 */
            sendCmd(CMD_GET_BATTERY_STATUS);
            /*     */
        } /*     */
        /*     */

        public static void getLinkedDeviceCount(int devIndex)
            /*     */
        {
            /* 212 */
            bleCmdType = SppCmdType.cmd_get_linked_device_count;
            /* 213 */
            sbyte[] CMD_GET_LINKED_DEVICE_COUNT = new sbyte[] {-86, 19, 2, (sbyte) devIndex, 69};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 221 */
            sendCmd(CMD_GET_LINKED_DEVICE_COUNT);
            /*     */
        } /*     */
        /*     */

        public static void setDeviceChannel(int devIndex, int channel)
            /*     */
        {
            /* 226 */
            sbyte[] CMD_SET_ACTIVE_CHANNEL = new sbyte[] {-86, 21, 3, (sbyte) devIndex, 70, (sbyte) channel};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 235 */
            sendCmd(CMD_SET_ACTIVE_CHANNEL);
            /*     */
        } /*     */
        /*     */

        public static void getActiveChannel(int devIndex)
            /*     */
        {
            /* 240 */
            bleCmdType = SppCmdType.cmd_get_active_channel;
            /* 241 */
            sbyte[] CMD_GET_ACTIVE_CHANNEL = new sbyte[] {-86, 19, 2, (sbyte) devIndex, 70};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 249 */
            sendCmd(CMD_GET_ACTIVE_CHANNEL);
            /*     */
        } /*     */
        /*     */

        public static void getAudioSource(int devIndex)
            /*     */
        {
            /* 254 */
            bleCmdType = SppCmdType.cmd_get_audio_source;
            /* 255 */
            sbyte[] CMD_GET_AUDIO_SOURCE = new sbyte[] {-86, 19, 2, (sbyte) devIndex, 71};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 263 */
            sendCmd(CMD_GET_AUDIO_SOURCE);
            /*     */
        } /*     */
        /*     */

        public static void reqDevInfo()
            /*     */
        {
            /* 268 */
            sbyte[] CMD_REQ_DEV_INFO = new sbyte[] {-86, 17, 0};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 274 */
            sendCmd(CMD_REQ_DEV_INFO);
            /*     */
        } /*     */
        /*     */

        public static void reqLinkDev(string mac)
            /*     */
        {
            /* 279 */
            bleCmdType = SppCmdType.cmd_req_link_dev;
            /* 280 */
            mac = mac.Replace(":", "");
            /* 281 */
            sbyte[] cmd = getReqLinkDevCmd(mac);
            /* 282 */
            sendCmd(cmd);
            /*     */
        } /*     */
        /*     */

        public static void reqDropLinkDev(int devIndex)
            /*     */
        {
            /* 287 */
            bleCmdType = SppCmdType.cmd_req_drop_link_dev;
            /*     */
            /* 289 */
            sbyte[] CMD_REQ_DROP_LINK_DEV = new sbyte[] {-86, 35, 1, (sbyte) devIndex};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 297 */
            if (devIndex == 0)
                /*     */
            {
                /* 299 */
                sendCmd(CMD_REQ_DROP_LINK_DEV);
                /*     */
            } /* 301 */
            else if (devIndex == 1)
                /*     */
            {
                /* 303 */
                sendCmd(CMD_REQ_DROP_LINK_DEV);
                /*     */
            } /* 305 */
            else if (devIndex == 2)
                /*     */
            {
                /* 307 */
                sendCmd(CMD_REQ_DROP_LINK_DEV);
                /*     */
            } /*     */
        } /*     */
        /*     */

        public static void ReqStartLinking(int second)
            /*     */
        {
            /* 313 */
            bleCmdType = SppCmdType.cmd_req_start_linking;
            /*     */
            /* 315 */
            sbyte[] CMD_REQ_START_LINKING = new sbyte[] {-86, 37, 1, (sbyte) second};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 323 */
            sendCmd(CMD_REQ_START_LINKING);
            /*     */
        } /*     */
        /*     */

        public static void reqLEDAndSoundFeedback(int devIndex)
            /*     */
        {
            /* 328 */
            bleCmdType = SppCmdType.cmd_req_led_and_sound_feedback;
            /* 329 */
            sbyte[] CMD_REQ_LED_AND_SOUND_FEEDBACK_DEVICE = new sbyte[] {-86, 49, 1, (sbyte) devIndex};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 336 */
            sendCmd(CMD_REQ_LED_AND_SOUND_FEEDBACK_DEVICE);
            /*     */
        } /*     */
        /*     */

        public static void cmd_getBrightness()
        {
            /* 340 */
            bleCmdType = SppCmdType.cmd_getBrightness;
            /* 341 */
            sbyte[] CMD_REQ_BRIGHTNESS = new sbyte[] {-86, 94, 0};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 347 */
            sendCmd(CMD_REQ_BRIGHTNESS);
            /*     */
        } /*     */
        /*     */

        public static void reqDeviceSoftwareVersion()
            /*     */
        {
            /* 352 */
            bleCmdType = SppCmdType.cmd_req_device_software_version;
            /* 353 */
            sbyte[] CMD_REQ_DEVICE_SOFTWARE_VERSION = new sbyte[] {-86, 65, 0};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 359 */
            sendCmd(CMD_REQ_DEVICE_SOFTWARE_VERSION);
            /*     */
        } /*     */
        /*     */

        public static sbyte[] reqDfuStart(int dfuCrc, int dfuSize)
            /*     */
        {
            /* 364 */
            bleCmdType = SppCmdType.cmd_req_dfu_start;
            /* 365 */
            sbyte[] CMD_REQ_DFU_START = new sbyte[] {-86, 67, 8};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 371 */
            sbyte[] data = new sbyte[CMD_REQ_DFU_START.Length + 8];
            /*     */
            /* 373 */
            sbyte[] dataDfuCrc = BinaryHelper.Int2ByteArray(dfuCrc);
            /* 374 */
            sbyte[] dataDfuCrc2 = new sbyte[4];
            /* 375 */
            dataDfuCrc2[0] = dataDfuCrc[2];
            /* 376 */
            dataDfuCrc2[1] = dataDfuCrc[3];
            /* 377 */
            dataDfuCrc2[2] = dataDfuCrc[0];
            /* 378 */
            dataDfuCrc2[3] = dataDfuCrc[1];
            /* 379 */
            dataDfuCrc = dataDfuCrc2;
            /*     */
            /* 381 */
            sbyte[] dataDfuSize = BinaryHelper.Int2ByteArray(dfuSize);
            /*     */
            /* 383 */
            for (int i = 0; i < data.Length; i++)
                /*     */
            {
                /* 385 */
                if (i < CMD_REQ_DFU_START.Length)
                    /*     */
                {
                    /* 387 */
                    data[i] = CMD_REQ_DFU_START[i];
                    /*     */
                } /* 389 */
                else if ((i >= CMD_REQ_DFU_START.Length) && (i < CMD_REQ_DFU_START.Length + 4))
                    /*     */
                {
                    /* 391 */
                    data[i] = dataDfuCrc[(i - CMD_REQ_DFU_START.Length)];
                    /*     */
                } /*     */
                else
                /*     */
                {
                    /* 395 */
                    data[i] = dataDfuSize[(i - CMD_REQ_DFU_START.Length - 4)];
                    /*     */
                } /*     */
            } /*     */
            /* 399 */
            string cmd = HexHelper.encodeHexStr(data);
            /* 400 */
            sendCmd(data);
            /* 401 */
            return data;
            /*     */
        } /*     */
        /*     */
        /*     */

        public static sbyte[] reqDfuStart(int dfuCrc, UpgradeSectionId dfuSecIdx, int dfuSize, int dfuCrc2,
            UpgradeSectionId dfuSecIdx2, int dfuSize2)
            /*     */
        {
            /* 407 */
            if ((dfuSecIdx == UpgradeSectionId.TRADITIONAL) || (dfuSecIdx2 == UpgradeSectionId.TRADITIONAL))
            {
                return null;
            }
            /* 408 */
            if (dfuSecIdx == UpgradeSectionId.None)
            {
                return null;
                /*     */
            } /* 410 */
            int payloadLen = dfuSecIdx2 == UpgradeSectionId.None ? 8 : 16;
            /*     */
            /* 412 */
            bleCmdType = SppCmdType.cmd_req_dfu_start_with_sec_id;
            /* 413 */
            sbyte[] CMD_REQ_DFU_START = new sbyte[] {-86, 67, (sbyte) payloadLen};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 419 */
            sbyte[] data = new sbyte[CMD_REQ_DFU_START.Length + payloadLen];
            /*     */
            /* 421 */
            int tmp_dfuSecIdx = -1;
            /* 422 */
            if (dfuSecIdx == UpgradeSectionId.MCU)
                /*     */
            {
                /* 424 */
                tmp_dfuSecIdx = 0;
                /*     */
            } /* 426 */
            else if (dfuSecIdx == UpgradeSectionId.BT)
                /*     */
            {
                /* 428 */
                tmp_dfuSecIdx = 1;
                /*     */
            } /*     */
            /* 431 */
            sbyte[] dataDfuCrc = BinaryHelper.Int2ByteArray(dfuCrc);
            /* 432 */
            sbyte[] tmp_dataDfuCrc = new sbyte[4];
            /* 433 */
            tmp_dataDfuCrc[0] = dataDfuCrc[2];
            /* 434 */
            tmp_dataDfuCrc[1] = dataDfuCrc[3];
            /* 435 */
            tmp_dataDfuCrc[2] = dataDfuCrc[0];
            /* 436 */
            tmp_dataDfuCrc[3] = dataDfuCrc[1];
            /* 437 */
            dataDfuCrc = tmp_dataDfuCrc;
            /*     */
            /*     */
            /* 440 */
            sbyte[] dataDfuSize = BinaryHelper.Int2ByteArray(dfuSize);
            /*     */
            /* 442 */
            data[0] = CMD_REQ_DFU_START[0];
            /* 443 */
            data[1] = CMD_REQ_DFU_START[1];
            /* 444 */
            data[2] = CMD_REQ_DFU_START[2];
            /*     */
            /* 446 */
            data[3] = dataDfuCrc[0];
            /* 447 */
            data[4] = dataDfuCrc[1];
            /* 448 */
            data[5] = dataDfuCrc[2];
            /* 449 */
            data[6] = dataDfuCrc[3];
            /*     */
            /* 451 */
            data[7] = ((sbyte) tmp_dfuSecIdx);
            /*     */
            /* 453 */
            data[8] = dataDfuSize[1];
            /* 454 */
            data[9] = dataDfuSize[2];
            /* 455 */
            data[10] = dataDfuSize[3];
            /*     */
            /* 457 */
            if (payloadLen == 16)
                /*     */
            {
                /* 459 */
                int tmp_dfuSecIdx2 = -1;
                /* 460 */
                if (dfuSecIdx2 == UpgradeSectionId.MCU)
                    /*     */
                {
                    /* 462 */
                    tmp_dfuSecIdx2 = 0;
                    /*     */
                } /* 464 */
                else if (dfuSecIdx2 == UpgradeSectionId.BT)
                    /*     */
                {
                    /* 466 */
                    tmp_dfuSecIdx2 = 1;
                    /*     */
                } /*     */
                /* 469 */
                sbyte[] dataDfuCrc2 = BinaryHelper.Int2ByteArray(dfuCrc2);
                /* 470 */
                sbyte[] tmp_dataDfuCrc2 = new sbyte[4];
                /* 471 */
                tmp_dataDfuCrc2[0] = dataDfuCrc[2];
                /* 472 */
                tmp_dataDfuCrc2[1] = dataDfuCrc[3];
                /* 473 */
                tmp_dataDfuCrc2[2] = dataDfuCrc[0];
                /* 474 */
                tmp_dataDfuCrc2[3] = dataDfuCrc[1];
                /* 475 */
                dataDfuCrc2 = tmp_dataDfuCrc2;
                /*     */
                /*     */
                /* 478 */
                sbyte[] dataDfuSize2 = BinaryHelper.Int2ByteArray(dfuSize2);
                /*     */
                /* 480 */
                data[11] = dataDfuCrc2[0];
                /* 481 */
                data[12] = dataDfuCrc2[1];
                /* 482 */
                data[13] = dataDfuCrc2[2];
                /* 483 */
                data[14] = dataDfuCrc2[3];
                /*     */
                /* 485 */
                data[15] = ((sbyte) tmp_dfuSecIdx2);
                /*     */
                /* 487 */
                data[16] = dataDfuSize2[1];
                /* 488 */
                data[17] = dataDfuSize2[2];
                /* 489 */
                data[18] = dataDfuSize2[3];
                /*     */
            } /*     */
            /*     */
            /* 493 */
            string cmd = HexHelper.encodeHexStr(data);
            /* 494 */
            sendCmd(data);
            /* 495 */
            return data;
            /*     */
        } /*     */
        /*     */

        public static void reqColorFromColorPicker()
            /*     */
        {
            /* 500 */
            bleCmdType = SppCmdType.cmd_req_color_from_color_picker;
            /* 501 */
            sbyte[] cmd = new sbyte[] {-86, 99, 0};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 507 */
            sendCmd(cmd);
            /*     */
        } /*     */
        /*     */

        public static sbyte[] notifySecStart(int dfuCrc, UpgradeSectionId dfuSecIdx, int dfuSize)
            /*     */
        {
            /* 512 */
            int dfuSecIdx2 = dfuSecIdx == UpgradeSectionId.MCU ? 0 : 1;
            /* 513 */
            bleCmdType = SppCmdType.cmd_req_dfu_start_with_sec_id;
            /* 514 */
            sbyte[] CMD_REQ_DFU_START = new sbyte[] {-86, 70, 8};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 520 */
            sbyte[] data = new sbyte[CMD_REQ_DFU_START.Length + 8];
            /*     */
            /* 522 */
            sbyte[] dataDfuCrc = BinaryHelper.Int2ByteArray(dfuCrc);
            /* 523 */
            sbyte[] dataDfuCrc2 = new sbyte[4];
            /* 524 */
            dataDfuCrc2[0] = dataDfuCrc[2];
            /* 525 */
            dataDfuCrc2[1] = dataDfuCrc[3];
            /* 526 */
            dataDfuCrc2[2] = dataDfuCrc[0];
            /* 527 */
            dataDfuCrc2[3] = dataDfuCrc[1];
            /* 528 */
            dataDfuCrc = dataDfuCrc2;
            /*     */
            /*     */
            /* 531 */
            sbyte[] dataDfuSize = BinaryHelper.Int2ByteArray(dfuSize);
            /*     */
            /* 533 */
            data[0] = CMD_REQ_DFU_START[0];
            /* 534 */
            data[1] = CMD_REQ_DFU_START[1];
            /* 535 */
            data[2] = CMD_REQ_DFU_START[2];
            /*     */
            /* 537 */
            data[3] = dataDfuCrc[0];
            /* 538 */
            data[4] = dataDfuCrc[1];
            /* 539 */
            data[5] = dataDfuCrc[2];
            /* 540 */
            data[6] = dataDfuCrc[3];
            /*     */
            /* 542 */
            data[7] = ((sbyte) dfuSecIdx2);
            /*     */
            /* 544 */
            data[8] = dataDfuSize[1];
            /* 545 */
            data[9] = dataDfuSize[2];
            /* 546 */
            data[10] = dataDfuSize[3];
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 566 */
            string cmd = HexHelper.encodeHexStr(data);
            /* 567 */
            sendCmd(data);
            /* 568 */
            return data;
            /*     */
        } /*     */
        /*     */

        public static sbyte[] setDfuData(int packIdx, sbyte[] dfuData)
            /*     */
        {
            /* 573 */
            bleCmdType = SppCmdType.cmd_set_dfu_data;
            /* 574 */
            sbyte[] CMD_SET_DFU_DATA = new sbyte[] {-86, 68, (sbyte) dfuData.Length};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 580 */
            sbyte[] data = new sbyte[CMD_SET_DFU_DATA.Length + dfuData.Length];
            /*     */
            /* 582 */
            for (int i = 0; i < CMD_SET_DFU_DATA.Length; i++)
                /*     */
            {
                /* 584 */
                data[i] = CMD_SET_DFU_DATA[i];
                /*     */
            } /*     */
            /* 587 */
            for (int i = 0; i < dfuData.Length; i++)
                /*     */
            {
                /* 589 */
                data[(i + 3)] = dfuData[i];
                /*     */
            } /*     */
            /* 592 */
            sendCmd(data);
            /* 593 */
            return data;
            /*     */
        } /*     */
        /*     */

        public static void notifyDfuCancel()
        {
            /* 597 */
            sbyte[] CMD_NOTIFY_DFU_CANCEL = new sbyte[] {-86, 71, 1, 1};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 605 */
            sendCmd(CMD_NOTIFY_DFU_CANCEL);
            /*     */
        } /*     */
        /*     */

        public static void reqLedPatternInfo()
            /*     */
        {
            /* 610 */
            bleCmdType = SppCmdType.cmd_reqLedPatternInfo;
            /* 611 */
            sbyte[] cmd = new sbyte[] {-86, 81, 0};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 618 */
            sendCmd(cmd);
            /*     */
        } /*     */
        /*     */

        public static int LedPattern
        {
            set
            {
                /* 622 */
                bleCmdType = SppCmdType.cmd_setLedPattern;
                /* 623 */
                sbyte[] cmd = new sbyte[4];
                /* 624 */
                cmd[0] = -86;
                /* 625 */
                cmd[1] = 83;
                /* 626 */
                cmd[2] = 1;
                /* 627 */
                cmd[3] = ((sbyte) value);
                /* 628 */
                sendCmd(cmd);
                /*     */
            }
        } /*     */
        /*     */

        public static void setLedPattern(int patternId, sbyte[] patternStatus)
        {
            /* 632 */
            bleCmdType = SppCmdType.cmd_setLedPattern;
            /* 633 */
            sbyte[] cmd = new sbyte[4 + patternStatus.Length];
            /* 634 */
            cmd[0] = -86;
            /* 635 */
            cmd[1] = 83;
            /* 636 */
            cmd[2] = ((sbyte) (patternStatus.Length + 1));
            /* 637 */
            cmd[3] = ((sbyte) patternId);
            /* 638 */
            for (int i = 4; i < cmd.Length; i++)
                /*     */
            {
                /* 640 */
                cmd[i] = patternStatus[(i - 4)];
                /*     */
            } /* 642 */
            sendCmd(cmd);
            /*     */
        } /*     */
        /*     */

        public static void SetBrightness(int brightness)
            /*     */
        {
            /* 647 */
            bleCmdType = SppCmdType.cmd_setBrightness;
            /* 648 */
            sbyte[] cmd = new sbyte[] {-86, 86, 1, (sbyte) brightness};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 656 */
            sendCmd(cmd);
            /*     */
        } /*     */
        /*     */

        public static void SetBackgroundColor(int safeColorIdx, bool inlcudeSlave)
            /*     */
        {
            /* 661 */
            bleCmdType = SppCmdType.cmd_setBackgroundColor;
            /* 662 */
            sbyte[] cmd = new sbyte[] {-86, 88, 2, (sbyte) safeColorIdx, (sbyte) (inlcudeSlave ? 1 : 0)};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 671 */
            sendCmd(cmd);
            /*     */
        } /*     */
        /*     */

        public static int[] ColorImage
        {
            set
            {
                /* 675 */
                bleCmdType = SppCmdType.cmd_setColorImage;
                /* 676 */
                sbyte[] cmd = new sbyte[value.Length + 3];
                /* 677 */
                cmd[0] = -86;
                /* 678 */
                cmd[1] = 89;
                /* 679 */
                cmd[2] = 99;
                /* 680 */
                for (int i = 3; i < value.Length + 3; i++)
                    /*     */
                {
                    /* 682 */
                    cmd[i] = ((sbyte) value[(i - 3)]);
                    /*     */
                } /* 684 */
                sendCmd(cmd);
                /*     */
            }
        } /*     */
        /*     */

        public static void SetCharacterPattern(char character, int foreground, int background, bool inlcudeSlave)
            /*     */
        {
            /* 689 */
            bleCmdType = SppCmdType.cmd_setCharacterPattern;
            /* 690 */
            sbyte[] cmd = new sbyte[]
            {-86, 92, 4, (sbyte) character, (sbyte) foreground, (sbyte) background, (sbyte) (inlcudeSlave ? 1 : 0)};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 701 */
            sendCmd(cmd);
            /*     */
        } /*     */
        /*     */

        public static void PropagateLedPattern()
            /*     */
        {
            /* 706 */
            bleCmdType = SppCmdType.cmd_propagateLedPattern;
            /* 707 */
            sbyte[] cmd = new sbyte[] {-86, 93, 0};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 714 */
            sendCmd(cmd);
            /*     */
        } /*     */
        /*     */

        public static void GetMicrophoneSoundLevel()
            /*     */
        {
            /* 719 */
            bleCmdType = SppCmdType.cmd_getMicrophoneSoundLevel;
            /* 720 */
            sbyte[] cmd = new sbyte[] {-86, 98, 0};
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /*     */
            /* 727 */
            sendCmd(cmd);
            /*     */
        } /*     */
    }

    /* Location:              D:\Hack\classes.jar!\com\harman\pulsesdk\SppCmdHelper.class
	 * Java compiler version: 7 (51.0)
	 * JD-Core Version:       0.7.1
	 */
}