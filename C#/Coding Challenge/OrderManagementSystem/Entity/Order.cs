using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Entity
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }

        public Order() { }

        public Order(int orderId, int userId, DateTime orderDate)
        {
            OrderId = orderId;
            UserId = userId;
            OrderDate = orderDate;
        }

        public Order(DateTime orderDate)
        {
            OrderDate = orderDate;
        }
    }
}
