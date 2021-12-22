﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
     partial class BL
    {

        /// <summary>
        /// add Station To BL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="slots"></param>
/*        public void addStationToBL(int id, int name, Location location, int slots)
        {
            foreach (var item in dalObject.GetStations().ToList())
            {
                if (item.Id == id)
                {
                    throw new IdAlreadyExist(id);
                }
            }


            Station stationBL = new Station() { Id = id, Name = name, Location = location, ChargeSlots = slots };
            List<DroneAtCharging> droneAtChargings = new List<DroneAtCharging>();
            IDAL.DO.Station stationDL = new IDAL.DO.Station() { Id = id, Name = name, ChargeSlots = slots, Latitude = location.Latitude, Longitude = location.Longitude };
            dalObject.addStation(stationDL);
        }*/

        /// <summary>
        /// add Drone To BL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="maxWeight"></param>
        /// <param name="model"></param>
        /// <param name="numberStaion"></param>
        public void addDroneToBL(int id, int maxWeight, string model, int numberStaion)
        {
            if(maxWeight < 1 || maxWeight > 3)
            {
                throw new OutOfRange("status");
            }

            if(dalObject.GetDrones().Any(d => d.Id == id))
                throw new IdAlreadyExist(id);

            Drone droneBL = new Drone() { Id = id, MaxWeight = (WeightCategories)maxWeight, Model = model };

            Station stationBL = FindStation(numberStaion);

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
            stationDL = dalObject.findStationById(numberStaion);
            stationDL.ChargeSlots -= 1;
            DO.DroneCharge droneChargeDL = new DO.DroneCharge() { DroneId = droneBL.Id, stationId = stationDL.Id };
            dalObject.addDronCharge(droneChargeDL);
            Location location = new Location(stationDL.Longitude, stationDL.Latitude);
            droneBL.Location = location;
            DO.Drone drone = new DO.Drone() { Id = id, MaxWeight = (DO.WeightCategories)maxWeight, Model = model };
            dalObject.addDrone(drone);
            dronesBL.Add(droneBL);
            dalObject.updateStation(stationDL);
        }

        /// <summary>
        /// add Customer To BL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="location"></param>
/*        public void addCustomerToBL(int id, string name, string phone, Location location)
        {
            if(dalObject.GetCustomer().Any(c => c.Id == id))
                throw new IdAlreadyExist(id);

            Customer customerBL = new Customer() { Id = id, Name = name, Phone = phone, Location = location };
            IDAL.DO.Customer customer = new IDAL.DO.Customer() { Id = id, Latitude = location.Latitude, Longitude = location.Longitude, Name = name, Phone = phone };
            dalObject.addCustomer(customer);
        }*/

        /// <summary>
        /// add Parcel To BL
        /// </summary>
        /// <param name="SenderId"></param>
        /// <param name="reciverId"></param>
        /// <param name="weight"></param>
        /// <param name="prionity"></param>
/*        public int addParcelToBL(int SenderId, int reciverId, int weight, int prionity)
        {
            if(weight <1 || weight > 3)
            {
                throw new OutOfRange("weight");
            }

            if (prionity < 1 || prionity > 3)
            {
                throw new OutOfRange("Pritorities");
            }

            bool flag;
            int id;
            do
            {
                flag = false;
                id = rand.Next() % ((dalObject.GetParcel().ToList()).Count + 10);
                foreach (var item in dalObject.GetParcel().ToList())
                {
                    if (item.Id == id)
                    {
                        flag = true;
                        break;
                    }

                }
            } while (flag == true);


           

            CustomerAtParcel customerAtParcelSender = new CustomerAtParcel() { Id = SenderId };
            CustomerAtParcel customerAtParcelReciver = new CustomerAtParcel() { Id = reciverId };
            Parcel parcelBL = new Parcel() {Id=id, customerAtParcelSender = customerAtParcelSender, customerAtParcelReciver = customerAtParcelReciver, Weight = (IDAL.DO.WeightCategories)weight, Pritority = (IDAL.DO.Pritorities)prionity, PickedUp=null, Requested= DateTime.Now, Delivered=null, Scheduled=null };
            IDAL.DO.Parcel parcelDL = new IDAL.DO.Parcel() {Id= id, SenderId = SenderId, TargetId = reciverId, Weight = (IDAL.DO.WeightCategories)weight, Pritority = (IDAL.DO.Pritorities)prionity };
            dalObject.addParcel(parcelDL);
            return id;
        }*/

        public void AddDroneCharge(DroneCharge droneCharge)
        {
            dalObject.addDronCharge(new DO.DroneCharge() { DroneId = droneCharge.DroneId, stationId = droneCharge.stationId});
        }
    }
}