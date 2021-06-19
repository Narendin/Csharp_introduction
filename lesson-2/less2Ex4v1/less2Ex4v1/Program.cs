using System;
using System.Globalization;
using System.IO;
using System.Threading;

namespace less2Ex4v1
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
        |ПРОДАЖА                       № 3284|
        |1                          =52210.00|
        |ИТОГ     =52210.00                  |
        | НАЛИЧНЫМИ                 =52210.00|
        |################ФП##################|
        |                    ЭКЛЗ 3333333333 |
        |                    00061528 #025843|
        --------------------------------------

        Чековые позиции и их стоимости сделаны массивами.
        Ширину чека можно менять в пределе от 38 до (теоретически) бесконечности и все будет норм.
        Ширина моего экрана позволила адекватно вывести до 235 символов. Дальше начинался перенос строки.
         */
        
        static void Main(string[] args)
        {
            int checkWidth = 38; // min 38 (|ККМ 11111111 ИНН 222222222222 № 3287|)

            string CompanyName = "ООО ChekNaZakaz";
            int CheckNum = 3287;
            string cashierName = "Петров";

            string[] Positions = new string[] { "Брус 18х18 7.2 куб.м. х 2700", "Доска обрезная сосна 11.3 куб. м. х 2900"};
            double[] Prices = new double[] { 19440.00, 32770.00};
            double fullPrice = 0;

            string KKM = "11111111";
            string INN = "222222222222";
            DateTime dateTime = new DateTime(2008, 06, 12, 11, 43, 00);
            int sale = 3284;
            long EKLZ = 3333333333;
            string strangeNumbers = "00061528 #025843";

            // проверка, что совпадают размерности массива товаров и ценников.
            // если не совпадают - ругаемся и выходим
            if (Positions.Length != Prices.Length)
            {
                Console.WriteLine("Количество товаров не совпадает с количеством цен!");
                return;
            }
            Console.WriteLine(new string('-', checkWidth));
            Console.WriteLine(("|".PadRight(GetNumOfChar(CompanyName.Length, checkWidth)) + $"{CompanyName}").PadRight(checkWidth-1) + "|");
            Console.WriteLine(("|".PadRight(GetNumOfChar($"Чек № {CheckNum}".Length, checkWidth)) + $"Чек № {CheckNum}").PadRight(checkWidth - 1) + "|");
            Console.WriteLine(("|".PadRight(GetNumOfChar($"Кассир: {cashierName}".Length, checkWidth)) + $"Кассир: {cashierName}").PadRight(checkWidth - 1) + "|");
            
            // вывод позиций и стоимостей
            for (int i = 0; i < Positions.Length; i++)
            {
                // тут проверка на длину строки позиции
                // если длина больше (макс длина - "|. " - "|") = макс. длина чека - (4 + длина номера позиции) => укажем как корректировку
                int correct = 4 + (i + 1).ToString().Length;
                // то творим дробление по словам и выводим нужное количество строк
                // иначе сразу выводим строку
                fullPrice += Prices[i];
                if (Positions[i].Length > checkWidth - correct)
                {
                    string[] words = Positions[i].Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    int numPosition = 0;
                    int leghtPosition = 0;
                    string textPosition = "";
                    for (int j = 0; j < words.Length; j++)
                    {
                        // тут надо каждое слово проверить, не больше ли оно длины строки
                        if (words[j].Length > checkWidth - correct)
                        {
                            Exit();
                        }
                        leghtPosition += words[j].Length;
                        if (leghtPosition > checkWidth - correct)
                        {
                            // первую строку выводим с номером позиции
                            if (numPosition == 0)
                            {
                                Console.WriteLine(($"|{i + 1}. {textPosition}").PadRight(checkWidth - 1) + "|");
                                textPosition = "";
                            }
                            else
                            {
                                Console.WriteLine(($"|   {textPosition}").PadRight(checkWidth - 1) + "|");
                                textPosition = "";
                            }
                            numPosition++;
                            leghtPosition = 0;
                            j--; // повторно обрабатываем последнее слово
                        }
                        else if (j == words.Length - 1)
                        {
                            //numPosition++;
                            textPosition += words[j];
                            Console.WriteLine(($"|   {textPosition}").PadRight(checkWidth - 1) + "|");
                            // после вывода каждого элемента необходим вывод его стоимости.
                            WritePrice("|   Стоимость", Prices[i], checkWidth, '.');
                            continue;
                        }
                        else
                        {
                            leghtPosition++; // корреляция на пробел после слова
                            textPosition += words[j];
                            if (leghtPosition< checkWidth - correct)
                            {
                                textPosition += " ";
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine(($"|{i + 1}. {Positions[i]}").PadRight(checkWidth - 1) + "|");
                    WritePrice("|   Стоимость", Prices[i], checkWidth, '.');
                }
            }
            Console.WriteLine("|" + new string('=', checkWidth - 2) + "|");
            WritePrice("|Всего", fullPrice, checkWidth, '.');
            Console.WriteLine($"|ККМ {KKM} ИНН {INN}" + $" № {CheckNum}|".PadLeft(checkWidth - ($"|ККМ {KKM} ИНН {INN}").Length)); 
            Console.WriteLine($"|{dateTime:dd.MM.yy HH:mm}" + $"{cashierName.ToUpper()}|".PadLeft(checkWidth - $"|{dateTime:dd.MM.yy HH:mm}".Length));
            Console.WriteLine("|ПРОДАЖА" + $"№ {sale}|".PadLeft(checkWidth - "|ПРОДАЖА".Length));
            Console.WriteLine("|1" + $"={fullPrice.ToString("#.00").Replace(',', '.')}|".PadLeft(checkWidth - "|1".Length));
            Console.WriteLine($"|ИТОГ     ={fullPrice.ToString("#.00").Replace(',', '.')}".PadRight(checkWidth - 1) + "|");
            Console.WriteLine($"|{new string('#',checkWidth/2-2)}ФП{new string('#', checkWidth - (checkWidth / 2) - 2)}|");
            Console.WriteLine("|" + $"ЭКЛЗ {EKLZ} |".PadLeft(checkWidth - 1));
            Console.WriteLine("|" + $"{strangeNumbers}|".PadLeft(checkWidth - 1));
            Console.WriteLine(new string('-', checkWidth));

            Console.ReadKey();
        }

        /// <summary>
        /// Метод, проверяющий длину строки и выясняющий, сколько символов надо еще подставить для получения нужного размера
        /// </summary>
        /// <param name="ParamLenght">Длина выводимого параметра</param>
        static int GetNumOfChar(int ParamLenght, int MaxLenght)
        {

            if (MaxLenght - 2 < ParamLenght)
            {
                Exit();
            }
            int needLenght = (MaxLenght - ParamLenght - 2)/2;
            return needLenght;
        }

        /// <summary>
        /// Метод завершения программы, если чек нет возможности напечатать
        /// </summary>
        static void Exit()
        {
            Console.WriteLine("Слишком большая длина строки");
            Console.WriteLine("Попробуйте увеличить ширину чека");
            Console.WriteLine("Приложение завершает свою работу");
            Console.ReadKey();
            System.Environment.Exit(0);
        }

        /// <summary>
        /// Метод вывода цены
        /// </summary>
        /// <param name="StartString">Текст в начале строки</param>
        /// <param name="SendPrice">выводимая цена</param>
        /// <param name="checkWidth">максимальная ширина чека</param>
        /// <param name="NewChar">символ, которым разделены начало строки и цена</param>
        static void WritePrice(string StartString, double SendPrice, int checkWidth, char NewChar)
        {
            string outputFirst = "";
            string outputSecond = "";
            int addCHar = 0;

            outputFirst = StartString + new string(NewChar, GetNumOfChar($"{StartString}{SendPrice.ToString("#.00")}".Length, checkWidth) * 2);
            outputSecond = $"{SendPrice.ToString("#.00").Replace(',', '.')}|";
            addCHar = checkWidth - outputFirst.Length - outputSecond.Length;
            outputFirst += new string('.', (addCHar > 0) ? addCHar : 0);
            Console.WriteLine(outputFirst + outputSecond);
        }
    }
}
