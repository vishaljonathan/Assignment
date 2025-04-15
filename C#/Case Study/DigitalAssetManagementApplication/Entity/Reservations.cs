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

        // Method to reserve an asset for an employee
        public static void ReserveAsset(Assets asset, Employees employee, DateTime reservationDate, DateTime startDate, DateTime endDate)
        {
            if (asset.Status != "Available")
            {
                throw new InvalidOperationException("Asset is not available for reservation.");
            }

            var reservation = new Reservations(
                reservationId: new Random().Next(1000, 9999),
                assetId: asset,
                employeeId: employee,
                reservationDate: reservationDate,
                startDate: startDate,
                endDate: endDate,
                status: "Reserved"
            );

            asset.Reservations.Add(reservation);
            asset.Status = "Reserved";
        }

        // Method to withdraw a reservation
        public static void WithdrawReservation(Assets asset, int reservationId)
        {
            var reservation = asset.Reservations.FirstOrDefault(r => r.ReservationID == reservationId);
            if (reservation != null)
            {
                asset.Reservations.Remove(reservation);
                asset.Status = "Available";
            }
            else
            {
                throw new InvalidOperationException("Reservation not found.");
            }
        }
    }
}
