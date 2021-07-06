using System;
using System.Threading;

namespace less6Ex1v2
{
    internal class Reader
    {
        /*
         * Данный код честно скопипастчен с сайта overcoder.net
         * и честно переделан, чтобы работало как надо
         */

        private static readonly AutoResetEvent GetInput;
        private static readonly AutoResetEvent GotInput;
        private static ConsoleKeyInfo _input;
        private static bool _isInput;

        static Reader()
        {
            GetInput = new AutoResetEvent(false);
            GotInput = new AutoResetEvent(false);
            var inputThread = new Thread(ReaderThread) { IsBackground = true };
            inputThread.Start();
        }

        private static void ReaderThread()
        {
            while (true)
            {
                GetInput.WaitOne();
                if (_isInput) continue;
                _input = Console.ReadKey();
                _isInput = true;
                GotInput.Set();
            }
        }

        public static bool TryReadLine(out ConsoleKeyInfo line, int timeOutMillisecs = Timeout.Infinite)
        {
            GetInput.Set();
            _isInput = false;
            var success = GotInput.WaitOne(timeOutMillisecs);
            line = success ? _input : new ConsoleKeyInfo();
            return success;
        }
    }
}