using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orderApp.Class
{
    internal class Receipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Steps { get; set; }

        public Receipe(int id, string name, string description, List<string> steps)
        {
            Id = id;
            Name = name;
            Description = description;
            Steps = steps;
        }
    }
}
