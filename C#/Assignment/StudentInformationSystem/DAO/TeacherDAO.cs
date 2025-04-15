using Microsoft.Data.SqlClient;
using StudentInformationSystem.Entity;
using StudentInformationSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StudentInformationSystem.Exception.Exceptions;

namespace StudentInformationSystem.DAO
{
    public class TeacherDAO
    {
        private readonly string connectionString;

        public TeacherDAO(string dbConfigPath)
        {
            connectionString = DBPropertyUtil.GetConnectionString(dbConfigPath);
        }

        // Task 9: Assign Teacher to a Course (with FirstName and LastName)
        public void AssignTeacherToCourse(string courseCode, Teacher teacher)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Insert teacher into Teacher table
                    string insertTeacherQuery = @"
                        INSERT INTO Teacher (FirstName, LastName, Email, Expertise)
                        OUTPUT INSERTED.TeacherID
                        VALUES (@FirstName, @LastName, @Email, @Expertise)";

                    int teacherId;
                    using (SqlCommand insertCmd = new SqlCommand(insertTeacherQuery, conn, transaction))
                    {
                        insertCmd.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                        insertCmd.Parameters.AddWithValue("@LastName", teacher.LastName);
                        insertCmd.Parameters.AddWithValue("@Email", teacher.Email);
                        insertCmd.Parameters.AddWithValue("@Expertise", teacher.Expertise);

                        teacherId = (int)insertCmd.ExecuteScalar();
                    }

                    // Update course table with the new TeacherID
                    string updateCourseQuery = @"
                        UPDATE Course
                        SET TeacherID = @TeacherID
                        WHERE CourseCode = @CourseCode";

                    using (SqlCommand updateCmd = new SqlCommand(updateCourseQuery, conn, transaction))
                    {
                        updateCmd.Parameters.AddWithValue("@TeacherID", teacherId);
                        updateCmd.Parameters.AddWithValue("@CourseCode", courseCode);

                        int rowsAffected = updateCmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new CourseNotFoundException($"Course with code {courseCode} not found.");
                        }
                    }

                    transaction.Commit();
                    Console.WriteLine($"Teacher '{teacher.FirstName} {teacher.LastName}' assigned to course '{courseCode}' successfully.");
                }
                catch (System.Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error assigning teacher to course: " + ex.Message);
                    throw;
                }
            }
        }

        // Retrieve all teachers
        public List<Teacher> GetAllTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Teacher";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        teachers.Add(new Teacher
                        {
                            TeacherId = Convert.ToInt32(reader["TeacherID"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Expertise = reader["Expertise"].ToString()
                        });
                    }
                }
            }

            return teachers;
        }

        // Get teacher by ID
        public Teacher GetTeacherById(int teacherId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Teacher WHERE TeacherID = @TeacherID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TeacherID", teacherId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Teacher
                            {
                                TeacherId = Convert.ToInt32(reader["TeacherID"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Expertise = reader["Expertise"].ToString()
                            };
                        }
                        else
                        {
                            throw new TeacherNotFoundException($"Teacher with ID {teacherId} not found.");
                        }
                    }
                }
            }
        }
    }
}

