using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    class Teacher
    {
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Course> AssignedCourses { get; set; } = new List<Course>();

        public Teacher(int teacherID, string firstName, string lastName, string email)
        {
            TeacherID = teacherID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public void UpdateTeacherInfo(string name, string email, string expertise)
        {
            FirstName = name.Split(' ')[0];
            LastName = name.Split(' ').Length > 1 ? name.Split(' ')[1] : "";
            Email = email;
        }

        public void DisplayTeacherInfo()
        {
            Console.WriteLine($"Teacher: {FirstName} {LastName}, Email: {Email}");
        }

        public List<Course> GetAssignedCourses() => AssignedCourses;
    }
}
