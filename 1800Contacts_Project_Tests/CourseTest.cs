using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _1800Contacts_Project.Models;

namespace _1800Contacts_Project_Tests
{
    [TestClass]
    public class CourseTest
    {
        private Course course1;
        private Course course2;
        private Course course3;
        private string name1 = "TestCourse1";
        private string name2 = "TestCourse2";
        private string name3 = "TestCourse3";

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
            Assert.AreEqual(name1, course1.GetName(), "Course1's name did not equal " + name1);
            Assert.AreEqual(name3, course3.GetName(), "Course3's name did not equal " + name3);
        }

        [TestMethod]
        public void TestGetPrerequisite()
        {
            Assert.IsNull(course2.GetPrerequisite());
            Assert.AreEqual(name2, course3.GetPrerequisite(), "Course3's prerequisite did not equal " + name2);
        }

        [TestMethod]
        public void TestNextCourse()
        {
            course2.SetNext(course3);
            Assert.AreEqual(course3, course2.GetNext(), "course2.getNext did not equal course3");
            course2.SetNext(null);
            Assert.IsNull(course2.GetNext());
        }

        [TestMethod]
        public void TestPreviousCourse()
        {
            course3.SetPrevious(course2);
            Assert.AreEqual(course2, course3.GetPrevious, "course3.getPrevious did not equal course2");
        }
    }
}
