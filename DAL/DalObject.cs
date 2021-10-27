using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;

namespace DalObject
{
    public class DalObject: IDal.IDal
    {
        public DalObject()
        {
            DataSource.Initialize();
        }

        public void addDrone(Drone drone)
        {
            
            DataSource.drones.Add(drone);
         
        }

        public void addCustomer(Customer customer)
        {
           
            DataSource.customers.Add(customer);
          
        }
        public void addParcel(Parcel parcel)
        {
            
            DataSource.parcels.Add( parcel);
         
        }
        public  void addStation(Station station)
        {
          
            DataSource.stations.Add(station);
          
        }
        public void addDronCharge(DroneCharge droneCharge)
        {
            DataSource.droneCharges.Add(droneCharge);
           
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
            throw new NotImplementedException();
        }

        public void CollectAParcelByDrone(Parcel parcel)
        {
            throw new NotImplementedException();
        }

        public void DeliverParcelToCustomer(Parcel parcel)
        {
            throw new NotImplementedException();
        }

        public void SendDroneForCharging(Drone drone, Station station)
        {
            throw new NotImplementedException();
        }

        public void ReleaseDroneFromCharging(Drone drone)
        {
            throw new NotImplementedException();
        }
    }


}





