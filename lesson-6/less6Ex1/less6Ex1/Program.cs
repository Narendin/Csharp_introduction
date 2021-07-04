using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;


namespace less6Ex1
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

        static void Main(string[] args)
        {
            Refresh();

            while (true)
            {
                Console.WriteLine("Введите команду либо '?' для вызова справки");
                (Command usersCoice, string[] argument) = ParseUsersChoice();
                var error = string.Empty;
                switch (usersCoice)
                {
                    case Command.Kill:
                        switch (argument[1].ToUpper())
                        {
                            case "ID":
                                error = KillProcessById(argument[2]);
                                break;
                            case "NAME":
                                error = KillProcessByName(argument[2]);
                                break;
                            default:
                                error = "Команда введена некорректно";
                                break;
                        }
                        if (string.IsNullOrEmpty(error))
                        {
                            Refresh();
                        }                        
                        DisplayError(error);
                        break; 
                    case Command.Sort:
                        switch (argument[1].ToUpper())
                        {
                            case "ID":
                                isSortByName = false;
                                break;
                            case "NAME":
                                isSortByName = true;
                                break;
                            default:
                                error = "Команда введена некорректно";
                                break;
                        }
                        if (string.IsNullOrEmpty(error))
                        {
                            Refresh();
                        }
                        DisplayError(error);
                        break;
                    case Command.Prop:
                        switch (argument[1].ToUpper())
                        {
                            case "ID":
                                error = DisplayProcessPropertiesById(argument[2]);
                                break;
                            case "NAME":
                                error = DisplayProcessPropertiesByName(argument[2]);
                                break;
                            default:
                                error = "Команда введена некорректно";
                                break;
                        }                        
                        DisplayError(error);
                        break;
                    case Command.Refresh:
                        Refresh();
                        break;
                    case Command.Error:
                        DisplayError("Команда введена некорректно");
                        break;
                    case Command.Help:
                        Help();
                        break;
                    case Command.GetId:
                        error = GetIdByName(argument[1]);
                        DisplayError(error);
                        break;
                }
                // почитать про возможность обновления раз в секунду списка процессов, если пользователь не начал ничего вводить
            }             
        }

        /// <summary> Закрытие процесса по его ID </summary>
        /// <param name="stringProcessId">Номер ID в формате строки</param>
        /// <returns>Возвращает ошибку либо пустую строку</returns>
        static string KillProcessById(string stringProcessId)
        {
            string error = ParseId(stringProcessId, out int processId);
            if (!String.IsNullOrEmpty(error)) return error;

            error = GetProcessByID(processId, out Process process);
            if (!String.IsNullOrEmpty(error)) return error;

            error = KillProcess(process);
            return error;
        }

        /// <summary> Закрытие процесса по его имени </summary>
        /// <param name="processName">Имя процесса</param>
        /// <returns>Возвращает ошибку либо пустую строку</returns>
        static string KillProcessByName(string processName)
        {
            var error = GetProcessByName(processName, out Process process);
            if (!String.IsNullOrEmpty(error)) return error;

            error = KillProcess(process);
            return error;
        }

        /// <summary> Закрытие указанного процесса </summary>
        /// <param name="process">Процесс</param>
        /// <returns>Возвращает ошибку либо пустую строку</returns>
        static string KillProcess(Process process)
        {
            try
            {
                process.Kill();
            }
            catch (System.ComponentModel.Win32Exception)
            {
                return "Процесс не может быть завершен";
            }
            catch (NotSupportedException)
            {
                return "Процесс не был завершен, т.к. запущен на удаленном компьютере";
            }
            catch (InvalidOperationException)
            {
                return "Процесс не найден. Вероятно, был завершен ранее";
            }
            return string.Empty;
        }

        /// <summary> Получение процесса по его ID </summary>
        /// <param name="processId"> ID процесса</param>
        /// <param name="process"> Процесс, который возвращается в результате </param>
        /// <returns>Возвращает ошибку либо пустую строку</returns>
        static string GetProcessByID(int processId, out Process process)
        {
            try
            {
                process = Process.GetProcessById(processId);
                return string.Empty;
            }
            catch (ArgumentException)
            {
                process = null;
                return "Процесс с указанным ID не обнаружен";
            }
            catch (InvalidOperationException ex)
            {
                process = null;
                return ex.InnerException.Message;
            }              
        }

        /// <summary> Получение процесса по его имени </summary>
        /// <param name="processName">Имя процесса</param>
        /// <param name="process">Процесс, который возвращается в результате </param>
        /// <returns>>Возвращает ошибку либо пустую строку</returns>
        static string GetProcessByName(string processName, out Process process)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            switch (processes.Length)
            {
                case 0:
                    process = null;
                    return "Процесс с указанным именем не обнаружен";
                case 1:
                    process = processes[0];
                    return string.Empty;
                default:
                    process = null;
                    return "Процессов с указанным именем несколько.\n" +
                           "Воспользуйтесь аналогичной командой с указанием ID";
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
        }

        /// <summary> Отображение свойств процесса по его ID </summary>
        /// <param name="stringProcessId">Номер ID в формате строки</param>
        /// <returns>Возвращает ошибку либо пустую строку</returns>
        static string DisplayProcessPropertiesById(string stringProcessId)
        {
            string error = ParseId(stringProcessId, out int processId);
            if (!String.IsNullOrEmpty(error)) return error;

            error = GetProcessByID(processId, out Process process);
            if (!String.IsNullOrEmpty(error)) return error;

            DisplayProcessProperties(process);
            return string.Empty;
        }

        /// <summary> Отображение свойств процесса по его имени </summary>
        /// <param name="processName">Имя процесса</param>
        /// <returns>Возвращает ошибку либо пустую строку</returns>
        static string DisplayProcessPropertiesByName(string processName)
        {
            var error = GetProcessByName(processName, out Process process);
            if (!String.IsNullOrEmpty(error)) return error;

            DisplayProcessProperties(process);
            return string.Empty;
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
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        /// <summary> Приведение ID строкового типа в тип int </summary>
        /// <param name="stringProcessId">Номер ID в формате строки</param>
        /// <param name="processId">Возвращаемый номер ID в формате int</param>
        /// <returns>Возвращает ошибку либо пустую строку</returns>
        static string ParseId(string stringProcessId, out int processId)
        {
            processId = -1;
            try
            {
                processId = int.Parse(stringProcessId);
            }
            catch (ArgumentNullException)
            {
                return "ID процесса не указан.";
            }
            catch (FormatException)
            {
                return "Неверно указан ID процесса. Присутствуют недопустимые символы.";
            }
            catch (OverflowException)
            {
                return "ID процесса содержит излишнее количество символов.\n" +
                        "Проверьте правильность написания ID процесса.\n" +
                        "В случае, если ошибок не найдено, обратитесь к системному администратору.";
            }
            return string.Empty;
        }
        
        /// <summary> Вывод команд, доступные пользователю </summary>
        static void Help()
        {
            Console.WriteLine("Kill ID 'ID'/Name 'Name' - завершение процесса по ID либо имени. Пример: Kill ID 2012");
            Console.WriteLine("Sort ID 'ID'/Name 'Name' - отсортировать процессы по ID либо имени. Пример: Sort Name");
            Console.WriteLine("Prop ID 'ID'/Name 'Name' - вывести свойства процесса ID либо имени. Пример: Prop Name notepad");
            Console.WriteLine("GetId 'Name' - получить список всех id для указанного имени процесса. Пример: GetId notepad");
            Console.WriteLine("Refresh - обновить список процессов и вывести на экран");            
        }

        /// <summary> Обработчик введенных пользователем команд</summary>
        /// <returns>Возвращает распознанную команду либо ошибку и массив введенных аргументов</returns>
        static (Command, string[]) ParseUsersChoice()
        {
            string[] empty = { string.Empty };
            // Раскомментируйте код ниже и закомментируйте следующую за ним строку, чтобы обновлять список процессов в консоли раз в 'second' секунд.
            // К сожалению, это сбрасывает введенные пользователем команды, если он не успел нажать Enter;
            /*
            int second = 10;
            bool isEnterUserData = Reader.TryReadLine(out string usersChoice, second * 1000);
            if (!isEnterUserData)
            {
                return (Command.Refresh, empty);
            }
            */
            string usersChoice = Console.ReadLine();
            
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
                case "?":
                    return (Command.Help, empty);
                default:
                    return (Command.Error, empty);
            }
        }

        /// <summary> Вывод ошибки на экран либо сигнализация об успешном выполнении команды</summary>
        /// <param name="error">Ошибка, лиюо пустая строка, если ошибок не было</param>
        static void DisplayError(string error)
        {
            if (!String.IsNullOrEmpty(error))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Команда выполнена успешно");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        /// <summary> Получение и вывод на экран консоли списка процессов с одинаковым именем</summary>
        /// <param name="processName">Имя процесса</param>
        /// <returns>Возвращает ошибку либо пустую строку</returns>
        static string GetIdByName(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            switch (processes.Length)
            {
                case 0:
                    return "Процесс с указанным именем не обнаружен";
                default:
                    WriteProcessList(processes);
                    return string.Empty;
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
