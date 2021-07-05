using System;
using System.Threading;

namespace less6Ex1v2
{
    class Reader
    {
        /*
         * Данный код честно скопипастчен с сайта overcoder.net
         */

        private static Thread inputThread;
        private static AutoResetEvent getInput, gotInput;
        private static ConsoleKeyInfo input;

        static Reader()
        {
            getInput = new AutoResetEvent(false);
            gotInput = new AutoResetEvent(false);
            inputThread = new Thread(reader);
            inputThread.IsBackground = true;
            inputThread.Start();
        }

        private static void reader()
        {
            while (true)
            {
                getInput.WaitOne();
                input = Console.ReadKey();
                gotInput.Set();
            }
        }

        public static bool TryReadLine(out ConsoleKeyInfo line, int timeOutMillisecs = Timeout.Infinite)
        {
            getInput.Set();
            bool success = gotInput.WaitOne(timeOutMillisecs);
            if (success)
                line = input;
            else
                line = new ConsoleKeyInfo();
            return success;
        }
    }
}
