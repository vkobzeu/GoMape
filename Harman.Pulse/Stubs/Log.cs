using System.Diagnostics;

namespace Harman.Pulse.Stubs
{
    public class Log
    {
        public static void d(string s1, string s2)
        {
            Debug.WriteLine($"[{s1}] {s2}");
        }
    }
}