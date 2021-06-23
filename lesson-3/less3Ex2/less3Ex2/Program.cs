using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace less3Ex2
{
    class Program
    {
        /*
            Долгуев Владимир.
            Написать программу «Телефонный справочник»: создать двумерный массив 5х2, 
            хранящий список телефонных контактов: 
            первый элемент хранит имя контакта, 
            второй — номер телефона/email.
         */
        static void Main(string[] args)
        {
            var phonebook = new string[5, 2];

            phonebook[0, 0] = "Иванов Иван";
            phonebook[0, 1] = "8-965-568-62-98";

            phonebook[1, 0] = "Сидоров Сидор";
            phonebook[1, 1] = "8-965-568-62-98";

            phonebook[2, 0] = "Петров Пётр";
            phonebook[2, 1] = "8-965-568-62-98";

            phonebook[3, 0] = "Михайлов Михаил";
            phonebook[3, 1] = "8-965-568-62-98";

            phonebook[4, 0] = "Николаев Николай";
            phonebook[4, 1] = "8-965-568-62-98";

            for (int i = 0; i < phonebook.GetLength(0); i++)
            {
                Console.WriteLine($"{phonebook[i, 0]}".PadRight(20) + $"{phonebook[i, 1]}");
            }

            Console.ReadKey();
        }
    }
}
