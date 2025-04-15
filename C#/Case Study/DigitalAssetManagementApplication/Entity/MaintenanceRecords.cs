using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalAssetManagementApplication.Entity
{
    public class MaintenanceRecords
    {
        public int MaintenanceID { get; set; }
        public Assets AssetId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public MaintenanceRecords(int maintenanceId, Assets assetId, DateTime maintenanceDate, string description, decimal cost)
        {
            MaintenanceID = maintenanceId;
            AssetId = assetId;
            MaintenanceDate = maintenanceDate;
            Description = description;
            Cost = cost;
        }
        
        // Method to perform maintenance on an asset
        public static void PerformMaintenance(Assets asset, DateTime maintenanceDate, string description, decimal cost)
        {
            var maintenance = new MaintenanceRecords(
                maintenanceId: new Random().Next(1000, 9999),
                assetId: asset,
                maintenanceDate: maintenanceDate,
                description: description,
                cost: cost
            );

            asset.MaintenanceRecords.Add(maintenance);
        }
    }
}
