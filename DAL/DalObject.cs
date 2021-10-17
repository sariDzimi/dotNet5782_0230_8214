using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;

namespace DalObject
{
    public class DalObject
    {
        public DalObject()
        {
            DataSource.Initialize();
        }

        public void addDrone(Drone drone)
        {
            DataSource.drones[DataSource.Config.dronesIndexer] = drone;
            DataSource.Config.dronesIndexer++;
        }

        public void addCustomer(Customer customer)
        {
            DataSource.customers[DataSource.Config.customersIndexer] = customer;
            DataSource.Config.customersIndexer++;
        }
        public void addParcel(Parcel parcel)
        {
            DataSource.parcels[DataSource.Config.parcelsIndexer] = parcel;
            DataSource.Config.parcelsIndexer++;
        }
        public void addStation(Station station)
        {
            DataSource.stations[DataSource.Config.stationsIndexer] = station;
            DataSource.Config.dronesIndexer++;
        }
        public Station[] GetStations()
        {
            int length = DataSource.stations.Length;
            Station[] newStations = new Station[length];
            for (int i = 0; i < length; i++)
            {
                newStations[i] = DataSource.stations[i];
            }
            return newStations;
        }

        public Drone[] GetDrones()
        {
            int length = DataSource.drones.Length;
            Drone[] newDrones = new Drone[length];
            for (int i = 0; i < length; i++)
            {
                newDrones[i] = DataSource.drones[i];
            }
            return newDrones;
        }

        public Customer[] GetCustomer()
        {
            int length = DataSource.customers.Length;
            Customer[] newCustomer = new Customer[length];
            for (int i = 0; i < length; i++)
            {
                newCustomer[i] = DataSource.customers[i];
            }
            return newCustomer;
        }

        public Parcel[] GetParcel()
        {
            int length = DataSource.parcels.Length;
            Parcel[] newParcel = new Parcel[length];
            for (int i = 0; i < length; i++)
            {
                newParcel[i] = DataSource.parcels[i];
            }
            return newParcel;
        }


     

    }
}





