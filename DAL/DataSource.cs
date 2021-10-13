using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    class DataSource
    {
        static internal Drone[] drones = new Drone[10];
        static internal Station[] stations = new Station[5];
        static internal Customer[] customers = new Customer[100];
        static internal Parcel[] parcels = new Parcel[100];
        internal class Config
        {
            static internal int dronesIndexer = 0;
            static internal int stationsIndexer = 0;
            static internal int customersIndexer = 0;
            static internal int parcelsIndexer = 0;
            static internal int parcelRecognizer = 0;

           
           
        }
        static public void Initialize()
        {
            Random rand = new Random();
            for (int i = 0; i < 2; i++)
            {
                Station station = stations[Config.stationsIndexer];
                station.Id = Config.stationsIndexer + 1;
                station.Name = Config.stationsIndexer + 1;
                station.Latitude = rand.Next();
                station.Longitude = rand.Next();
                station.ChargeSlots = rand.Next(300);
                Config.stationsIndexer++;
            }

            for (int i = 0; i < 5; i++)
            {
                Drone drone = drones[Config.dronesIndexer];
                drone.Id = Config.dronesIndexer + 1;
                drone.Model = "MarvicAir2";
                drone.MaxWeight = (WeightCategories)(rand.Next() % 3);
                drone.Status = (DroneStatus)(rand.Next() % 3);
                drone.Battery = rand.Next(100);
                Config.dronesIndexer++;
            }

            for (int i = 0; i < 10; i++)
            {
                Customer customer = customers[Config.customersIndexer];
                customer.Id = Config.customersIndexer + 1;
                customer.Name = $"customer{i}";
                customer.Phone = $"{rand.Next(111111111, 999999999)}";
                customer.Latitude = rand.Next();
                customer.Longitude = rand.Next();
                Config.customersIndexer++;
            }

            for (int i = 0; i < 10; i++)
            {
                Parcel parcel = parcels[Config.parcelsIndexer];
                parcel.Id = Config.parcelsIndexer + 1;
                parcel.SenderId = rand.Next() % Config.customersIndexer;
                parcel.TargetId = rand.Next() % Config.stationsIndexer;
                parcel.Weight = (WeightCategories)(rand.Next() % 3);
                parcel.Pritority = (Pritorities)(rand.Next() % 3);
                parcel.Requested = new DateTime(rand.Next(12), rand.Next(24), rand.Next(30), rand.Next(60), rand.Next(60), rand.Next(60));
                parcel.DroneId = rand.Next() % Config.dronesIndexer;
                parcel.Scheduled = new DateTime(rand.Next(12), rand.Next(24), rand.Next(30), rand.Next(60), rand.Next(60), rand.Next(60));
                parcel.PickedUp = new DateTime(rand.Next(12), rand.Next(24), rand.Next(30), rand.Next(60), rand.Next(60), rand.Next(60));
                parcel.Delivered = new DateTime(rand.Next(12), rand.Next(24), rand.Next(30), rand.Next(60), rand.Next(60), rand.Next(60));
                Config.parcelsIndexer++;

            }

            Config.parcelRecognizer = Config.parcelsIndexer + 2;

        }
    }
}
