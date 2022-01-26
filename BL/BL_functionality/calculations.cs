using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BL
{

    partial class BL : BlApi.IBL
    {

        /// <summary>
        /// distance Between Two Locationds
        /// </summary>
        /// <param name="location1"></param>
        /// <param name="location2"></param>
        /// <returns></returns>
        private double calculateDistanceBetweenTwoLocationds(Location location1, Location location2)
        {
            return Math.Sqrt(Math.Pow(location1.Longitude - location2.Longitude, 2)
                + Math.Pow(location1.Latitude - location2.Latitude, 2) * 1.0);


        }

        public double calculateBatteryForDelivery(Location droneLocation, int senderId, int reciverId, WeightCategories parcelWeight)
        {
            Location senderLocation = GetCustomerById(senderId).Location;
            Location reciverLocation = GetCustomerById(reciverId).Location;
            Location closlestStationLocation = closestStationToLoacationWithFreeChargeSlots(reciverLocation).Location;
            double electricityNeeded = calculateElectricity(senderLocation, reciverLocation, parcelWeight)
                + calculateElectricityWhenFree(droneLocation, senderLocation)
                + calculateElectricityWhenFree(reciverLocation, closlestStationLocation);
            return electricityNeeded;
        }

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

        private double calculateElectricityWhenFree(Location location1, Location location2)
        {

            double distance = calculateDistanceBetweenTwoLocationds(location1, location2);

            return distance * ElectricityUseWhenLight;

        }

        /// <summary>
        /// closest Station To Loacation
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
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

        private bool isEnoughBattaryToDeliverTheParcel(double battry, Location droneLoacation, Parcel parcel)
        {
            double batteryNeededForDeleivery = calculateBatteryForDelivery(droneLoacation,
                parcel.customerAtParcelSender.Id,
                parcel.customerAtParcelReciver.Id,
                parcel.Weight);

            return battry >= batteryNeededForDeleivery;
        }
    }
}