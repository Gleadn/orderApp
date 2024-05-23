using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orderApp.Class
{
    internal class Foodstuff
    {
        public string Name { get; set; } 
        public int Quantity { get; set; }
        public string Status { get; set; }

        public Foodstuff(string name, int quantity, string status)
        {
            Name = name;
            Quantity = quantity;
            Status = status;
        }
    }
}
