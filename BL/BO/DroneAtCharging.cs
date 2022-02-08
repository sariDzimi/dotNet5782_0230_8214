using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    /// <summary>
    /// detatiles of drone when it is charging
    /// </summary>
    public class DroneAtCharging
    {
        public int ID { get; set; }

        public double Battery { get; set; }

        public override string ToString()
        {
            return $"DroneAtCharging   : {ID}, " +
                $" battery: {Battery}";
        }
    }
}

