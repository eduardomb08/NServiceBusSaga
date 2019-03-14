using System.Diagnostics;

namespace NServiceBusSagaService
{
    public static class MyTimer
    {
        private static Stopwatch _sw = new Stopwatch();

        public static long ElapsedMilliseconds() => _sw.ElapsedMilliseconds;

        public static void Start()
        {
            _sw.Reset();
            _sw.Start();
        }

        public static void Stop()
        {
            _sw.Stop();
        }
    }
}

