using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _1800Contacts_Project.Models;

namespace _1800Contacts_Project_Tests
{
    [TestClass]
    public class CourseTest
    {
        public Course course1;
        public Course course2;
        public Course course3;
        public string name1 = "TestCourse1";
        public string name2 = "TestCourse2";
        public string name3 = "TestCourse3";

        [TestInitialize]
        public void SetUp()
        {
            course1 = new Course(name1);
            course2 = new Course(name2);
            course3 = new Course(name3, name2);
        }

        [TestMethod]
        public void TestGetName()
        {
            Assert.AreEqual(name1, course1.Name, "Course1's name did not equal " + name1);
            Assert.AreEqual(name3, course3.Name, "Course3's name did not equal " + name3);
        }

        [TestMethod]
        public void TestGetPrerequisite()
        {
            Assert.IsNull(course2.Prerequisite);
            Assert.AreEqual(name2, course3.Prerequisite, "Course3's prerequisite did not equal " + name2);
        }

        [TestMethod]
        public void TestNextCourse()
        {
            course2.Next = null;
            Assert.IsNull(course2.Next);
            course2.Next = course3;
            Assert.AreEqual(course3, course2.Next, "course2.getNext did not equal course3");
        }

        [TestMethod]
        public void TestPreviousCourse()
        {
            course3.Previous = null;
            Assert.IsNull(course3.Previous);
            course3.Previous = course2;
            Assert.AreEqual(course2, course3.Previous, "course3.getPrevious did not equal course2");
        }
    }
}
