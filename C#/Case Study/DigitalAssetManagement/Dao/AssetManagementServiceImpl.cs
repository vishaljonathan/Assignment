using DigitalAssetManagement.Entity;
using DigitalAssetManagement.Util;
using Microsoft.Data.SqlClient;
using static DigitalAssetManagement.MyExceptions.Exception;

namespace DigitalAssetManagement.Dao
{
    public class AssetManagementServiceImpl : IAssetManagementService
    {
        private SqlConnection connection;

        public AssetManagementServiceImpl(string configFile)
        {
            connection = DBConnUtil.GetConnection(configFile);
            connection.Open();
        }

        public bool AddAsset(Asset asset)
        {
            try
            {
                // Check if the OwnerId exists in the database before attempting to insert
                string checkOwnerQuery = "SELECT COUNT(*) FROM Employees WHERE employee_id = @OwnerId";
                using (SqlCommand cmd = new SqlCommand(checkOwnerQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@OwnerId", asset.OwnerId);

                    int ownerCount = (int)cmd.ExecuteScalar();
                    if (ownerCount == 0)
                    {
                        throw new AssetNotFoundException("Asset could not be added due to invalid OwnerId.");
                    }
                }

                // If the OwnerId is valid, proceed to add the asset
                string query = "INSERT INTO Assets (name, type, serial_number, purchase_date, location, status, owner_id) " +
                               "VALUES (@Name, @Type, @SerialNumber, @PurchaseDate, @Location, @Status, @OwnerId)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", asset.Name);
                    cmd.Parameters.AddWithValue("@Type", asset.Type);
                    cmd.Parameters.AddWithValue("@SerialNumber", asset.SerialNumber);
                    cmd.Parameters.AddWithValue("@PurchaseDate", asset.PurchaseDate);
                    cmd.Parameters.AddWithValue("@Location", asset.Location);
                    cmd.Parameters.AddWithValue("@Status", asset.Status);
                    cmd.Parameters.AddWithValue("@OwnerId", asset.OwnerId);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Returns true if at least one row is affected
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL error adding asset: {ex.Message}");
                return false;
            }
            catch (AssetNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General error adding asset: {ex.Message}");
                return false;
            }
        }


        public bool UpdateAsset(Asset asset)
        {
            try
            {
                string query = "UPDATE Assets SET Name = @Name, Type = @Type, SerialNumber = @SerialNumber, " +
                               "PurchaseDate = @PurchaseDate, Location = @Location, Status = @Status, OwnerId = @OwnerId " +
                               "WHERE asset_id = @AssetId";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@AssetId", asset.AssetId);
                    cmd.Parameters.AddWithValue("@Name", asset.Name);
                    cmd.Parameters.AddWithValue("@Type", asset.Type);
                    cmd.Parameters.AddWithValue("@SerialNumber", asset.SerialNumber);
                    cmd.Parameters.AddWithValue("@PurchaseDate", asset.PurchaseDate);
                    cmd.Parameters.AddWithValue("@Location", asset.Location);
                    cmd.Parameters.AddWithValue("@Status", asset.Status);
                    cmd.Parameters.AddWithValue("@OwnerId", asset.OwnerId);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Returns true if the update was successful
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating asset: {ex.Message}");
                return false;
            }
        }

        public bool DeleteAsset(int assetId)
        {
            try
            {
                string query = "DELETE FROM Assets WHERE asset_id = @AssetId";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@AssetId", assetId);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Returns true if the asset was deleted
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting asset: {ex.Message}");
                return false;
            }
        }

        public bool AllocateAsset(int assetId, int employeeId, string allocationDate)
        {
            try
            {
                string query = "INSERT INTO asset_allocation (Asset_Id, Employee_Id, allocation_date) " +
                               "VALUES (@AssetId, @EmployeeId, @AllocationDate)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@AssetId", assetId);
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmd.Parameters.AddWithValue("@AllocationDate", allocationDate);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Returns true if allocation was successful
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error allocating asset: {ex.Message}");
                return false;
            }
        }

        public bool DeallocateAsset(int assetId, int employeeId, string returnDate)
        {
            try
            {
                string query = "UPDATE asset_allocation SET return_date = @ReturnDate " +
                               "WHERE Asset_id = @AssetId AND Employee_ID = @EmployeeId AND return_date IS NULL";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@AssetId", assetId);
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmd.Parameters.AddWithValue("@ReturnDate", returnDate);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Returns true if deallocation was successful
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deallocating asset: {ex.Message}");
                return false;
            }
        }

        public bool PerformMaintenance(int assetId, string maintenanceDate, string description, double cost)
        {
            try
            {
                string query = "INSERT INTO maintenance_record (Asset_ID, maintenance_date, description, cost) " +
                               "VALUES (@AssetId, @MaintenanceDate, @Description, @Cost)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@AssetId", assetId);
                    cmd.Parameters.AddWithValue("@MaintenanceDate", maintenanceDate);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Cost", cost);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Returns true if maintenance was added successfully
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error performing maintenance: {ex.Message}");
                return false;
            }
        }

        public bool ReserveAsset(int assetId, int employeeId, string reservationDate, string startDate, string endDate)
        {
            try
            {
                string query = "INSERT INTO reservations (Asset_iD, Employee_id, reservation_date, start_date, end_date) " +
                               "VALUES (@AssetId, @EmployeeId, @ReservationDate, @StartDate, @EndDate)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@AssetId", assetId);
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmd.Parameters.AddWithValue("@ReservationDate", reservationDate);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Returns true if the reservation was successful
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reserving asset: {ex.Message}");
                return false;
            }
        }

        public bool WithdrawReservation(int reservationId)
        {
            try
            {
                string query = "DELETE FROM reservations WHERE reservation_id = @ReservationId";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ReservationId", reservationId);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Returns true if the reservation was withdrawn
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error withdrawing reservation: {ex.Message}");
                return false;
            }
        }
    }

}
