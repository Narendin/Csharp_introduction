using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace less5Ex5
{
    class ToDo
    {
        /// <summary>
        /// Задача
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///  Флаг выполнения задачи
        /// </summary>
        public bool IsDone { get; set; }

        /// <summary>
        /// Безпараметрический конструктор для десериализации json
        /// </summary>
        public ToDo()
        {
        }

        /// <summary>
        /// Конструктор добавления новой задачи.
        /// По умолчанию флаг выполнения задачи - не выполнена.
        /// </summary>
        /// <param name="title"> Текст новой задачи </param>
        public ToDo(string title) 
        {
            Title = title;
            IsDone = false;
        }

        /// <summary>
        /// Конструктор добавления новой задачи с возможностью установки флага "Выполнено"
        /// </summary>
        /// <param name="title"> Текст новой задачи </param>
        /// <param name="isDone"> Флаг выполнения </param>
        public ToDo(string title, bool isDone)
        {
            Title = title;
            IsDone = isDone;
        }

        public override string ToString()
        {
            return (IsDone ? "[X] " : "[ ] ") + Title;
        }
    }
}
