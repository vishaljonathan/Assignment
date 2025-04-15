using Microsoft.Data.SqlClient;
using StudentInformationSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.DAO
{
    public class CoursesDAO
    {
        private readonly string _connectionString;

        public CoursesDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Method to retrieve a course by its code
        public Course GetCourseByCode(string courseCode)
        {
            Course course = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    string query = "SELECT CourseID, CourseName, CourseCode, InstructorName FROM Courses WHERE CourseCode = @CourseCode";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CourseCode", courseCode);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            course = new Course
                            {
                                CourseId = reader.GetInt32(0),
                                CourseName = reader.GetString(1),
                                CourseCode = reader.GetString(2),
                                InstructorName = reader.GetString(3)
                            };
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Error fetching course details: {ex.Message}");
                }
            }

            return course;
        }

        // Method to retrieve all courses
        public List<Course> GetAllCourses()
        {
            List<Course> courses = new List<Course>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    string query = "SELECT CourseID, CourseName, CourseCode, InstructorName FROM Courses";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Course course = new Course
                            {
                                CourseId = reader.GetInt32(0),
                                CourseName = reader.GetString(1),
                                CourseCode = reader.GetString(2),
                                InstructorName = reader.GetString(3)
                            };
                            courses.Add(course);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Error fetching courses: {ex.Message}");
                }
            }

            return courses;
        }

        // Method to add a new course to the database
        public void AddCourse(Course course)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    string query = "INSERT INTO Courses (CourseName, CourseCode, InstructorName) VALUES (@CourseName, @CourseCode, @InstructorName)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CourseName", course.CourseName);
                    command.Parameters.AddWithValue("@CourseCode", course.CourseCode);
                    command.Parameters.AddWithValue("@InstructorName", course.InstructorName);

                    int result = command.ExecuteNonQuery();
                    Console.WriteLine(result > 0 ? "Course added successfully." : "Failed to add the course.");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Error adding course: {ex.Message}");
                }
            }
        }

        // Method to update course details
        public void UpdateCourse(Course course)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    string query = "UPDATE Courses SET CourseName = @CourseName, InstructorName = @InstructorName WHERE CourseCode = @CourseCode";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CourseName", course.CourseName);
                    command.Parameters.AddWithValue("@InstructorName", course.InstructorName);
                    command.Parameters.AddWithValue("@CourseCode", course.CourseCode);

                    int result = command.ExecuteNonQuery();
                    Console.WriteLine(result > 0 ? "Course updated successfully." : "Failed to update the course.");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Error updating course: {ex.Message}");
                }
            }
        }

        // Method to delete a course from the database
        public void DeleteCourse(string courseCode)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    string query = "DELETE FROM Courses WHERE CourseCode = @CourseCode";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CourseCode", courseCode);

                    int result = command.ExecuteNonQuery();
                    Console.WriteLine(result > 0 ? "Course deleted successfully." : "Failed to delete the course.");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Error deleting course: {ex.Message}");
                }
            }
        }
    }
}
