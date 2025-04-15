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
    public class PaymentsDAO
    {
        private readonly string _connectionString;

        public PaymentsDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void RecordPayment(Payment payment)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // 1. Validate student exists
                    SqlCommand checkStudent = new SqlCommand("SELECT COUNT(*) FROM Students WHERE StudentID = @StudentID", connection, transaction);
                    checkStudent.Parameters.AddWithValue("@StudentID", payment.Student);
                    int count = (int)checkStudent.ExecuteScalar();

                    if (count == 0)
                    {
                        throw new StudentNotFoundException($"Student with ID {payment.Student} not found.");
                    }

                    // 2. Insert payment
                    SqlCommand insertPayment = new SqlCommand(
                        "INSERT INTO Payments (StudentID, Amount, PaymentDate) VALUES (@StudentID, @Amount, @PaymentDate)", connection, transaction);
                    insertPayment.Parameters.AddWithValue("@StudentID", payment.Student);
                    insertPayment.Parameters.AddWithValue("@Amount", payment.Amount);
                    insertPayment.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                    insertPayment.ExecuteNonQuery();

                    // 3. Update student's balance (assuming 'Balance' column in Students table)
                    SqlCommand updateBalance = new SqlCommand(
                        "UPDATE Students SET Balance = Balance - @Amount WHERE StudentID = @StudentID", connection, transaction);
                    updateBalance.Parameters.AddWithValue("@Amount", payment.Amount);
                    updateBalance.Parameters.AddWithValue("@StudentID", payment.Student);
                    updateBalance.ExecuteNonQuery();

                    transaction.Commit();
                    Console.WriteLine("Payment recorded and student balance updated successfully.");
                }
                catch (System.Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error recording payment: {ex.Message}");
                }
            }
        }

    }
}
