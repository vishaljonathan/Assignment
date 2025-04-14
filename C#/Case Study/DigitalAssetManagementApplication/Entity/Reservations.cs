using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalAssetManagementApplication.Entity
{
    public class Reservations
    {
        public int ReservationID { get; set; }
        public Assets assetid { get; set; }
        public Employees Employeeid { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public Reservations(int reservationId, Assets assetId, Employees employeeId, DateTime reservationDate, DateTime startDate, DateTime endDate, string status)
        {
            ReservationID = reservationId;
            assetid = assetId;
            Employeeid = employeeId;
            ReservationDate = reservationDate;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
        }
    }
}
