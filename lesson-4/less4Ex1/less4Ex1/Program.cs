using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace less4Ex1
{
    class Program
    {

        /*
            Долгуев Владимир.
            Написать метод GetFullName(string firstName, string lastName, string patronymic), 
            принимающий на вход ФИО в разных аргументах и возвращающий объединённую строку с ФИО. 
            Используя метод, написать программу, выводящую в консоль 3–4 разных ФИО.
         */

        static void Main(string[] args)
        {
            var users = new Users[] {new Users() { firstName = "Иван", lastName = "Иванов", patronymic = "Иванович" },
                                     new Users() { firstName = "Пётр", lastName = "Петров", patronymic = "Петрович" },
                                     new Users() { firstName = "Семён", lastName = "Семёнов", patronymic = "Семёнович" },
                                     new Users() { firstName = "Фёдор", lastName = "Фёдоров", patronymic = "Фёдорович" }
                                    };

            foreach (var user in users)
            {
                Console.WriteLine(user.GetFullName());
            }

            Console.ReadKey();
        }

        
    }
}
