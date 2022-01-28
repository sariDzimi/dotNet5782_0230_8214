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
        private DroneCharge ConvertToDroneChargeBL(DO.DroneCharge droneChargeDL)
        {
            return new DroneCharge(droneChargeDL.DroneId, droneChargeDL.stationId);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDronesCharges()
        {
            lock (dal)
            {
                return from drone in dal.GetDroneCharges()
                       select ConvertToDroneChargeBL(drone);
            }

        }
        /// <summary>
        /// add DroneCharge To DL
        /// </summary>
        /// <param name="droneCharge"></param>
        /*        public void addDroneChargeToDL(DroneCharge droneCharge)
                {

                    IDAL.DO.DroneCharge droneChargeDL = new IDAL.DO.DroneCharge() { DroneId = droneCharge.DroneId, stationId = droneCharge.stationId };
                    dalObject.addDronCharge(droneChargeDL);

                }*/
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            lock (dal)
            {
                dal.AddDroneCharge(new DO.DroneCharge() { DroneId = droneCharge.DroneId, stationId = droneCharge.StationId });
            }
        }

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
            catch(NotFoundException)
            {
                throw new NotFound($"couldn't find a droneChagre for drone: {droneId}");
            }
        }
    }
}


