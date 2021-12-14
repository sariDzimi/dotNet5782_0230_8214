using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {

        public class Station
        {
            public Station()
            {
                droneAtChargings = new List<DroneAtCharging>();
            }
            private int chargeSlots;
            public int Id { get; set; }
            public int Name { get; set; }
            public Location Location { get; set; }

            public int ChargeSlots
            {
                get
                {
                    return chargeSlots;
                }
                set
                {
                    if (value < 0)
                        throw new OutOfRange("chargeSlots");
                    chargeSlots = value;
                }
            }
            public override string ToString()
            {
                string droneAtCharging = " ";
                if (droneAtChargings.Count != 0)
                {
                    foreach (var d in droneAtChargings)
                    {
                        droneAtCharging += d;
                        droneAtCharging += " ";
                    }
                }
               
                return $"station {Name} : {Id}, 'Location' {Location} , 'ChargeSlots': {ChargeSlots}," +
                    $"{droneAtCharging}  ";
            }

            public List<DroneAtCharging> droneAtChargings;



        }

    }

}
