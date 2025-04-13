using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class SIS
    {
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
        public List<Enrollment> Enrollments { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Payment> Payments { get; set; }
        public SIS()
        {
            Students = new List<Student>();
            Courses = new List<Course>();
            Enrollments = new List<Enrollment>();
            Teachers = new List<Teacher>();
            Payments = new List<Payment>();
        }
    }
}
