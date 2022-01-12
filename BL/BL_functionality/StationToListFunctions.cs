using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DAL;


namespace BL
{
    partial class BL
    {
        public IEnumerable<StationToList> GetStationToLists()
        {
            return from station in GetStations()
                   select convertStationToStationToList(station);

        }

        public IEnumerable<StationToList> GetStationToListBy(Predicate<StationToList> findBy)
        {
            return from station in GetStationToLists()
                   where findBy(station)
                   select station;

        }





    }
}


