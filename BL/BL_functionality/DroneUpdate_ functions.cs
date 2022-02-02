using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using System.Runtime.CompilerServices;
using System.Threading;



namespace BL
{

    partial class BL : BlApi.IBL

    {    /// <summary>
         /// releases drone from charging 
         /// </summary>
         /// <param name="idDrone">id of drone</param>
         /// <param name="timeInCharging">time of drone in charging</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void releaseDroneFromCharging(int idDrone, double timeInCharging)
        {
            Drone drone = GetDroneById(idDrone);

            if (drone.DroneStatus != DroneStatus.Maintenance)
                throw new BO.DroneIsNotInCorrectStatus("drone is not in Maintenance");

            drone.Battery += timeInCharging * RateOfCharching;
            drone.DroneStatus = DroneStatus.Free;
            updateDrone(drone);
            DeleteDroneCharge(drone.Id);

        }


        /// <summary>
        /// collect parcle from sender by drone
        /// </summary>
        /// <param name="idDrone">id of drone</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void collectParcleByDrone(int idDrone)
        {
            Drone drone = GetDroneById(idDrone);

            if (drone.DroneStatus != DroneStatus.Delivery || drone.ParcelInDelivery == null)
                throw new DroneIsNotInCorrectStatus("Delivery");

            Parcel parcel = GetParcelById(drone.ParcelInDelivery.Id);

            Location locationSender = drone.ParcelInDelivery.locationCollect;
            double useElectricity = calculateElectricity(drone.Location, locationSender, parcel.Weight);
            drone.Battery -= useElectricity;
            drone.Location = locationSender;
            drone.ParcelInDelivery.IsWating = false;
            updateDrone(drone);
            parcel.PickedUp = DateTime.Now;
            updateParcel(parcel);


        }


        /// <summary>
        /// suplly parcel to reciver by drone
        /// </summary>
        /// <param name="droneId">id of drone</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void supplyParcelByDrone(int droneId)
        {

            Drone drone = GetDroneById(droneId);

            if (drone.DroneStatus != DroneStatus.Delivery)
                throw new DroneIsNotInCorrectStatus("drone is not in delivery");

            if (drone.ParcelInDelivery == null)
                throw new NotFound("parcel in drone");

            Location locationSender = drone.ParcelInDelivery.locationCollect;
            Location locationReciver = drone.ParcelInDelivery.locationTarget;
            double electricityUsed = calculateElectricity(locationSender, locationReciver, drone.ParcelInDelivery.weightCategories);
            drone.Battery -= electricityUsed;
            drone.Location = locationReciver;
            drone.DroneStatus = DroneStatus.Free;

            Parcel parcel = GetParcelById(drone.ParcelInDelivery.Id);
            parcel.Delivered = DateTime.Now;
            updateParcel(parcel);

            drone.ParcelInDelivery = null;
            updateDrone(drone);

        }


        /// <summary>
        /// send drone to a station to be charged
        /// </summary>
        /// <param name="droneId">id of drone</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void sendDroneToCharge(int droneId)
        {
            Drone drone = GetDroneById(droneId);

            if (drone.DroneStatus != DroneStatus.Free)
                throw new DroneIsNotInCorrectStatus("drone is not free");

            Station closestStation = closestStationToLoacationWithFreeChargeSlots(drone.Location);
            double electricityNeeded = calculateElectricityWhenFree(closestStation.Location, drone.Location);

            if (electricityNeeded < drone.Battery)
            {
                drone.Battery -= electricityNeeded;
                drone.Location = closestStation.Location;
                drone.DroneStatus = DroneStatus.Maintenance;
                updateDrone(drone);
                DroneCharge droneCharge = new DroneCharge(droneId, closestStation.Id);
                AddDroneCharge(droneCharge);
            }
            else
            {
                throw new DroneDoesNotHaveEnoughBattery(droneId);
            }
        }


        /// <summary>
        /// assing a parcel that need to be delivered to a free drone
        /// </summary>
        /// <param name="id"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AssignAParcelToADrone(int id)
        {
            Drone drone = GetDroneById(id);
            Parcel bestParcel = null;
            try
            {
                foreach (var parcel in GetParcelsBy(t => t.Scheduled == null))
                {
                    if (isEnoughBattaryToDeliverTheParcel(drone.Battery, drone.Location, parcel) && drone.MaxWeight >= parcel.Weight)
                    {
                        bestParcel = parcel;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw new NotFound("all parcels are scheduled");
            }

            if (bestParcel == null)
                throw new NotFound("the drone does not have enough battery to delivery any parcel");

            foreach (var parcel in GetParcels())
            {
                //if the parcel is already assigned to a drone, continue
                if (parcel.droneAtParcel != null || parcel.Scheduled != null) continue; 

                if (isEnoughBattaryToDeliverTheParcel(drone.Battery, drone.Location, parcel) && drone.MaxWeight >= parcel.Weight)
                {
                    if (bestParcel.Pritority < parcel.Pritority)
                    {
                        bestParcel = parcel;
                    }
                    else if (bestParcel.Pritority == parcel.Pritority)
                    {
                        if (bestParcel.Weight < parcel.Weight)
                        {
                            bestParcel = parcel;
                        }
                        else if (bestParcel.Weight == parcel.Weight)
                        {
                            Location bestParcelLocaion = GetCustomerById(bestParcel.customerAtParcelSender.Id).Location;
                            Location senderLocation = GetCustomerById(parcel.customerAtParcelSender.Id).Location;
                            if (calculateDistanceBetweenTwoLocationds(drone.Location, senderLocation) < calculateDistanceBetweenTwoLocationds(drone.Location, bestParcelLocaion))
                            {
                                bestParcel = parcel;
                            }
                        }
                    }
                }
            }

            drone.DroneStatus = DroneStatus.Delivery;
            drone.ParcelInDelivery = convertParcelToTypeOfParcelInDelivery(bestParcel, true);
            updateDrone(drone);
            bestParcel.droneAtParcel = new DroneAtParcel { Id = drone.Id, Battery = drone.Id, Location = drone.Location };
            bestParcel.Scheduled = DateTime.Now;
            updateParcel(bestParcel);
            updateDrone(drone);
        }
    }
}
