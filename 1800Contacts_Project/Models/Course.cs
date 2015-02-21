using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1800Contacts_Project.Models
{
    public class Course
    {
        public string Name { get; set; }
        public string Prerequisite { get; set; }
        public Course Next { get; set; }
        public Course Previous { get; set; }

        public Course(string name)
        {
            Name = name;
            Prerequisite = null;
        }

        public Course(string name, string prerequisite)
        {
            Name = name;
            Prerequisite = prerequisite;
        }


    }
}
