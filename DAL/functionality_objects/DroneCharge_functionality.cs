using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal partial class DalObject : DalApi.IDal
    {
        #region droneCharge

        /// <summary>
        /// adds the droneCharge to the droneCharges list in the DataSource
        /// If the ID alredy exist the function will throw exception
        /// </summary>
        /// <param name="droneCharge"></param>
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            if (DataSource.droneCharges.Any(dg => dg.DroneId == droneCharge.DroneId))
            {
                throw new IdAlreadyExist(droneCharge.DroneId);
            }
            DataSource.droneCharges.Add(droneCharge);

        }
        /// <summary>
        /// returns droneCharges form datasource
        /// </summary>
        /// <returns>DataSource.droneCharges</returns>
        public IEnumerable<DroneCharge> GetDroneCharges(Predicate<DroneCharge> getBy = null)
        {
            getBy ??= (DroneCharge => true);
            return from droneCharge in DataSource.droneCharges
                   where (getBy(droneCharge))
                   select droneCharge;
        }

        public DroneCharge GetDroneChargeById(int droneId)
        {
            try
            {
                return GetDroneCharges(droneCharge => droneCharge.DroneId == droneId).First();
            }
            catch
            {
                throw new NotFoundException("droneCharge");
            }
        }

        /// <summary>
        /// removes droneCharge from dronecharges list in database
        /// </summary>
        /// <param name="id"></param>
        public void DeleteDroneCharge(int droneId)
        {
            DroneCharge droneCharge = GetDroneChargeById(droneId);
            DataSource.droneCharges.Remove(droneCharge);
        }


        public void DeleteAllDroneCharges()
        {
            DataSource.droneCharges = new List<DroneCharge>();
        }
        #endregion
    }
}