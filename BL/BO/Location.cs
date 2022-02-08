using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    /// <summary>
    /// a fixed geographical point
    /// </summary>
    public class Location
    {
        public double Longitude;
        public double Latitude;

        public override string ToString()
        {
            return $"Location  : {Longitude}, {Latitude} ";
        }
    }
}



