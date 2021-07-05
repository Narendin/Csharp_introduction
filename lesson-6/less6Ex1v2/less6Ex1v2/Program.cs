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

    class Program
    {
        static bool isSortByName = true;
        static List<string> LastCommand = new List<string>();
        static int autoRefreshSecond = 10;

        static void Main(string[] args)
        {
            Refresh();
            while (true)
            {
                try
                {
                    (Command usersCoice, string[] argument) = ParseUsersChoice();
                    UserChoiceProcessing(usersCoice, argument);
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
        /// <param name="usersCoice"> Введенная команда </param>
        /// <param name="argument"> массив аргументов введенной команды </param>
        static void UserChoiceProcessing(Command usersCoice, string[] argument)
        {
            switch (usersCoice)
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
                    break;
                case Command.Sort:
                    if (argument.Length == 2)
                    {
                        switch (argument[1].ToUpper())
                        {
                            case "ID":
                                isSortByName = false;
                                break;
                            case "NAME":
                                isSortByName = true;
                                break;
                            default:
                                throw new Exception("Команда введена некорректно");
                        }
                        Refresh();
                    }
                    else throw new Exception("Команда введена некорректно");
                    break;
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
                    break;
                case Command.Refresh:
                    Refresh();
                    break;
                case Command.Help:
                    Help();
                    break;
                case Command.GetId:
                    if (argument.Length == 2)
                    {
                        GetIdByName(argument[1]);
                    }
                    else throw new Exception("Команда введена некорректно");
                    break;
                case Command.AutoRefresh:
                    if (argument.Length == 2)
                    {
                        autoRefreshSecond = ParseToInt(argument[1], "ARS");
                    }
                    else throw new Exception("Команда введена некорректно");
                    break;
                case Command.Exit:
                    Environment.Exit(0);
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Команда выполнена успешно");
            Console.ResetColor();
            Console.WriteLine("Введите команду либо '?' для вызова справки");
        }

        /// <summary> Закрытие процесса по его ID </summary>
        /// <param name="stringProcessId">Номер ID в формате строки</param>
        static void KillProcessById(string stringProcessId)
        {
            int processId = ParseToInt(stringProcessId, "ID");
            //if (!String.IsNullOrEmpty(error)) return error;
            Process process = GetProcessByID(processId);
            //if (!String.IsNullOrEmpty(error)) return error;
            KillProcess(process);
        }

        /// <summary> Закрытие процесса по его имени </summary>
        /// <param name="processName">Имя процесса</param>
        static void KillProcessByName(string processName)
        {
            Process process = GetProcessByName(processName);
            //if (!String.IsNullOrEmpty(error)) return error;
            KillProcess(process);
        }

        /// <summary> Закрытие указанного процесса </summary>
        /// <param name="process">Процесс</param>
        static void KillProcess(Process process)
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
        static Process GetProcessByID(int processId)
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
        static Process GetProcessByName(string processName)
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
        static void Refresh()
        {
            Console.Clear();
            Process[] processes = Process.GetProcesses();
            IEnumerable<Process> sortProcesses = new Process[processes.Length];
            if (isSortByName)
            {
                sortProcesses = from process in processes orderby process.ProcessName, process.Id select process;
            }
            else
            {
                sortProcesses = from process in processes orderby process.Id select process;
            }
            WriteProcessList(sortProcesses);
            Console.WriteLine("Введите команду либо '?' для вызова справки");
        }

        /// <summary> Отображение свойств процесса по его ID </summary>
        /// <param name="stringProcessId">Номер ID в формате строки</param>
        static void DisplayProcessPropertiesById(string stringProcessId)
        {
            int processId = ParseToInt(stringProcessId, "ID");
            //if (!String.IsNullOrEmpty(error)) return error;
            Process process = GetProcessByID(processId);
            //if (!String.IsNullOrEmpty(error)) return error;
            DisplayProcessProperties(process);
        }

        /// <summary> Отображение свойств процесса по его имени </summary>
        /// <param name="processName">Имя процесса</param>
        static void DisplayProcessPropertiesByName(string processName)
        {
            Process process = GetProcessByName(processName);
            //if (!String.IsNullOrEmpty(error)) return error;
            DisplayProcessProperties(process);
        }

        /// <summary> Отображение свойств процесса на экран косноли </summary>
        /// <param name="process">Процесс</param>
        static void DisplayProcessProperties(Process process)
        {
            Type myType = typeof(Process);
            PropertyInfo[] myProeprties = myType.GetProperties();

            foreach (var propetry in myProeprties)
            {
                try
                {
                    Console.WriteLine($"{propetry.Name}".PadRight(30, '-') + (propetry.GetValue(process) ?? "null"));
                }
                catch (Exception ex)
                {
                    Console.Write($"{propetry.Name}".PadRight(30, '-'));
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.InnerException.Message);
                    Console.ResetColor();
                }
            }
        }

        /// <summary> Приведение строкового типа в тип int </summary>
        /// <param name="stringToInt">Номер в формате строки</param>
        /// <returns>Возвращает номер в формате int</returns>
        static int ParseToInt(string stringToInt, string valueType)
        {
            string result = string.Empty;
            int resultInt = 0;
            try
            {                
                 resultInt = int.Parse(stringToInt);
                if (resultInt >= 0)
                {
                    return resultInt;
                }
                if (valueType == "ID") result = "ID процесса не может быть отрицательным.";
                else if (valueType == "ARS") result = "Время автообновления не может быть отрицательным";
                throw new Exception(result);
            }
            catch (ArgumentNullException)
            {
                if (valueType == "ID") result = "ID процесса не указан.";
                else if (valueType == "ARS") result = "Время автообновления не указано";
                throw new Exception(result);
            }
            catch (FormatException)
            {
                if (valueType == "ID") result = "Неверно указан ID процесса. Присутствуют недопустимые символы.";
                else if (valueType == "ARS") result = "Неверно указан время автообновления. Присутствуют недопустимые символы.";
                throw new Exception(result);
            }
            catch (OverflowException)
            {
                if (valueType == "ID") result = "ID процесса содержит излишнее количество символов.\n";
                else if (valueType == "ARS") result = "Время автообновления содержит излишнее количество символов.\n";
                throw new Exception(result +
                                    "Проверьте правильность написания.\n" +
                                    "В случае, если ошибок не найдено, обратитесь к системному администратору.");
            }
            
        }

        /// <summary> Вывод команд, доступные пользователю </summary>
        static void Help()
        {
            Console.WriteLine("Kill ID 'ID'/Name 'Name' - завершение процесса по ID либо имени. Пример: Kill ID 2012");
            Console.WriteLine("Sort ID 'ID'/Name 'Name' - отсортировать процессы по ID либо имени. Пример: Sort Name");
            Console.WriteLine("Prop ID 'ID'/Name 'Name' - вывести свойства процесса ID либо имени. Пример: Prop Name notepad");
            Console.WriteLine("GetId 'Name' - получить список всех id для указанного имени процесса. Пример: GetId notepad");
            Console.WriteLine("Refresh - обновить список процессов и вывести на экран");
            Console.WriteLine("AutoRefresh 'second' - автообновление списка процессов. Введите AutoRefresh 0 для отключения автообновления\n" +
                              "                       По умолчанию время автообновления - 10 секунд.");
            Console.WriteLine("Exit - выход из программы");
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
        static string GetUserCommand(int second)
        {
            string usersChoice = string.Empty;
            ConsoleKeyInfo symbol;
            bool isEnterCompleted = false;
            int left, top, numOfLastCommand = 0;
            while (!isEnterCompleted)
            {
                left = Console.CursorLeft;
                top = Console.CursorTop;

                if (string.IsNullOrEmpty(usersChoice))
                {
                    bool isEnterUserData = Reader.TryReadLine(out symbol, second * 1000);
                    if (!isEnterUserData)
                    {
                        Refresh();
                        usersChoice = string.Empty;
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
                        if (usersChoice.Length > 0)
                        {
                            if (left > 0)
                            {
                                Console.SetCursorPosition(left - 1, top);
                                Console.Write(" ");
                                Console.SetCursorPosition(left - 1, top);
                            }
                            if (left == 1)
                            {
                                usersChoice = string.Empty;
                            }
                            else
                            {
                                usersChoice = usersChoice.Substring(0, usersChoice.Length - 1);
                            }                            
                        }
                        break;                                      
                    case ConsoleKey.UpArrow:
                        if (LastCommand.Count > 0 && numOfLastCommand < LastCommand.Count)
                        {
                            Console.SetCursorPosition(0, top);
                            Console.Write(new string(' ', usersChoice.Length + 10));
                            if (numOfLastCommand < LastCommand.Count) numOfLastCommand++;
                            Console.SetCursorPosition(0, top);
                            usersChoice = LastCommand[LastCommand.Count - numOfLastCommand];
                            Console.Write(usersChoice);                            
                        }
                        else Console.SetCursorPosition(left, top);
                        break;
                    case ConsoleKey.DownArrow:
                        if (LastCommand.Count > 0 && numOfLastCommand > 0)
                        {
                            Console.SetCursorPosition(0, top);
                            Console.Write(new string(' ', usersChoice.Length + 10));
                            numOfLastCommand--;
                            if (numOfLastCommand == 0)
                            {
                                usersChoice = string.Empty;
                            }
                            else
                            {
                                usersChoice = LastCommand[LastCommand.Count - numOfLastCommand];
                            }
                            Console.SetCursorPosition(0, top);                            
                            Console.Write(usersChoice);
                        }
                        else Console.SetCursorPosition(left, top);
                        break;
                    default:
                        usersChoice += symbol.KeyChar;
                        break;
                }
            }
            if (!string.IsNullOrEmpty(usersChoice))
            {
                LastCommand.Add(usersChoice);
            }            
            return usersChoice.Trim();
        }

        /// <summary> Обработчик введенных пользователем команд</summary>
        /// <returns>Возвращает распознанную команду либо ошибку и массив введенных аргументов</returns>
        static (Command, string[]) ParseUsersChoice()
        {
            string[] empty = { string.Empty };
            string usersChoice = string.Empty;
            if (autoRefreshSecond > 0)
            {
                usersChoice = GetUserCommand(autoRefreshSecond);
            }
            else
            {
                usersChoice = Console.ReadLine().Trim();
            }
            var args = usersChoice.Split(' ');
            switch (args[0].ToUpper())
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
        static void GetIdByName(string processName)
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
        /// <param name="processes">Список процессов в массиве либо формате IEnumerable<Process></param>
        static void WriteProcessList(IEnumerable<Process> processes)
        {
            int maxNameLength = (from process in processes select process.ProcessName.Length).Max();
            int maxIDLength = (from process in processes select process.Id.ToString().Length).Max();
            int maxNumLength = (processes.Count() - 1).ToString().Length;

            Console.WriteLine($"| №".PadRight(maxNumLength + 2) + $" | ID".PadRight(maxIDLength + 3) + " | Process".PadRight(maxNameLength + 3) + " |");
            Console.WriteLine(new string('_', maxNumLength + maxIDLength + maxNameLength + 10));
            int processNumber = 0;
            foreach (var process in processes)
            {
                processNumber++;
                Console.WriteLine($"| {processNumber}".PadRight(maxNumLength + 2) + $" | {process.Id}".PadRight(maxIDLength + 3) + $" | {process.ProcessName}".PadRight(maxNameLength + 3) + " |");
            }
            Console.WriteLine(new string('_', maxNumLength + maxIDLength + maxNameLength + 10));
        }
    }
}
