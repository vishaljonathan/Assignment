using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Entity
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public Student Studentid { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        //Implement Constructors
        public Payment(int Id, Student S_Id, decimal amnt, DateTime payment_date)
        {
            PaymentID = Id;
            Studentid = S_Id;
            Amount = amnt;
            PaymentDate = payment_date;
        }
    }
}
