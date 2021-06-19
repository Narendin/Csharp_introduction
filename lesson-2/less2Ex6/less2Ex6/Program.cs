using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace less2Ex6
{
    class Program
    {
        /*
            Долгуев Владимир.
            (*) Для полного закрепления битовых масок, попытайтесь создать универсальную структуру расписания недели, 
            к примеру, чтобы описать работу какого либо офиса. Явный пример - офис номер 1 работает со вторника до пятницы, 
            офис номер 2 работает с понедельника до воскресенья и выведите его на экран консоли.
         */
        [Flags]
        enum DayOfWeek
        {
            Monday    = 0b_0000001,
            Tuesday   = 0b_0000010,
            Wednesday = 0b_0000100,
            Thursday  = 0b_0001000,
            Friday    = 0b_0010000,
            Saturday  = 0b_0100000,
            Sunday    = 0b_1000000
        }
        static void Main(string[] args)
        {
            DayOfWeek FirstOfficeDays = (DayOfWeek)0b_0011111;
            DayOfWeek SecondOfficeDays = (DayOfWeek)0b_1111100;
            DayOfWeek ThirdOfficeDays = (DayOfWeek)0b_1111111;

            Console.WriteLine($"Первый офис работает по дням: {FirstOfficeDays}");
            Console.WriteLine($"Второй офис работает по дням: {SecondOfficeDays}");
            Console.WriteLine($"Третий офис работает по дням: {ThirdOfficeDays}");

            Console.ReadKey();
        }
    }
}
