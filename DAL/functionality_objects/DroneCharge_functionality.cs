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
        #region Add DroneCharge

        /// <summary>
        /// adds droneCharge to DataSource
        /// </summary>
        /// <param name="droneCharge">droneCharge</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            if (DataSource.droneCharges.Any(dg => dg.DroneId == droneCharge.DroneId))
            {
                throw new IdAlreadyExist(droneCharge.DroneId);
            }
            DataSource.droneCharges.Add(droneCharge);

        }

        #endregion

        #region Get DroneCharge

        /// <summary>
        /// returns droneCharges form datasource
        /// </summary>
        /// <param name="getBy">condition</param>
        /// <returns>droneCharges that full-fill the conditon</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDroneCharges(Predicate<DroneCharge> getBy = null)
        {
            getBy ??= (DroneCharge => true);
            return from droneCharge in DataSource.droneCharges
                   where (getBy(droneCharge))
                   select droneCharge;
        }

        /// <summary>
        /// finds a droneCharge by id of drone
        /// </summary>
        /// <param name="droneId">id of drone</param>
        /// <returns>droneCharge with the given id</returns>
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

        #endregion

        #region Delete DroneCharge

        /// <summary>
        /// removes droneCharge from database
        /// </summary>
        /// <param name="id">id of drone to remove</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDroneCharge(int droneId)
        {
            DroneCharge droneCharge = GetDroneChargeById(droneId);
            DataSource.droneCharges.Remove(droneCharge);
        }

        /// <summary>
        /// removes all droneCharges in database
        /// </summary>
        public void DeleteAllDroneCharges()
        {
            DataSource.droneCharges = new List<DroneCharge>();
        }

        #endregion
    }
}