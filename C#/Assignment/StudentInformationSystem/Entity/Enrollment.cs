using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    class Enrollment
    {
        public int EnrollmentID { get; set; }
        public Student EnrolledStudentName { get; set; }
        public Course EnrolledCourseID { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public Enrollment(int id, Student student, Course course, DateTime date)
        {
            EnrollmentID = id;
            EnrolledStudentName = student;
            EnrolledCourseID = course;
            EnrollmentDate = date;
        }
    }
}
