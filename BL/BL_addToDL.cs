﻿using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    public partial class BL
    {
        /// <summary>
        /// add Drone To DL
        /// </summary>
        /// <param name="drone"></param>
        public void addDroneToDL(DroneBL drone)
        {
            foreach (var item in dalObject.GetStations().ToList())
            {
                if (item.Id == drone.Id)
                {
                    throw new IdAlreadyExist(drone.Id);
                }
            }


            IDAL.DO.DroneDL droneDL = new IDAL.DO.DroneDL() { Id = drone.Id, MaxWeight = drone.MaxWeight, Model = drone.Model };
            dalObject.addDrone(droneDL);

        }
        /// <summary>
        /// add Station To DL
        /// </summary>
        /// <param name="station"></param>
        public void addStationToDL(StationBL station)
        {
            foreach (var item in dalObject.GetStations().ToList())
            {
                if (item.Id == station.Id)
                {
                    throw new IdAlreadyExist(station.Id);
                }
            }

            IDAL.DO.StationDL stationDL = new IDAL.DO.StationDL() { Id = station.Id, Name = station.Name, Longitude = station.Location.Longitude, ChargeSlots = station.ChargeSlots, Latitude = station.Location.Latitude };
            dalObject.addStation(stationDL);

        }

        /// <summary>
        /// add Parcel To DL
        /// </summary>
        /// <param name="parcel"></param>
        public void addParcelToDL(ParcelBL parcel)
        {


            foreach (var item in dalObject.GetStations().ToList())
            {
                if (item.Id == parcel.Id)
                {
                    throw new IdAlreadyExist(parcel.Id);
                }
            }

            IDAL.DO.ParcelDL parcelDL = new IDAL.DO.ParcelDL()
            {
                Id = parcel.Id,
                SenderId = parcel.customerAtParcelSender.Id,
                TargetId = parcel.customerAtParcelReciver.Id,
                Weight = parcel.Weight,
                Pritority = parcel.Pritority,

                Requested = parcel.Requested,
                DroneId = parcel.droneAtParcel.Id,
                Scheduled = parcel.Scheduled,
                Delivered = parcel.Delivered,
                PickedUp = parcel.PickedUp
            };
            dalObject.addParcel(parcelDL);

        }

        /// <summary>
        /// add Customer To DL
        /// </summary>
        /// <param name="customer"></param>
        public void addCustomerToDL(CustomerBL customer)
        {
            foreach (var item in dalObject.GetStations().ToList())
            {
                if (item.Id == customer.Id)
                {
                    throw new IdAlreadyExist(customer.Id);
                }
            }

            IDAL.DO.CustomerDL customerDL = new IDAL.DO.CustomerDL() { Id = customer.Id, Name = customer.Name, Longitude = customer.Location.Longitude, Phone = customer.Phone, Latitude = customer.Location.Latitude };
            dalObject.addCustomer(customerDL);

        }

        /// <summary>
        /// add DroneCharge To DL
        /// </summary>
        /// <param name="droneCharge"></param>
        public void addDroneChargeToDL(DroneChargeBL droneCharge)
        {
            
            IDAL.DO.DroneChargeDL droneChargeDL = new IDAL.DO.DroneChargeDL() { DroneId = droneCharge.DroneId, stationId = droneCharge.stationId };
            dalObject.addDronCharge(droneChargeDL);

        }
    }
}
