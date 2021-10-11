using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public class Drone
        {

            public override string ToString()
            {
                return $"{Id} + {Model}";
            }
            public int Id{ get; set; }
            public string Model { get; set; }
            public weightCategories MaxWeight { get; set; }
            public droneStatus Status { get; set; }
            public double Battery { get; set; }
        }
    }
}
