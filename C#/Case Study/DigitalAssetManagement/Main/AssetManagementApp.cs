using DigitalAssetManagement.Dao;
using DigitalAssetManagement.Entity;
using Microsoft.Data.SqlClient;
using static DigitalAssetManagement.MyExceptions.Exception;

namespace DigitalAssetManagement.Main
{
    public class AssetManagementApp
    {
        static void Main(string[] args)
        {
            string configFile = "AppSettings.json";
            IAssetManagementService service = new AssetManagementServiceImpl(configFile);

            while (true)
            {
                try
                {
                    Console.WriteLine("\n--- Digital Asset Management System ---");
                    Console.WriteLine("Welcome! Select a choice from the below menu:");
                    Console.WriteLine("1. Add Asset");
                    Console.WriteLine("2. Update Asset");
                    Console.WriteLine("3. Delete Asset");
                    Console.WriteLine("4. Allocate Asset");
                    Console.WriteLine("5. Deallocate Asset");
                    Console.WriteLine("6. Perform Maintenance");
                    Console.WriteLine("7. Reserve Asset");
                    Console.WriteLine("8. Withdraw Reservation");
                    Console.WriteLine("9. Exit");

                    Console.Write("Enter your choice: ");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            AddAsset(service);
                            break;

                        case 2:
                            UpdateAsset(service);
                            break;

                        case 3:
                            DeleteAsset(service);
                            break;

                        case 4:
                            AllocateAsset(service);
                            break;

                        case 5:
                            DeallocateAsset(service);
                            break;

                        case 6:
                            PerformMaintenance(service);
                            break;

                        case 7:
                            ReserveAsset(service);
                            break;

                        case 8:
                            WithdrawReservation(service);
                            break;

                        case 9:
                            Console.WriteLine("Exiting... Thank you!");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (AssetNotFoundException ex)
                {
                    Console.WriteLine("Asset Error: " + ex.Message);
                }
                catch (AssetNotMaintainException ex)
                {
                    Console.WriteLine("Maintenance Error: " + ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input format incorrect. Please try again.");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Database Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected Error: " + ex.Message);
                }
            }
        }

        private static void AddAsset(IAssetManagementService service)
        {
            Asset newAsset = new Asset();
            Console.Write("Enter Asset Name: ");
            newAsset.Name = Console.ReadLine();
            Console.Write("Enter Asset Type: ");
            newAsset.Type = Console.ReadLine();
            Console.Write("Enter Serial Number: ");
            newAsset.SerialNumber = Console.ReadLine();
            Console.Write("Enter Purchase Date (yyyy-MM-dd): ");
            newAsset.PurchaseDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter Location: ");
            newAsset.Location = Console.ReadLine();
            Console.Write("Enter Status: ");
            newAsset.Status = Console.ReadLine();
            Console.Write("Enter Owner ID: ");
            newAsset.OwnerId = Convert.ToInt32(Console.ReadLine());

            if (service.AddAsset(newAsset))
                Console.WriteLine("Asset added successfully.");
            else
                throw new Exception("Asset could not be added.");
        }

        private static void UpdateAsset(IAssetManagementService service)
        {
            Asset updateAsset = new Asset();
            Console.Write("Enter Asset ID to update: ");
            updateAsset.AssetId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter New Asset Name: ");
            updateAsset.Name = Console.ReadLine();
            Console.Write("Enter New Type: ");
            updateAsset.Type = Console.ReadLine();
            Console.Write("Enter New Serial Number: ");
            updateAsset.SerialNumber = Console.ReadLine();
            Console.Write("Enter New Purchase Date (yyyy-MM-dd): ");
            updateAsset.PurchaseDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter New Location: ");
            updateAsset.Location = Console.ReadLine();
            Console.Write("Enter New Status: ");
            updateAsset.Status = Console.ReadLine();
            Console.Write("Enter New Owner ID: ");
            updateAsset.OwnerId = Convert.ToInt32(Console.ReadLine());

            if (service.UpdateAsset(updateAsset))
                Console.WriteLine("Asset updated successfully.");
            else
                throw new AssetNotFoundException();
        }

        private static void DeleteAsset(IAssetManagementService service)
        {
            Console.Write("Enter Asset ID to delete: ");
            int deleteId = Convert.ToInt32(Console.ReadLine());
            if (service.DeleteAsset(deleteId))
                Console.WriteLine("Asset deleted successfully.");
            else
                throw new AssetNotFoundException();
        }

        private static void AllocateAsset(IAssetManagementService service)
        {
            AssetAllocation allocation = new AssetAllocation();
            Console.Write("Enter Asset ID: ");
            allocation.AssetId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Employee ID: ");
            allocation.EmployeeId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Allocation Date (yyyy-MM-dd): ");
            allocation.AllocationDate = DateTime.Parse(Console.ReadLine());

            string allocationDate = allocation.AllocationDate.ToString("yyyy-MM-dd");
            if (service.AllocateAsset(allocation.AssetId, allocation.EmployeeId, allocationDate))
                Console.WriteLine("Asset allocated successfully.");
            else
                throw new AssetNotFoundException();
        }

        private static void DeallocateAsset(IAssetManagementService service)
        {
            AssetAllocation deallocation = new AssetAllocation();
            Console.Write("Enter Asset ID: ");
            deallocation.AssetId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Employee ID: ");
            deallocation.EmployeeId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Return Date (yyyy-MM-dd): ");
            deallocation.ReturnDate = DateTime.Parse(Console.ReadLine());

            string returnDate = deallocation.ReturnDate?.ToString("yyyy-MM-dd");
            if (service.DeallocateAsset(deallocation.AssetId, deallocation.EmployeeId, returnDate))
                Console.WriteLine("Asset deallocated successfully.");
            else
                throw new AssetNotFoundException();
        }

        private static void PerformMaintenance(IAssetManagementService service)
        {
            MaintenanceRecord maintenance = new MaintenanceRecord();
            Console.Write("Enter Asset ID: ");
            maintenance.AssetId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Maintenance Date (yyyy-MM-dd): ");
            maintenance.MaintenanceDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter Maintenance Description: ");
            maintenance.Description = Console.ReadLine();
            Console.Write("Enter Maintenance Cost: ");
            maintenance.Cost = Convert.ToDouble(Console.ReadLine());

            string maintenanceDate = maintenance.MaintenanceDate.ToString("yyyy-MM-dd");
            if (service.PerformMaintenance(maintenance.AssetId, maintenanceDate, maintenance.Description, maintenance.Cost))
                Console.WriteLine("Maintenance added successfully.");
            else
                throw new AssetNotFoundException();
        }

        private static void ReserveAsset(IAssetManagementService service)
        {
            Reservation reservation = new Reservation();
            Console.Write("Enter Asset ID: ");
            reservation.AssetId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Employee ID: ");
            reservation.EmployeeId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Reservation Date (yyyy-MM-dd): ");
            string reservationDate = Console.ReadLine();

            Console.Write("Enter Start Date (yyyy-MM-dd): ");
            string startDate = Console.ReadLine();

            Console.Write("Enter End Date (yyyy-MM-dd): ");
            string endDate = Console.ReadLine();

            if (service.ReserveAsset(reservation.AssetId, reservation.EmployeeId, reservationDate, startDate, endDate))
                Console.WriteLine("Asset reserved successfully.");
            else
                throw new AssetNotMaintainException();
        }

        private static void WithdrawReservation(IAssetManagementService service)
        {
            Console.Write("Enter Reservation ID to withdraw: ");
            int reservationId = Convert.ToInt32(Console.ReadLine());

            if (service.WithdrawReservation(reservationId))
                Console.WriteLine("Reservation withdrawn successfully.");
            else
                throw new Exception("Reservation not found.");
        }
    }
}
