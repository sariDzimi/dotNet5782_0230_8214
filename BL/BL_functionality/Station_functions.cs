using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;



namespace BL
{
    partial class BL
    {

        #region Add Station

        /// <summary>
        /// adds station to Dal
        /// </summary>
        /// <param name="station">station</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(Station station)
        {
            DO.Station stationDL = convertStationFromBlToDal(station);
            try
            {
                lock (dal)
                {
                    dal.AddStation(stationDL);
                }
            }
            catch (DalApi.IdAlreadyExistException)
            {
                throw new IdAlreadyExist("station", station.Id);
            }

        }

        #endregion

        #region Get Station

        /// <summary>
        /// gets all stations from dal
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStations()
        {
            lock (dal)
            {
                return from station in dal.GetStations()
                       select convertToStationBL(station);
            }
        } 

        /// <summary>
        /// gets all stations that full-fill the condition
        /// </summary>
        /// <param name="findBy">condition</param>
        /// <returns>all stations that full-fill the condition</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStationsBy(Predicate<Station> findBy)
        {
            return from station in GetStations()
                   where findBy(station)
                   select station;
        }


        /// <summary>
        /// finds a certain station
        /// </summary>
        /// <param name="id">id of station</param>
        /// <returns>station</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        /// randoms a station
        /// </summary>
        /// <returns>random station</returns>
        private Station getRandomStation()
        {
            List<Station> stationList = GetStations().ToList();
            int numOfStations = stationList.Count;
            Station station = stationList[rand.Next(0, numOfStations)];
            return station;
        }

        /// <summary>
        /// gets all stations as type of StationToList
        /// </summary>
        /// <returns>stations as type of StationToList</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> GetStationToLists()
        {
            return from station in GetStations()
                   select convertStationToTypeOfStationToList(station);

        }

        /// <summary>
        /// gets all stations that full- fill the condition, as StationToList
        /// </summary>
        /// <param name="findBy">condition</param>
        /// <returns>stations that full- fill the condition, as StationToList</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> GetStationToListBy(Predicate<StationToList> findBy)
        {
            return from station in GetStationToLists()
                   where findBy(station)
                   select station;

        }

        #endregion

        #region Update Station

        /// <summary>
        /// updates some of data Station
        /// </summary>
        /// <param name="id">id of station</param>
        /// <param name="name">updated name</param>
        /// <param name="totalChargeSlots">updated number of total charge slots</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDataStation(int id, int name = -1, int totalChargeSlots = -1)
        {
            lock (dal)
            {
                try
                {
                    DO.Station station = dal.GetStationById(id);
                    if (name != -1)
                        station.Name = name;
                    if (totalChargeSlots != -1)
                        station.ChargeSlots = totalChargeSlots;
                    dal.UpdateStation(station);
                }
                catch (DalApi.NotFoundException)
                {
                    throw new NotFound("station", id);
                }

            }
        }

        /// <summary>
        /// updates station in Dal
        /// </summary>
        /// <param name="station">updated station</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(Station station)
        {
            dal.UpdateStation(convertStationFromBlToDal(station));
        }

        #endregion

        #region Convert Station

        /// <summary>
        /// converts station of type BL to type Dal 
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        private DO.Station convertStationFromBlToDal(Station station)
        {
            return new DO.Station()
            {
                Id = station.Id,
                Name = station.Name,
                ChargeSlots = station.FreeChargeSlots,
                Longitude = station.Location.Longitude,
                Latitude = station.Location.Latitude
            };
        }

        /// <summary>
        /// convert station of type Dal to type BL
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        private Station convertToStationBL(DO.Station station)
        {
            Station StationBL = new Station()
            {
                Id = station.Id,
                Name = station.Name,
                Location = new Location(){ Longitude = station.Longitude , Latitude = station.Latitude }
            };
            int totalOfFreeChargeSlots = station.ChargeSlots;
            //StationBL.FreeChargeSlots = calculateFreeChargeSlotsInStation(station.Id);
            foreach (var dronecharge in GetDronesCharges())
            {
                if (dronecharge.StationId == station.Id)
                {
                    StationBL.DroneAtChargings ??= new List<DroneAtCharging>();
                    StationBL.DroneAtChargings.Add(new DroneAtCharging()
                    {
                        Id = dronecharge.DroneId,
                        Battery = GetDroneById(dronecharge.DroneId).Battery
                    });
                    totalOfFreeChargeSlots--;
                }

            }
            StationBL.FreeChargeSlots = totalOfFreeChargeSlots;
            return StationBL;
        }

        /// <summary>
        /// converts a station to type of StationToList
        /// </summary>
        /// <param name="station">station</param>
        /// <returns>stationToList</returns>
        private StationToList convertStationToTypeOfStationToList(Station station)
        {
            return new StationToList()
            {
                Id = station.Id,
                Name = station.Name,
                NumberOfFreeChargeSlots = station.FreeChargeSlots,
                NumberOfUsedChargeSlots = dal.GetStationById(station.Id).ChargeSlots - station.FreeChargeSlots
            };
        }

        #endregion
    }
}

