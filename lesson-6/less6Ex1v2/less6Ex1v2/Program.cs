using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace less6Ex1v2
{
    /*
        Долгуев Владимир.
        Написать консольное приложение Task Manager, которое выводит на экран
        запущенные процессы и позволяет завершить указанный процесс.
        Предусмотреть возможность завершения процессов с помощью указания его ID или имени процесса.
        В качестве примера можно использовать консольные утилиты Windows tasklist и taskkill.
     */

    internal class Program
    {
        private static bool _isSortByName = true;
        private static readonly List<string> LastCommand = new List<string>();
        private static int _autoRefreshSecond = 10;

        private static void Main()
        {
            Refresh();
            while (true)
            {
                try
                {
                    (Command userChoice, string[] argument) = ParseUsersChoice();
                    bool isExit = UserChoiceProcessing(userChoice, argument);
                    if (isExit) break;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                    Console.WriteLine("Введите команду либо '?' для вызова справки");
                }
            }
        }

        /// <summary> Обработка и выполнение введенной пользователем команды </summary>
        /// <param name="userChoice"> Введенная команда </param>
        /// <param name="argument"> массив аргументов введенной команды </param>
        private static bool UserChoiceProcessing(Command userChoice, string[] argument)
        {
            switch (userChoice)
            {
                case Command.Kill:
                    if (argument.Length == 3)
                    {
                        switch (argument[1].ToUpper())
                        {
                            case "ID":
                                KillProcessById(argument[2]);
                                break;

                            case "NAME":
                                KillProcessByName(argument[2]);
                                break;

                            default:
                                throw new Exception("Команда введена некорректно");
                        }
                        Refresh();
                    }
                    else throw new Exception("Команда введена некорректно");
                    return false;

                case Command.Sort:
                    if (argument.Length == 2)
                    {
                        switch (argument[1].ToUpper())
                        {
                            case "ID":
                                _isSortByName = false;
                                break;

                            case "NAME":
                                _isSortByName = true;
                                break;

                            default:
                                throw new Exception("Команда введена некорректно");
                        }
                        Refresh();
                    }
                    else throw new Exception("Команда введена некорректно");
                    return false;

                case Command.Prop:
                    if (argument.Length == 3)
                    {
                        switch (argument[1].ToUpper())
                        {
                            case "ID":
                                DisplayProcessPropertiesById(argument[2]);
                                break;

                            case "NAME":
                                DisplayProcessPropertiesByName(argument[2]);
                                break;

                            default:
                                throw new Exception("Команда введена некорректно");
                        }
                    }
                    else throw new Exception("Команда введена некорректно");
                    return false;

                case Command.Refresh:
                    Refresh();
                    return false;

                case Command.Help:
                    Help();
                    return false;

                case Command.GetId:
                    if (argument.Length == 2)
                    {
                        GetIdByName(argument[1]);
                    }
                    else throw new Exception("Команда введена некорректно");
                    return false;

                case Command.AutoRefresh:
                    if (argument.Length == 2)
                    {
                        _autoRefreshSecond = ParseToInt(argument[1], "ARS");
                    }
                    else throw new Exception("Команда введена некорректно");
                    return false;

                case Command.Exit:
                    return true;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Команда выполнена успешно");
            Console.ResetColor();
            Console.WriteLine("Введите команду либо '?' для вызова справки");
            return false;
        }

        /// <summary> Закрытие процесса по его ID </summary>
        /// <param name="stringProcessId">Номер ID в формате строки</param>
        private static void KillProcessById(string stringProcessId)
        {
            int processId = ParseToInt(stringProcessId, "ID");
            //if (!String.IsNullOrEmpty(error)) return error;
            Process process = GetProcessById(processId);
            //if (!String.IsNullOrEmpty(error)) return error;
            KillProcess(process);
        }

        /// <summary> Закрытие процесса по его имени </summary>
        /// <param name="processName">Имя процесса</param>
        private static void KillProcessByName(string processName)
        {
            Process process = GetProcessByName(processName);
            //if (!String.IsNullOrEmpty(error)) return error;
            KillProcess(process);
        }

        /// <summary> Закрытие указанного процесса </summary>
        /// <param name="process">Процесс</param>
        private static void KillProcess(Process process)
        {
            try
            {
                process.Kill();
            }
            catch (System.ComponentModel.Win32Exception)
            {
                throw new Exception("Процесс не может быть завершен");
            }
            catch (NotSupportedException)
            {
                throw new Exception("Процесс не был завершен, т.к. запущен на удаленном компьютере");
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Процесс не найден. Вероятно, был завершен ранее");
            }
        }

        /// <summary> Получение процесса по его ID </summary>
        /// <param name="processId"> ID процесса</param>
        /// <returns>Возвращает процесс</returns>
        private static Process GetProcessById(int processId)
        {
            try
            {
                return Process.GetProcessById(processId);
            }
            catch (ArgumentException)
            {
                throw new Exception("Процесс с указанным ID не обнаружен");
            }
            catch (InvalidOperationException ex)
            {   // тут просто не понял, как своими словами указать на ошибку.
                // по идее без этого catch можно обойтись
                throw new Exception(ex.Message);
            }
        }

        /// <summary> Получение процесса по его имени </summary>
        /// <param name="processName">Имя процесса</param>
        /// <returns>>Возвращает процесс</returns>
        private static Process GetProcessByName(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            switch (processes.Length)
            {
                case 0:
                    throw new Exception("Процесс с указанным именем не обнаружен");
                case 1:
                    return processes[0];

                default:
                    throw new Exception("Процессов с указанным именем несколько.\n" +
                                        "Воспользуйтесь аналогичной командой с указанием ID");
            }
        }

        /// <summary> Обновление списка процессов в консоли </summary>
        private static void Refresh()
        {
            Console.Clear();
            Process[] processes = Process.GetProcesses();
            IEnumerable<Process> sortProcesses;
            if (_isSortByName)
            {
                sortProcesses = from process in processes orderby process.ProcessName, process.Id select process;
            }
            else
            {
                sortProcesses = from process in processes orderby process.Id select process;
            }
            WriteProcessList(sortProcesses.ToArray());
            Console.WriteLine("Введите команду либо '?' для вызова справки");
        }

        /// <summary> Отображение свойств процесса по его ID </summary>
        /// <param name="stringProcessId">Номер ID в формате строки</param>
        private static void DisplayProcessPropertiesById(string stringProcessId)
        {
            int processId = ParseToInt(stringProcessId, "ID");
            //if (!String.IsNullOrEmpty(error)) return error;
            Process process = GetProcessById(processId);
            //if (!String.IsNullOrEmpty(error)) return error;
            DisplayProcessProperties(process);
        }

        /// <summary> Отображение свойств процесса по его имени </summary>
        /// <param name="processName">Имя процесса</param>
        private static void DisplayProcessPropertiesByName(string processName)
        {
            Process process = GetProcessByName(processName);
            //if (!String.IsNullOrEmpty(error)) return error;
            DisplayProcessProperties(process);
        }

        /// <summary> Отображение свойств процесса на экран косноли </summary>
        /// <param name="process">Процесс</param>
        private static void DisplayProcessProperties(Process process)
        {
            Type myType = typeof(Process);
            PropertyInfo[] myProperties = myType.GetProperties();

            foreach (var property in myProperties)
            {
                try
                {
                    Console.WriteLine($"{property.Name}".PadRight(30, '-') + (property.GetValue(process) ?? "null"));
                }
                catch (Exception ex)
                {
                    Console.Write($"{property.Name}".PadRight(30, '-'));
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                    Console.ResetColor();
                }
            }
        }

        /// <summary> Приведение строкового типа в тип int </summary>
        /// <param name="stringToInt">Номер в формате строки</param>
        /// <param name="valueType">Тип переменной (ID, ARS и т.д.)</param>
        /// <returns>Возвращает номер в формате int</returns>
        private static int ParseToInt(string stringToInt, string valueType)
        {
            string result = string.Empty;
            try
            {
                int resultInt = int.Parse(stringToInt);
                if (resultInt >= 0)
                {
                    return resultInt;
                }
                switch (valueType)
                {
                    case "ID":
                        result = "ID процесса не может быть отрицательным.";
                        break;

                    case "ARS":
                        result = "Время автообновления не может быть отрицательным";
                        break;
                }
                throw new Exception(result);
            }
            catch (ArgumentNullException)
            {
                switch (valueType)
                {
                    case "ID":
                        result = "ID процесса не указан.";
                        break;

                    case "ARS":
                        result = "Время автообновления не указано";
                        break;
                }

                throw new Exception(result);
            }
            catch (FormatException)
            {
                switch (valueType)
                {
                    case "ID":
                        result = "Неверно указан ID процесса. Присутствуют недопустимые символы.";
                        break;

                    case "ARS":
                        result = "Неверно указан время автообновления. Присутствуют недопустимые символы.";
                        break;
                }

                throw new Exception(result);
            }
            catch (OverflowException)
            {
                switch (valueType)
                {
                    case "ID":
                        result = "ID процесса содержит излишнее количество символов.\n";
                        break;

                    case "ARS":
                        result = "Время автообновления содержит излишнее количество символов.\n";
                        break;
                }

                throw new Exception(result +
                                    "Проверьте правильность написания.\n" +
                                    "В случае, если ошибок не найдено, обратитесь к системному администратору.");
            }
        }

        /// <summary> Вывод команд, доступные пользователю </summary>
        private static void Help()
        {
            Console.WriteLine("Kill ID 'ID'/Name 'Name' - завершение процесса по ID либо имени. Пример: Kill ID 2012");
            Console.WriteLine("Sort ID/Name ------------- отсортировать процессы по ID либо имени. Пример: Sort Name");
            Console.WriteLine("Prop ID 'ID'/Name 'Name' - вывести свойства процесса ID либо имени. Пример: Prop Name notepad");
            Console.WriteLine("GetId 'Name' ------------- получить список всех id для указанного имени процесса. Пример: GetId notepad");
            Console.WriteLine("Refresh ------------------ обновить список процессов и вывести на экран");
            Console.WriteLine("AutoRefresh 'second' ----- автообновление списка процессов. Введите AutoRefresh 0 для отключения автообновления\n" +
                              "                           По умолчанию время автообновления - 10 секунд.");
            Console.WriteLine("Exit --------------------- выход из программы");
        }

        /// <summary>
        /// Метод получения на ввод команды пользователя
        /// Если за 'second' секунд ничего не было введено - консоль очищается и список процессов обновляется.
        /// То же происходит через 5 секунд после стирания введенной строки.
        /// Т.к. при использовании ReadKey() теряется функция перебора последних команд клавишами
        /// то здесь на реализована вручную.
        /// </summary>
        /// <param name="second"> Частота, с которой обновлять список процессов</param>
        /// <returns> Введенная пользователем команда </returns>
        private static string GetUserCommand(int second)
        {
            string userChoice = string.Empty;
            ConsoleKeyInfo symbol;
            bool isEnterCompleted = false;
            int numOfLastCommand = 0;
            while (!isEnterCompleted)
            {
                var left = Console.CursorLeft;
                var top = Console.CursorTop;

                if (string.IsNullOrEmpty(userChoice))
                {
                    bool isEnterUserData = Reader.TryReadLine(out symbol, second * 1000);
                    if (!isEnterUserData)
                    {
                        Refresh();
                        userChoice = string.Empty;
                        continue;
                    }
                }
                else
                {
                    symbol = Console.ReadKey();
                }
                switch (symbol.Key)
                {
                    case ConsoleKey.Enter:
                        isEnterCompleted = true;
                        break;

                    case ConsoleKey.Backspace:
                        if (!string.IsNullOrEmpty(userChoice))
                        {
                            if (left > 0)
                            {
                                Console.SetCursorPosition(left - 1, top);
                                Console.Write(" ");
                                Console.SetCursorPosition(left - 1, top);
                            }
                            if (left == 1)
                            {
                                userChoice = string.Empty;
                            }
                            else
                            {
                                userChoice = userChoice.Substring(0, userChoice.Length - 1);
                            }
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (LastCommand.Count > 0 && numOfLastCommand < LastCommand.Count)
                        {
                            Console.SetCursorPosition(0, top);
                            if (userChoice != null)
                            {
                                Console.Write(new string(' ', userChoice.Length + 10));
                                if (numOfLastCommand < LastCommand.Count) numOfLastCommand++;
                                Console.SetCursorPosition(0, top);
                            }

                            userChoice = LastCommand[LastCommand.Count - numOfLastCommand];
                            Console.Write(userChoice);
                        }
                        else Console.SetCursorPosition(left, top);
                        break;

                    case ConsoleKey.DownArrow:
                        if (LastCommand.Count > 0 && numOfLastCommand > 0)
                        {
                            Console.SetCursorPosition(0, top);
                            if (userChoice != null)
                            {
                                Console.Write(new string(' ', userChoice.Length + 10));
                                numOfLastCommand--;
                                if (numOfLastCommand == 0)
                                {
                                    userChoice = string.Empty;
                                }
                                else
                                {
                                    userChoice = LastCommand[LastCommand.Count - numOfLastCommand];
                                }

                                Console.SetCursorPosition(0, top);
                                Console.Write(userChoice);
                            }
                        }
                        else Console.SetCursorPosition(left, top);
                        break;

                    default:
                        userChoice += symbol.KeyChar;
                        break;
                }
            }
            if (!string.IsNullOrEmpty(userChoice))
            {
                LastCommand.Add(userChoice);
            }
            return userChoice?.Trim();
        }

        /// <summary> Обработчик введенных пользователем команд</summary>
        /// <returns>Возвращает распознанную команду либо ошибку и массив введенных аргументов</returns>
        private static (Command, string[]) ParseUsersChoice()
        {
            string[] empty = { string.Empty };
            string usersChoice;
            if (_autoRefreshSecond > 0)
            {
                usersChoice = GetUserCommand(_autoRefreshSecond);
            }
            else
            {
                usersChoice = Console.ReadLine()?.Trim();
            }
            var args = usersChoice?.Split(' ');
            switch (args?[0].ToUpper())
            {
                case "KILL":
                    return (Command.Kill, args);

                case "SORT":
                    return (Command.Sort, args);

                case "PROP":
                    return (Command.Prop, args);

                case "GETID":
                    return (Command.GetId, args);

                case "REFRESH":
                    return (Command.Refresh, empty);

                case "AUTOREFRESH":
                    return (Command.AutoRefresh, args);

                case "?":
                    return (Command.Help, empty);

                case "EXIT":
                    return (Command.Exit, empty);

                default:
                    throw new Exception("Команда введена некорректно");
            }
        }

        /// <summary> Получение и вывод на экран консоли списка процессов с одинаковым именем</summary>
        /// <param name="processName">Имя процесса</param>
        private static void GetIdByName(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            switch (processes.Length)
            {
                case 0:
                    throw new Exception("Процесс с указанным именем не обнаружен");
                default:
                    WriteProcessList(processes);
                    break;
            }
        }

        /// <summary> Вывод на экран консоли списка процессов </summary>
        /// <param name="processes">Список процессов в массиве либо формате IEnumerable(Process)</param>
        private static void WriteProcessList(Process[] processes)
        {
            int maxNameLength = (from process in processes select process.ProcessName.Length).Max();
            int maxIdLength = (from process in processes select process.Id.ToString().Length).Max();
            int maxNumLength = (processes.Count() - 1).ToString().Length;

            Console.WriteLine($"| №".PadRight(maxNumLength + 2) + $" | ID".PadRight(maxIdLength + 3) + " | Process".PadRight(maxNameLength + 3) + " |");
            Console.WriteLine(new string('_', maxNumLength + maxIdLength + maxNameLength + 10));
            int processNumber = 0;
            foreach (var process in processes)
            {
                processNumber++;
                Console.WriteLine($"| {processNumber}".PadRight(maxNumLength + 2) + $" | {process.Id}".PadRight(maxIdLength + 3) + $" | {process.ProcessName}".PadRight(maxNameLength + 3) + " |");
            }
            Console.WriteLine(new string('_', maxNumLength + maxIdLength + maxNameLength + 10));
        }
    }
}