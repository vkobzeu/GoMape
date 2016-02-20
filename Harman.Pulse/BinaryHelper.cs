using System;
using System.IO;

/*    */

namespace Harman.Pulse
{
    /*    */
    /*    */ /*    */
    /*    */

    public class BinaryHelper
    {
        /*    */

        public static sbyte[] Int2ByteArray(int num)
        {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(num);
                return ByteHelper.FromByteArray(stream.GetBuffer());
            }
            //    /*  7 */
            //    java.io.ByteArrayOutputStream bos = new java.io.ByteArrayOutputStream();
            ///*    */
            ///*  9 */
            //sbyte[] buffer = null;
            ///*    */
            //try
            //{
            //    /* 11 */
            //    DataOutputStream oos = new DataOutputStream(bos);
            //    /* 12 */
            //    oos.writeInt(num);
            //    /* 13 */
            //    oos.flush();
            //    /* 14 */
            //    oos.close();
            //    /* 15 */
            //    buffer = bos.toByteArray();
            //    /* 16 */
            //    bos.close();
            //    /*    */
            //}
            //catch (Exception e)
            //{
            //    /* 18 */
            //    Console.WriteLine(e.ToString());
            //    Console.Write(e.StackTrace);
            //    /*    */
            //} /*    */
            ///* 21 */
            //return buffer;
            /*    */
        } /*    */
        /*    */

        public static int ByteArray2Int(sbyte[] buffer)
        {
            using (var stream = new MemoryStream(ByteHelper.ToByteArray(buffer)))
            using (var reader = new BinaryReader(stream))
            {
                return reader.ReadInt32();
            }

            //    /* 25 */
            //    java.io.ByteArrayInputStream bis = new java.io.ByteArrayInputStream(buffer);
            ///*    */
            ///* 27 */
            //int num = -1;
            ///*    */
            //try
            //{
            //    /* 29 */
            //    java.io.DataInputStream dis = new java.io.DataInputStream(bis);
            //    /* 30 */
            //    return dis.readInt();
            //    /*    */
            //} /*    */
            //catch (Exception e)
            //{
            //    /* 33 */
            //    Console.WriteLine(e.ToString());
            //    Console.Write(e.StackTrace);
            //    /*    */
            //} /*    */
            ///* 36 */
            //return num;
            /*    */
        } /*    */
        /*    */

        public static short ByteArray2Short(sbyte[] buffer)
        {
            /* 40 */
            return (short)(buffer[1] << 8 | buffer[0] & 0xFF);
            /*    */
        } /*    */
    }

    /* Location:              D:\Hack\classes.jar!\com\harman\pulsesdk\BinaryHelper.class
	 * Java compiler version: 7 (51.0)
	 * JD-Core Version:       0.7.1
	 */
}