﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class DroneAtParcel
        {
            public int Id { set; get; }

            public double Battery { get; set; }

            public Location Location { get; set; }
        }
    }
}
