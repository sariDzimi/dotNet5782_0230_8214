using System;
using System.Collections.Generic;
using System.IO;
using DO;
using System.Linq;
using System.Xml.Linq;
using DalApi;

namespace Dal
{
    public partial class DalXml : IDal
    {
        #region DroneCharge
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            List<DroneCharge> droneChargeList = GetDroneCharges().ToList();
            if (droneChargeList.Any(dc => dc.DroneId == droneCharge.DroneId))
            {
                throw new IdAlreadyExist(droneCharge.DroneId);
            }

            droneChargeList.Add(droneCharge);

            XMLTools.SaveListToXMLSerializer(droneChargeList, dir + droneChargeFilePath);
        }
        public void DeleteDroneCharge(int id)
        {
            List<DO.DroneCharge> droneChargeList = GetDroneCharges().ToList();
            DroneCharge droneCharge;
            try
            {
                droneCharge = GetDroneChargeById(id);
            }
            catch
            {
                throw new NotFoundException("droneCharge");
            }

            droneChargeList.Remove(droneCharge);

            XMLTools.SaveListToXMLSerializer(droneChargeList, dir + droneChargeFilePath);
        }
        public DO.DroneCharge GetDroneChargeById(int droneId)
        {
            try
            {
                return GetDroneCharges(c => c.DroneId == droneId).First();
            }
            catch (Exception)
            {
                throw new NotFoundException("droneCharge");
            }

        }
        public IEnumerable<DroneCharge> GetDroneCharges(Predicate<DO.DroneCharge> predicat = null)
        {
            IEnumerable<DroneCharge> droneChargeList = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dir + droneChargeFilePath);

            predicat ??= ((st) => true);
            return from droneCharge in droneChargeList
                   where predicat(droneCharge)
                   orderby droneCharge.DroneId
                   select droneCharge;

        }

        public void DeleteAllDroneCharges()
        {
            XMLTools.SaveListToXMLSerializer(new List<DroneCharge>(), dir + droneChargeFilePath);
        }

        #endregion
    }
}