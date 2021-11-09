using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        
        public class StationBL
        {
            public int Id { get; set; }
            public int Name { get; set; }

            public Location Location { get; set; }
            public int ChargeSlots { get; set; }
            public override string ToString()
            {
                return $"station {Name} : {Id}";
            }

            List<DroneAtChargingBL> droneAtChargings = new List<DroneAtChargingBL>();

           

        }

    }

}
