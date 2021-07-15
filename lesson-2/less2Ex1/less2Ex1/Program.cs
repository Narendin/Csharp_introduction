using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace less2Ex1
{
    class Program
    {
        /*
         Долгуев Владимир.
         Запросить у пользователя минимальную и максимальную температуру за сутки и вывести среднесуточную температуру.
         */
        static void Main(string[] args)
        {
            Console.WriteLine("Введите максимальную температуру за сутки (°С)");
            bool maxTempIsNum = Int32.TryParse(Console.ReadLine(), out int maxTemperature);

            Console.WriteLine("Введите минимальную температуру за сутки (°С)");
            bool minTempIsNum = Int32.TryParse(Console.ReadLine(), out int minTemperature);

            if (!(maxTempIsNum && minTempIsNum))
            {
                Console.WriteLine("Как минимум одно число содержало недопустимые символы");
            }
            else
            {
                bool mustContinue = false;
                if (maxTemperature < minTemperature)
                {
                    Console.WriteLine("Ошибка ввода Минимальная температура больше максимальной");
                    Console.WriteLine("Все равно продолжить? (Д/Н)");
                    var choise = Console.ReadLine().ToUpper();
                    switch (choise)
                    {
                        case "Д":
                            mustContinue = true;
                            break;
                        case "Н":
                            Console.WriteLine("Завершение работы программы");
                            break;
                        default:
                            Console.WriteLine("Введен неверный символ. Завершение работы программы");
                            break;
                    }
                }
                else
                {
                    mustContinue = true;
                }

                if (mustContinue)
                {
                    double mean = (double)(maxTemperature + minTemperature) / 2;
                    Console.WriteLine($"Среднесуточная температура: {mean} °С");
                }
            }
            Console.ReadLine();
        }
    }
}