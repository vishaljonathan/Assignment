using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }

        // Constructor to initialize attributes
        public Enrollment(int enrollmentId, int studentId, int courseId, DateTime enrollmentDate)
        {
            EnrollmentId = enrollmentId;
            StudentId = studentId;
            CourseId = courseId;
            EnrollmentDate = enrollmentDate;
        }
        public Enrollment(Student student, Course course)
        {
            Student = student;
            Course = course;
        }
        public Enrollment() 
        { 
        }

        // Methods
        public Student GetStudent()
        {
            // Retrieve student based on StudentId (assuming data source or service)
            return new Student(StudentId, "John", "Doe", DateTime.Now.AddYears(-20), "john.doe@mail.com", "1234567890");
        }
        public Course GetCourse()
        {
            // Retrieve course based on CourseId (assuming data source or service)
            return new Course(CourseId, "Math 101", "MTH101", "Dr. Smith");
        }
    }
}
