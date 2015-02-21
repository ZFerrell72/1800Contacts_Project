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

        public override bool Equals(object obj)
        {
            bool equals = true;

            if (obj == null || this.GetType() != obj.GetType())
            {
                equals = false;
            }
            else
            {
                Course course = (Course)obj;
                if (!Name.Equals(course.Name))
                {
                    equals = false;
                }
                if ((Prerequisite == null && course.Prerequisite != null) || (Prerequisite != null && course.Prerequisite == null))
                {
                    equals = false;
                }
                else if (Prerequisite != null && course.Prerequisite != null)
                {
                    if (!Prerequisite.Equals(course.Prerequisite)) { equals = false; }
                }
            }

            return equals;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
