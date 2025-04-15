using DigitalAssetManagementApplication.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DigitalAssetManagementApplication.MyExceptions.Exceptions;

namespace DigitalAssetManagementApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            bool flag = true;
            while (flag)
            {
                Console.Clear();
                Console.WriteLine("Digital Asset Management System");
                Console.WriteLine("1. Add Asset");
                Console.WriteLine("2. Delete Asset");
                Console.WriteLine("3. Update Asset");
                Console.WriteLine("4. Allocate Asset");
                Console.WriteLine("5. Deallocate Asset");
                Console.WriteLine("6. Perform Maintenance");
                Console.WriteLine("7. Reserve Asset");
                Console.WriteLine("8. Withdraw Reservation");
                Console.WriteLine("9. Exit");

                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        try
                        {
                            // Add Asset logic
                            Console.Write("Enter Asset ID: ");
                            int assetId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Asset Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter Asset Type: ");
                            string type = Console.ReadLine();
                            Console.Write("Enter Asset Serial Number: ");
                            string serialNumber = Console.ReadLine();
                            Console.Write("Enter Asset Purchase Date (yyyy-MM-dd): ");
                            DateTime purchaseDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter Asset Location: ");
                            string location = Console.ReadLine();
                            Console.Write("Enter Asset Status: ");
                            string status = Console.ReadLine();
                            Console.Write("Enter Owner Employee ID: ");
                            int ownerId = Convert.ToInt32(Console.ReadLine());

                            // Create Employee object (mock for simplicity)
                            Employees owner = new Employees(ownerId, "Employee Name", "Department", "email@example.com", "password");

                            // Add the asset
                            Assets newAsset = new Assets(assetId, name, type, serialNumber, purchaseDate, location, status, owner);
                            Assets.AddAsset(newAsset);

                            Console.WriteLine("Asset added successfully.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        Console.WriteLine("Press Enter to return to the menu.");
                        Console.ReadLine();
                        break;

                    case "2":
                        try
                        {
                            // Delete Asset logic
                            Console.Write("Enter Asset ID to delete: ");
                            int assetIdToDelete = Convert.ToInt32(Console.ReadLine());

                            Assets.DeleteAsset(assetIdToDelete);
                            Console.WriteLine("Asset deleted successfully.");
                        }
                        catch (AssetNotFoundException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        Console.WriteLine("Press Enter to return to the menu.");
                        Console.ReadLine();
                        break;

                    case "3":
                        try
                        {
                            // Update Asset logic
                            Console.Write("Enter Asset ID to update: ");
                            int assetIdToUpdate = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter New Asset Name: ");
                            string newName = Console.ReadLine();
                            Console.Write("Enter New Asset Type: ");
                            string newType = Console.ReadLine();
                            Console.Write("Enter New Asset Location: ");
                            string newLocation = Console.ReadLine();
                            Console.Write("Enter New Asset Status: ");
                            string newStatus = Console.ReadLine();

                            Assets.UpdateAsset(assetIdToUpdate, newName, newType, newLocation, newStatus);
                            Console.WriteLine("Asset updated successfully.");
                        }
                        catch (AssetNotFoundException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        Console.WriteLine("Press Enter to return to the menu.");
                        Console.ReadLine();
                        break;

                    case "4":
                        try
                        {
                            // Allocate Asset logic
                            Console.Write("Enter Asset ID to allocate: ");
                            int assetIdToAllocate = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Employee ID to allocate to: ");
                            int employeeIdToAllocate = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Allocation Date (yyyy-MM-dd): ");
                            DateTime allocationDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter Return Date (yyyy-MM-dd): ");
                            DateTime returnDate = DateTime.Parse(Console.ReadLine());

                            // Find asset and employee
                            var asset = Assets.AssetList.FirstOrDefault(a => a.AssetID == assetIdToAllocate);
                            var employee = new Employees(employeeIdToAllocate, "Employee Name", "Department", "email@example.com", "password");

                            if (asset == null)
                                throw new AssetNotFoundException("Asset not found.");

                            // Perform allocation
                            AssetAllocations allocation = new AssetAllocations(1, asset, employee, allocationDate, returnDate); // Example AllocationID
                            asset.Allocations.Add(allocation);

                            Console.WriteLine("Asset allocated successfully.");
                        }
                        catch (AssetNotFoundException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        Console.WriteLine("Press Enter to return to the menu.");
                        Console.ReadLine();
                        break;

                    case "5":
                        try
                        {
                            // Deallocate Asset logic
                            Console.Write("Enter Asset ID to deallocate: ");
                            int assetIdToDeallocate = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Employee ID: ");
                            int employeeIdToDeallocate = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Return Date (yyyy-MM-dd): ");
                            DateTime returnDate = DateTime.Parse(Console.ReadLine());

                            // Find asset
                            var asset = Assets.AssetList.FirstOrDefault(a => a.AssetID == assetIdToDeallocate);

                            if (asset == null)
                                throw new AssetNotFoundException("Asset not found.");

                            // Perform deallocation
                            var allocation = asset.Allocations.FirstOrDefault(a => a.EmployeeId.EmployeeID == employeeIdToDeallocate && a.ReturnDate == returnDate);

                            if (allocation != null)
                            {
                                asset.Allocations.Remove(allocation);
                                Console.WriteLine("Asset deallocated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Allocation not found.");
                            }
                        }
                        catch (AssetNotFoundException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        Console.WriteLine("Press Enter to return to the menu.");
                        Console.ReadLine();
                        break;

                    case "6":
                        try
                        {
                            // Perform Maintenance logic
                            Console.Write("Enter Asset ID to perform maintenance: ");
                            int assetIdForMaintenance = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Maintenance Date (yyyy-MM-dd): ");
                            DateTime maintenanceDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter Description of Maintenance: ");
                            string description = Console.ReadLine();
                            Console.Write("Enter Cost of Maintenance: ");
                            decimal cost = Convert.ToDecimal(Console.ReadLine());

                            // Find asset
                            var asset = Assets.AssetList.FirstOrDefault(a => a.AssetID == assetIdForMaintenance);

                            if (asset == null)
                                throw new AssetNotFoundException("Asset not found.");

                            // Perform maintenance
                            MaintenanceRecords record = new MaintenanceRecords(1, asset, maintenanceDate, description, cost); // Example MaintenanceID
                            asset.MaintenanceRecords.Add(record);

                            Console.WriteLine("Maintenance performed successfully.");
                        }
                        catch (AssetNotFoundException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        Console.WriteLine("Press Enter to return to the menu.");
                        Console.ReadLine();
                        break;

                    case "7":
                        try
                        {
                            // Reserve Asset logic
                            Console.Write("Enter Asset ID to reserve: ");
                            int assetIdToReserve = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Employee ID to reserve for: ");
                            int employeeIdForReservation = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Reservation Date (yyyy-MM-dd): ");
                            DateTime reservationDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter Start Date (yyyy-MM-dd): ");
                            DateTime startDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter End Date (yyyy-MM-dd): ");
                            DateTime endDate = DateTime.Parse(Console.ReadLine());

                            // Find asset and employee
                            var asset = Assets.AssetList.FirstOrDefault(a => a.AssetID == assetIdToReserve);

                            if (asset == null)
                                throw new AssetNotFoundException("Asset not found.");

                            // Perform reservation
                            Reservations reservation = new Reservations(1, asset, new Employees(employeeIdForReservation, "Employee Name", "Department", "email@example.com", "password"), reservationDate, startDate, endDate, "Reserved");
                            asset.Reservations.Add(reservation);

                            Console.WriteLine("Asset reserved successfully.");
                        }
                        catch (AssetNotFoundException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        Console.WriteLine("Press Enter to return to the menu.");
                        Console.ReadLine();
                        break;

                    case "8":
                        try
                        {
                            // Withdraw Reservation logic
                            Console.Write("Enter Reservation ID to withdraw: ");
                            int reservationIdToWithdraw = Convert.ToInt32(Console.ReadLine());

                            // Find the asset that has this reservation
                            var asset = Assets.AssetList.FirstOrDefault(a => a.Reservations.Any(r => r.ReservationID == reservationIdToWithdraw));

                            if (asset == null)
                            {
                                // If no asset with the given reservation ID is found
                                throw new AssetNotFoundException("Asset with the given reservation not found.");
                            }

                            // Retrieve the specific reservation to withdraw
                            var reservation = asset.Reservations.FirstOrDefault(r => r.ReservationID == reservationIdToWithdraw);

                            if (reservation == null)
                            {
                                throw new Exception("Reservation not found.");
                            }

                            // Remove the reservation
                            asset.Reservations.Remove(reservation);

                            Console.WriteLine("Reservation withdrawn successfully.");
                        }
                        catch (AssetNotFoundException ex)
                        {
                            // Handle case where the asset or reservation isn't found
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            // General exception handling
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        Console.WriteLine("Press Enter to return to the menu.");
                        Console.ReadLine();
                        break;

                    case "9":
                        flag = false;
                        Console.WriteLine("Exiting the window...");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
