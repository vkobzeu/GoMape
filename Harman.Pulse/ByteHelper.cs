using System;

namespace Harman.Pulse
{
    public static class ByteHelper
    {
        public static byte[] ToByteArray(sbyte[] data)
        {
            var bytes = new byte[data.Length];
            Buffer.BlockCopy(data, 0, bytes, 0, data.Length);
            return bytes;
        }

        public static sbyte[] FromByteArray(byte[] bytes)
        {
            var data = new sbyte[bytes.Length];
            Buffer.BlockCopy(bytes, 0, data, 0, bytes.Length);
            return data;
        }
    }
}