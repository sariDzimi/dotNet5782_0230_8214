using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    /// <summary>
    /// a point for charging drones
    /// </summary>
    public class Station
    {
        //public Station()
        //{
        //    droneAtChargings = new List<DroneAtCharging>();
        //}
        private int freeChargeSlots;
        public int Id { get; set; }
        public int Name { get; set; }
        public Location Location { get; set; }

        public int FreeChargeSlots
        {
            get
            {
                return freeChargeSlots;
            }
            set
            {
                if (value < 0)
                    throw new OutOfRange("chargeSlots");
                freeChargeSlots = value;
            }
        }

        public List<DroneAtCharging> DroneAtChargings; 

        public override string ToString()
        {
            string droneAtCharging = " ";
            if (DroneAtChargings.Count != 0)
            {
                foreach (var d in DroneAtChargings)
                {
                    droneAtCharging += d;
                    droneAtCharging += " ";
                }
            }

            return $"station {Name} : {Id}, 'Location' {Location} , 'ChargeSlots': {FreeChargeSlots}," +
                $"{droneAtCharging}  ";
        }

    }


}
