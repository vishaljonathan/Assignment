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
    }
}
