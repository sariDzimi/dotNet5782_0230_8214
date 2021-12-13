using System;
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
            public static double free = .1 ;
            public static double light = .4;
            public static double medium = .5 ;
            public static double heavy = .7;
            public static double rateChargePerHour = .5;
        }

        /// <summary>
        /// intilizes the lists in he database by randomng data
        /// </summary>
        static public void Initialize()
        {

            int maxRand = 10;
            Random rand = new Random();
            for (int i = 0; i < 2; i++)
            {

                StationDL station = new StationDL();

                station.Id = stations.Count + 1;
                station.Name = stations.Count + 1;
                station.Latitude = rand.Next()% maxRand +1;
                station.Longitude = rand.Next() % maxRand +1;
                station.ChargeSlots = rand.Next(300);
                stations.Add(station);

            }

            for (int i = 0; i < 5; i++)
            {

                DroneDL drone = new DroneDL();
                drone.Id = (drones.Count) + 1;
                drone.Model = "MarvicAir2";
                drone.MaxWeight = (WeightCategories)(rand.Next() % 3)+1;
                drones.Add(drone);
          

            }

            for (int i = 0; i < 10; i++)
            {


                CustomerDL customer = new CustomerDL();
                customer.Id = (customers.Count) + 1;
                customer.Name = $"customer{i}";
                customer.Phone = $"{rand.Next(111111111, 999999999)}";
                customer.Latitude = rand.Next() % maxRand+1;
                customer.Longitude = rand.Next() % maxRand+1;
                customers.Add(customer);

            }

            for (int i = 0; i < 10; i++)
            {


                ParcelDL parcel = new ParcelDL();
                parcel.Id = parcels.Count + 1;
                parcel.SenderId = customers[rand.Next() % (customers.Count-1)].Id;
                parcel.TargetId = customers[rand.Next() % (customers.Count-1)].Id;
                parcel.Weight = (WeightCategories)(rand.Next() % 3) + 1;
                parcel.Pritority = (Pritorities)(rand.Next() % 3);
                parcel.Requested = RandomDate();
                parcel.DroneId = rand.Next() % (drones.Count + 1);
                parcel.Scheduled = null;
                parcel.PickedUp = null;
                parcel.Delivered = null;
                parcels.Add(parcel);


            }


        }


        /// <summary>
        /// randoms a date
        /// </summary>
        /// <returns>date</returns>
        public static DateTime RandomDate()
        {
            Random rand = new Random();
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Seconds;
            return start.AddDays(rand.Next(range));
        }
    }

}
