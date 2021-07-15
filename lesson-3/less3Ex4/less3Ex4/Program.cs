using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace less3Ex4
{
    class Program
    {
        /*
            Владимир Долгуев.
            *«Морской бой»: вывести на экран массив 10х10, состоящий из символов X и O, где Х — элементы кораблей, а О — свободные клетки.
         */
        static void Main(string[] args)
        {
            /*
             * За основу принимаем следующее:
             * 4-хпалубных кораблей - 1 шт.
             * 3-хпалубных кораблей - 2 шт.
             * 2-хпалубных кораблей - 3 шт.
             * 1-палубных кораблей - 4 шт.
             * Построение - классическое, в линию. Т.е. корабли не извиваются и не гнутся.
             * Размер поля 10х10.
             * Корабли не могут касаться друг друга.
             * Чтобы корабли было лучше видно, их буду отмечать заглавной "Х", а клетки без кораблей - малой "о"
             * и к каждому символу добавим пробел
             * 
             * 0__________X
             * |oooooooXoo
             * |ooXooooXoo
             * |oooooooXoo
             * |oXooXXoooX
             * |oooooooooo
             * |oXoXXXXooo
             * |oXoooooooo
             * |oooooooooX
             * |oXXooooooX
             * |ooooooXooX
             * Y
             */
            
            // В массив буду записывать координаты начала и конца корабля.
            // Итого 4 + 3 + 2 + 1 = 10 кораблей + две координаты по Х и две координаты по Y -> массив 10х2х2
            int xPoint = 0;
            int yPoint = 1;

            int start = 0;
            int end = 1;

            var seaBattle = new int[10, 2, 2];

            seaBattle[0, xPoint, start] = 8;
            seaBattle[0, xPoint, end] = 8;
            seaBattle[0, yPoint, start] = 1;
            seaBattle[0, yPoint, end] = 3;

            seaBattle[1, xPoint, start] = 3;
            seaBattle[1, xPoint, end] = 3;
            seaBattle[1, yPoint, start] = 2;
            seaBattle[1, yPoint, end] = 2;

            seaBattle[2, xPoint, start] = 2;
            seaBattle[2, xPoint, end] = 2;
            seaBattle[2, yPoint, start] = 4;
            seaBattle[2, yPoint, end] = 4;

            seaBattle[3, xPoint, start] = 4;
            seaBattle[3, xPoint, end] = 5;
            seaBattle[3, yPoint, start] = 4;
            seaBattle[3, yPoint, end] = 4;

            seaBattle[4, xPoint, start] = 10;
            seaBattle[4, xPoint, end] = 10;
            seaBattle[4, yPoint, start] = 4;
            seaBattle[4, yPoint, end] = 4;

            seaBattle[5, xPoint, start] =  2;
            seaBattle[5, xPoint, end] =  2;
            seaBattle[5, yPoint, start] =  6;
            seaBattle[5, yPoint, end] =  7;

            seaBattle[6, xPoint, start] =  4;
            seaBattle[6, xPoint, end] =  7;
            seaBattle[6, yPoint, start] =  6;
            seaBattle[6, yPoint, end] =  6;

            seaBattle[7, xPoint, start] =  2;
            seaBattle[7, xPoint, end] =  3;
            seaBattle[7, yPoint, start] =  9;
            seaBattle[7, yPoint, end] =  9;

            seaBattle[8, xPoint, start] =  7;
            seaBattle[8, xPoint, end] =  7;
            seaBattle[8, yPoint, start] =  10;
            seaBattle[8, yPoint, end] =  10;

            seaBattle[9, xPoint, start] =  8;
            seaBattle[9, xPoint, end] =  10;
            seaBattle[9, yPoint, start] =  10;
            seaBattle[9, yPoint, end] =  10;

            // составим массив 10х10, расставим корабли и выведем его.

            var battleMap = new string[10, 10];

            for (int i = 0; i < seaBattle.GetLength(0); i++)
            {
                // прорисовываем горизонтальные корабли
                for (int j = seaBattle[i, xPoint, start] - 1; j < seaBattle[i, xPoint, end]; j++)
                {
                    battleMap[seaBattle[i, yPoint, start] - 1, j] = "X ";
                }
                // прорисовываем вертикальные корабли
                for (int j = seaBattle[i, yPoint, start] - 1; j < seaBattle[i, yPoint, end]; j++)
                {
                    battleMap[j, seaBattle[i, xPoint, start] - 1] = "X ";
                }
            }

            for (int i = 0; i < battleMap.GetLength(0); i++)
            {
                for (int j = 0; j < battleMap.GetLength(1); j++)
                {
                    if (battleMap[i, j] != "X ")
                    {
                        Console.Write("o ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(battleMap[i, j]);
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }

            Console.ReadKey();
            

        }
    }
}
