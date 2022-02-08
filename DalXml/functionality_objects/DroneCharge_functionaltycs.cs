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
        #region Add DroneCharge

        /// <summary>
        /// adds droneChare to droneCharges xml file
        /// </summary>
        /// <param name="droneCharge">droneCharge</param>
  
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            List<DroneCharge> droneChargeList = GetDroneCharges().ToList();
            if (droneChargeList.Any(dc => dc.DroneId == droneCharge.DroneId))
            {
                throw new IdAlreadyExistException("droneCharge", droneCharge.DroneId);
            }

            droneChargeList.Add(droneCharge);

            XMLTools.SaveListToXMLSerializer(droneChargeList, dir + droneChargeFilePath);
        }

        #endregion

        #region Get DroneCahrge

        /// <summary>
        /// returns droneCharges form droneCharges xml file
        /// </summary>
        /// <param name="getBy">condition</param>
        /// <returns>droneCharges that full-fill the conditon</returns>
        public IEnumerable<DroneCharge> GetDroneCharges(Predicate<DO.DroneCharge> getBy = null)
        {
            IEnumerable<DroneCharge> droneChargeList = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dir + droneChargeFilePath);

            getBy ??= ((st) => true);
            return from droneCharge in droneChargeList
                   where getBy(droneCharge)
                   orderby droneCharge.DroneId
                   select droneCharge;

        }

        /// <summary>
        /// finds a droneChatge by the drone id
        /// </summary>
        /// <param name="id">id of drone</param>
        /// <returns>droneCharge with the given drone id</returns>
        public DO.DroneCharge GetDroneChargeById(int droneId)
        {
            try
            {
                return GetDroneCharges(c => c.DroneId == droneId).First();
            }
            catch (Exception)
            {
                throw new NotFoundException("droneCharge", droneId);
            }

        }


        #endregion

        #region Delete DroneCharge

        /// <summary>
        /// deletes DroneCharge from DroneCharges xml file
        /// </summary>
        /// <param name="id">drone id of droneCharge</param>
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
                throw new NotFoundException("droneCharge", id);
            }

            droneChargeList.Remove(droneCharge);

            XMLTools.SaveListToXMLSerializer(droneChargeList, dir + droneChargeFilePath);
        }

        /// <summary>
        /// deletes all droneCharges from droneCharges xml files
        /// </summary>
        public void DeleteAllDroneCharges()
        {
            XMLTools.SaveListToXMLSerializer(new List<DroneCharge>(), dir + droneChargeFilePath);
        }

        #endregion
    }
}