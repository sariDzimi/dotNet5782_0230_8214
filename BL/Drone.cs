using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        
        public class Drone
        {
            IDAL.DO.Drone drone;
            public Drone()
            {
                drone = new IDAL.DO.Drone();

            }
          
           
      

        public override string ToString()
            {
                return $"drone  : {drone.Id}";
            }
        }

    }
}