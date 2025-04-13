using StudentInformationSystem.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Enrollment> Enrollments { get; set; }
        public List<Payment> Payments { get; set; }
        public Student(int studentID, string firstName, string lastName, DateTime dob, string email, string phone)
        {
            StudentID = studentID;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dob;
            Email = email;
            PhoneNumber = phone;
            Enrollments = new List<Enrollment>();
            Payments = new List<Payment>();
        }

        public Student(int v1, string v2, string v3, string v4, string v5)
        {
        }

        public void EnrollInCourse(Course course)
        {
            Enrollment enrollment = new Enrollment(GenerateID(), this, course, DateTime.Now);
            Enrollments.Add(enrollment);
            course.Enrollments.Add(enrollment);
        }
        public void UpdateStudentInfo(string firstName, string lastName, DateTime dob, string email, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dob;
            Email = email;
            PhoneNumber = phone;
        }
        public void MakePayment(decimal amount, DateTime paymentDate)
        {
            // Validate payment amount
            if (amount <= 0)
                throw new PaymentValidationException("Payment amount must be greater than zero.");

            // Validate payment date
            if (paymentDate > DateTime.Now)
                throw new PaymentValidationException("Payment date cannot be in the future.");

            // Create payment and add it to the student's payment list
            Payment payment = new Payment(this, amount, paymentDate);
            Payments.Add(payment);
        }
        public void DisplayStudentInfo()
        {
            Console.WriteLine($"ID: {StudentID}, Name: {FirstName} {LastName}, DOB: {DateOfBirth.ToShortDateString()}, Email: {Email}, Phone: {PhoneNumber}");
        }
        public List<Course> GetEnrolledCourses()
        {
            return Enrollments.Select(e => e.Course).ToList();
        }
        public List<Payment> GetPaymentHistory()
        {
            return Payments;
        }
        private int GenerateID() => new Random().Next(1000, 9999);
    }
}
