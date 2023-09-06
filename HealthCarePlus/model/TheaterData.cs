using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCarePlus.model
{
    public class TheaterData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int MaxPatient { get; set; }
        public string Specification { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}
