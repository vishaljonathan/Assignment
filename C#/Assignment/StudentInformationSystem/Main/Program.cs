using StudentInformationSystem.Entity;
using System;
using System.Linq;
using static StudentInformationSystem.Exception.Exceptions;

namespace StudentInformationSystem.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            SIS sis = new SIS();
            bool flag = true;

            while (flag)
            {
                Console.WriteLine("\n--- Student Information System Menu ---");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Add Course");
                Console.WriteLine("3. Add Teacher");
                Console.WriteLine("4. Enroll Student in Course");
                Console.WriteLine("5. Assign Course to Teacher");
                Console.WriteLine("6. Add Payment for Student");
                Console.WriteLine("7. View Enrollments for Student");
                Console.WriteLine("8. View Courses for Teacher");
                Console.WriteLine("9. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter Student ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int sid))
                            {
                                Console.WriteLine("Invalid Student ID. Please enter a valid number.");
                                break;
                            }
                            Console.Write("Enter First Name: ");
                            string firstName = Console.ReadLine();
                            Console.Write("Enter Last Name: ");
                            string lastName = Console.ReadLine();
                            Console.Write("Enter DOB (yyyy-mm-dd): ");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dob))
                            {
                                Console.WriteLine("Invalid Date of Birth format.");
                                break;
                            }
                            Console.Write("Enter Email: ");
                            string email = Console.ReadLine();
                            Console.Write("Enter Phone: ");
                            string phone = Console.ReadLine();

                            if (sis.Students.Any(s => s.StudentId == sid))
                            {
                                Console.WriteLine("Student with this ID already exists.");
                                break;
                            }

                            Student student = new Student(sid, firstName, lastName, dob, email, phone);
                            sis.Students.Add(student);
                            Console.WriteLine("Student added successfully.");
                            break;

                        case "2":
                            Console.Write("Enter Course ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int cid))
                            {
                                Console.WriteLine("Invalid Course ID.");
                                break;
                            }
                            Console.Write("Enter Course Name: ");
                            string cname = Console.ReadLine();
                            Console.Write("Enter Course Code: ");
                            string ccode = Console.ReadLine();
                            Console.Write("Enter Instructor Name: ");
                            string instructor = Console.ReadLine();

                            if (sis.Courses.Any(c => c.CourseId == cid))
                            {
                                Console.WriteLine("Course with this ID already exists.");
                                break;
                            }

                            Course course = new Course(cid, cname, ccode, instructor);
                            sis.Courses.Add(course);
                            Console.WriteLine("Course added successfully.");
                            break;

                        case "3":
                            Console.Write("Enter Teacher ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int tid))
                            {
                                Console.WriteLine("Invalid Teacher ID.");
                                break;
                            }
                            Console.Write("Enter First Name: ");
                            string tfname = Console.ReadLine();
                            Console.Write("Enter Last Name: ");
                            string tlname = Console.ReadLine();
                            Console.Write("Enter Email: ");
                            string temail = Console.ReadLine();

                            if (sis.Teachers.Any(t => t.TeacherId == tid))
                            {
                                Console.WriteLine("Teacher with this ID already exists.");
                                break;
                            }

                            Teacher teacher = new Teacher(tid, tfname, tlname, temail);
                            sis.Teachers.Add(teacher);
                            Console.WriteLine("Teacher added successfully.");
                            break;

                        case "4":
                            Console.Write("Enter Student ID: ");
                            if (!int.TryParse(Console.ReadLine(), out sid))
                            {
                                Console.WriteLine("Invalid Student ID.");
                                break;
                            }
                            Console.Write("Enter Course ID: ");
                            if (!int.TryParse(Console.ReadLine(), out cid))
                            {
                                Console.WriteLine("Invalid Course ID.");
                                break;
                            }
                            Console.Write("Enter Enrollment Date (yyyy-mm-dd): ");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime edate))
                            {
                                Console.WriteLine("Invalid Enrollment Date format.");
                                break;
                            }

                            student = sis.Students.Find(s => s.StudentId == sid) ?? throw new StudentNotFoundException("Student not found.");
                            course = sis.Courses.Find(c => c.CourseId == cid) ?? throw new CourseNotFoundException("Course not found.");
                            sis.AddEnrollment(student, course, edate);
                            Console.WriteLine("Enrollment added successfully.");
                            break;

                        case "5":
                            Console.Write("Enter Course ID: ");
                            if (!int.TryParse(Console.ReadLine(), out cid))
                            {
                                Console.WriteLine("Invalid Course ID.");
                                break;
                            }
                            Console.Write("Enter Teacher ID: ");
                            if (!int.TryParse(Console.ReadLine(), out tid))
                            {
                                Console.WriteLine("Invalid Teacher ID.");
                                break;
                            }

                            course = sis.Courses.Find(c => c.CourseId == cid) ?? throw new CourseNotFoundException("Course not found.");
                            teacher = sis.Teachers.Find(t => t.TeacherId == tid) ?? throw new TeacherNotFoundException("Teacher not found.");
                            sis.AssignCourseToTeacher(course, teacher);
                            Console.WriteLine("Course assigned to teacher.");
                            break;

                        case "6":
                            Console.Write("Enter Student ID: ");
                            if (!int.TryParse(Console.ReadLine(), out sid))
                            {
                                Console.WriteLine("Invalid Student ID.");
                                break;
                            }
                            Console.Write("Enter Payment Amount: ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                            {
                                Console.WriteLine("Invalid payment amount.");
                                break;
                            }
                            Console.Write("Enter Payment Date (yyyy-mm-dd): ");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime pdate))
                            {
                                Console.WriteLine("Invalid Payment Date format.");
                                break;
                            }

                            student = sis.Students.Find(s => s.StudentId == sid) ?? throw new StudentNotFoundException("Student not found.");
                            sis.AddPayment(student, (double) amount, pdate);
                            Console.WriteLine("Payment added successfully.");
                            break;

                        case "7":
                            Console.Write("Enter Student ID: ");
                            if (!int.TryParse(Console.ReadLine(), out sid))
                            {
                                Console.WriteLine("Invalid Student ID.");
                                break;
                            }
                            student = sis.Students.Find(s => s.StudentId == sid) ?? throw new StudentNotFoundException("Student not found.");
                            var enrollments = sis.GetEnrollmentsForStudent(student);
                            if (!enrollments.Any())
                            {
                                Console.WriteLine("No enrollments found for this student.");
                                break;
                            }
                            Console.WriteLine($"Enrollments for {student.FirstName} {student.LastName}:");
                            foreach (var e in enrollments)
                            {
                                Console.WriteLine($"- Course ID: {e.CourseId}, Enrollment Date: {e.EnrollmentDate.ToShortDateString()}");
                            }
                            break;

                        case "8":
                            Console.Write("Enter Teacher ID: ");
                            if (!int.TryParse(Console.ReadLine(), out tid))
                            {
                                Console.WriteLine("Invalid Teacher ID.");
                                break;
                            }
                            teacher = sis.Teachers.Find(t => t.TeacherId == tid) ?? throw new TeacherNotFoundException("Teacher not found.");
                            var courses = sis.GetCoursesForTeacher(teacher);
                            if (!courses.Any())
                            {
                                Console.WriteLine("No courses assigned to this teacher.");
                                break;
                            }
                            Console.WriteLine($"Courses for Teacher {teacher.FirstName} {teacher.LastName}:");
                            foreach (var c in courses)
                            {
                                Console.WriteLine($"- {c.CourseName}");
                            }
                            break;

                        case "9":
                            flag = false;
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Try again.");
                            break;
                    }
                }
                catch (StudentNotFoundException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (CourseNotFoundException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (TeacherNotFoundException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (DuplicateEnrollmentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }

                Console.WriteLine("Press Enter to return to the menu...");
                Console.ReadLine();
            }
        }
    }
}
