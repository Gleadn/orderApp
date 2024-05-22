using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orderApp.Class
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public float Price { get; set; }
        public Receipe ReceipeID { get; set; }
        public List<Foodstuff> Foodstuffs { get; set; }

        public Product(int id,string name, float price, string description, string category, Receipe receipeId, List<Foodstuff> foodstuffs)
        {
            Id = id;
            Name = name;
            Description = description;
            Category = category;
            Price = price;
            ReceipeID = receipeId;
            Foodstuffs = foodstuffs;
        }
    }
}
