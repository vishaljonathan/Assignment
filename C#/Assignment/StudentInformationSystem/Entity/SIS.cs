using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class SIS
    {
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
        public List<Enrollment> Enrollments { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Payment> Payments { get; set; }
        public SIS()
        {
            Students = new List<Student>();
            Courses = new List<Course>();
            Enrollments = new List<Enrollment>();
            Teachers = new List<Teacher>();
            Payments = new List<Payment>();
        }
        public void EnrollStudentInCourse(Student student, Course course)
        {
            student.EnrollInCourse(course);
            Enrollments.Add(new Enrollment(new Random().Next(1000, 9999), student, course, DateTime.Now));
        }
        public void AssignTeacherToCourse(Teacher teacher, Course course)
        {
            course.AssignTeacher(teacher);
        }
        public void RecordPayment(Student student, decimal amount, DateTime date)
        {
            student.MakePayment(amount, date);
            Payments.Add(new Payment(new Random().Next(1000, 9999), student, amount, date));
        }
        public void GenerateEnrollmentReport(Course course)
        {
            Console.WriteLine($"Enrollment Report for {course.CourseName}:");
            foreach (var e in course.GetEnrollments())
            {
                Console.WriteLine($" - {e.Student.FirstName} {e.Student.LastName}");
            }
        }
        public void GeneratePaymentReport(Student student)
        {
            Console.WriteLine($"Payment Report for {student.FirstName} {student.LastName}:");
            foreach (var p in student.GetPaymentHistory())
            {
                Console.WriteLine($" - Amount: {p.Amount}, Date: {p.PaymentDate.ToShortDateString()}");
            }
        }
        public void CalculateCourseStatistics(Course course)
        {
            int totalEnrollments = course.GetEnrollments().Count;
            decimal totalPayments = course.GetEnrollments().Sum(e =>
                e.Student.GetPaymentHistory().Sum(p => p.Amount));

            Console.WriteLine($"Statistics for {course.CourseName}:\n - Total Enrollments: {totalEnrollments}\n - Total Payments: {totalPayments:C}");
        }
    }
}
