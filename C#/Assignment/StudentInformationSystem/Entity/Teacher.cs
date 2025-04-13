using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Expertise { get; set; }
        public List<Course> AssignedCourses { get; set; }
        public Teacher(int teacherID, string firstName, string lastName, string email)
        {
            TeacherID = teacherID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            AssignedCourses = new List<Course>();
        }
        public void UpdateTeacherInfo(string name, string email, string expertise)
        {
            FirstName = name.Split(' ')[0];
            LastName = name.Split(' ')[1];
            Email = email;
            Expertise = expertise;
        }
        public void DisplayTeacherInfo()
        {
            Console.WriteLine($"ID: {TeacherID}, Name: {FirstName} {LastName}, Email: {Email}, Expertise: {Expertise}");
        }
        public List<Course> GetAssignedCourses()
        {
            return AssignedCourses;
        }
    }
}
