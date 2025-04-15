using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int StudentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public Student Student { get; set; }

        // Constructor to initialize attributes
        public Payment(int paymentId, int studentId, decimal amount, DateTime paymentDate)
        {
            PaymentId = paymentId;
            StudentId = studentId;
            Amount = amount;
            PaymentDate = paymentDate;
        }
        public Payment(Student student)
        {
            Student = student;
        }

        // Methods
        public Student GetStudent()
        {
            return new Student(StudentId, "John", "Doe", DateTime.Now.AddYears(-20), "john.doe@mail.com", "1234567890");
        }
        public decimal GetPaymentAmount()
        {
            return Amount;
        }
        public DateTime GetPaymentDate()
        {
            return PaymentDate;
        }
    }
}
