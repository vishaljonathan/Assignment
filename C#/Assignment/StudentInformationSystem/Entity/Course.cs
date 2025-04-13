using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string InstructorName { get; set; }
        public Teacher AssignedTeacher { get; set; }
        public List<Enrollment> Enrollments { get; set; }
        public Course(int courseID, string courseName, string courseCode, string instructorName)
        {
            CourseID = courseID;
            CourseName = courseName;
            CourseCode = courseCode;
            InstructorName = instructorName;
            Enrollments = new List<Enrollment>();
        }

        public Course(string v1, string v2)
        {
        }

        public void AssignTeacher(Teacher teacher)
        {
            AssignedTeacher = teacher;
            teacher.AssignedCourses.Add(this);
        }
        public void UpdateCourseInfo(string code, string name, string instructor)
        {
            CourseCode = code;
            CourseName = name;
            InstructorName = instructor;
        }
        public void DisplayCourseInfo()
        {
            Console.WriteLine($"ID: {CourseID}, Name: {CourseName}, Code: {CourseCode}, Instructor: {InstructorName}");
        }
        public List<Enrollment> GetEnrollments()
        {
            return Enrollments;
        }
        public Teacher GetTeacher()
        {
            return AssignedTeacher;
        }
    }
}
