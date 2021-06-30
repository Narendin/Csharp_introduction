using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace less5Ex3
{
    class Program
    {
        /*
            Долгуев Владимир.
            Ввести с клавиатуры произвольный набор чисел (0...255) и записать их в бинарный файл.
         */
        static void Main(string[] args)
        {
            string fileName = "bytes.bin";
            while (true)
            {
                Console.WriteLine("Введите произвольный набор чисел (0...255) через пробел");
                var line = Console.ReadLine();
                byte[] array = GetByteArray(line, out bool correctLine);
                if (correctLine)
                {
                    File.WriteAllBytes(fileName, array);
                    break;
                }
            }

            Console.WriteLine($"Успешная запись в файл {fileName}");
            Console.ReadKey();
        }

        /// <summary>
        /// Получение массива байтов из строки
        /// </summary>
        /// <param name="line"> Вводная строка</param>
        /// <param name="correctLine"> Выходной флаг корректности строки</param>
        /// <returns>Возвращает массив байтов из строки и флаг успешного преобразования</returns>
        static byte[] GetByteArray(string line, out bool correctLine)
        {
            var numbers = line.Split(' ');
            byte[] array = new byte[numbers.Length];
            for (int i = 0; i < numbers.Length; i++)
            {
                bool isValid = byte.TryParse(numbers[i], out byte currentNum);
                if (isValid)
                {
                    array[i] = currentNum;
                    continue;
                }
                else
                {
                    Console.WriteLine("В строке присутствуют некорректные символы либо числа, не входящие в диапазон 0...255");
                    correctLine = false;
                    return array;
                }
            }
            correctLine = true;
            return array;   
        }
    }
}
