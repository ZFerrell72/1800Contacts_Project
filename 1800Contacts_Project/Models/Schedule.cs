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

            if (NumCourses == 0)
            {
                Head = course;
                Tail = course;
                success = true;
            }
            else if (course.Prerequisite == null)
            {
                PutAtHead(course);
                success = true;
            }
            else
            {
                if (Tail.Name.Equals(course.Prerequisite))
                {
                    PutAtTail(course);
                    success = true;
                }
                else
                {
                    Course prerequisiteCourse = FindPrerequisite(course);
                    if (prerequisiteCourse == null)
                    {
                        // Prerequisite course has not been added yet.
                        // Add to tail
                        PutAtTail(course);
                        success = true;
                    }
                    else
                    {
                        InsertAfterPrerequisite(prerequisiteCourse, course);
                        success = true;
                    }
                }
            }

            if (success) { NumCourses++; }

            return success;
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

        public string GetSchedule(string[] courses)
        {
            return null;
        }

        // Shoves coure to the head of the Linked List.
        public void PutAtHead(Course course)
        {
            Head.Previous = course;
            course.Next = Head;
            Head = course;
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
                index++;
            }
            return targetCourse;
        }

        public void InsertAfterPrerequisite(Course prerequisiteCourse, Course course)
        {
            Course nextCourse = prerequisiteCourse.Next;
            prerequisiteCourse.Next = course;
            course.Previous = prerequisiteCourse;
            course.Next = nextCourse;
            nextCourse.Previous = course;
        }
    }
}
