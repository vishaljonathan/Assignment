using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public Enrollment(int enrollmentID, Student student, Course course, DateTime enrollmentDate)
        {
            EnrollmentID = enrollmentID;
            Student = student;
            Course = course;
            EnrollmentDate = enrollmentDate;
        }
        public Student GetStudent() => Student;
        public Course GetCourse() => Course;
    }
}
