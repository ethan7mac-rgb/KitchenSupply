using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2KitchenSupply.Models
{
    public class Product
    {
        public string ProductID { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Price.ToString("c")} - {ProductName}";
        }
    }
}
