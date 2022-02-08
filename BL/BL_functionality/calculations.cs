using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;


namespace BL
{

    partial class BL : BlApi.IBL
    {

        /// <summary>
        /// calculates distance Between Two Locationds
        /// </summary>
        /// <param name="location1">source</param>
        /// <param name="location2">distination</param>
        /// <returns>distance</returns>
        private double calculateDistanceBetweenTwoLocationds(Location location1, Location location2)
        {
            return Math.Sqrt(Math.Pow(location1.Longitude - location2.Longitude, 2)
                + Math.Pow(location1.Latitude - location2.Latitude, 2) * 1.0);


        }

        /// <summary>
        /// calculates how much battary a drone needs for a certain delivery
        /// </summary>
        /// <param name="droneLocation">source</param>
        /// <param name="senderId">id of sender</param>
        /// <param name="reciverId">idi of reciver</param>
        /// <param name="parcelWeight">weight of delivers parcel</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private double calculateBatteryForDelivery(Location droneLocation, int senderId, int reciverId, WeightCategories parcelWeight)
        {
            Location senderLocation = GetCustomerById(senderId).Location;
            Location reciverLocation = GetCustomerById(reciverId).Location;
            Location closlestStationLocation = closestStationToLoacationWithFreeChargeSlots(reciverLocation).Location;
            double electricityNeeded = calculateElectricity(senderLocation, reciverLocation, parcelWeight)
                + calculateElectricityWhenFree(droneLocation, senderLocation)
                + calculateElectricityWhenFree(reciverLocation, closlestStationLocation);
            return electricityNeeded;
        }

        /// <summary>
        /// calculates how much electricity a drone needs to transfer a parcel from one location to another
        /// </summary>
        /// <param name="location1">source</param>
        /// <param name="location2">destionation</param>
        /// <param name="weight">weight of parcel</param>
        /// <returns>electricity needed</returns>
        private double calculateElectricity(Location location1, Location location2, WeightCategories weight)
        {

            double distance = calculateDistanceBetweenTwoLocationds(location1, location2);
            switch (weight)
            {
                case WeightCategories.Light:
                    return (distance * this.ElectricityUseWhenLight);
                case WeightCategories.Medium:
                    return (distance * this.ElectricityUseWhenMedium);
                case WeightCategories.Heavy:
                    return (distance * this.ElectricityUseWhenheavy);

                default:
                    return 0;


            }
        }

        /// <summary>
        /// calculates how much electricity a drone needs to move from one location to another with no parcels
        /// </summary>
        /// <param name="location1">source</param>
        /// <param name="location2">destionation</param>
        /// <returns>electricity needed</returns>
        private double calculateElectricityWhenFree(Location location1, Location location2)
        {

            double distance = calculateDistanceBetweenTwoLocationds(location1, location2);

            return distance * ElectricityUseWhenLight;

        }

        /// <summary>
        /// finds closest Station To Loacation that has free charge slots
        /// </summary>
        /// <param name="location">location of drone</param>
        /// <returns>closest station with free charge slots</returns>
        private Station closestStationToLoacationWithFreeChargeSlots(Location location)
        {
            Station closestStation;

            closestStation = GetStations().ToList().First();


            foreach (Station station in GetStations())
            {
                double currentDistance = calculateDistanceBetweenTwoLocationds(station.Location, location);
                double closesetStationDistance = calculateDistanceBetweenTwoLocationds(closestStation.Location, location);
                if (currentDistance < closesetStationDistance && station.FreeChargeSlots > 0)
                {
                    closestStation = station;
                }
            }

            if (closestStation.FreeChargeSlots <= 0)
                throw new NotFound("no station with free charge slots");
            return closestStation;
        }

        /// <summary>
        /// calculates if the drone has enough battary fo the delivery
        /// </summary>
        /// <param name="battry">battary of drone</param>
        /// <param name="droneLoacation">source</param>
        /// <param name="parcel">parcel that needes to be delivered</param>
        /// <returns></returns>
        private bool isEnoughBattaryToDeliverTheParcel(double battry, Location droneLoacation, Parcel parcel)
        {
            double batteryNeededForDeleivery = calculateBatteryForDelivery(droneLoacation,
                parcel.customerAtParcelSender.Id,
                parcel.customerAtParcelReciver.Id,
                parcel.Weight);

            return battry >= batteryNeededForDeleivery;
        }

        /// <summary>
        /// calculate how many free ChargeSlots are in the station
        /// </summary>
        /// <param name="statioinId">id of station</param>
        /// <returns>number of free charge slots</returns>
        private int calculateFreeChargeSlotsInStation(int statioinId)
        {
            int total = dal.GetStationById(statioinId).ChargeSlots;
            foreach (var chargeDrone in GetDronesCharges())
            {
                if (chargeDrone.StationId == statioinId)
                    total--;
            }
            return total;
        }

        /// <summary>
        /// calculates the status of the parcel
        /// </summary>
        /// <param name="parcel">parcel</param>
        /// <returns>status of parcel</returns>
        private ParcelStatus calculateParcelsStatus(Parcel parcel)
        {
            ParcelStatus parcelStatus = ParcelStatus.Requested;
            if (parcel.Requested != null)
                parcelStatus = ParcelStatus.Requested;
            if (parcel.Scheduled != null)
                parcelStatus = ParcelStatus.Scheduled;
            if (parcel.PickedUp != null && parcel.Delivered == null)
                parcelStatus = ParcelStatus.PickedUp;
            if (parcel.Delivered != null)
                parcelStatus = ParcelStatus.Delivered;
            return parcelStatus;

        }
    }
}