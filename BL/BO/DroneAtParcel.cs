using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// detatils of drone that apear on a parcel 
    /// </summary>
    public class DroneAtParcel
    {
        public int Id { set; get; }

        public double Battery { get; set; }

        public Location Location { get; set; }

        public override string ToString()
        {
            return $"DroneAtParcel id  : {Id}, " +
                $" Battery: {Battery}, Location: {Location}, ";

            ;
        }


    }
}

