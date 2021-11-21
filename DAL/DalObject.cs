using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DalObject;
using IDAL.DO;

namespace DalObject
{
    public class DalObject : IDal.IDal
    {
        private static DalObject instance;

        public DalObject()
        {
            DataSource.Initialize();
        }

        public static DalObject GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new DalObject();
                return instance;
            }
        }

        /// <summary>
        /// Adds the drone to the drones list in the DataSource
        /// If the ID alredy exist the function will throw exception
        /// </summary>
        /// <param name="drone"></param>
        public void addDrone(DroneDL drone)
        {
            if (DataSource.drones.Any(dr => dr.Id == drone.Id))
            {
                throw new IdAlreadyExist(drone.Id);
            }

            DataSource.drones.Add(drone);

        }

        /// <summary>
        /// adds the customer to the customers list in the DataSource
        ///  If the ID alredy exist the function will throw exception
        /// </summary>
        /// <param name="customer"></param>
        public void addCustomer(CustomerDL customer)
        {
            if (DataSource.customers.Any(cs => cs.Id == customer.Id))
            {
                throw new IdAlreadyExist(customer.Id);
            }

            DataSource.customers.Add(customer);

        }

        /// <summary>
        /// adds the parcel to the parcels list in the DataSource
        /// If the ID alredy exist the function will throw exception
        /// </summary>
        /// <param name="parcel"></param>
        public void addParcel(ParcelDL parcel)
        {
            if (DataSource.parcels.Any(ps => ps.Id == parcel.Id))
            {
                throw new IdAlreadyExist(parcel.Id);
            }

            DataSource.parcels.Add(parcel);

        }

        /// <summary>
        /// Adds the station to the stations list in the DataSource
        /// If the ID alredy exist the function will throw exception
        /// </summary>
        /// <param name="station"></param>
        public void addStation(StationDL station)
        {
            if (DataSource.customers.Any(st => st.Id == station.Id))
            {
                throw new IdAlreadyExist(station.Id);
            }

            DataSource.stations.Add(station);

        }

        /// <summary>
        /// adds the droneCharge to the droneCharges list in the DataSource
        /// If the ID alredy exist the function will throw exception
        /// </summary>
        /// <param name="droneCharge"></param>
        public void addDronCharge(DroneChargeDL droneCharge)
        {
            if (DataSource.droneCharges.Any(dg => dg.DroneId == droneCharge.DroneId))
            {
                throw new IdAlreadyExist(droneCharge.DroneId);
            }
            DataSource.droneCharges.Add(droneCharge);

        }

/*        public List<StationDL> GetStationsList()
        {
            return DataSource.stations;
        }*/

/*        public List<DroneDL> GetDronesList()
        {
            return DataSource.drones;
        }*/

/*        public List<CustomerDL> GetCustomersList()
        {
            return DataSource.customers;
        }*/

