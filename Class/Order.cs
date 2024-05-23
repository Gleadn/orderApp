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
        public List<int> ProductsId { get; set; }
        public float TotalPrice { get; set; }
        public string Status { get; set; }

        public Order(int id, List<int> productsId, float totalPrice, string status)
        {
            Id = id;
            ProductsId = productsId;
            TotalPrice = totalPrice;
            Status = status;
        }
    }
}
