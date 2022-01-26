using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal partial class DalObject : DalApi.IDal
    {
        #region Station

        /// <summary>
        /// Adds the station to the stations list in the DataSource
        /// If the ID alredy exist the function will throw exception
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(Station station)
        {
            if (DataSource.stations.Any(st => st.Id == station.Id))
            {
                throw new IdAlreadyExist(station.Id);
            }

            DataSource.stations.Add(station);

        }
        /// <summary>
        /// returns stations form datasource
        /// </summary>
        /// <returns>DataSource.stations</returns>
        public IEnumerable<Station> GetStations(Predicate<Station> getBy = null)
        {
            getBy ??= (station => true);
            return from station in DataSource.stations
                   where (getBy(station))
                   select station;
        }
        public Station GetStationById(int id)
        {
            try
            {

                return GetStations(s => s.Id == id).First();
            }
            catch
            {
                throw new NotFoundException("station");
            }
        }
        /// <summary>
        /// updates the stations list in the database
        /// </summary>
        /// <param name="station"></param>
        public void UpdateStation(Station station)
        {
            int index = DataSource.stations.FindIndex(p => p.Id == station.Id);
            if (index == -1)
                throw new NotFoundException("station");
            DataSource.stations[index] = station;
        }

        #endregion
    }
}