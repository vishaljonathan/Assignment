using DigitalAssetManagement.Entity;

namespace DigitalAssetManagement.Dao
{
    public interface IAssetManagementService
    {
        // Asset CRUD
        bool AddAsset(Asset asset);
        bool UpdateAsset(Asset asset);
        bool DeleteAsset(int assetId);

        // Allocation
        bool AllocateAsset(int assetId, int employeeId, string allocationDate);
        bool DeallocateAsset(int assetId, int employeeId, string returnDate);

        // Maintenance
        bool PerformMaintenance(int assetId, string maintenanceDate, string description, double cost);

        // Reservation
        bool ReserveAsset(int assetId, int employeeId, string reservationDate, string startDate, string endDate);
        bool WithdrawReservation(int reservationId);
    }
}
