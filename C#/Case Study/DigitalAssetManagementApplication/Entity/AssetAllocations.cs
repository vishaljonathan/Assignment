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
    }
}
