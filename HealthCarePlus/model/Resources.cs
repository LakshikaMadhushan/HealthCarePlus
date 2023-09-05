using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCarePlus.model
{
    public class Resources
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime BuyingDate { get; set; }
        public DateTime RepairedDate { get; set; }
        public string Remark { get; set; }
        public decimal Price { get; set; }
    }
}
