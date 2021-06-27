using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace less4Ex2
{
    class Program
    {
        /*
            Долгуев Владимир.
            Написать программу, принимающую на вход строку — набор чисел, 
            разделенных пробелом, и возвращающую число — сумму всех чисел в строке. 
            Ввести данные с клавиатуры и вывести результат на экран.
         */
        static void Main(string[] args)
        {
            Console.WriteLine("Введите числа через пробел");
            string numbers = Console.ReadLine();
            var words = numbers.Split(' ');
            int sum = 0;

            foreach (var word in words)
            {
                sum += ParseLine(word);
            }
            Console.WriteLine($"Сумма введенных чисел равна {sum}");

            Console.ReadKey();
        }

        static int ParseLine(string line)
        {
            bool isValid = int.TryParse(line, out int number);
            if (!isValid) Exit();
            return number;
        }

        private static void Exit()
        {
            Console.WriteLine("Строка содержит недопустимые символы");
            Console.WriteLine("Для завершения работы приложения нажмите любую клавишу...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
