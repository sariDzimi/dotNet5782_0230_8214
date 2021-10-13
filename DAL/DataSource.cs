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

            static Random rand = new Random();
            static public void Initialize()
            {
                for (int i = 0; i < 2; i++)
                {
                    stations[stationsIndexer].Id = stationsIndexer + 1;
                    stations[stationsIndexer].Name = stationsIndexer + 1;
                    stations[stationsIndexer].Latitude = rand.Next();
                    stations[stationsIndexer].Longitude = rand.Next();
                    stations[stationsIndexer].ChargeSlots = rand.Next(300);
                    stationsIndexer++;
                }

                for (int i = 0; i < 5; i++)
                {
                    drones[dronesIndexer].Id = dronesIndexer + 1;
                    drones[dronesIndexer].Model = "MarvicAir2";
                    drones[dronesIndexer].MaxWeight = WeightCategories.Heavy + i;
                    drones[dronesIndexer].Status = DroneStatus.Delivery + i;
                    drones[dronesIndexer].Battery = rand.Next(100);
                    dronesIndexer++;
                }

                for(int i = 0; i < 10; i++)
                {
                    customers[customersIndexer].Id = customersIndexer + 1;
                    customers[customersIndexer].Name = $"customer{i}";
                    customers[customersIndexer].Phone = $"{rand.Next(111111111, 999999999)}";
                    customers[customersIndexer].Latitude = rand.Next();
                    customers[customersIndexer].Longitude = rand.Next();
                    customersIndexer++;
                }

                for (int i = 0; i < 10; i++)
                {
                    parcels[parcelsIndexer].Id = parcelsIndexer + 1;
                    parcels[parcelsIndexer].SenderId = rand.Next()%customersIndexer;
                    parcels[parcelsIndexer].TargetId = rand.Next()%stationsIndexer;
                    parcels[parcelsIndexer].Weight = WeightCategories.Heavy + i;
                    parcels[parcelsIndexer].Pritority = Pritorities.Emergency + i;
                    parcels[parcelsIndexer].Requested = new DateTime(rand.Next(12), rand.Next(24), rand.Next(30), rand.Next(60), rand.Next(60), rand.Next(60));
                    parcels[parcelsIndexer].DroneId = rand.Next() % dronesIndexer;
                    parcels[parcelsIndexer].Scheduled = new DateTime(rand.Next(12), rand.Next(24), rand.Next(30), rand.Next(60), rand.Next(60), rand.Next(60));
                    parcels[parcelsIndexer].PickedUp = new DateTime(rand.Next(12), rand.Next(24), rand.Next(30), rand.Next(60), rand.Next(60), rand.Next(60));
                    parcels[parcelsIndexer].Delivered = new DateTime(rand.Next(12), rand.Next(24), rand.Next(30), rand.Next(60), rand.Next(60), rand.Next(60)); ;
                    parcelsIndexer++;
                    
                }

            }
        }
    }
}
