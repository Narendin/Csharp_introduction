using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace less3Ex3
{
    class Program
    {
        /*
            Долгуев Владимир.
            Написать программу, выводящую введённую пользователем 
            строку в обратном порядке (olleH вместо Hello).
         */
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку");
            var line = Console.ReadLine();

            for (int i = line.Length - 1; i >= 0; i--)
            {
                Console.Write(line[i]);
            }

            Console.WriteLine();
            Console.WriteLine();

            // второй способ
            var charArray = line.ToCharArray();
            Array.Reverse(charArray);
            Console.WriteLine(new string(charArray));
            
            Console.ReadKey();
        }
    }
}
