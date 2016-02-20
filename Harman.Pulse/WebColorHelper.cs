using System;

/*    */

namespace Harman.Pulse
{
    /*    */
    /*    */
    /*    */

    public class WebColorHelper
        /*    */
    {
        /*    */

        public static sbyte rgbToWeb216(sbyte r, sbyte g, sbyte b)
            /*    */
        {
            /*  8 */
            int result = r/51*36 + g/51*6 + b/51;
            /*  9 */
            sbyte ret = (sbyte) result;
            /* 10 */
            return ret;
            /*    */
        } /*    */
        /*    */

        public static sbyte rgbToWeb216(PulseColor color)
        {
            /* 14 */
            int result = color.red/51*36 + color.green/51*6 + color.blue/51;
            /* 15 */
            sbyte ret = (sbyte) result;
            /* 16 */
            return ret;
            /*    */
        } /*    */
        /*    */

        public static PulseColor web216ToRGB(sbyte webIndex)
            /*    */
        {
            /* 21 */
            int index = webIndex;
            /* 22 */
            if (index > 215)
            {
                /* 23 */
                index = 215;
                /*    */
            } /* 25 */
            PulseColor color = new PulseColor();
            /* 26 */
            color.red = ((sbyte) (index/36*51));
            /* 27 */
            index %= 36;
            /* 28 */
            color.green = ((sbyte) (index/6*51));
            /* 29 */
            index %= 6;
            /* 30 */
            color.blue = ((sbyte) (index*51));
            /* 31 */
            return color;
            /*    */
        } /*    */
        /*    */

        public static int RGBToWeb216Index(PulseColor color)
        {
            /* 35 */
            int safeColorValue = 0;
            /* 36 */
            int[] BGRValue = new int[] {color.blue & 0xFF, color.green & 0xFF, color.red & 0xFF};
            /*    */
            /* 38 */
            for (int i = 0; i < 3; i++)
            {
                /* 39 */
                if (Math.Abs(BGRValue[i] - 51) < 25)
                {
                    /* 40 */
                    safeColorValue = (int) (safeColorValue + Math.Pow(6.0D, i));
                    /* 41 */
                }
                else if (Math.Abs(BGRValue[i] - 102) < 25)
                {
                    /* 42 */
                    safeColorValue = (int) (safeColorValue + Math.Pow(6.0D, i)*2.0D);
                    /* 43 */
                }
                else if (Math.Abs(BGRValue[i] - 153) < 25)
                {
                    /* 44 */
                    safeColorValue = (int) (safeColorValue + Math.Pow(6.0D, i)*3.0D);
                    /* 45 */
                }
                else if (Math.Abs(BGRValue[i] - 204) < 25)
                {
                    /* 46 */
                    safeColorValue = (int) (safeColorValue + Math.Pow(6.0D, i)*4.0D);
                    /* 47 */
                }
                else if (Math.Abs(BGRValue[i] - 255) < 25)
                {
                    /* 48 */
                    safeColorValue = (int) (safeColorValue + Math.Pow(6.0D, i)*5.0D);
                    /*    */
                } /*    */
            } /* 51 */
            return safeColorValue;
            /*    */
        } /*    */
    }

    /* Location:              D:\Hack\classes.jar!\com\harman\pulsesdk\WebColorHelper.class
	 * Java compiler version: 7 (51.0)
	 * JD-Core Version:       0.7.1
	 */
}