using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using DO;
using System.Runtime.CompilerServices;

namespace Dal
{
    internal partial class DalObject : DalApi.IDal
    {
        #region Add Station

        /// <summary>
        ///adds station to DataSource
        /// </summary>
        /// <param name="station">station</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(Station station)
        {
            if (DataSource.stations.Any(st => st.Id == station.Id))
            {
                throw new IdAlreadyExistException("station", station.Id);
            }

            DataSource.stations.Add(station);

        }
        #endregion

        #region Get Station
        /// <summary>
        /// returns stations form datasource
        /// </summary>
        /// <param name="getBy">condition</param>
        /// <returns>stations that full-fill the conditon</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStations(Predicate<Station> getBy = null)
        {
            getBy ??= (station => true);
            return from station in DataSource.stations
                   where (getBy(station))
                   select station;
        }
        
        /// <summary>
        /// finds a station by id
        /// </summary>
        /// <param name="id">id of station</param>
        /// <returns>station with the given id</returns>
        public Station GetStationById(int id)
        {
            try
            {

                return GetStations(s => s.Id == id).First();
            }
            catch
            {
                throw new NotFoundException("station", id);
            }
        }

        #endregion

        #region Update Station

        /// <summary>
        /// update staion in the DataSource
        /// </summary>
        /// <param name="station">station with updated details</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(Station station)
        {
            int index = DataSource.stations.FindIndex(p => p.Id == station.Id);
            if (index == -1)
                throw new NotFoundException("station", station.Id);
            DataSource.stations[index] = station;
        }

        #endregion
    }
}