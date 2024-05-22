using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orderApp.Class
{
    internal class Order
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }
        public float TotalPrice { get; set; }
        public string Status { get; set; }

        public Order(int id, List<Product> products, float totalPrice, string status)
        {
            Id = id;
            Products = products;
            TotalPrice = totalPrice;
            Status = status;
        }
    }
}
