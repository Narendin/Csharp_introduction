using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace less2Ex4
{
    class Program
    {
        /*
            Долгуев Владимир.
            Для полного закрепления понимания простых типов найдите любой чек, 
            либо фотографию этого чека в интернете и схематично нарисуйте его в консоли, 
            только за место динамических, по вашему мнению, данных (это может быть дата, 
            название магазина, сумма покупок) подставляйте переменные, которые были 
            заранее заготовлены до вывода на консоль.
        --------------------------------------
        |          ООО ChekNaZakaz           |
        |             Чек № 3287             |
        |           Кассир: Петров           |
        |1. Брус 18х18 7.2 куб.м. х 2700     |
        |   Стоимость................19440.00|
        |2. Доска обрезная сосна 11.3 куб. м.|
        |   х 2900                           |
        |   Стоимость................32770.00|
        |====================================|
        |Всего.......................52210.00|
        |ККМ 11111111 ИНН 222222222222 № 3287|
        |12.06.08 11:43                ПЕТРОВ|
        |ПРОДАЖА                        №3284|
        |1                          =52210.00|
        |ИТОГ     =52210.00                  |
        | НАЛИЧНЫМИ                 =52210.00|
        |################ФП##################|
        |                    ЭКЛЗ 3333333333 |
        |                    00061528 #025843|
        --------------------------------------
         */
        static void Main(string[] args)
        {
            string CompanyName = "ООО ChekNaZakaz";
            int CheckNum = 3287;
            string cashier = "Петров";
            string firstPosition = "Брус 18х18 7.2 куб.м. х 2700";
            double firstPrice = 19440.00;
            string secondPositionStart = "Доска обрезная сосна 11.3 куб. м.";
            string secondPositionEnd = "х 2900";
            double secondPrice = 32770.00;
            string KKM = "11111111";
            string INN = "222222222222";
            DateTime dateTime = new DateTime(2008, 06, 12, 11, 43, 00);
            int sale = 3284;
            long EKLZ = 3333333333;
            string strangeNumbers = "00061528 #025843";

            Console.WriteLine(new string('-', 38));
            Console.WriteLine($"|          {CompanyName}           |");
            Console.WriteLine($"|             Чек № {CheckNum}             |");
            Console.WriteLine($"|           Кассир: {cashier}           |");
            Console.WriteLine($"|1. {firstPosition}     |");
            Console.WriteLine($"|   Стоимость...................{firstPrice}|");
            Console.WriteLine($"|2. {secondPositionStart}|");
            Console.WriteLine($"|   {secondPositionEnd}                           |");
            Console.WriteLine($"|   Стоимость...................{secondPrice}|");
            Console.WriteLine($"|====================================|");
            Console.WriteLine($"|Всего..........................{firstPrice + secondPrice}|");
            Console.WriteLine($"|ККМ {KKM} ИНН {INN} № {CheckNum}|");
            Console.WriteLine($"|{dateTime:yy.MM.dd HH:mm}                {cashier}|");
            Console.WriteLine($"|ПРОДАЖА                        №{sale}|");
            Console.WriteLine($"|1                             ={firstPrice + secondPrice}|");
            Console.WriteLine($"|ИТОГ     ={firstPrice + secondPrice}                     |");
            Console.WriteLine($"| НАЛИЧНЫМИ                    ={firstPrice + secondPrice}|");
            Console.WriteLine($"|################ФП##################|");
            Console.WriteLine($"|                    ЭКЛЗ {EKLZ} |");
            Console.WriteLine($"|                    {strangeNumbers}|");
            Console.WriteLine(new string('-', 38));
            Console.ReadKey();
        }
    }
}
