using lesson_8.Properties;
using System;

namespace lesson_8
{
    internal class Program
    {
        /*
            Долгуев Владимир.
            Создать консольное приложение, которое при старте выводит приветствие, записанное в настройках приложения (application-scope).
            Запросить у пользователя имя, возраст и род деятельности, а затем сохранить данные в настройках.
            При следующем запуске отобразить эти сведения.
            Задать приложению версию и описание.
         */

        private static void Main()
        {
            Console.WriteLine(Settings.Default.Greeting);

            var isCorrectName = IsSettingFull("Введите Ваше имя:", Settings.Default.UserName);
            var isCorrectSex = IsSettingFull("Введите Ваш пол:", Settings.Default.Sex);
            var isCorrectOccupation = IsSettingFull("Укажите род Вашей деятельности:", Settings.Default.Occupation);

            if (isCorrectName && isCorrectSex && isCorrectOccupation)
            {
                Console.WriteLine($"Ваше имя: {Settings.Default.UserName}");
                Console.WriteLine($"Ваш пол: {Settings.Default.Sex}");
                Console.WriteLine($"Род Вашей деятельности: {Settings.Default.Occupation}");
            }

            Console.ReadKey();
        }

        private static bool IsSettingFull(string line, string setting)
        {
            if (!string.IsNullOrEmpty(setting)) return true;
            Console.WriteLine(line);
            Settings.Default.Occupation = Console.ReadLine();
            Settings.Default.Save();
            return false;
        }
    }
}