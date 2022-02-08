using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    /// <summary>
    /// describes a drone that is charging
    /// </summary>
    public class DroneCharge
    {
        public int DroneId { get; set; }
        public int StationId { get; set; }
        public DateTime StartCharging { get; set; }

        public override string ToString()
        {
            return $"DroneCharge {DroneId} : {StationId}";
        }
    }
}


