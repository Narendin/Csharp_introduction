using System;
using System.IO;

namespace less5Ex2
{
    class Program
    {
        /*
            Долгуев Владимир.
            Написать программу, которая при старте дописывает текущее время в файл «startup.txt».
         */
        static void Main(string[] args)
        {
            string fileName = "startup.txt";
            File.AppendAllText(fileName, $"{DateTime.Now:T}\n");

            Console.ReadKey();
        }
    }
}
