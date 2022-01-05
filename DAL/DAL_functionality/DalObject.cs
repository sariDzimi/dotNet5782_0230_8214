using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DalObject;
using DO;

namespace DalObject
{
    internal class DalObject : DalApi.IDal
    {
        internal static DalObject instance;


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

        #region Drone
        /// <summary>
        /// Adds the drone to the drones list in the DataSource
        /// If the ID alredy exist the function will throw exception
        /// </summary>
        /// <param name="drone"></param>
        public void AddDrone(Drone drone)
        {
            if (DataSource.drones.Any(dr => dr.Id == drone.Id))
            {
                throw new IdAlreadyExist(drone.Id);
            }

            DataSource.drones.Add(drone);
        }
        /// <summary>
        /// returns drones form datasource
        /// </summary>
        /// <returns>DataSource.drones</returns>
        public IEnumerable<Drone> GetDrones(Predicate<Drone> getBy = null)
        {
            getBy ??= (drone => true);
            return from drone in DataSource.drones
                   where (getBy(drone))
                   select drone;
        }
        public Drone GetDroneById(int id)
        {
            try
            {
                return GetDrones(d => d.Id == id).First();

            }
            catch
            {
                throw new NotFoundException("drone");
            }
        }

        /// <summary>
        /// updates the drones list in the database
        /// </summary>
        /// <param name="drone"></param>
        public void UpdateDrone(Drone drone)
        {
            int index = DataSource.drones.FindIndex(d => d.Id == drone.Id);
            if (index == -1)
                throw new NotFoundException("drone");
            DataSource.drones[index] = drone;
        }
        #endregion

        #region Customer
        /// <summary>
        /// adds the customer to the customers list in the DataSource
        ///  If the ID alredy exist the function will throw exception
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            if (DataSource.customers.Any(cs => cs.Id == customer.Id))
            {
                throw new IdAlreadyExist(customer.Id);
            }
            DataSource.customers.Add(customer);
        }

        /// <summary>
        /// returns customers form datasource
        /// </summary>
        /// <returns>DataSource.customers</returns>
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> getBy = null)
        {
            getBy ??= (customer => true);
            return from customer in DataSource.customers
                   where (getBy(customer))
                   select customer;
        }

        public Customer GetCustomerById(int id)
        {
            try
            {

                return GetCustomers(c => c.Id == id).First();
            }
            catch
            {
                throw new NotFoundException("customer");
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            int index = DataSource.customers.FindIndex(p => p.Id == customer.Id);
            if (index == -1)
                throw new NotFoundException("customer");
            DataSource.customers[index] = customer;

        }
        #endregion

        #region Parcel
        /// <summary>
        /// adds the parcel to the parcels list in the DataSource
        /// If the ID alredy exist the function will throw exception
        /// </summary>
        /// <param name="parcel"></param>
        public void AddParcel(Parcel parcel)
        {
            if (DataSource.parcels.Any(ps => ps.Id == parcel.Id))
            {
                throw new IdAlreadyExist(parcel.Id);
            }
            DataSource.parcels.Add(parcel);
        }

        /// <summary>
        /// returns parcel form datasource
        /// </summary>
        /// <returns>DataSource.customers</returns>
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> getBy = null)
        {
            getBy ??= (parcel => true);
            return (from parcel in DataSource.parcels
                    where (getBy(parcel) && parcel.IsActive)
                    select parcel);
        }
        public Parcel GetParcelById(int id)
        {
            try
            {

                return GetParcels(p => p.Id == id).First();
            }
            catch
            {
                throw new NotFoundException("parcel");
            }
        }

        /// <summary>
        /// updates the drones list in the database
        /// </summary>
        /// <param name="parcel"></param>
        public void UpdateParcel(Parcel parcel)
        {
            int index = DataSource.parcels.FindIndex(p => p.Id == parcel.Id);
            if (index == -1)
                throw new NotFoundException("parcel");
            DataSource.parcels[index] = parcel;

        }
        public void DeleteParcel(int id)
        {
            Parcel parcel = GetParcelById(id);
            parcel.IsActive = false;
            UpdateParcel(parcel);
        }

        #endregion

        #region Station

        /// <summary>
        /// Adds the station to the stations list in the DataSource
        /// If the ID alredy exist the function will throw exception
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(Station station)
        {
            if (DataSource.stations.Any(st => st.Id == station.Id))
            {
                throw new IdAlreadyExist(station.Id);
            }

            DataSource.stations.Add(station);

        }
        /// <summary>
        /// returns stations form datasource
        /// </summary>
        /// <returns>DataSource.stations</returns>
        public IEnumerable<Station> GetStations(Predicate<Station> getBy = null)
        {
            getBy ??= (station => true);
            return from station in DataSource.stations
                   where (getBy(station))
                   select station;
        }
        public Station GetStationById(int id)
        {
            try
            {

                return GetStations(s => s.Id == id).First();
            }
            catch
            {
                throw new NotFoundException("station");
            }
        }
        /// <summary>
        /// updates the stations list in the database
        /// </summary>
        /// <param name="station"></param>
        public void UpdateStation(Station station)
        {
            int index = DataSource.stations.FindIndex(p => p.Id == station.Id);
            if (index == -1)
                throw new NotFoundException("station");
            DataSource.stations[index] = station;
        }

        #endregion

        #region droneCharge

        /// <summary>
        /// adds the droneCharge to the droneCharges list in the DataSource
        /// If the ID alredy exist the function will throw exception
        /// </summary>
        /// <param name="droneCharge"></param>
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            if (DataSource.droneCharges.Any(dg => dg.DroneId == droneCharge.DroneId))
            {
                throw new IdAlreadyExist(droneCharge.DroneId);
            }
            DataSource.droneCharges.Add(droneCharge);

        }
        /// <summary>
        /// returns droneCharges form datasource
        /// </summary>
        /// <returns>DataSource.droneCharges</returns>
        public IEnumerable<DroneCharge> GetDroneCharges(Predicate<DroneCharge> getBy = null)
        {
            getBy ??= (DroneCharge => true);
            return from droneCharge in DataSource.droneCharges
                   where(getBy(droneCharge))
                   select droneCharge;
        }

        public DroneCharge GetDroneChargeById(int droneId)
        {
            try
            {
                return GetDroneCharges(droneCharge => droneCharge.DroneId == droneId).First();
            }
            catch
            {
                throw new NotFoundException("droneCharge");
            }
        }

        /// <summary>
        /// removes droneCharge from dronecharges list in database
        /// </summary>
        /// <param name="id"></param>
        public void DeleteDroneCharge(int droneId)
        {
            DroneCharge droneCharge = GetDroneChargeById(droneId);
            DataSource.droneCharges.Remove(droneCharge);
        }
        #endregion


        public IEnumerable<Manager> GetManagers(Predicate<Manager> getBy = null)
        {
            getBy ??= (manager => true);
            return from manager in DataSource.Managers
                   where(getBy(manager))
                   select manager;
        }

        public double[] RequestElectricityUse()
        {
            double[] Electricity = { DataSource.Config.free, DataSource.Config.light, DataSource.Config.medium, DataSource.Config.heavy, DataSource.Config.rateChargePerHour };
            return Electricity;

        }

    }

}





