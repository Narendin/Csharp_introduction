using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace less3Ex1
{
    class Program
    {
        /*
            Владимир Долгуев.
            Написать программу, выводящую элементы двумерного массива по диагонали.
         */
        static void Main(string[] args)
        {
            bool heightIsNum = false;
            bool widthIsNum = false;
            int arrayHeight = 0;
            int arrayWidth = 0;

            while (!(heightIsNum && widthIsNum))
            {
                Console.WriteLine("Введите первый размер двумерного массива");
                heightIsNum = ParseReadLine(Console.ReadLine(), out arrayHeight);
                Console.WriteLine("Введите второй размер двумерного массива");
                widthIsNum = ParseReadLine(Console.ReadLine(), out arrayWidth);
                if (!(heightIsNum && widthIsNum))
                {
                    Console.WriteLine("Как минимум одно число содержало недопустимые символы");
                    Console.WriteLine("Начните сначала, либо введите ВЫХОД для завершения программы");
                }
            }
            
            var array = new int[arrayHeight, arrayWidth];
           
            // заполняем массив и выводим его значения по диагонали
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = i * arrayWidth + j;
                    Console.WriteLine($"{array[i, j]}".PadLeft(i * arrayWidth + j + 1));
                }
            }
            Console.WriteLine();

            // тут выводим те же значения, но уже по обратной диагонали. По сути все то же самое
            for (int i = (array.GetLength(0) - 1); i >= 0; i--)
            {
                for (int j = (array.GetLength(1) - 1); j >= 0; j--)
                {
                    Console.WriteLine($"{array[i, j]}".PadLeft(i * arrayWidth + j + 1));
                }
            }
            Console.WriteLine();

            // тут ниже рисую относительно нормальный крест.
            // т.к. сам крест симметричный, то и отступы от края, соответственно, симмметричны.
            // и данных должно быть четное количество, делящееся на 4 без остатка
            // т.к. крест симметричен не только по вертикали, но и по горизонтали
            // И чтобы отбрасывать как можно меньше символов - объединим двумерный массив в одномерный
            // а там уже возьмем нужное количество итемов

            var comArray = new int[arrayHeight * arrayWidth];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    comArray[i * arrayWidth + j] = array[i, j];
                }
            }
            array = null;

            int height = comArray.Length / 4 * 4;
            int correct = (comArray[height / 2].ToString().Length - 2) % 2;     // поправка на разной длины числа в середине креста
            int item = -1;

            for (int i = 0; i < height; i += 2)
            {
                if (i < height / 2)
                {
                    item++;
                }
                else if (i > height / 2)
                {
                    item--;
                }
                int indent = height / 2 - item * 2 - comArray[i].ToString().Length + correct;
                Console.WriteLine(new string(' ', item) + comArray[i] + new string(' ', indent) + comArray[i + 1]);
            }

            Console.ReadKey();
        }

        static bool ParseReadLine(string line, out int arrayLenght)
        {
            bool isValid = false;
            arrayLenght = 0;

            if (line.ToUpper() == "ВЫХОД")
            {
                Console.WriteLine("Для завершения работы нажмите любую клавишу");
                Console.ReadKey();
                System.Environment.Exit(0);
            }
            else
            {
                isValid = Int32.TryParse(line, out arrayLenght);
            }

            return isValid;
        }
    }
}
