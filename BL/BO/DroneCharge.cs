using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class DroneCharge
    {

        public DroneCharge(int droneId, int stationId1)
        {
            DroneId = droneId;
            StationId = stationId1;
        }
        public int DroneId { get; set; }
        public int StationId { get; set; }

        public DateTime StartCharging { get; set; }

        public override string ToString()
        {
            return $"DroneCharge {DroneId} : {StationId}";
        }



    }
}


