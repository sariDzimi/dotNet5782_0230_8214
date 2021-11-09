using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct DroneChargeDL
        {
            public int DroneId { get; set; }
            public int stationId { get; set; }
            public override string ToString()
            {
                return $"DroneCharge {DroneId} : {stationId}";
            }
        }
    }

}
