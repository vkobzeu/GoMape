/*    */

namespace Harman.Pulse
{
    /*    */
    /*    */
    /*    */

    public class SppConstant
        /*    */
    {
        /*    */
        public const string BASE_ADDRESS = "00000000-0000-1000-8000-00805F9B34FB";
        /*    */
        /*    */
        public const string GAP = "00001800-0000-1000-8000-00805F9B34FB";
        /*    */
        /*    */
        public const string GATT = "00001801-0000-1000-8000-00805F9B34FB";
        /*    */
        /*    */
        public const string BLE_RX_TX = "65786365-6c70-6f69-6e74-2e636f6d0000";
        /*    */
        /*    */
        public const string DEVICE_NAME_CHAR = "00002a00-0000-1000-8000-00805F9B34FB";
        /*    */
        /*    */
        public const string MANUFACTURE_SPECIFIC_DATA_CHAR = "00002a01-0000-1000-8000-00805F9B34FB";
        /*    */
        /*    */
        public const string RX_CHAR = "65786365-6c70-6f69-6e74-2e636f6d0001";
        /*    */
        public const string TX_CHAR = "65786365-6c70-6f69-6e74-2e636f6d0002";
        /*    */
        public const string RET_CMD_SUCCESS = "aa00021500";
        /*    */
        public const string RET_CMD_DEV_ACK = "aa00";
        /*    */
        public const string RET_CMD_APP_ACK = "aa01";
        /*    */
        public const string RET_GET_DEV_INFO = "aa12";
        /* 24 */
        public static readonly sbyte[] RET_CMD_ACK = new sbyte[] {-86, 0};
        /* 25 */
        public static readonly sbyte[] RET_CMD_DEV_INFO = new sbyte[] {-86, 18};
        /* 26 */
        public static readonly sbyte[] RET_LED_PATTERN = new sbyte[] {-86, 82};
        /* 27 */
        public static readonly sbyte[] RET_LED_PATTERN_CHANGE = new sbyte[] {-86, 85};
        /* 28 */
        public static readonly sbyte[] RET_SOUND_EVENT = new sbyte[] {-86, 97};
        /* 29 */
        public static readonly sbyte[] RET_COLOR_PICKER = new sbyte[] {-86, 87};
        /* 30 */
        public static readonly sbyte[] RET_BRIGHTNESS = new sbyte[] {-86, 95};
        /*    */
        public const sbyte RET_DEVICE_NAME = -63;
        /*    */
        public const sbyte RET_PID = 66;
        /*    */
        public const sbyte RET_MID = 67;
        /*    */
        public const sbyte RET_BATTERY_STATUS = 68;
        /*    */
        public const sbyte RET_LINKED_DEVICE_COUNT = 69;
        /*    */
        public const sbyte RET_ACTIVE_CHANNEL = 70;
        /*    */
        public const sbyte RET_AUDIO_SOURCE = 71;
        /*    */
        public const sbyte RET_DEVICE_MAC = 72;
        /*    */
        public const sbyte RET_SET_DEV_ACK = 21;
        /*    */
        public const sbyte RET_SET_LED_PATTERN = 83;
        /*    */
        public const string CMD_DEV_BYE_BYE = "aa02";
        /*    */
        public const string CMD_APP_BYE_BYE = "aa03";
        /*    */
        public const string CMD_GET_DEVICE_NAME = "c1";
        /*    */
        public const string CMD_GET_PID = "42";
        /*    */
        public const string CMD_GET_MID = "43";
        /*    */
        public const string CMD_GET_BATTERY_STATUS = "44";
        /*    */
        public const string CMD_GET_LINKED_DEVICE_COUNT = "45";
        /*    */
        public const string CMD_GET_ACTIVE_CHANNEL = "46";
        /*    */
        public const string CMD_GET_AUDIO_SOURCE = "47";
        /*    */
        public const string CMD_GET_DEVICE_MAC = "48";
        /*    */
        public const string RET_REQ_LINK_DEV = "aa00022100";
        /*    */
        public const string CMD_NOTIFY_LINK_DEV_DROP = "aa22";
        /*    */
        public const string RET_REQ_DROP_LINK_DEV = "aa00022300";
        /*    */
        public const string RET_REQ_VER = "aa42";
        /*    */
        public const string NOTIFY_DFU_STATUS_CHANGE = "aa4501";
        /*    */
        public const string RET_CMD_ACK_REQ_DFU_START_WITH_SECTION_ID = "aa000246";
        /*    */
        public const string RET_RetLedPatternInfo = "aa52";
        /*    */
        public const string RET_NotifyInsLelChange = "aa5404";
        /*    */
        public const string RET_NotifyLedPattem = "aa55";
        /*    */
        public const string RET_RetColorSniffer = "aa5703";
        /*    */
    }

    /* Location:              D:\Hack\classes.jar!\com\harman\pulsesdk\SppConstant.class
	 * Java compiler version: 7 (51.0)
	 * JD-Core Version:       0.7.1
	 */
}