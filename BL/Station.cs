using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        
        public class Station
        {
            IDAL.DO.Station station;
            public Station()
            {
                station = new IDAL.DO.Station();
            }

            public List<Drone> ChargingDrones { get; set; }

            public override string ToString()
            {
                return $"station {station.Name} : {station.Id}";
            }

        }

    }

}
