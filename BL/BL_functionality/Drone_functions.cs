using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace BL
{
    partial class BL
    {

        #region Add Drone

        /// <summary>
        /// adds drone to BL and Dal
        /// </summary>
        /// <param name="id">id of drone</param>
        /// <param name="maxWeight">maxWeight of drone</param>
        /// <param name="model">model of drone</param>
        /// <param name="idStation">number of station for first charge</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(int id, WeightCategories maxWeight, string model, int idStation)
        {
            if (GetDrones().Any(d => d.Id == id))
                throw new IdAlreadyExist("drone", id);

            Station stationBL = GetStationById(idStation);

            if (stationBL.FreeChargeSlots == 0)
                throw new NotFound($"no space in station: {idStation}, to charge the drone");


            DroneCharge droneChargeDL = new DroneCharge() { DroneId = id, StationId = idStation };
            AddDroneCharge(droneChargeDL);

            Drone droneBL = new Drone()
            {
                Id = id,
                MaxWeight = (WeightCategories)maxWeight,
                Model = model,
                DroneStatus = DroneStatus.Maintenance,
                Battery = rand.Next(20, 40),
                Location = stationBL.Location,
                ParcelInDelivery = null
            };

            try
            {
                lock (dal)
                {
                    dal.AddDrone(new DO.Drone()
                    {
                        Id = droneBL.Id,
                        MaxWeight = (DO.WeightCategories)droneBL.MaxWeight,
                        Model = droneBL.Model
                    });
                    dronesBL.Add(droneBL);
                }
            }
            catch (DalApi.IdAlreadyExistException)
            {
                throw new IdAlreadyExist("droneCharge", id);
            }
        }

        #endregion

        #region Update Drone

        /// <summary>
        /// updates Drone Model 
        /// </summary>
        /// <param name="id">id of drone</param>
        /// <param name="model">updated model</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateDroneModel(int id, string model)
        {
            try
            {
                Drone droneBL = dronesBL.First(d => d.Id == id);
                droneBL.Model = model;
                updateDrone(droneBL);
            }
            catch
            {
                throw new NotFound("drone", id);
            }
        }

        /// <summary>
        /// updates drone in BL and Dal
        /// </summary>
        /// <param name="drone">drone with update detatils</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateDrone(Drone drone)
        {
            try
            {

                lock (dal)
                {
                    dal.UpdateDrone(new DO.Drone()
                    {
                        Id = drone.Id,
                        MaxWeight = (DO.WeightCategories)drone.MaxWeight,
                        Model = drone.Model
                    });
                }

                int index = dronesBL.FindIndex(d => d.Id == drone.Id);
                dronesBL[index] = drone;
            }
            catch (DalApi.NotFoundException) { throw new NotFound("drone", drone.Id); }
            catch { throw new NotFound("drone", drone.Id); }

        }

        #endregion

        #region Get Drone

        /// <summary>
        /// gets drones of BL
        /// </summary>
        /// <returns>drones</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDrones()
        {
            return from drone in dronesBL
                   select drone;

        }

        /// <summary>
        /// gets drones of BL that full-fill the condition
        /// </summary>
        /// <param name="findBy">condtion</param>
        /// <returns>drones that full-fill the condition</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDronesBy(Predicate<Drone> findBy)
        {
            return from drone in GetDrones()
                   where findBy(drone)
                   select drone;
        }

        /// <summary>
        /// finds a drone with a certain id
        /// </summary>
        /// <param name="id">id of drone</param>
        /// <returns>drone</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDroneById(int id)
        {
            try
            {
                return GetDronesBy(c => c.Id == id).First();
            }
            catch
            {
                throw new NotFound($"couldn't find Drone ${id}");
            }
        }

        /// <summary>
        /// gets all drones at DroneToList type
        /// </summary>
        /// <returns>drones as DroneToList type</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetDroneToLists()
        {
            return from drone in GetDrones()
                   select ConvertDroneToTypeOfDroneToList(drone);

        }

        /// <summary>
        /// gets all drones that full-fill the condition as DroneToList type
        /// </summary>
        /// <param name="findBy">condition</param>
        /// <returns>drones that full-fill the condition as DroneToList</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetDroneToListsBy(Predicate<Drone> findBy)
        {
            return from drone in GetDrones()
                   where findBy(drone)
                   select ConvertDroneToTypeOfDroneToList(drone);
        }
        #endregion

        #region Convert Drone
        /// <summary>
        /// converts a drone to type of DroneToList
        /// </summary>
        /// <param name="drone"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneToList ConvertDroneToTypeOfDroneToList(Drone drone)
        {
            DroneToList droneToList = new DroneToList()
            {
                Id = drone.Id,
                Battery = drone.Battery,
                DroneStatus = drone.DroneStatus,
                Location = drone.Location,
                Model = drone.Model
            };

            if (!(drone.ParcelInDelivery == null))
                droneToList.NumberOfSendedParcel = drone.ParcelInDelivery.Id;
            return droneToList;

        }

        /// <summary>
        /// converts from type DroneToList to Drone
        /// </summary>
        /// <param name="droneToList">droneToList</param>
        /// <returns>drone</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone ConvertDroneToListToDrone(DroneToList droneToList)
        {
            return GetDroneById(droneToList.Id);
        }
        #endregion

    }
}
