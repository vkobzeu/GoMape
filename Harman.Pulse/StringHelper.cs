/*    */

namespace Harman.Pulse
{
    /*    */
    /*    */
    /*    */
    /*    */

    public class StringHelper
        /*    */
    {
        /*    */

        public static bool IsNullOrEmpty(string str)
            /*    */
        {
            /*  9 */
            if ((str == null) || (str.Length == 0))
            {
                return true;
            }
            /* 10 */
            return false;
            /*    */
        } /*    */
    }

    /* Location:              D:\Hack\classes.jar!\com\harman\pulsesdk\StringHelper.class
	 * Java compiler version: 7 (51.0)
	 * JD-Core Version:       0.7.1
	 */
}