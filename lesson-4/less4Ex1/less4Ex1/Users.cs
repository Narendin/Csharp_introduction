using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace less4Ex1
{
    class Users
    {        
        public string firstName;
        public string lastName;
        public string patronymic;

        public string GetFullName()
        {
            return $"{lastName} {firstName} {patronymic}";
        }
    }
}
