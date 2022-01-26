using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal partial class DalObject : DalApi.IDal
    {
        #region Add Drone

        /// <summary>
        /// adds drone to DataSource
        /// </summary>
        /// <param name="drone">drone</param>
        public void AddDrone(Drone drone)
        {
            if (DataSource.drones.Any(dr => dr.Id == drone.Id))
            {
                throw new IdAlreadyExist(drone.Id);
            }

            DataSource.drones.Add(drone);
        }

        #endregion

        #region Get Drone

        /// <summary>
        /// returns drones form datasource
        /// </summary>
        /// <param name="getBy">condition</param>
        /// <returns>drones that full-fill the conditon</returns>
        public IEnumerable<Drone> GetDrones(Predicate<Drone> getBy = null)
        {
            getBy ??= (drone => true);
            return from drone in DataSource.drones
                   where (getBy(drone))
                   select drone;
        }

        /// <summary>
        /// finds a drone by id
        /// </summary>
        /// <param name="id">id of drone</param>
        /// <returns>drone with the given id</returns>
        public Drone GetDroneById(int id)
        {
            try
            {
                return GetDrones(d => d.Id == id).First();

            }
            catch
            {
                throw new NotFoundException("drone");
            }
        }

        #endregion

        #region Update Drone

        /// <summary>
        /// update drone in the DataSource
        /// </summary>
        /// <param name="drone">drone with updated details</param>
        public void UpdateDrone(Drone drone)
        {
            int index = DataSource.drones.FindIndex(d => d.Id == drone.Id);
            if (index == -1)
                throw new NotFoundException("drone");
            DataSource.drones[index] = drone;
        }

        #endregion
    }
}