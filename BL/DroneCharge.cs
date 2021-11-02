using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DroneCharge
        {
            IDAL.DO.DroneCharge droneCharge;
            public DroneCharge()
            {
                droneCharge = new IDAL.DO.DroneCharge();
            }
           

            public override string ToString()
            {
                return $"DroneCharge {droneCharge.DroneId} : {droneCharge.stationId}";
            }
        }
    }

}
