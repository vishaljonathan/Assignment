﻿using System;
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
        public Teacher(int teacherID, string firstName, string lastName, string email)
        {
            TeacherID = teacherID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
