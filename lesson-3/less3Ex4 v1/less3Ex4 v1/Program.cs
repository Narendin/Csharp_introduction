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

        public static int arrayLenght = 10;
        public static int arrayWidth = 10;

        static void Main(string[] args)
        {
            /*
             * За основу принимаем следующее:
             * Количество кораблей запрашиваем у пользователя
             * Построение - классическое, в линию. Т.е. корабли не извиваются и не гнутся.
             * Размер поля по умолчанию 10х10.
             * Корабли не могут касаться друг друга.
             * Чтобы корабли было лучше видно, их буду отмечать заглавной "Х", а клетки без кораблей - малой "о"
             * и к каждому символу добавим пробел
             * 
             * Отличие от предыдущей версии - авторасстановка кораблей.
             * Используется функция random для получения положения корабля (вертикальный или горизонтальный)
             * и дополнительная матрица, где будут проставляться запрещенные зоны.
             */

            
            int tryNum = 10;

            int kolOne = ParseReadLine("Введите количество однопалубников");
            int kolTwo = ParseReadLine("Введите количество двухпалубников");
            int kolThree = ParseReadLine("Введите количество трехпалубников");
            int kolFour = ParseReadLine("Введите количество четырехпалубников");

            // Код ниже раскомментировать, если нужно запрашивать у пользователя дополнительные параметры.
            //arrayLenght = ParseReadLine("Укажите ширину поля");
            //arrayWidth = ParseReadLine("Укажите высоту поля");
            //tryNum = ParseReadLine("Введите количество попыток построения");



            var testBattleMap = new string[arrayLenght, arrayWidth];
            var battleMap = new string[arrayLenght, arrayWidth];

            bool success = false;
            int tryRand = 0;

            while ((!success)&&(tryRand <= tryNum)) // По умолчанию даем 10 попыток на поиск решения 
            {
                tryRand++;
                for (int i = 1; i <= kolFour; i++)
                {
                    success = GenerateRandomPosition(4, ref testBattleMap, ref battleMap);
                }
                if (success)
                {
                    for (int i = 1; i <= kolThree; i++)
                    {
                        success = GenerateRandomPosition(3, ref testBattleMap, ref battleMap) && success;
                    }
                }
                if (success)
                {
                    for (int i = 1; i <= kolTwo; i++)
                    {
                        success = GenerateRandomPosition(2, ref testBattleMap, ref battleMap) && success;
                    }
                }
                if (success)
                {
                    for (int i = 1; i <= kolOne; i++)
                    {
                        success = GenerateRandomPosition(1, ref testBattleMap, ref battleMap) && success;
                    }
                }
                else
                {
                    Array.Clear(testBattleMap, 0, testBattleMap.Length);
                    Array.Clear(battleMap, 0, battleMap.Length);
                }
            }
            if (!success)
            {
                Console.WriteLine("Не удалось расставить корабли");
                Console.ReadKey();
                return;
            }
            Console.WriteLine();
            // вывод оставляю тот же
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

        /// <summary>
        /// Запрос ввода данных. Метод повторяется, пока не будут введены корректные данные.
        /// </summary>
        /// <param name="Text"> Текст, который необходимо вывести в консоль</param>
        /// <returns> Возвращает полученное значение в типе int</returns>
        static int ParseReadLine(string Text)
        {
            bool isValid = false;
            int retInt = 0;
            
            while (!isValid)
            {
                Console.WriteLine(Text);
                isValid = int.TryParse(Console.ReadLine(), out retInt);
            }
            return retInt;
        }

        /// <summary>
        /// Генерация корабля на поле. Учитывает уже существующие корабли.
        /// </summary>
        /// <param name="lenght">Размер корабля. Предполагается, что они линейные</param>
        /// <param name="testBattleMap">Массив-ограничитель. Нужен для отрисовки зоны, занятой кораблями</param>
        /// <param name="battleMap">Итоговая карта игры с расставленными кораблями</param>
        /// <returns>Возвращает bool переменную, сигнализирующую, удалось ли добавить корабль.</returns>
        static bool GenerateRandomPosition(int lenght, ref string[,] testBattleMap, ref string [,] battleMap)
        {
            bool vertical = false;
            bool horizontal = true;
            bool newSheep;
            Random random = new Random();
            bool position = false;
            bool success = true;

            int tryNum = 500; // ограничим на 500 попыток поиска. Увеличивать тут нет смысла. Проще у пользователя запрашивать количество попыток построения карты.

            int xPosStart;
            int yPosStart;

            int tryRand = 0;
            do
            {
                newSheep = true;
                tryRand++;
                position = random.Next(2) == 0; // если 0 - вертикальное, если 1 - горизонтальное расположение
                xPosStart = random.Next(0, arrayLenght  - ((position == horizontal) ? lenght - 1 : 1));
                yPosStart = random.Next(0, arrayWidth  - ((position == vertical) ? lenght - 1 : 1));

                // проверка в массиве-ограничителе, не столкнемся ли с другим кораблем
            
                for (int i = xPosStart; i <= xPosStart + ((position == horizontal) ? lenght - 1 : 0); i++)
                {
                    if (!newSheep)
                    {
                        break;
                    }
                    if ((i < 0) || (i > testBattleMap.GetLength(0)))
                    {
                        continue;
                    }
                    for (int j = yPosStart; j <= yPosStart + ((position == vertical) ? lenght - 1 : 0); j++)
                    {
                        if ((j < 0) || (j > testBattleMap.GetLength(1)))
                        {
                            continue;
                        }
                        string testline = testBattleMap[j, i];
                        if (testBattleMap[j, i] == "X")
                        {
                            newSheep = false;                         
                            break;
                        }
                    }
                }
            } while ((!newSheep)&&(tryRand <= tryNum));

            if (tryRand > tryNum)
            {
                success = false;
            }

            tryRand = 0;

            if (success)
            {
                // запись в массив-ограничитель
                for (int i = xPosStart - 1; i <= xPosStart + ((position == horizontal) ? lenght : 1); i++)
                {
                    if ((i < 0) || (i > testBattleMap.GetLength(0)))
                    {
                        continue;
                    }
                    for (int j = yPosStart - 1; j <= yPosStart + ((position == vertical) ? lenght : 1); j++)
                    {
                        if ((j < 0) || (j > testBattleMap.GetLength(1)))
                        {
                            continue;
                        }
                        testBattleMap[j, i] = "X";
                    }
                }

                // запись в массив поля игры
                for (int i = xPosStart; i <= xPosStart + ((position == horizontal) ? lenght - 1 : 0); i++)
                {
                    if ((i < 0) || (i > battleMap.GetLength(0)))
                    {
                        continue;
                    }
                    for (int j = yPosStart; j <= yPosStart + ((position == vertical) ? lenght - 1 : 0); j++)
                    {
                        if ((j < 0) || (j > battleMap.GetLength(1)))
                        {
                            continue;
                        }
                        battleMap[j, i] = "X ";
                    }
                }
            }

            return success;
        }
    }
}
