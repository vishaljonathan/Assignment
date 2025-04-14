using StudentInformationSystem.Exception;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public List<Course> EnrolledCourses { get; set; } = new List<Course>();
        public List<Payment> PaymentHistory { get; set; } = new List<Payment>();
        public List<Enrollment> Enrollments { get; set; }
        public List<Payment> Payments { get; set; } = new List<Payment>();


        // Constructor to initialize attributes
        public Student(int studentId, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            StudentId = studentId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
        }
        public Student()
        {
            Enrollments = new List<Enrollment>();
        }

        //Methods
        public void EnrollInCourse(Course course)
        {
            EnrolledCourses.Add(course);
        }
        public void UpdateStudentInfo(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
        }
        public void MakePayment(decimal amount, DateTime paymentDate)
        {
            PaymentHistory.Add(new Payment(0, this.StudentId, amount, paymentDate));
        }
        public void DisplayStudentInfo()
        {
            Console.WriteLine($"Student ID: {StudentId}, Name: {FirstName} {LastName}, DOB: {DateOfBirth}, Email: {Email}, Phone: {PhoneNumber}");
        }
        public List<Course> GetEnrolledCourses()
        {
            return EnrolledCourses;
        }
        public List<Payment> GetPaymentHistory()
        {
            return PaymentHistory;
        }
    }
}
