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
        static internal List<Drone> drones;
        static internal List<Station> stations;
        static internal List<Customer> customers;
        static internal List<Parcel> parcels;
        static internal List<DroneCharge> droneCharges; 


        //static internal Drone[] drones = new Drone[10];
        //static internal Station[] stations = new Station[5];
        //static internal Customer[] customers = new Customer[100]; 
        //static internal Parcel[] parcels = new Parcel[100];
        //static internal DroneCharge[] droneCharges = new DroneCharge[0];


        internal class Config
        {
            //static internal int dronesIndexer = 0;
            //static internal int stationsIndexer = 0;
            //static internal int customersIndexer = 0;
            //static internal int parcelsIndexer = 0;
            //static internal int droneChargeIndexer = 0;
            //static internal int parcelRecognizer = 0;
        }
        static public void Initialize()
        {

            Random rand = new Random();
            for (int i = 0; i < 2; i++)
            {
                //Station station = stations[Config.stationsIndexer];
                Station station = new Station();
                
                station.Id = stations.Count+1;
                station.Name = stations.Count + 1;
                station.Latitude = rand.Next();
                station.Longitude = rand.Next();
                station.ChargeSlots = rand.Next(300);
                stations.Add(station);
               // Config.stationsIndexer++;
            }

            for (int i = 0; i < 5; i++)
            {
                //Drone drone = drones[Config.dronesIndexer];
                Drone drone = new Drone();
                drone.Id = drones.Count+1;
                drone.Model = "MarvicAir2";
                drone.MaxWeight = (WeightCategories)(rand.Next() % 3);
                drone.Status = (DroneStatus)(rand.Next() % 3);
                drone.Battery = rand.Next(100);
                drones.Add(drone);
               // Config.dronesIndexer++;
            }

            for (int i = 0; i < 10; i++)
            {

                //Customer customer = customers[Config.customersIndexer];
                Customer customer = new Customer();
                customer.Id = customers.Count+1;
                customer.Name = $"customer{i}";
                customer.Phone = $"{rand.Next(111111111, 999999999)}";
                customer.Latitude = rand.Next();
                customer.Longitude = rand.Next();
                customers.Add(customer);
               // Config.customersIndexer++;
            }

            for (int i = 0; i < 10; i++)
            {

                //Parcel parcel = parcels[Config.parcelsIndexer];
                Parcel parcel = new Parcel();
                parcel.Id = parcels.Count+1;
                parcel.SenderId = rand.Next() % parcels.Count + 1;
                parcel.TargetId = rand.Next() % parcels.Count + 1;
                parcel.Weight = (WeightCategories)(rand.Next() % 3);
                parcel.Pritority = (Pritorities)(rand.Next() % 3);
                parcel.Requested = RandomDate();
                parcel.DroneId = rand.Next() % parcels.Count() + 1;
                parcel.Scheduled = RandomDate();
                parcel.PickedUp = RandomDate();
                parcel.Delivered = RandomDate();
                parcels.Add(parcel);
             //   Config.parcelsIndexer++;

            }

           // Config.parcelRecognizer = Config.parcelsIndexer + 2;
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
