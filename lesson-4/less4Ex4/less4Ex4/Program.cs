using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace less4Ex4
{
    class Program
    {
        /*
            Долгуев Владимир.
            Написать программу, вычисляющую число Фибоначчи для заданного значения рекурсивным способом. 

            Я не совсем понял, что нужно сделать в задании.. 
            Нужно найти число Фибоначчи, находящееся в ряду под указанным номером?
         */

        static void Main(string[] args)
        {
            Console.WriteLine("Введите номер числа из ряда Фибоначчи");
            bool isValid = false;
            while(!isValid)
            {
                isValid = int.TryParse(Console.ReadLine(), out int number);
                if (!isValid)
                {
                    Console.WriteLine("Введены некорректные данные. Попробуйте еще раз.");
                }

                if (number > 0)
                {
                    Console.WriteLine("Число фибоначи по указанному номеру: " + GetFibonacciPlus(number));
                }
                else if (number < 0)
                {
                    Console.WriteLine("Число фибоначи по указанному номеру: " + GetFibonacciMinus(number));
                }    
                else
                {
                    Console.WriteLine("Число фибоначи по указанному номеру: 0");
                }

                Console.ReadKey();
            }
            

        }

        static int GetFibonacciPlus(int mumber)
        {
            if (mumber == 0 || mumber == 1)
            {
                return mumber * 1;
            }

            return GetFibonacciPlus(mumber - 1) + GetFibonacciPlus(mumber - 2);
        }

        static int GetFibonacciMinus(int mumber)
        {
            if (mumber == 0 || mumber == -1)
            {
                return mumber * -1;
            }

            return GetFibonacciMinus(mumber + 2) - GetFibonacciMinus(mumber + 1);
        }
    }
}
