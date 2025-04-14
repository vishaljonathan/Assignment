using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int CourseCode { get; set; }
        public string InstructorName { get; set; }

        //Implement Constructors
        public Course(int id, string course_name, int code, string instructor_name)
        {
            CourseID = id;
            CourseName = course_name;
            CourseCode = code;
            InstructorName = instructor_name;
        }
    }
}
