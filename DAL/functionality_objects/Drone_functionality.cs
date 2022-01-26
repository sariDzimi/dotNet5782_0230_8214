using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal partial class DalObject : DalApi.IDal
    {
        #region Drone
        /// <summary>
        /// Adds the drone to the drones list in the DataSource
        /// If the ID alredy exist the function will throw exception
        /// </summary>
        /// <param name="drone"></param>
        public void AddDrone(Drone drone)
        {
            if (DataSource.drones.Any(dr => dr.Id == drone.Id))
            {
                throw new IdAlreadyExist(drone.Id);
            }

            DataSource.drones.Add(drone);
        }
        /// <summary>
        /// returns drones form datasource
        /// </summary>
        /// <returns>DataSource.drones</returns>
        public IEnumerable<Drone> GetDrones(Predicate<Drone> getBy = null)
        {
            getBy ??= (drone => true);
            return from drone in DataSource.drones
                   where (getBy(drone))
                   select drone;
        }
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

        /// <summary>
        /// updates the drones list in the database
        /// </summary>
        /// <param name="drone"></param>
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