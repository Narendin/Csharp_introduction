using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace less2Ex3
{
    class Program
    {
        /*
            Долгуев Владимир.
            Определить, является ли введённое пользователем число чётным.
         */
        static void Main(string[] args)
        {
            Console.WriteLine("Введите целое число");
            if (Int32.TryParse(Console.ReadLine(), out int number))
            {
                if ((number % 2) != 0)
                {
                    Console.WriteLine("Введенное число - нечетное");
                }
                else
                {
                    Console.WriteLine("Введенное число - четное");
                }
            }
            else
            {
                Console.WriteLine("Введенное число содержит недопустимые символы либо содержит слишком большое количество символов");
            }

            Console.ReadKey();
        }
    }
}
