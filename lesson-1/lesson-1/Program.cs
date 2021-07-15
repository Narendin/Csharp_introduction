using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson_1
{
    /*
     Написать программу, выводящую в консоль текст: «Привет, %имя пользователя%, сегодня %дата%». 
     Имя пользователя сохранить из консоли в промежуточную переменную.
     Поставить точку останова и посмотреть значение этой переменной в режиме отладки. 
     Запустить исполняемый файл через системный терминал.
     */
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите Ваше имя");
            var name = Console.ReadLine();

            DateTime dateTime = DateTime.Now;

            Console.WriteLine($"Привет, {name}, сегодня {dateTime.ToShortDateString()}");
            Console.ReadKey();
        }
    }
}
