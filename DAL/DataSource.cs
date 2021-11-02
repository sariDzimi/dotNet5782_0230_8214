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
        static internal List<Drone> drones = new List<Drone>();
        static internal List<Station> stations = new List<Station>();
        static internal List<Customer> customers = new List<Customer>();
        static internal List<Parcel> parcels = new List<Parcel>();
        static internal List<DroneCharge> droneCharges = new List<DroneCharge>();


        internal class Config
        {
            static double free = 10 ;
            static double light = 30;
            static double medium = 40 ;
            static double heavy = 70;
            static double rateChargePerHour = 50;
        }
        static public void Initialize()
        {

            Random rand = new Random();
            for (int i = 0; i < 2; i++)
            {

                Station station = new Station();

                station.Id = stations.Count + 1;
                station.Name = stations.Count + 1;
                station.Latitude = rand.Next();
                station.Longitude = rand.Next();
                station.ChargeSlots = rand.Next(300);
                stations.Add(station);

            }

            for (int i = 0; i < 5; i++)
            {

                Drone drone = new Drone();
                drone.Id = drones.Count + 1;
                drone.Model = "MarvicAir2";
                drone.MaxWeight = (WeightCategories)(rand.Next() % 3);
                drones.Add(drone);

            }

            for (int i = 0; i < 10; i++)
            {


                Customer customer = new Customer();
                customer.Id = customers.Count + 1;
                customer.Name = $"customer{i}";
                customer.Phone = $"{rand.Next(111111111, 999999999)}";
                customer.Latitude = rand.Next();
                customer.Longitude = rand.Next();
                customers.Add(customer);

            }

            for (int i = 0; i < 10; i++)
            {


                Parcel parcel = new Parcel();
                parcel.Id = parcels.Count + 1;
                parcel.SenderId = rand.Next() % (parcels.Count + 1);
                parcel.TargetId = rand.Next() % (parcels.Count + 1);
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
