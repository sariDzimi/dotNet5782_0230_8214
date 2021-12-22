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
            stationId = stationId1;
        }
        public int DroneId { get; set; }
        public int stationId { get; set; }
        public override string ToString()
        {
            return $"DroneCharge {DroneId} : {stationId}";
        }



    }
}


