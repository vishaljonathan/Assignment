using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int CourseCode { get; set; }
        public string InstructorName { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public Course(int courseID, string courseName, int courseCode, string instructorName)
        {
            CourseID = courseID;
            CourseName = courseName;
            CourseCode = courseCode;
            InstructorName = instructorName;
        }

        public void AssignTeacher(string instructorName)
        {
            InstructorName = instructorName;
        }

        public void UpdateCourseInfo(int courseCode, string courseName, string instructorName)
        {
            CourseCode = courseCode;
            CourseName = courseName;
            InstructorName = instructorName;
        }

        public void DisplayCourseInfo()
        {
            Console.WriteLine($"Course ID: {CourseID}");
            Console.WriteLine($"Course Name: {CourseName}");
            Console.WriteLine($"Course Code: {CourseCode}");
            Console.WriteLine($"Instructor Name: {InstructorName}");
            Console.WriteLine($"Number of Enrollments: {Enrollments.Count}");
        }

        public List<Enrollment> GetEnrollments()
        {
            return Enrollments;
        }

        public string GetInstructorName()
        {
            return InstructorName;
        }
    }
}
