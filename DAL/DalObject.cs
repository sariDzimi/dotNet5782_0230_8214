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

        public static void addDrone(Drone drone)
        {
            DataSource.drones[DataSource.Config.dronesIndexer] = drone;
            DataSource.Config.dronesIndexer++;
        }

        public static void addCustomer(Customer customer)
        {
            DataSource.customers[DataSource.Config.customersIndexer] = customer;
            DataSource.Config.customersIndexer++; 
        }
        public static void addParcel(Parcel parcel)
        {
            DataSource.parcels[DataSource.Config.parcelsIndexer] = parcel;
            DataSource.Config.parcelsIndexer++;
        }
        public static  void addStation(Station station)
        {
            DataSource.stations[DataSource.Config.stationsIndexer] = station;
            DataSource.Config.dronesIndexer++;
        }
        public void addDronCharge(DroneCharge droneCharge)
        {
            DataSource.droneCharges[DataSource.Config.droneChargeIndexer] = droneCharge;
            DataSource.Config.droneChargeIndexer++;
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

        public static Drone[] GetDrones()
        {
            int length = DataSource.drones.Length;
            Drone[] newDrones = new Drone[length];
            for (int i = 0; i < length; i++)
            {
                newDrones[i] = DataSource.drones[i];
            }
            return newDrones;
        }

        public static Customer[] GetCustomer()
        {
            int length = DataSource.customers.Length;
            Customer[] newCustomer = new Customer[length];
            for (int i = 0; i < length; i++)
            {
                newCustomer[i] = DataSource.customers[i];
            }
            return newCustomer;
        }

        public static Parcel[] GetParcel()
        {
            int length = DataSource.parcels.Length;
            Parcel[] newParcel = new Parcel[length];
            for (int i = 0; i < length; i++)
            {
                newParcel[i] = DataSource.parcels[i];
            }
            return newParcel;
        }


        public static Parcel findParcel(int id)
        {
            for(int i=0; i< DataSource.parcels.Length; i++)
            {
                if (DataSource.parcels[i].Id == id)
                {
                    return DataSource.parcels[i];
                }
            }
            return DataSource.parcels[0];
        }





        public static void belongPacelToADrone(Parcel parcel)
        {


            for (int i = 0; i < DataSource.drones.Length; i++)
            {
                if (DataSource.drones[i].Status == DroneStatus.Free)
                {
                    parcel.DroneId = DataSource.drones[i].Id;
                    DataSource.drones[i].Status = DroneStatus.Delivery;
                    return;
                }
            }
            //TODO: "no available drone";
            //TODO: "no available drone";
            //TODO: "no available drone";
            //TODO: "no available drone";
        }
        
        public static void CollectAParcelByDrone(Parcel parcel)
        {
            parcel.PickedUp = DateTime.Now;
        }

        public static void DeliverParcelToCustomer(Parcel parcel)
        {
            parcel.Delivered = DateTime.Now;
        }

        public static void SendDroneForCharging(Drone drone)
        {
            drone.Status = DroneStatus.Maintenance;
            DroneCharge droneCharge = new DroneCharge();
            droneCharge.DroneId = drone.Id;

            for (int i = 0; i < DataSource.stations.Length; i++)
            {
                int ChargeSlots = 0;
                for (int j = 0; j < DataSource.droneCharges.Length; j++)
                {
                    if (DataSource.droneCharges[j].stationId == DataSource.stations[i].Id)
                        ChargeSlots++;

                }
                if (ChargeSlots < DataSource.stations[i].ChargeSlots)
                {
                    droneCharge.stationId = DataSource.stations[i].Id;
                    break;
                }
            }
            addDronCharge(droneCharge);
        }
        public void ReleaseDroneFromCharging(Drone drone)
        {
            int index = 0;
            drone.Status = DroneStatus.Free;
            for (int i = 0; i < DataSource.droneCharges.Length; i++)
            {
                if (DataSource.droneCharges[i].DroneId == drone.Id)
                    index = i;

            }
            //TODO: if not found...
            for (int i = index; i < DataSource.droneCharges.Length - 1; i++)
            {
                DataSource.droneCharges[i] = DataSource.droneCharges[i + 1];
            }
            DataSource.Config.droneChargeIndexer--;
            //DataSource.droneCharges[DataSource.Config.droneChargeIndexer] = null;
        }
    }
}





