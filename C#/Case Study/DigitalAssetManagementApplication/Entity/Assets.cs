using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalAssetManagementApplication.Entity
{
    public class Assets
    {
        public int AssetID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string SerialNumber { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public Employees OwnerID { get; set; }
        public List<AssetAllocations> Allocations { get; set; } = new List<AssetAllocations>();
        public List<MaintenanceRecords> MaintenanceRecords { get; set; } = new List<MaintenanceRecords>();
        public List<Reservations> Reservations { get; set; } = new List<Reservations>();
        public static List<Assets> AssetList { get; set; } = new List<Assets>();
        public Assets(int assetId, string name, string type, string serialNumber, DateTime purchaseDate, string location, string status, Employees ownerId)
        {
            AssetID = assetId;
            Name = name;
            Type = type;
            SerialNumber = serialNumber;
            PurchaseDate = purchaseDate;
            Location = location;
            Status = status;
            OwnerID = ownerId;
        }

        // Add Asset Method
        public static void AddAsset(Assets asset)
        {
            AssetList.Add(asset);
        }

        // Delete Asset Method
        public static void DeleteAsset(int assetId)
        {
            var asset = AssetList.FirstOrDefault(a => a.AssetID == assetId);
            if (asset != null)
            {
                AssetList.Remove(asset);
            }
            else
            {
                throw new InvalidOperationException("Asset not found.");
            }
        }

        // Update Asset Method
        public static void UpdateAsset(int assetId, string newName, string newType, string newLocation, string newStatus)
        {
            var asset = AssetList.FirstOrDefault(a => a.AssetID == assetId);
            if (asset != null)
            {
                asset.Name = newName;
                asset.Type = newType;
                asset.Location = newLocation;
                asset.Status = newStatus;
            }
            else
            {
                throw new InvalidOperationException("Asset not found.");
            }
        }
    }
}
