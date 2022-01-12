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
        /// <summary>
        /// add Station To DL
        /// </summary>
        /// <param name="station"></param>
        public void addStationToDL(Station station)
        {
            DO.Station stationDL = new DO.Station()
            {
                Id = station.Id,
                Name = station.Name,
                ChargeSlots = station.FreeChargeSlots,
                Longitude = station.Location.Longitude,
                Latitude = station.Location.Latitude
            };
            try
            {
                dalObject.AddStation(stationDL);
            }
            catch (DAL.IdAlreadyExist)
            {
                throw new IdAlreadyExist(station.Id);
            }

        }

        /// <summary>
        /// convert To Station BL
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private Station ConvertToStationBL(DO.Station s)
        {
            Station StationBL = new Station() { Id = s.Id, Name = s.Name, Location = new Location(s.Longitude, s.Latitude) };
            StationBL.FreeChargeSlots = calculateFreeChargeSlotsInStation(s.Id);
            foreach (var dronecharge in dalObject.GetDroneCharges())
            {
                if (dronecharge.stationId == s.Id)
                    StationBL.droneAtChargings.Add(new DroneAtCharging() { ID = dronecharge.DroneId, Battery = GetDroneById(dronecharge.DroneId).Battery });
            }
            return StationBL;
        }

        /// <summary>
        /// update Data Station
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="totalChargeSlots"></param>
        public void updateDataStation(int id, int name = -1, int totalChargeSlots = -1)
        {
            DO.Station station = dalObject.GetStationById(id);
            if (name != -1)
                station.Name = name;
            if (totalChargeSlots != -1)
                station.ChargeSlots = totalChargeSlots;
            dalObject.UpdateStation(station);
        }

        public IEnumerable<Station> GetStationsBy(Predicate<Station> findBy)
        {
            return from station in GetStations()
                   where findBy(station)
                   select station;
        }

        public Station GetStationById(int id)
        {
            try
            {
                return GetStationsBy(c => c.Id == id).First();
            }
            catch
            {
                throw new NotFound($"couldn't find Station ${id}");
            }
        }
        /// <summary>
        /// Get Stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> GetStations()
        {
            return from station in dalObject.GetStations()
                   select ConvertToStationBL(station);

        }

        private StationToList convertStationToStationToList(Station station)
        {
            StationToList stationToList = new StationToList()
            {
                ID = station.Id,
                Name = station.Name,
                numberOfFreeChargeSlots = station.FreeChargeSlots,
                numberOfUsedChargeSlots = dalObject.GetStationById(station.Id).ChargeSlots - station.FreeChargeSlots
            };
            return stationToList;
        }






    }
}

