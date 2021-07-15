using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace less2Ex2
{
    class Program
    {
        /*
            Долгуев Владимир.
            Запросить у пользователя порядковый номер текущего месяца и вывести его название.
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
            Console.WriteLine("Введите номер месяца");
            if (Int32.TryParse(Console.ReadLine(), out int month))
            {
                if (month <= 12 && month > 0)
                {
                    Console.WriteLine((Month)month);
                }
                else
                {
                    Console.WriteLine("Месяца по введенному номеру не существует!");
                }
            }
            else
            {
                Console.WriteLine("Введенный номер содержит некорректные символы либо количество символов слишком велико");
            }
            Console.ReadLine();
        }
    }
}