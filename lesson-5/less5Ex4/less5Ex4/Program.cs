using System;
using System.IO;

namespace less5Ex4
{
    class Program
    {
        /*
            Долгуев Владимир.
            Сохранить дерево каталогов и файлов по заданному пути в текстовый файл — с рекурсией и без.
         */
        static void Main(string[] args)
        {
            string workDir;
            while (true)
            {
                Console.WriteLine("Введите путь, для которого необходимо сохранить дерево каталогов и файлов.");
                workDir = Console.ReadLine();
                if (Directory.Exists(workDir))
                {
                    File.WriteAllText("exportDirV1.txt", workDir + "\n");
                    break;
                }
                else
                {
                    Console.WriteLine("Введенный путь не существует.");
                }
            }

            #region Вариант 1. Вывод всех файлов и папок сплошным списком, исключая родительский путь

            string[] entries = Directory.GetFileSystemEntries(workDir, "*", SearchOption.AllDirectories);
            if (entries.Length == 0)
            {
                Console.WriteLine("Указанная папка ничего не содержит");
            }
            else
            {
                string[] outEntries = new string[entries.Length];
                for (int i = 0; i < entries.Length; i++)
                {
                    outEntries[i] = new string(' ', workDir.Length) + entries[i].Replace(workDir, "");
                }
                File.AppendAllLines("exportDirV1.txt", outEntries);
                Console.WriteLine("Экспорт по варианту 1 в файл exportDirV1.txt завершен.");
            }

            #endregion

            #region Вариант 2. Рекурсивное получение файлов и папок и запись их в файл

            File.WriteAllText("exportDirV2.txt", workDir  + "\n");
            GetFoldersAndFiles(workDir);
            Console.WriteLine("Экспорт по варианту 2 в файл exportDirV2.txt завершен.");

            #endregion

            Console.ReadKey();
        }

        static void GetFoldersAndFiles(string workDir)
        {
            string[] files = Directory.GetFiles(workDir, "*", SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                File.AppendAllText("exportDirV2.txt", new string(' ', workDir.Length) + file.Replace(workDir, "") + "\n");
            }

            string[] folders = Directory.GetDirectories(workDir, "*", SearchOption.TopDirectoryOnly);
            if (folders.Length == 0) return;

            foreach (var folder in folders)
            {
                File.AppendAllText("exportDirV2.txt", new string(' ', workDir.Length) + folder.Replace(workDir, "") + "\n");
                GetFoldersAndFiles(folder);                
            }  
        }
    }
}