/*        public List<ParcelDL> GetParcelsList()
        {
            return DataSource.parcels;
        }*/

        public IEnumerable<StationDL> GetStations()
        {
            foreach (var station in DataSource.stations)
            {
                yield return station;
            }
        }

        public IEnumerable<DroneDL> GetDrones()
        {
            foreach (var drone in DataSource.drones)
            {
                yield return drone;
            }
        }

        public IEnumerable<CustomerDL> GetCustomer()
        {
            foreach (var customer in DataSource.customers)
            {
                yield return customer;
            }
        }

        public IEnumerable<ParcelDL> GetParcel()
        {
            foreach (var parcel in DataSource.parcels)
            {
                yield return parcel;
            }
        }

        public IEnumerable<DroneChargeDL> GetDroneCharges()
        {
            foreach (var droneCharge in DataSource.droneCharges)
            {
                yield return droneCharge;
            }
        }

        public ParcelDL findParcel(int id)
        {
            ParcelDL parcel = DataSource.parcels.First(parcel => parcel.Id == id);

            if (parcel.Equals(null))
                throw new NotFoundException($"parcel number {id}");
            return parcel;
        }


        public StationDL findStation(int id)
        {

            StationDL station = DataSource.stations.Find(sat => sat.Id == id);

            if (station.Id==0)
                throw new NotFoundException($" station number{ id }");
            return station;
 

        }

        public CustomerDL findCustomer(int id)
        {

            CustomerDL customer = DataSource.customers.Find(customer => customer.Id == id);
            if (customer.Id == 0)
                throw new NotFoundException($"customer number {id}");
            return customer;


        }

        public DroneDL findDrone(int id)
        {

            DroneDL drone = DataSource.drones.Find(drone => drone.Id == id);
            if (drone.Id == 0)
                throw new NotFoundException($"drone number {id}");
          
              
            return drone;
        }

        public DroneChargeDL findDroneCharge(int id)
        {

            DroneChargeDL droneCharge = DataSource.droneCharges.Find(droneCahrge => droneCahrge.DroneId == id);
            if (droneCharge.DroneId == 0)
                throw new NotFoundException($"droneCharge number {id}");
            return droneCharge;
        }

        public void updateDrone(DroneDL drone)
        {
            int index = DataSource.drones.FindIndex(d => d.Id == drone.Id);
            DataSource.drones[index] = drone;
        }


        public void updateParcel(ParcelDL parcel)
        {
            int index = DataSource.parcels.FindIndex(p => p.Id == parcel.Id);
            DataSource.parcels[index] = parcel;

        }
        public void updateCustomer(CustomerDL customer)
        {
            int index = DataSource.customers.FindIndex(p => p.Id == customer.Id);
            DataSource.customers[index] = customer;

        }

        public void updateStation(StationDL station)
        {
            int index = DataSource.stations.FindIndex(p => p.Id == station.Id);
            DataSource.stations[index] = station;
        }
        public void updateDronecharge(DroneChargeDL dronecharge)
        {
            int index = DataSource.droneCharges.FindIndex(p => p.DroneId == dronecharge.DroneId);
            DataSource.droneCharges[index] = dronecharge;

        }

        public IEnumerable<ParcelDL> GetNotBelongedParcels()
        {
            foreach (var parcel in DataSource.parcels)
            {
                if(parcel.DroneId == 0)
                    yield return parcel;
            }
        }



        public double[] RequestElectricityUse()
        {
            double[] Electricity = { DataSource.Config.free, DataSource.Config.light, DataSource.Config.medium,DataSource.Config.heavy, DataSource.Config.rateChargePerHour };
            return Electricity;

        }

/*
        public void belongPacelToADrone(ParcelDL parcel)
        {
            ParcelDL parcel1 = new ParcelDL();
            parcel1 = parcel;
            var drone = DataSource.drones.Find(c => c.Status == DroneStatus.Free);//TODO
            int indexDrone = DataSource.drones.FindIndex(c => c.Id == parcel.Id);
            parcel1.DroneId = drone.Id;
            updateDrone(drone);
            updateParcel(parcel1);


        }*/

        public void CollectAParcelByDrone(ParcelDL parcel)
        {
            parcel.PickedUp = DateTime.Now;
            updateParcel(parcel);
        }

        public void DeliverParcelToCustomer(ParcelDL parcel)
        {
            parcel.Delivered = DateTime.Now;
            updateParcel(parcel);
        }

        public void SendDroneForCharging(DroneDL drone, StationDL station)
        {

            DroneChargeDL droneCharge = new DroneChargeDL();
            droneCharge.DroneId = drone.Id;
            droneCharge.stationId = station.Id;

            addDronCharge(droneCharge);
        }
        public void ReleaseDroneFromCharging(DroneDL drone)
        {
            int index = 0;
            
            for (int i = 0; i < DataSource.droneCharges.Count; i++)
            {
                if (DataSource.droneCharges[i].DroneId == drone.Id)
                {
                    index = i;
                    break;
                }


            }

            for (int i = index; i < DataSource.droneCharges.Count - 1; i++)
            {
                DataSource.droneCharges[i] = DataSource.droneCharges[i + 1];
            }

        }

        public IEnumerable<StationDL> GetStationsWithEmptyChargingSlots()
        {
            //TODO//////////////////////////////////////////
            foreach (var station in DataSource.stations)
            {
                yield return station;
            }
        }

        public void belongPacelToADrone(ParcelDL parcel)
        {
            ((IDal.IDal)GetInstance).belongPacelToADrone(parcel);
        }

        public void removeDroneCharge(int id)
        {
            DataSource.droneCharges.Remove(findDroneCharge(id));
        }
    }

}





