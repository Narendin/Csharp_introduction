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
            var charArray = line.ToCharArray();
            Array.Reverse(charArray);
            Console.WriteLine(new string(charArray));
            
            Console.ReadKey();
        }
    }
}
