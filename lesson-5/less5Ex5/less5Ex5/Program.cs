using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace less5Ex5
{
    class Program
    {
        /*
            Долгуев Владимир.
            Список задач (ToDo-list):
                * написать приложение для ввода списка задач;
                * задачу описать классом ToDo с полями Title и IsDone;
                * на старте, если есть файл tasks.json/xml/bin (выбрать формат), загрузить из него массив имеющихся задач и вывести их на экран;
                * если задача выполнена, вывести перед её названием строку «[x]»;
                * вывести порядковый номер для каждой задачи;
                * при вводе пользователем порядкового номера задачи отметить задачу с этим порядковым номером как выполненную;
                * записать актуальный массив задач в файл tasks.json/xml/bin.
         */
        static List<ToDo> toDoList = new List<ToDo>();  // не знаю, как тут сделать без коллекций, учитывая, что пользователь может ввести новые задачи,
                                                        // а переопределение размерности массива - неприятная и неудобная штука.
        static string workDoc = "tasks.json";

        static void Main(string[] args)
        {
            ReadJson();

            while (true)
            {
                WriteMenu();
                string line = Console.ReadLine();
                bool isValid = int.TryParse(line, out int numTask);
                if (isValid)
                {
                    ReverseIsDone(numTask);
                }
                else
                {
                    AddDellTaskOrExit(line);
                }
                WriteToDoList(toDoList); 
            }
        }

        /// <summary>
        /// Методы вывода списка дел на экран
        /// </summary>
        /// <param name="toDoList">Список задач</param>
        static void WriteToDoList(List<ToDo> toDoList)
        {
            int numberOfTask = 1;
            Console.Clear();
            foreach (var toDo in toDoList)
            {
                Console.WriteLine($"{numberOfTask}. {toDo}");
                numberOfTask++;
            }
        }

        /// <summary>
        /// Метод получения сохраненных данных с файла json
        /// и вывода их на экран консоли
        /// </summary>
        static void ReadJson()
        {
            if (File.Exists(workDoc))
            {
                string json = File.ReadAllText(workDoc);
                toDoList = JsonConvert.DeserializeObject<List<ToDo>>(json);
                WriteToDoList(toDoList);
            }
        }

        /// <summary>
        /// Метод изменения состояния выполнения задачи
        /// </summary>
        /// <param name="numTask">Номер задачи, состояние которой требуется изменить</param>
        static void ReverseIsDone(int numTask)
        {
            if (numTask > 0 && numTask <= toDoList.Count)
            {
                toDoList[numTask - 1].IsDone = !toDoList[numTask - 1].IsDone;
            }
            else
            {
                Console.WriteLine("Задачи по введенному номеру не существует.");
                Console.WriteLine("Для продолжения нажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Добавление/удаление задачи либо выход с сохранением в файл json
        /// в зависимости от того, что ввел пользователь
        /// </summary>
        /// <param name="line">Введенная строка</param>
        static void AddDellTaskOrExit(string line)
        {
            switch (line.ToUpper())
            {
                case "ВЫХОД":
                    string outJson = JsonConvert.SerializeObject(toDoList);
                    File.WriteAllText(workDoc, outJson);
                    Environment.Exit(0);
                    break;
                case "УДАЛИТЬ":
                    while (true)
                    {
                        Console.WriteLine("Введите номер задачи, которую требуется удалить\n" +
                                          "либо введите ОТМЕНА для отмены операции");
                        var numOfDelTask = Console.ReadLine();
                        bool isValid = int.TryParse(numOfDelTask, out int numTask);
                        if (isValid)
                        {
                            toDoList.Remove(toDoList[numTask - 1]);
                            return;
                        }
                        else
                        {
                            switch (numOfDelTask.ToUpper())
                            {
                                case "ОТМЕНА":
                                    return;
                                default:
                                    Console.WriteLine("Некорректный ввод");
                                    break;
                            }
                        } 
                    }
                    break;
                default:
                    if (line != string.Empty)
                    {
                        toDoList.Add(new ToDo(line));
                    }
                    break;
            }
        }

        /// <summary>
        /// Вывод меню на жкран консоли
        /// </summary>
        static void WriteMenu()
        {
            Console.WriteLine("Введите:");
            if (toDoList.Count > 0)
            {
                Console.WriteLine("- Номер задачи для изменения флага её выполнения");
            }
            Console.WriteLine("- Текст задачи для добавления в список\n" +
                              "- ВЫХОД для завершения работы программы\n" +
                              "- УДАЛИТЬ для указания номера задачи, которую требуется удалить");
        }
    }
}
