﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public class DataSource
    {
        static internal List<DroneDL> drones = new List<DroneDL>();
        static internal List<StationDL> stations = new List<StationDL>();
        static internal List<CustomerDL> customers = new List<CustomerDL>();
        static internal List<ParcelDL> parcels = new List<ParcelDL>();
        static internal List<DroneChargeDL> droneCharges = new List<DroneChargeDL>();


        internal class Config
        {
            public static double free = .01 ;
            public static double light = .3;
            public static double medium = .4 ;
            public static double heavy = .7;
            public static double rateChargePerHour = .5;
        }
        static public void Initialize()
        {

            int maxRand = 10;
            Random rand = new Random();
            for (int i = 0; i < 2; i++)
            {

                StationDL station = new StationDL();

                station.Id = stations.Count + 1;
                station.Name = stations.Count + 1;
                station.Latitude = rand.Next()% maxRand;
                station.Longitude = rand.Next() % maxRand;
                station.ChargeSlots = rand.Next(300);
                stations.Add(station);

            }

            for (int i = 0; i < 5; i++)
            {

                DroneDL drone = new DroneDL();
                drone.Id = (drones.Count) + 1;
                drone.Model = "MarvicAir2";
                drone.MaxWeight = (WeightCategories)(rand.Next() % 3);
                drones.Add(drone);
          

            }

            for (int i = 0; i < 10; i++)
            {


                CustomerDL customer = new CustomerDL();
                customer.Id = (customers.Count) + 1;
                customer.Name = $"customer{i}";
                customer.Phone = $"{rand.Next(111111111, 999999999)}";
                customer.Latitude = rand.Next() % maxRand;
                customer.Longitude = rand.Next() % maxRand;
                customers.Add(customer);

            }

            for (int i = 0; i < 10; i++)
            {


                ParcelDL parcel = new ParcelDL();
                parcel.Id = parcels.Count + 1;
                parcel.SenderId = customers[rand.Next() % (customers.Count-1)].Id;
                parcel.TargetId = customers[rand.Next() % (customers.Count-1)].Id;
                parcel.Weight = (WeightCategories)(rand.Next() % 3);
                parcel.Pritority = (Pritorities)(rand.Next() % 3);
                parcel.Requested = RandomDate();
                parcel.DroneId = rand.Next() % (parcels.Count + 1);
                parcel.Scheduled = RandomDate();
                parcel.PickedUp = RandomDate();
                parcel.Delivered = RandomDate();
                parcels.Add(parcel);


            }


        }

        public static DateTime RandomDate()
        {
            Random rand = new Random();
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Seconds;
            return start.AddDays(rand.Next(range));
        }
    }
}
