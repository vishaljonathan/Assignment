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

        public Course(int courseID, string courseName, int courseCode, string instructorName)
        {
            CourseID = courseID;
            CourseName = courseName;
            CourseCode = courseCode;
            InstructorName = instructorName;
        }
    }
}
