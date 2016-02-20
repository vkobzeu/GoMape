using System;

/*    */

namespace Harman.Pulse
{
    /*    */
    /*    */

    public class HexHelper
        /*    */
    {
        /*  5 */

        private static readonly char[] DIGITS_LOWER = new char[]
        {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'};

        /*    */
        /*    */
        /*  8 */

        private static readonly char[] DIGITS_UPPER = new char[]
        {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};

        /*    */
        /*    */

        public static char[] encodeHex(sbyte[] data)
            /*    */
        {
            /* 12 */
            return encodeHex(data, true);
            /*    */
        } /*    */
        /*    */

        public static char[] encodeHex(sbyte[] data, bool toLowerCase)
        {
            /* 16 */
            return encodeHex(data, toLowerCase ? DIGITS_LOWER : DIGITS_UPPER);
            /*    */
        } /*    */
        /*    */

        protected internal static char[] encodeHex(sbyte[] data, char[] toDigits)
        {
            /* 20 */
            int l = data.Length;
            /* 21 */
            char[] @out = new char[l << 1];
            /* 22 */
            int i = 0;
            for (int j = 0; i < l; i++)
            {
                /* 23 */
                @out[(j++)] = toDigits[((int) ((uint) (0xF0 & data[i]) >> 4))];
                /* 24 */
                @out[(j++)] = toDigits[(0xF & data[i])];
                /*    */
            } /* 26 */
            return @out;
            /*    */
        } /*    */
        /*    */

        public static string encodeHexStr(sbyte[] data)
        {
            /* 30 */
            return encodeHexStr(data, true);
            /*    */
        } /*    */
        /*    */

        public static string encodeHexStr(sbyte[] data, bool toLowerCase)
        {
            /* 34 */
            return encodeHexStr(data, toLowerCase ? DIGITS_LOWER : DIGITS_UPPER);
            /*    */
        } /*    */
        /*    */

        protected internal static string encodeHexStr(sbyte[] data, char[] toDigits)
        {
            /* 38 */
            return new string(encodeHex(data, toDigits));
            /*    */
        } /*    */
        /*    */

        public static sbyte[] decodeHex(char[] data)
        {
            /* 42 */
            int len = data.Length;
            /* 43 */
            if ((len & 0x1) != 0)
            {
                /* 44 */
                throw new Exception("unknown");
                /*    */
            } /* 46 */
            sbyte[] @out = new sbyte[len >> 1];
            /* 47 */
            int i = 0;
            for (int j = 0; j < len; i++)
            {
                /* 48 */
                int f = toDigit(data[j], j) << 4;
                /* 49 */
                j++;
                /* 50 */
                f |= toDigit(data[j], j);
                /* 51 */
                j++;
                /* 52 */
                @out[i] = (unchecked((sbyte) (f & 0xFF)));
                /*    */
            } /* 54 */
            return @out;
            /*    */
        } /*    */
        /*    */

        protected internal static int toDigit(char ch, int index)
        {
            switch (ch)
            {
                case '0':
                    return 0;
                case '1':
                    return 1;
                case '2':
                    return 2;
                case '3':
                    return 3;
                case '4':
                    return 4;
                case '5':
                    return 5;
                case '6':
                    return 6;
                case '7':
                    return 7;
                case '8':
                    return 8;
                case '9':
                    return 9;
                case 'A':
                case 'a':
                    return 10;
                case 'B':
                case 'b':
                    return 11;
                case 'C':
                case 'c':
                    return 12;
                case 'D':
                case 'd':
                    return 13;
                case 'E':
                case 'e':
                    return 14;
                case 'F':
                case 'f':
                    return 15;
                default:
                    throw new Exception("illegal char : " + ch + " index: " + index);

            }

            ///* 58 */
            //int digit = Character.digit(ch, 16);
            ///* 59 */
            //if (digit == -1)
            //{
            //    /* 60 */
            //    throw new Exception("illegal char : " + ch + " index: " + index);
            //    /*    */
            //} /*    */
            ///* 63 */
            //return digit;
            /*    */
        } /*    */
        /*    */

        public static string byteToArray(sbyte[] data)
        {
            /* 67 */
            string result = "";
            /* 68 */
            for (int i = 0; i < data.Length; i++)
            {
                /* 69 */
                result = result + (data[i] & 0xFF | 0x100).ToString("x").ToUpper().Substring(1, 2);
                /*    */
            } /* 71 */
            return result;
            /*    */
        } /*    */
    }

    /* Location:              D:\Hack\classes.jar!\com\harman\pulsesdk\HexHelper.class
	 * Java compiler version: 7 (51.0)
	 * JD-Core Version:       0.7.1
	 */
}