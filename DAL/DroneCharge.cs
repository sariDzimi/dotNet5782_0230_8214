﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
    class DroneCharge
    {

            public override string ToString()
            {
                return $"{DroneId} : {stationId}";
            }


            public int DroneId { get; set; }
       public int stationId { get; set; }
    }
    } 
   
}
