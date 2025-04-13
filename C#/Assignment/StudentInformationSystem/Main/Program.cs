using StudentInformationSystem.Entity;
using System;
using StudentInformationSystem.Exception;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize SIS system
            SIS sis = new SIS();

            // Create Teachers
            Teacher teacher1 = new Teacher(1, "Dr. John Smith", "john.smith@example.com", "Computer Science");
            Teacher teacher2 = new Teacher(2, "Dr. Alice Brown", "alice.brown@example.com", "Mathematics");

            // Add teachers to the system
            sis.Teachers.Add(teacher1);
            sis.Teachers.Add(teacher2);

            // Create Courses
            Course course1 = new Course(101, "Data Structures", "CS101", "Dr. John Smith");
            Course course2 = new Course(102, "Calculus", "MATH101", "Dr. Alice Brown");

            // Assign Teachers to Courses
            sis.AssignTeacherToCourse(teacher1, course1);
            sis.AssignTeacherToCourse(teacher2, course2);

            // Add courses to the system
            sis.Courses.Add(course1);
            sis.Courses.Add(course2);

            // Create Students
            Student student1 = new Student(1001, "Emily Davis", "1999-05-22", "emily.davis@example.com", "555-1234");
            Student student2 = new Student(1002, "Michael Johnson", "1998-07-10", "michael.johnson@example.com", "555-5678");

            // Add students to the system
            sis.Students.Add(student1);
            sis.Students.Add(student2);

            // Enroll Students in Courses
            try
            {
                sis.EnrollStudentInCourse(student1, course1);
                sis.EnrollStudentInCourse(student2, course2);
                sis.EnrollStudentInCourse(student1, course2);  // Student 1 also enrolls in a second course
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Record Payments
            try
            {
                sis.RecordPayment(student1, 500, DateTime.Now);
                sis.RecordPayment(student2, 300, DateTime.Now);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Generate Reports
            try
            {
                sis.GenerateEnrollmentReport(course1);
                sis.GenerateEnrollmentReport(course2);

                sis.GeneratePaymentReport(student1);
                sis.GeneratePaymentReport(student2);

                sis.CalculateCourseStatistics(course1);
                sis.CalculateCourseStatistics(course2);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Display Teacher Info
            Console.WriteLine("\nTeachers Information:");
            foreach (var teacher in sis.Teachers)
            {
                teacher.DisplayTeacherInfo();
            }

            // Display Student Info
            Console.WriteLine("\nStudents Information:");
            foreach (var student in sis.Students)
            {
                student.DisplayStudentInfo();
            }
        }
    }
}
