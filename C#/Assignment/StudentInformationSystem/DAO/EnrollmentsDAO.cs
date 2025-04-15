using Microsoft.Data.SqlClient;
using StudentInformationSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StudentInformationSystem.Exception.Exceptions;

namespace StudentInformationSystem.DAO
{
    public class EnrollmentsDAO
    {
        private readonly string _connectionString;

        public EnrollmentsDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Add a new enrollment
        public void AddEnrollment(Enrollment enrollment)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Enrollments (StudentID, CourseID, EnrollmentDate)
                                 VALUES (@StudentID, @CourseID, @EnrollmentDate);";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", enrollment.StudentId);
                    cmd.Parameters.AddWithValue("@CourseID", enrollment.CourseId);
                    cmd.Parameters.AddWithValue("@EnrollmentDate", enrollment.EnrollmentDate);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new InvalidEnrollmentDataException("Failed to add enrollment.");
                }
            }
        }

        // Get all enrollments
        public List<Enrollment> GetAllEnrollments()
        {
            List<Enrollment> enrollments = new List<Enrollment>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Enrollments";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        enrollments.Add(new Enrollment
                        {
                            EnrollmentId = Convert.ToInt32(reader["EnrollmentID"]),
                            StudentId = Convert.ToInt32(reader["StudentID"]),
                            CourseId = Convert.ToInt32(reader["CourseID"]),
                            EnrollmentDate = Convert.ToDateTime(reader["EnrollmentDate"])
                        });
                    }
                }
            }

            return enrollments;
        }

        // Method to generate enrollment report for a specific course
        public List<Student> GetEnrollmentReport(string courseName)
        {
            List<Student> enrolledStudents = new List<Student>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    // Query to get students enrolled in the specified course
                    string query = @"
                        SELECT s.StudentID, s.FirstName, s.LastName, s.Email, s.PhoneNumber
                        FROM Students s
                        JOIN Enrollments e ON s.StudentID = e.StudentID
                        JOIN Courses c ON e.CourseID = c.CourseID
                        WHERE c.CourseName = @CourseName";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CourseName", courseName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Student student = new Student
                            {
                                StudentId = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Email = reader.GetString(3),
                                PhoneNumber = reader.GetString(4)
                            };

                            enrolledStudents.Add(student);
                        }
                    }

                    return enrolledStudents;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Error generating enrollment report: {ex.Message}");
                    return null;
                }
            }
        }

        // Method to generate and display the enrollment report
        public void GenerateAndDisplayReport(string courseName)
        {
            List<Student> students = GetEnrollmentReport(courseName);

            if (students == null || students.Count == 0)
            {
                Console.WriteLine($"No students enrolled in {courseName}.");
                return;
            }

            Console.WriteLine($"Enrollment Report for {courseName}:");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("StudentID | FirstName | LastName | Email | PhoneNumber");
            Console.WriteLine("--------------------------------------------------");

            foreach (var student in students)
            {
                Console.WriteLine($"{student.StudentId} | {student.FirstName} | {student.LastName} | {student.Email} | {student.PhoneNumber}");
            }
        }

        //Update Enrollment
        public void UpdateEnrollment(int enrollmentId, DateTime newDate)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"UPDATE Enrollments 
                                 SET EnrollmentDate = @EnrollmentDate 
                                 WHERE EnrollmentID = @EnrollmentID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EnrollmentDate", newDate);
                    cmd.Parameters.AddWithValue("@EnrollmentID", enrollmentId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new InvalidEnrollmentDataException("No such enrollment found to update.");
                }
            }
        }
    }
}
