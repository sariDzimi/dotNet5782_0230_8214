using System;
using System.Collections.Generic;
using System.IO;
using DO;
using System.Linq;
using System.Xml.Linq;
using DalApi;

namespace Dal
{
    partial class DalXml : IDal
    {
        #region Drone

        /// <summary>
        /// adds drone to drones xml file
        /// </summary>
        /// <param name="drone">drone</param>
        /// 
        public void AddDrone(Drone drone)
        {
            List<DO.Drone> droneList = GetDrones().ToList();

            if (droneList.Any(d => d.Id == drone.Id))
            {
                throw new IdAlreadyExistException("drone", drone.Id);
            }

            droneList.Add(drone);
            XMLTools.SaveListToXMLSerializer(droneList, dir + droneFilePath);
        }

        /// <summary>
        /// deletes customer from customers xml file
        /// </summary>
        /// <param name="id">id of drone</param>
        public void DeleteDrone(int id)
        {
            List<DO.Drone> droneList = GetDrones().ToList();
            Drone drone;
            try
            {
                drone = GetDroneById(id);
            }
            catch
            {
                throw new NotFoundException("drone", id);
            }

            droneList.Remove(drone);

            XMLTools.SaveListToXMLSerializer(droneList, dir + droneFilePath);
        }

        /// <summary>
        /// updates the drone in the drones xml file
        /// </summary>
        /// <param name="drone">drone with updated details</param>
        public void UpdateDrone(Drone drone)
        {
            List<DO.Drone> droneList = GetDrones().ToList();

            int index = droneList.FindIndex(d => d.Id == drone.Id);

            if (index == -1)
                throw new NotFoundException("drone", drone.Id);

            droneList[index] = drone;

            XMLTools.SaveListToXMLSerializer(droneList, dir + droneFilePath);
        }

        /// <summary>
        /// finds a drone by id
        /// </summary>
        /// <param name="id">id of drone</param>
        /// <returns>drone with the given id</returns>
        public DO.Drone GetDroneById(int id)
        {
            try
            {
                return GetDrones(d => d.Id == id).First();
            }
            catch (Exception)
            {
                throw new NotFoundException("drone", id);
            }
        }

        /// <summary>
        /// returns drones form drones xml file
        /// </summary>
        /// <param name="getBy">condition</param>
        /// <returns>drones that full-fill the conditon</returns>
        public IEnumerable<DO.Drone> GetDrones(Predicate<DO.Drone> getBy = null)
        {
            IEnumerable<DO.Drone> droneList = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dir + droneFilePath);

            getBy ??= ((st) => true);
            return from drone in droneList
                   where getBy(drone)
                   orderby drone.Id
                   select drone;

        }


        #endregion
    }
}