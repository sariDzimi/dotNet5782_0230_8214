using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DO
{

    /// <summary>
    /// a point for charging drones
    /// </summary>
    public struct Station
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int ChargeSlots { get; set; }
        public override string ToString()
        {
            return $"station {Name} : {Id}";
        }

    }

}

