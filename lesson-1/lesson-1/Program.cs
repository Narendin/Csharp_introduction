using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите Ваше имя");
            var name = Console.ReadLine();

            DateTime dateTime = DateTime.Now;

            Console.WriteLine($"Привет, {name}, сегодня {dateTime.ToShortDateString()}");
        }
    }
}
