using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    class Payment
    {
        public long PaymentID { get; set; }
        public Student PayingStudent { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        
        public Payment(long payment, Student student, decimal amount, DateTime date)
        {
            PaymentID = payment;
            PayingStudent = student;
            Amount = amount;
            PaymentDate = date;
        }

        public Student GetStudent()
        {
            return PayingStudent;
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
