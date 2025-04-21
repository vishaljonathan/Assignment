using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Entity
{
    public class Clothing
    {
        public int ProductId { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }

        public Clothing() { }

        public Clothing(int productId, string size, string color)
        {
            ProductId = productId;
            Size = size;
            Color = color;
        }
    }
}
