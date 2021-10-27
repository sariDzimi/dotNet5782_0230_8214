using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace BLDalObject
{
    public class BalObject: BL.IBL
    {
        public BalObject()
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
        public IEnumerable<Station> GetStations()
        {
            foreach(var station in DataSource.stations)
            {
                yield return station;
            }
        }

        public IEnumerable<Drone> GetDrones()
        {
            foreach (var drone in DataSource.drones)
            {
                yield return drone;
            }
        }

        public IEnumerable<Customer> GetCustomer()
        {
            foreach (var customer in DataSource.customers)
            {
                yield return customer;
            }
        }

        public IEnumerable<Parcel> GetParcel()
        {
            foreach (var parcel in DataSource.parcels)
            {
                yield return parcel;
            }
        }
        
        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            foreach (var droneCharge in DataSource.droneCharges)
            {
                yield return droneCharge;
            }
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


        public void updateDrone(Drone drone)
        {
            int index = DataSource.drones.FindIndex(d => d.Id == drone.Id);
            DataSource.drones[index] = drone;

        }
        public void updateParcel(Parcel parcel)
        {
            int index = DataSource.parcels.FindIndex(p => p.Id == parcel.Id);
            DataSource.parcels[index] = parcel;

        }
        public void updateCustomer(Customer customer)
        {
            int index = DataSource.customers.FindIndex(p => p.Id == customer.Id);
            DataSource.customers[index] = customer;

        }

        public void updateDronecharge(DroneCharge dronecharge)
        {
            int index = DataSource.droneCharges.FindIndex(p => p.DroneId == dronecharge.DroneId);
            DataSource.droneCharges[index] = dronecharge;

        }



        public void belongPacelToADrone(Parcel parcel)
        {
            Parcel parcel1 = new Parcel();
            parcel1 = parcel;
            var drone = DataSource.drones.Find(c => c.Status == DroneStatus.Free);
            int indexDrone = DataSource.drones.FindIndex(c => c.Id == parcel.Id);
            parcel1.DroneId = drone.Id;
            drone.Status = DroneStatus.Delivery;
            updateDrone(drone);
            updateParcel(parcel1);

            //int index = DataSource.drones.FindIndex(c => c.Id == parcel.Id);
            //DataSource.parcels[index] = parcel1;

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
            updateParcel(parcel);
        }

        public void DeliverParcelToCustomer(Parcel parcel)
        {
            parcel.Delivered = DateTime.Now;
            updateParcel(parcel);
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





