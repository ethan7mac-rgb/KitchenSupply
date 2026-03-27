using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2KitchenSupply.Models
{
    public class Service
    {
        public string ServiceID { get; set; }

        public string ServiceName { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Price.ToString("c")} {ServiceName}";
        }
    }
}
