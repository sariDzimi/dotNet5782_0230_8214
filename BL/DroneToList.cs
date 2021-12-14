using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DroneToList
        {
            public int Id { get; set; }

            public string Model { get; set; }

            public double Battery { get; set; }

            public DroneStatus DroneStatus { get; set; }

            public Location Location { get; set; }

            public int NumberOfSendedParcel { get; set; }

            public override string ToString()
            {
                return $"id:{Id}, model:{Model}, battery:{Battery}, drone status:{DroneStatus}, location:{Location}, number of sended parcels:{NumberOfSendedParcel}";
            }

        }


    }
}
