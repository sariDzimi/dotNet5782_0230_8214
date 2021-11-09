using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        
        public class DroneBL
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public IDAL.DO.WeightCategories MaxWeight { get; set; }

            double battery { get; set; }

            public DroneStatus droneStatus { get; set; }

            public ParcelAtTransfor parcelAtTransfor { get; set; }

            public Location location { get; set; }

            public override string ToString()
            {
                return $"drone  : {Id}";
            }
        }

    }
}