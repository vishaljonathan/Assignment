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
    public class StudentsDAO
    {
        private readonly string connectionString;

        public StudentsDAO(string configFilePath)
        {
            connectionString = DBPropertyUtil.GetConnectionString(configFilePath);
        }

        public int AddStudent(Student student)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string insertStudent = @"INSERT INTO Students (FirstName, LastName, DateOfBirth, Email, PhoneNumber)
                                         OUTPUT INSERTED.StudentID
                                         VALUES (@FirstName, @LastName, @DateOfBirth, @Email, @PhoneNumber)";

                using (SqlCommand cmd = new SqlCommand(insertStudent, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", student.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Email", student.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);

                    int studentId = (int)cmd.ExecuteScalar();
                    return studentId;
                }
            }
        }

        public void EnrollStudentInCourses(int studentId, List<int> courseIds)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    foreach (int courseId in courseIds)
                    {
                        string insertEnrollment = @"INSERT INTO Enrollments (StudentID, CourseID, EnrollmentDate)
                                                    VALUES (@StudentID, @CourseID, @EnrollmentDate)";
                        using (SqlCommand cmd = new SqlCommand(insertEnrollment, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@StudentID", studentId);
                            cmd.Parameters.AddWithValue("@CourseID", courseId);
                            cmd.Parameters.AddWithValue("@EnrollmentDate", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch (System.Exception)
                {
                    transaction.Rollback();
                    throw new DuplicateEnrollmentException("Error enrolling student. Transaction rolled back.");
                }
            }
        }
        public int AddStudentAndEnroll(Student student, List<int> courseIds)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Insert student
                    string insertStudentQuery = @"
                INSERT INTO Students (FirstName, LastName, DateOfBirth, Email, PhoneNumber)
                VALUES (@FirstName, @LastName, @DateOfBirth, @Email, @PhoneNumber);
                SELECT SCOPE_IDENTITY();";

                    int studentId;
                    using (SqlCommand cmd = new SqlCommand(insertStudentQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", student.LastName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                        cmd.Parameters.AddWithValue("@Email", student.Email);
                        cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                        studentId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 2. Enroll in courses
                    foreach (int courseId in courseIds)
                    {
                        string enrollQuery = @"
                    INSERT INTO Enrollments (StudentID, CourseID, EnrollmentDate)
                    VALUES (@StudentID, @CourseID, @EnrollmentDate);";

                        using (SqlCommand cmd = new SqlCommand(enrollQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@StudentID", studentId);
                            cmd.Parameters.AddWithValue("@CourseID", courseId);
                            cmd.Parameters.AddWithValue("@EnrollmentDate", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // 3. Commit transaction
                    transaction.Commit();
                    return studentId;
                }
                catch (System.Exception ex)
                {
                    transaction.Rollback();
                    throw new System.Exception("Error during AddStudentAndEnroll: " + ex.Message);
                }
            }
        }
    }
}
