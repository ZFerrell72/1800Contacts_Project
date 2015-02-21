using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1800Contacts_Project.Models
{
    public class Schedule
    {
        public Course Head { get; set; }
        public Course Tail { get; set; }
        public int NumCourses { get; set; }

        public Schedule() { }
    }
}
