using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DAL;


namespace BL
{
    partial class BL
    {
        private DroneCharge ConvertToDroneChargeBL(DO.DroneCharge droneChargeDL)
        {
            return new DroneCharge(droneChargeDL.DroneId, droneChargeDL.stationId);
        }
        public IEnumerable<DroneCharge> GetDronesCharges()
        {
            return from drone in dal.GetDroneCharges()
                   select ConvertToDroneChargeBL(drone);

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
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            dal.AddDroneCharge(new DO.DroneCharge() { DroneId = droneCharge.DroneId, stationId = droneCharge.StationId });
        }
    }
}


