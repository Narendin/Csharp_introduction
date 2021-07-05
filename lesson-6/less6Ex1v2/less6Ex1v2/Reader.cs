using System;
using System.Threading;

namespace less6Ex1v2
{
    class Reader
    {
        /*
         * Данный код честно скопипастчен с сайта overcoder.net
         * и честно переделан, чтобы работало как надо
         */

        private static Thread inputThread;
        private static AutoResetEvent getInput, gotInput;
        private static ConsoleKeyInfo input;
        private static bool isInput = false;

        static Reader()
        {            
            getInput = new AutoResetEvent(false);
            gotInput = new AutoResetEvent(false);
        }

        private static void reader()
        {
            while (!isInput)
            {
                getInput.WaitOne();
                if (!isInput)
                {
                    input = Console.ReadKey();
                    isInput = true;
                    inputThread.Abort();
                }                    
                gotInput.Set();
            } 
        }

        public static bool TryReadLine(out ConsoleKeyInfo line, int timeOutMillisecs = Timeout.Infinite)
        {
            isInput = false;
            inputThread = new Thread(reader);
            inputThread.IsBackground = true;
            inputThread.Start();

            getInput.Set();
            bool success = gotInput.WaitOne(timeOutMillisecs);
            if (success)
            {                
                line = input;                
            }                
            else
                line = new ConsoleKeyInfo();

            return success;
        }
    }
}
