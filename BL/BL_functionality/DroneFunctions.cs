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
        /// <summary>
        /// add Drone To BL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="maxWeight"></param>
        /// <param name="model"></param>
        /// <param name="numberStaion"></param>
        public void addDroneToBL(int id, int maxWeight, string model, int numberStaion)
        {
            if (maxWeight < 1 || maxWeight > 3)
            {
                throw new OutOfRange("status");
            }

            if (dal.GetDrones().Any(d => d.Id == id))
                throw new IdAlreadyExist(id);

            Drone droneBL = new Drone() { Id = id, MaxWeight = (WeightCategories)maxWeight, Model = model };

            Station stationBL = GetStationById(numberStaion);

            if (stationBL.FreeChargeSlots == 0)
            {
                throw new NotFound($"space in the station number {numberStaion} to put the drone");
            }
            else
            {
                DroneAtCharging droneAtChargingBL = new DroneAtCharging() { ID = id, Battery = droneBL.Battery };
                stationBL.droneAtChargings.Add(droneAtChargingBL);
                stationBL.FreeChargeSlots -= 1;
            }


            droneBL.DroneStatus = DroneStatus.Maintenance;

            droneBL.Battery = rand.Next(20, 40);
            DO.Station stationDL = new DO.Station();
            stationDL = dal.GetStationById(numberStaion);
            stationDL.ChargeSlots -= 1;
            DO.DroneCharge droneChargeDL = new DO.DroneCharge() { DroneId = droneBL.Id, stationId = stationDL.Id };
            dal.AddDroneCharge(droneChargeDL);
            Location location = new Location(stationDL.Longitude, stationDL.Latitude);
            droneBL.Location = location;
            DO.Drone drone = new DO.Drone() { Id = id, MaxWeight = (DO.WeightCategories)maxWeight, Model = model };
            dal.AddDrone(drone);
            dronesBL.Add(droneBL);
            dal.UpdateStation(stationDL);
        }

        /// <summary>
        /// Get Drones
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Drone> GetDrones()
        {
            return from drone in dronesBL
                   select drone;

        }

        /// <summary>
        /// updat eDrone Model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public void updateDroneModel(int id, string model)
        {
            DO.Drone droneDL = dal.GetDroneById(id);
            droneDL.Model = model;
            dal.UpdateDrone(droneDL);

            Drone droneBL = dronesBL.First(d => d.Id == id);
            droneBL.Model = model;
            updateDrone(droneBL);
        }

        /// <summary>
        /// update Drone
        /// </summary>
        /// <param name="drone"></param>
        public void updateDrone(Drone drone)
        {
            int index = dronesBL.FindIndex(d => d.Id == drone.Id);
            dronesBL[index] = drone;
            dal.UpdateDrone(new DO.Drone() { Id = drone.Id, MaxWeight = (DO.WeightCategories)drone.MaxWeight, Model = drone.Model });
        }

        public void updateStation(Station station)
        {
            dal.UpdateStation(new DO.Station()
            {
                ChargeSlots = station.FreeChargeSlots,
                Id = station.Id,
                Latitude = station.Location.Latitude,
                Longitude = station.Location.Longitude,
                Name = station.Name
            });
        }

        public IEnumerable<Drone> GetDronesBy(Predicate<Drone> findBy)
        {
            return from drone in GetDrones()
                   where findBy(drone)
                   select drone;
        }


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

        public DroneToList ConvertDroneToDroneToList(Drone drone)
        {
            DroneToList droneToList = new DroneToList() { Id = drone.Id, Battery = drone.Battery, DroneStatus = drone.DroneStatus, Location = drone.Location, Model = drone.Model };
            
            if (!(drone.ParcelInDelivery == null) )
                if(drone.ParcelInDelivery.Id != 0)
                droneToList.NumberOfSendedParcel = drone.ParcelInDelivery.Id;
            else
                droneToList.NumberOfSendedParcel = 0;
            return droneToList;

        }

    }
}
