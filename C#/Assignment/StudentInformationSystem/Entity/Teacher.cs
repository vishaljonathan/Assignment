﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Expertise { get; set; }

        public List<Course> AssignedCourses { get; set; }

        // Constructor to initialize attributes
        public Teacher(int teacherId, string firstName, string lastName, string email, string exp)
        {
            TeacherId = teacherId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Expertise = exp;
        }
        public Teacher()
        {
            AssignedCourses = new List<Course>();
        }

        // Methods
        public void UpdateTeacherInfo(string name, string email, string expertise)
        {
            FirstName = name.Split(' ')[0];
            LastName = name.Split(' ')[1];
            Email = email;
        }
        public void DisplayTeacherInfo()
        {
            Console.WriteLine($"Teacher ID: {TeacherId}, Name: {FirstName} {LastName}, Email: {Email}");
        }
        public List<Course> GetAssignedCourses()
        {
            return new List<Course> { new Course(1, "Math 101", "MTH101", "Dr. Smith") };
        }
    }
}
