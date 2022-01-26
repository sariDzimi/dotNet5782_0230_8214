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
        public void AddDrone(Drone drone)
        {
            List<DO.Drone> droneList = GetDrones().ToList();

            if (droneList.Any(d => d.Id == drone.Id))
            {
                throw new IdAlreadyExist(drone.Id);
            }

            droneList.Add(drone);
            XMLTools.SaveListToXMLSerializer(droneList, dir + droneFilePath);
        }
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
                throw new NotFoundException("drone");
            }

            droneList.Remove(drone);

            XMLTools.SaveListToXMLSerializer(droneList, dir + droneFilePath);
        }
        public void UpdateDrone(Drone drone)
        {
            List<DO.Drone> droneList = GetDrones().ToList();

            int index = droneList.FindIndex(d => d.Id == drone.Id);

            if (index == -1)
                throw new NotFoundException("drone");

            droneList[index] = drone;

            XMLTools.SaveListToXMLSerializer(droneList, dir + droneFilePath);
        }
        public DO.Drone GetDroneById(int id)
        {
            try
            {
                return GetDrones(d => d.Id == id).First();
            }
            catch (Exception)
            {
                throw new NotFoundException("drone");
            }
        }
        public IEnumerable<DO.Drone> GetDrones(Predicate<DO.Drone> predicat = null)
        {
            IEnumerable<DO.Drone> droneList = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dir + droneFilePath);

            predicat ??= ((st) => true);
            return from drone in droneList
                   where predicat(drone)
                   orderby drone.Id
                   select drone;

        }


        #endregion
    }
}