using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1800Contacts_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            string classDesc = "class: prereq";
            string[] classArray = classDesc.Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(classArray.Length);
            foreach (string s in classArray)
            {
                Console.WriteLine(s);
            }
            Console.ReadKey();
        }
    }
}
