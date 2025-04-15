using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalAssetManagementApplication.Entity
{
    public class AssetAllocations
    {
        public int AllocationID { get; set; }
        public Assets Assetid { get; set; }
        public Employees EmployeeId { get; set; }
        public DateTime AllocationDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public AssetAllocations(int allocationId, Assets assetId, Employees employeeId, DateTime allocationDate, DateTime returnDate)
        {
            AllocationID = allocationId;
            Assetid = assetId;
            EmployeeId = employeeId;
            AllocationDate = allocationDate;
            ReturnDate = returnDate;
        }
        
        // Method to allocate an asset to an employee
        public static void AllocateAsset(Assets asset, Employees employee, DateTime allocationDate)
        {
            if (asset.Status != "Available")
            {
                throw new InvalidOperationException("Asset is not available for allocation.");
            }

            var allocation = new AssetAllocations(
                allocationId: new Random().Next(1000, 9999),
                assetId: asset,
                employeeId: employee,
                allocationDate: allocationDate,
                returnDate: DateTime.MinValue // Return date to be set later
            );

            asset.Allocations.Add(allocation);
            asset.Status = "Allocated";
        }

        // Method to deallocate an asset
        public static void DeallocateAsset(Assets asset, DateTime returnDate)
        {
            var allocation = asset.Allocations.FirstOrDefault(a => a.ReturnDate == DateTime.MinValue);
            if (allocation != null)
            {
                allocation.ReturnDate = returnDate;
                asset.Status = "Available";
            }
            else
            {
                throw new InvalidOperationException("Asset is not currently allocated.");
            }
        }
    }
}
