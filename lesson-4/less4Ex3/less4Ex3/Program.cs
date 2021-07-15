using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace less4Ex3
{
    class Program
    {
        /*
            Долгуев Владимир.
            Написать метод по определению времени года. 
            На вход подаётся число – порядковый номер месяца. 
            На выходе — значение из перечисления (enum) — Winter, Spring, Summer, Autumn. 
            Написать метод, принимающий на вход значение из этого перечисления 
            и возвращающий название времени года (зима, весна, лето, осень). 
            Используя эти методы, ввести с клавиатуры номер месяца и вывести название времени года. 
            Если введено некорректное число, вывести в консоль текст «Ошибка: введите число от 1 до 12».
         */


        static void Main(string[] args)
        {
            Console.WriteLine("Введите номер месяца");
            int month = GetMonthNum();
            Console.WriteLine($"Текущее время года: {GetSeason(month)}");
            
            Console.ReadKey();
        }

        static int GetMonthNum()
        {
            bool isValid = false;
            int number = 0;

            while (!isValid)
            {
                string line = Console.ReadLine();
                isValid = int.TryParse(line, out number);
                if ((number < 1) || (number > 12))
                {
                    isValid = false;
                    Console.WriteLine("Ошибка: введите число от 1 до 12");
                }
            }
            return number;
        }

        static string GetSeason(int monthNum)
        {
            string season = "";
            switch (monthNum)
            {
                case 12:
                case 1:
                case 2:
                    season = ((enum_Seasons)0).ToString();
                    break;
                case 3:
                case 4:
                case 5:
                    season = ((enum_Seasons)1).ToString();
                    break;
                case 6:
                case 7:
                case 8:
                    season = ((enum_Seasons)2).ToString();
                    break;
                case 9:
                case 10:
                case 11:
                    season = ((enum_Seasons)3).ToString();
                    break;
            }
            return season.ToString();
        }

    }
}
