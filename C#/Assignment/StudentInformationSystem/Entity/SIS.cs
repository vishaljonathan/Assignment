using static StudentInformationSystem.Exception.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class SIS
    {
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public List<Payment> Payments { get; set; } = new List<Payment>();

        // Methods
        public void EnrollStudentInCourse(Student student, Course course)
        {
            student.EnrollInCourse(course);
        }
        public void AssignTeacherToCourse(Teacher teacher, Course course)
        {
            course.AssignTeacher(teacher);
        }
        public void RecordPayment(Student student, decimal amount, DateTime paymentDate)
        {
            student.MakePayment(amount, paymentDate);
        }
        public void GenerateEnrollmentReport(Course course)
        {
            Console.WriteLine("Generating enrollment report for course: " + course.CourseName);
            foreach (var enrollment in course.GetEnrollments())
            {
                var student = enrollment.GetStudent();
                Console.WriteLine($"Student ID: {student.StudentId}, Name: {student.FirstName} {student.LastName}");
            }
        }
        public void GeneratePaymentReport(Student student)
        {
            Console.WriteLine("Generating payment report for student: " + student.FirstName + " " + student.LastName);
            foreach (var payment in student.GetPaymentHistory())
            {
                Console.WriteLine($"Payment ID: {payment.PaymentId}, Amount: {payment.Amount}, Date: {payment.PaymentDate}");
            }
        }
        public void CalculateCourseStatistics(Course course)
        {
            Console.WriteLine("Calculating statistics for course: " + course.CourseName);
            Console.WriteLine($"Number of enrollments: {course.GetEnrollments().Count}");
            decimal totalPayments = 0;
            foreach (var enrollment in course.GetEnrollments())
            {
                var student = enrollment.GetStudent();
                foreach (var payment in student.GetPaymentHistory())
                {
                    totalPayments += payment.Amount;
                }
            }
            Console.WriteLine($"Total payments: {totalPayments}");
        }
        public void AddEnrollment(Student student, Course course, DateTime enrollmentDate)
        {
            if (!Students.Contains(student)) throw new StudentNotFoundException("Student not found.");
            if (!Courses.Contains(course)) throw new CourseNotFoundException("Course not found.");

            foreach (var e in student.Enrollments)
            {
                if (e.Course == course)
                    throw new DuplicateEnrollmentException("Student already enrolled in this course.");
            }

            Enrollment enrollment = new Enrollment(student.StudentId, course.CourseId, student.StudentId, enrollmentDate);
            enrollment.Student = student;
            enrollment.Course = course;

            student.Enrollments.Add(enrollment);
            course.Enrollments.Add(enrollment);
            Enrollments.Add(enrollment);
        }

        public void AssignCourseToTeacher(Course course, Teacher teacher)
        {
            if (!Courses.Contains(course)) throw new CourseNotFoundException("Course not found.");
            if (!Teachers.Contains(teacher)) throw new TeacherNotFoundException("Teacher not found.");

            teacher.AssignedCourses.Add(course);
        }

        public void AddPayment(Student student, double amount, DateTime paymentDate)
        {
            if (!Students.Contains(student)) throw new StudentNotFoundException("Student not found.");
            if (amount <= 0) throw new PaymentValidationException("Payment amount must be greater than zero.");

            Payment payment = new Payment(student.StudentId, student.StudentId, (decimal)amount, paymentDate);
            payment.Student = student;
            if (student.Payments == null) student.Payments = new List<Payment>();
            student.Payments.Add(payment);
            Payments.Add(payment);
        }

        public List<Enrollment> GetEnrollmentsForStudent(Student student)
        {
            if (!Students.Contains(student)) throw new StudentNotFoundException("Student not found.");
            return student.Enrollments;
        }

        public List<Course> GetCoursesForTeacher(Teacher teacher)
        {
            if (!Teachers.Contains(teacher)) throw new TeacherNotFoundException("Teacher not found.");
            return teacher.AssignedCourses;
        }
    }
}
