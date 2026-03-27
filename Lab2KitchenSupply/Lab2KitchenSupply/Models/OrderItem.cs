using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2KitchenSupply.Models
{
    public class OrderItem
    {
        public int LineID { get; set; }

        public int OrderID { get; set; }

        public string ProductID { get; set; }

        public string ServiceID { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"";
        }
    }
}
