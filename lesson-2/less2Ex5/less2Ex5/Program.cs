using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace less2Ex5
{
    class Program
    {
        /*
            Долгуев Владимир.
            (*) Если пользователь указал месяц из зимнего периода, а средняя температура > 0, вывести сообщение «Дождливая зима»

            За основу беру задачи 1 и 2 из урока №2
         */

        enum Month
        {
            Январь = 1,
            Февраль,
            Март,
            Апрель,
            Май,
            Июнь,
            Июль,
            Август,
            Сентябрь,
            Октябрь,
            Ноябрь,
            Декабрь
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите максимальную температуру за сутки (°С)");
            bool maxTempIsNum = Int32.TryParse(Console.ReadLine(), out int maxTemperature);

            Console.WriteLine("Введите минимальную температуру за сутки (°С)");
            bool minTempIsNum = Int32.TryParse(Console.ReadLine(), out int minTemperature);

            Console.WriteLine("Введите номер месяца");
            bool monthIsNum = Int32.TryParse(Console.ReadLine(), out int month);

            if (!(maxTempIsNum && minTempIsNum && monthIsNum))
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
                    switch (month)
                    {
                        case (int)Month.Декабрь:
                        case (int)Month.Январь:
                        case (int)Month.Февраль:
                            double mean = (double)(maxTemperature + minTemperature) / 2;
                            if (mean > 0)
                            {
                                Console.WriteLine($"Дождливая зима");
                            }
                            else
                            {
                                Console.WriteLine("Ничего тебе не скажу");
                            }
                            break;
                        default:
                            Console.WriteLine("Ничего тебе не скажу");
                            break;
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
