using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;


namespace BL
{

    partial class BL : BlApi.IBL
    {        /// <summary>
             /// release Drone From Charging 
             /// </summary>
             /// <param name="idDrone"></param>
             /// <param name="timeInCharging"></param>
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
        /// collect Parcle By Drone
        /// </summary>
        /// <param name="idDrone"></param>
        public void collectParcleByDrone(int idDrone)
        {
            Drone drone = GetDroneById(idDrone);

            if (drone.DroneStatus != DroneStatus.Delivery || drone.ParcelInDelivery == null) //MIRIAM-TODO check if the second condition is needed
                throw new DroneIsNotInCorrectStatus("drone is not in Delivery");

            Parcel parcel = GetParcelById(drone.ParcelInDelivery.Id);

            Location locationSender = drone.ParcelInDelivery.locationCollect;
            double useElectricity = calculateElectricity(drone.Location, locationSender, parcel.Weight);
            drone.Battery -= useElectricity;
            drone.Location = locationSender;
            updateDrone(drone);

            parcel.PickedUp = DateTime.Now;
            updateParcel(parcel);

        }

        public void supplyParcelByDrone(int DroneID)
        {

            Drone drone = GetDroneById(DroneID);

            if (drone.DroneStatus != DroneStatus.Delivery)
                throw new DroneIsNotInCorrectStatus("drone is not in delivery");

            //MIRIAM-TODO check if this condition is needed
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
        /// send Drone To Charge
        /// </summary>
        /// <param name="droneId"></param>
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
                throw new DroneDoesNotHaveEnoughBattery();
            }
        }

        public void AssignAParcelToADrone(int id)
        {
            Drone drone = GetDroneById(id);
            Parcel bestParcel = null;
            try
            {
                foreach (var parcel in GetParcelsBy(t => t.Scheduled == null))
                {
                    if (isEnoughBattaryToDeliverTheParcel(drone.Battery,drone.Location ,parcel) && drone.MaxWeight >= parcel.Weight)
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
            drone.ParcelInDelivery = convertPArcelToParcelInDelivery(bestParcel, false);
            updateDrone(drone);
            bestParcel.droneAtParcel = new DroneAtParcel { Id = drone.Id, Battery = drone.Id, Location = drone.Location };
            bestParcel.Scheduled = DateTime.Now;
            updateParcel(bestParcel);

        }
        /// <summary>
        /// supply Parcel By Drone
        /// </summary>
        /// <param name="DroneID"></param>
    }
}
