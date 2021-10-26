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
            //To do condion to check the size of the array
            //if(DataSource.Config.stationsIndexer<10)
            //Console.WriteLine($"{ DataSource.Config.dronesIndexer}");
            DataSource.drones.Add(drone);
          //  DataSource.Config.dronesIndexer++;
        }

        public void addCustomer(Customer customer)
        {
            //To do condion to check the size of the array
            //if(DataSource.Config.stationsIndexer<100)
            DataSource.customers.Add(customer);
            //DataSource.Config.customersIndexer++; 
        }
        public void addParcel(Parcel parcel)
        {
            //To do condion to check the size of the array
            //if(DataSource.Config.stationsIndexer<100)
            DataSource.parcels.Add( parcel);
            //DataSource.Config.parcelsIndexer++;
        }
        public  void addStation(Station station)
        {
            //To do condion to check the size of the array
            //if(DataSource.Config.stationsIndexer<5)
            DataSource.stations.Add(station);
            //DataSource.Config.stationsIndexer++;
        }
        public void addDronCharge(DroneCharge droneCharge)
        {
            DataSource.droneCharges.Add(droneCharge);
            //int length = DataSource.droneCharges.Length;
            //DroneCharge[] newDronechage = new DroneCharge[length + 1];
            //for (int i = 0; i < length; i++)
            //{
            //    newDronechage[i] = DataSource.droneCharges[i];
            //}
            //newDronechage[DataSource.Config.droneChargeIndexer] = droneCharge;
            //DataSource.droneCharges = newDronechage;
            //DataSource.Config.droneChargeIndexer++;
        }
        public List<Station> GetStations()
        {
            int length = DataSource.stations.Count;
            List<Station> newStations = new List<Station>();
            for (int i = 0; i < length; i++)
            {
                newStations[i] = DataSource.stations[i];
            }
            return newStations;
        }

        public List<Drone> GetDrones()
        {
            int length = DataSource.drones.Count;
            List<Drone> newDrones = new List<Drone>();
            for (int i = 0; i < length; i++)
            {
                newDrones[i] = DataSource.drones[i];
            }
            return newDrones;
        }

        public List<Customer> GetCustomer()
        {
            int length = DataSource.customers.Count;
            List<Customer> newCustomer = new List<Customer>();
            for (int i = 0; i < length; i++)
            {
                newCustomer[i] = DataSource.customers[i];
            }
            return newCustomer;
        }

        public List<Parcel> GetParcel()
        {
            int length = DataSource.parcels.Count;
            List<Parcel> newParcel = new List<Parcel>();
            for (int i = 0; i < length; i++)
            {
                newParcel[i] = DataSource.parcels[i];
            }
            return newParcel;
        }
        
        public List<DroneCharge> GetDroneCharges()
        {
            int length = DataSource.droneCharges.Count;
            List<DroneCharge> newDroneCharge = new List<DroneCharge>();
            for (int i = 0; i < length; i++)
            {
                newDroneCharge[i] = DataSource.droneCharges[i];
            }
            return newDroneCharge;
        }

        public Parcel findParcel(int id)
        {
            for (int i = 0; i < DataSource.parcels.Count; i++)
            {
                if (DataSource.parcels[i].Id == id)
                {
                    return DataSource.parcels[i];
                }
            }
            return DataSource.parcels[0];
        }

        public Station findStation(int id)
        {
            for (int i = 0; i < DataSource.stations.Count; i++)
            {
                if (DataSource.stations[i].Id == id)
                {
                    return DataSource.stations[i];
                }
            }
            return DataSource.stations[0];
        }

        public Customer findCustomer(int id)
        {
            for (int i = 0; i < DataSource.customers.Count; i++)
            {
                if (DataSource.parcels[i].Id == id)
                {
                    return DataSource.customers[i];
                }
            }
            return DataSource.customers[0];
        }

        public Drone findDrone(int id)
        {
            for (int i = 0; i < DataSource.drones.Count; i++)
            {
                if (DataSource.drones[i].Id == id)
                {
                    return DataSource.drones[i];
                }
            }
            return DataSource.drones[0];
        }


        public void belongPacelToADrone(Parcel parcel)
        {
            Parcel parcel1 = new Parcel();
            parcel1 = parcel;
            var drone = DataSource.drones.Find(c => c.Status == DroneStatus.Free);
            parcel1.DroneId = drone.Id;

            int index = DataSource.drones.FindIndex(c => c.Id == parcel.Id);
            DataSource.parcels[index] = parcel1;

            //for (int i = 0; i < DataSource.drones.Count; i++)
            //{
            //    if (DataSource.drones[i].Status == DroneStatus.Free)
            //    {
            //        parcel.DroneId = DataSource.drones[i].Id;
            //        //DataSource.drones[i].Status = DroneStatus.Delivery;
            //        return;
            //    }
            //}
            //TODO: "no available drone";
            //TODO: "no available drone";
            //TODO: "no available drone";
            //TODO: "no available drone";
        }
        
        public void CollectAParcelByDrone(Parcel parcel)
        {
            parcel.PickedUp = DateTime.Now;
        }

        public void DeliverParcelToCustomer(Parcel parcel)
        {
            parcel.Delivered = DateTime.Now;
        }

        public void SendDroneForCharging(Drone drone, Station station)
        {
            drone.Status = DroneStatus.Maintenance;
            DroneCharge droneCharge = new DroneCharge();
            droneCharge.DroneId = drone.Id;
            droneCharge.stationId = station.Id;
/*            for (int i = 0; i < DataSource.stations.Length; i++)
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
            }*/
            addDronCharge(droneCharge);
        }
        public void ReleaseDroneFromCharging(Drone drone)
        {
            int index = 0;
            drone.Status = DroneStatus.Free;
            for (int i = 0; i < DataSource.droneCharges.Count; i++)
            {
                if (DataSource.droneCharges[i].DroneId == drone.Id)
                {
                    index = i;
                    break;
                }
                   

            }
            //TODO: if not found...
            for (int i = index; i < DataSource.droneCharges.Count - 1; i++)
            {
                DataSource.droneCharges[i] = DataSource.droneCharges[i + 1];
            }
            //DataSource.Config.droneChargeIndexer--;
            //DataSource.droneCharges[DataSource.Config.droneChargeIndexer] = null;
        }
 
    }


}





