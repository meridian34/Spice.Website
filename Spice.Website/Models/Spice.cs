using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Website.Models
{
    public class Spice
    {
        public int SpiceID { get; set; }

        public string Name { get; set; }

        public byte[] Photo { get; set; }

        public string Description { get; set; }

        public double Weight { get; set; }

        public Unit WeightUnit { get; set; }

        public decimal Price { get; set; }

        public Unit PriceUnit { get; set; }
    }
}
