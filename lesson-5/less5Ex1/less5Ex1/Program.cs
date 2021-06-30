using System;
using System.IO;

namespace less5Ex1
{
    class Program
    {
        /*
            Долгуев Владимир.
            Ввести с клавиатуры произвольный набор данных и сохранить его в текстовый файл.
         */

        static void Main(string[] args)
        {
            Console.WriteLine("Введите имя файла");
            var fileName = GenerateFileName();

            Console.WriteLine("Введите любые данные");
            var line = Console.ReadLine();

            
            if (File.Exists(fileName))
            {
                if(NeedReCreateAFile())
                {
                    File.WriteAllText(fileName, line);
                    Console.WriteLine($"Файл {fileName} был перезаписан");
                }
                else
                {
                    File.AppendAllText(fileName, Environment.NewLine);
                    File.AppendAllText(fileName, line);
                    Console.WriteLine($"Данные добавлены в файл {fileName}");
                }
            }
            else
            {
                File.WriteAllText(fileName, line);
                Console.WriteLine($"Файл {fileName} с введенными данными был создан");
            }

            Console.ReadKey();
        }

        static bool NeedReCreateAFile()
        {
            while(true)
            {
                Console.WriteLine("Введите \"Д\", чтобы добавить строку в файл, либо \"П\", чтобы перезаписать файл полностью");
                var choice = Console.ReadLine().ToUpper();
                switch (choice)
                {
                    case "Д":
                        return false;
                    case "П":
                        return true;
                    default:
                        Console.WriteLine("Некорректный ввод.");
                        break;
                }
            }
        }

        static string GenerateFileName()
        {
            var fileName = Console.ReadLine();
            var words = fileName.Split('.');
            return words[words.Length - 1] == "txt" ? fileName : fileName + ".txt";
        }
    }
}
