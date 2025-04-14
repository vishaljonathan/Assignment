using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public Student StudentId { get; set; }
        public Course CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }

        //Implement Constructors
        public Enrollment(int Id, Student s_id, Course c_id, DateTime e_date)
        {
            EnrollmentID = Id;
            StudentId = s_id;
            CourseId = c_id;
            EnrollmentDate = e_date;
        }
        
    }
}
