using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Entity
{
    public class Electronics
    {
        public int ProductId { get; set; }
        public string Brand { get; set; }
        public int WarrantyPeriod { get; set; }

        public Electronics() { }

        public Electronics(int productId, string brand, int warrantyPeriod)
        {
            ProductId = productId;
            Brand = brand;
            WarrantyPeriod = warrantyPeriod;
        }
    }
}
