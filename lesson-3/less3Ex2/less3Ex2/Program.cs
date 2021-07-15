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
            int userName = 0;
            int phoneNumber = 1;
            var phoneBook = new string[5, 2];

            phoneBook[0, userName] = "Иванов Иван";
            phoneBook[0, phoneNumber] = "8-965-568-62-98";

            phoneBook[1, userName] = "Сидоров Сидор";
            phoneBook[1, phoneNumber] = "8-965-568-62-98";

            phoneBook[2, userName] = "Петров Пётр";
            phoneBook[2, phoneNumber] = "8-965-568-62-98";

            phoneBook[3, userName] = "Михайлов Михаил";
            phoneBook[3, phoneNumber] = "8-965-568-62-98";

            phoneBook[4, userName] = "Николаев Николай";
            phoneBook[4, phoneNumber] = "8-965-568-62-98";

            for (int i = 0; i < phoneBook.GetLength(0); i++)
            {
                Console.WriteLine($"{phoneBook[i, userName]}".PadRight(20) + $"{phoneBook[i, phoneNumber]}");
            }

            Console.ReadKey();
        }
    }
}
