using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DO
{
    /// <summary>
    /// entity that saves the relation between drones and the station their been charched in
    /// </summary>
    public struct DroneCharge
    {
        public int DroneId { get; set; }
        public int stationId { get; set; }

        public DateTime StartCharging { get; set; }
        public override string ToString()
        {
            return $"DroneCharge {DroneId} : {stationId}";
        }
    }
}
