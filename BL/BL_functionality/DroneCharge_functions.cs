using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using System.Runtime.CompilerServices;



namespace BL
{
    partial class BL
    {
        #region Add DroneCharge

        /// <summary>
        /// adds droneCharge to Dal
        /// </summary>
        /// <param name="droneCharge">droneCharge</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            try
            {
                lock (dal)
                {
                    dal.AddDroneCharge(new DO.DroneCharge()
                    {
                        DroneId = droneCharge.DroneId,
                        StationId = droneCharge.StationId
                    });
                }
            }
            catch (DalApi.IdAlreadyExistException)
            {
                throw new IdAlreadyExist("droneCharge", droneCharge.DroneId);
            }
        }
        #endregion

        #region Get DroneCharge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDronesCharges()
        {
            lock (dal)
            {
                return from drone in dal.GetDroneCharges()
                       select ConvertDroneChargeDalToBL(drone);
            }

        }
        #endregion

        #region Delete DroneCharge

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDroneCharge(int droneId)
        {
            try
            {
                lock (dal)
                {
                    dal.DeleteDroneCharge(droneId);

                }

            }
            catch (NotFoundException)
            {
                throw new NotFound($"couldn't find a droneChagre for drone: {droneId}");
            }
        }

        #endregion

        #region Convert DroneCharge
        private DroneCharge ConvertDroneChargeDalToBL(DO.DroneCharge droneChargeDL)
        {
            return new DroneCharge(droneChargeDL.DroneId, droneChargeDL.StationId);
        }
        #endregion
    }
}


