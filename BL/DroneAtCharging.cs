﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
    public class DroneAtChargingBL
        {

       
            
       public int ID { get; set; }

       public double Battery { get; set; }

            public override string ToString()
            {
                return $"DroneAtCharging   : {ID}, " +
                    $" battery: {Battery}";

                ;
            }



        }
    }
}
