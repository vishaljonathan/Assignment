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
        public string Password { get; set; }

        //Implement Constructors
        public Student(int id, string firstname, string lastname, DateTime dob, string email, string password)
        {
            StudentID = id;
            FirstName = firstname;
            LastName = lastname;
            DateOfBirth = dob;
            Email = email;
            Password = password;
        }
    }
}
