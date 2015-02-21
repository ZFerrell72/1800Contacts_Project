using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _1800Contacts_Project.Models;

namespace _1800Contacts_Project_Tests
{
    [TestClass]
    public class ScheduleTest
    {
        private Schedule schedule;
        private Course head;
        private Course tail;
        private string headName = "head";
        private string tailName = "tail";

        private Course course1;
        private Course course2;
        private Course course3;
        private Course course4;
        private Course course5;
        private string name1 = "name1";
        private string name2 = "name2";
        private string name3 = "name3";
        private string name4 = "name4";
        private string name5 = "name5";

        [TestInitialize]
        public void SetUp()
        {
            schedule = new Schedule();
            head = new Course(headName);
            tail = new Course(tailName, headName);
        }

        [TestMethod]
        public void TestHead()
        {
            Assert.IsNull(schedule.GetHead());
            Assert.AreEqual(0, schedule.GetNumCourses());
            schedule.SetHead(head);
            Assert.AreEqual(head, schedule.GetHead());
            Assert.AreEqual(1, schedule.GetNumCourses());
        }

        [TestMethod]
        public void TestTail()
        {
            Assert.IsNull(schedule.GetTail());
            Assert.AreEqual(0, schedule.GetNumCourses());
            schedule.SetTail(tail);
            Assert.AreEqual(tail, schedule.GetTail());
            Assert.AreEqual(1, schedule.GetNumCourses());
        }

        [TestMethod]
        public void TestAddCourse()
        {
            Assert.AreEqual(0, schedule.GetNumCourses());
            Assert.IsTrue(schedule.AddCourse(head));
            Assert.AreEqual(1, schedule.GetNumCourses());
        }

        [TestMethod]
        public void TestRemoveCourse()
        {
            schedule.AddCourse(head);
            Assert.AreEqual(1, schedule.GetNumCourses());
            Assert.IsTrue(schedule.RemoveCourse(head));
            Assert.AreEqual(0, schedule.GetNumCourses());
        }

        [TestMethod]
        public void TestOneCourse()
        {
            course1 = new Course(name1);
            schedule.AddCourse(course1);
            Assert.AreEqual(name1, schedule.GetSchedule());
        }

        [TestMethod]
        public void TestCourseWithPrerequisite()
        {
            course1 = new Course(name1, name2);
            course2 = new Course(name2);
            schedule.AddCourse(course1);
            schedule.AddCourse(course2);

            string expected = name2 + ", " + name1;
            Assert.AreEqual(expected, schedule.GetSchedule());
        }

        [TestMethod]
        public void TestMultipleCoursesWithPrerequisites()
        {
            course1 = new Course(name1, name2);
            course2 = new Course(name2, name3);
            course3 = new Course(name3);
            course4 = new Course(name4, name2);
            schedule.AddCourse(course1);
            schedule.AddCourse(course2);
            schedule.AddCourse(course3);
            schedule.AddCourse(course4);

            string expected = name3 + ", " + name2 + ", " + name1 + ", " + name4;
            Assert.AreEqual(expected, schedule.GetSchedule());
        }

        [TestMethod]
        public void TestCircularDependency()
        {
            course1 = new Course(name1, name2);
            course2 = new Course(name2, name1);

            schedule.AddCourse(course1);
            try
            {
                schedule.AddCourse(course2);
                Assert.Fail("Schedule with circular dependency was allowed.");
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Course causes circular dependency!", e.Message);
            }
        }

        [TestMethod]
        public void TestMultipleCoursesWithCircularDependency()
        {
            course1 = new Course(name1, name4);
            course2 = new Course(name2, name1);
            course3 = new Course(name3, name4);
            course4 = new Course(name4, name2);
            schedule.AddCourse(course4);
            schedule.AddCourse(course3);
            schedule.AddCourse(course2);
            try
            {
                schedule.AddCourse(course1);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Course causes circular dependency!", e.Message);
            }
        }
    }
}
