using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public List<Course> EnrolledCourses { get; set; } = new List<Course>();
        public List<Payment> Payments { get; set; } = new List<Payment>();
        public Student(int s_id, string firstname, string lastname, DateTime dob, string email, string phone)
        {
            StudentID = s_id;
            FirstName = firstname;
            LastName = lastname;
            DateOfBirth = dob;
            Email = email;
            PhoneNumber = phone;
        }

        public void EnrollInCourse(Course course)
        {
            EnrolledCourses.Add(course);
        }

        public void UpdateStudentInfo(string firstName, string lastName, DateTime dob, string email, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dob;
            Email = email;
            PhoneNumber = phone;
        }

        public void DisplayStudentInfo()
        {
            Console.WriteLine($"Student: {FirstName} {LastName}, $DOB: {DateOfBirth.ToShortDateString()}, $Email: {Email}, $Phone: {PhoneNumber}");
        }

        public List<Course> GetEnrolledCourses()
        {
            return EnrolledCourses;
        }
        
        public List<Payment> GetPaymentHistory()
        {
            return Payments;
        }
    }
}
