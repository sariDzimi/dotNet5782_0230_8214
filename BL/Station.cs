﻿using System;
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

/*            public StationBL(IDAL.DO.StationDL stationDL)
            {
                Id = stationDL.Id;
                Name = stationDL.Name;
                Location = new Location(stationDL.Longitude, stationDL.Latitude);
                ChargeSlots = stationDL.ChargeSlots;
                droneAtChargings = new List<DroneAtChargingBL>();


            }*/



            

            public StationBL()
            {
                droneAtChargings = new List<DroneAtChargingBL>();
            }
            public int Id { get; set; }
            public int Name { get; set; }
            public Location Location { get; set; }
            public int ChargeSlots { get; set; }
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

            public List<DroneAtChargingBL> droneAtChargings;



        }

    }

}
