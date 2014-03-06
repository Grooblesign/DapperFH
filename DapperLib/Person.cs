using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DapperLib
{
    public class Person
    {
        public int Id { get; set; }
        public string Forenames { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public int FatherId { get; set; }
        public int MotherId { get; set; }

        public string Fullname
        {
            get
            {
                return (Forenames.Trim() + " " + Surname.Trim()).Trim();
            }
        }

        public override string ToString()
        {
            return String.Format("Id: {0}, Forenames: {1}, Surname: {2}", Id, Forenames.Trim(), Surname.Trim());
        }
    }
}
