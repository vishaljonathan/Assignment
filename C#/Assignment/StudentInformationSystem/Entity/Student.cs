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
        public long PhoneNumber { get; set; }

        public Student(int s_id, string firstname, string lastname, DateTime dob, string email, long phone)
        {
            StudentID = s_id;
            FirstName = firstname;
            LastName = lastname;
            DateOfBirth = dob;
            Email = email;
            PhoneNumber = phone;
        }
    }
}
