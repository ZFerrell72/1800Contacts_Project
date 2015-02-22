using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1800Contacts_Project.Models
{
    public class Schedule
    {
        public Course Head { get; private set; }
        public Course Tail { get; private set; }
        public int NumCourses { get; set; }

        public Schedule() { }

        // Account for prerequisite when adding course.
        public bool AddCourse(Course course)
        {
            bool success = false;

            if (course.Prerequisite == null)
            {
                PutAtHead(course);
                success = true;
            }
            else
            {
                Course prerequisiteCourse = FindPrerequisite(course);
                if (prerequisiteCourse == null)
                {
                    // Prerequisite course has not been added yet.
                    // Create course for prereq
                    AddPrerequisite(course);
                    success = AddCourse(course);
                    if (success)
                    {
                        NumCourses--;
                    }
                }
                else
                {
                    InsertAfterPrerequisite(prerequisiteCourse, course);
                    success = true;
                }
            }

            if (success) { NumCourses++; }

            return success;
        }

        public void AddPrerequisite(Course course)
        {
            Course prerequisiteCourse = new Course(course.Prerequisite);
            AddCourse(prerequisiteCourse);
        }

        public bool RemoveCourse(Course course)
        {
            bool success = false;

            Course currentCourse = Head;
            // Index is defensive.
            int index = 0;
            while (currentCourse != null && !success && index < NumCourses)
            {
                if (currentCourse.Equals(course))
                {
                    if (course.Previous != null)
                    {
                        course.Previous.Next = course.Next;
                    }
                    if (course.Next != null)
                    {
                        course.Next.Previous = course.Previous;
                    }
                    success = true;
                }
                else
                {
                    currentCourse = currentCourse.Next;
                    index++;
                }
            }

            if (success) { NumCourses--; }

            return success;
        }

        public void UpdateCourse(Course dupCourse, Course course)
        {
            dupCourse.Prerequisite = course.Prerequisite;
            Course prerequisiteCourse = FindPrerequisite(dupCourse);
            if (prerequisiteCourse == null)
            {
                AddPrerequisite(dupCourse);
            }
            else
            {
                prerequisiteCourse.Next = dupCourse;
                dupCourse.Previous = prerequisiteCourse;
            }
        }

        public string GetSchedule(string[] courses)
        {
            for (int i = 0; i < courses.Length; i++)
            {
                string[] courseDesc = courses[i].Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
                Course course = null;
                if (courseDesc.Length == 1)
                {
                    course = new Course(courseDesc[0]);
                }
                else
                {
                    course = new Course(courseDesc[0], courseDesc[1]);
                }
                Course dupCourse = null;
                if ((dupCourse = containsCourse(course)) == null)
                {
                    AddCourse(course);
                }
                else if(course.Prerequisite != null)
                {
                    UpdateCourse(dupCourse, course);
                }
            }

            if (containsCircularDependency())
            {
                throw new ArgumentException("Course causes circular dependency!");
            }

            return this.ToString();
        }

        public bool containsCircularDependency()
        {
            bool containsCircularDependency = false;

            int index = 0;
            Course currentCourse = Head;
            while (!containsCircularDependency && currentCourse.Next != null && NumCourses > 1)
            {
                if (index >= NumCourses)
                {
                    containsCircularDependency = true;
                }
                else
                {
                    currentCourse = currentCourse.Next;
                    index++;
                }
            }

            return containsCircularDependency;
        }

        public Course containsCourse(Course course)
        {
            bool containsCourse = false;
            int index = 0;
            Course currentCourse = Head;
            while (index < NumCourses && !containsCourse)
            {
                if (currentCourse.Equals(course))
                {
                    containsCourse = true;
                }
                else
                {
                    index++;
                    currentCourse = currentCourse.Next;
                }
            }
            return currentCourse;
        }

        // Shoves coure to the head of the Linked List.
        public void PutAtHead(Course course)
        {
            if (Head == null)
            {
                Head = course;
                Tail = course;
            }
            else
            {
                Head.Previous = course;
                course.Next = Head;
                Head = course;
            }
        }

        // Adds course to the tail of the Linked List.
        public void PutAtTail(Course course)
        {
            Tail.Next = course;
            course.Previous = Tail;
            Tail = course;
        }

        public Course FindPrerequisite(Course course)
        {
            Course targetCourse = null;
            Course currentCourse = Head;
            int index = 0;
            while (index < NumCourses && targetCourse == null)
            {
                if (currentCourse.Name.Equals(course.Prerequisite))
                {
                    targetCourse = currentCourse;
                }
                currentCourse = currentCourse.Next;
                index++;
            }
            return targetCourse;
        }

        public void InsertAfterPrerequisite(Course prerequisiteCourse, Course course)
        {
            if (prerequisiteCourse.Equals(Tail))
            {
                Tail = course;
            }
            else
            {
                Course nextCourse = prerequisiteCourse.Next;
                course.Next = nextCourse;
                nextCourse.Previous = course;
            }
            prerequisiteCourse.Next = course;
            course.Previous = prerequisiteCourse;
        }

        public override string ToString()
        {
            Course currentCourse = Head;
            string schedule = "";
            int index = 0;
            while (index < NumCourses && currentCourse != null)
            {
                schedule += currentCourse.Name;
                if (currentCourse.Next != null) { schedule += ", "; }
                currentCourse = currentCourse.Next;
                index++;
            }
            return schedule;
        }
    }
}
