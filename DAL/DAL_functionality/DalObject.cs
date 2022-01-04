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

        /// <summary>
        /// Adds the drone to the drones list in the DataSource
        /// If the ID alredy exist the function will throw exception
        /// </summary>
        /// <param name="drone"></param>
        public void addDrone(Drone drone)
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
        public void addCustomer(Customer customer)
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
        public void addParcel(Parcel parcel)
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
        public void addStation(Station station)
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
        public void addDronCharge(DroneCharge droneCharge)
        {
            if (DataSource.droneCharges.Any(dg => dg.DroneId == droneCharge.DroneId))
            {
                throw new IdAlreadyExist(droneCharge.DroneId);
            }
            DataSource.droneCharges.Add(droneCharge);

        }


        /// <summary>
        /// returns stations form datasource
        /// </summary>
        /// <returns>DataSource.stations</returns>
        public IEnumerable<Station> GetStations()
        {
            return from station in DataSource.stations
                   select station;
        }

        /// <summary>
        /// returns drones form datasource
        /// </summary>
        /// <returns>DataSource.drones</returns>
        public IEnumerable<Drone> GetDrones()
        {
            return from drone in DataSource.drones
                   select drone;
        }

        /// <summary>
        /// returns customers form datasource
        /// </summary>
        /// <returns>DataSource.customers</returns>
        public IEnumerable<Customer> GetCustomer()
        {
            return from customer in DataSource.customers
                   select customer;
        }

        /// <summary>
        /// returns customers form datasource
        /// </summary>
        /// <returns>DataSource.customers</returns>
        public IEnumerable<Parcel> GetParcel()
        {
            return from parcel in DataSource.parcels
                   where parcel.IsActive == true
                   select parcel;
        }

        public IEnumerable<Manager> GetManeger()
        {
            return from maneger in DataSource.Managers
                   select maneger;
        }

        /// <summary>
        /// returns droneCharges form datasource
        /// </summary>
        /// <returns>DataSource.droneCharges</returns>
        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            return from droneCharge in DataSource.droneCharges
                   select droneCharge;
        }

        /// <summary>
        /// returns parcel by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>parcel</returns>
        /// 


        public Parcel FindParcelBy(Predicate<Parcel> findBy)
        {

            Parcel parcelDL = new Parcel();

            try
            {
                parcelDL = (from parcel in DataSource.parcels
                            where findBy(parcel)
                            select parcel).First();
            }
            catch (Exception ex)
            {
                throw new NotFoundException($" {ex}");
            }
            return parcelDL;
        }

        public Manager findManegerBy(Predicate<Manager> findBy)
        {
            DO.Manager manager = new DO.Manager();

            try
            {
                manager = (from man in DataSource.Managers
                            where findBy(man)
                            select man).First();
            }
            catch (Exception ex)
            {
                throw new NotFoundException($"{ex}");
            }
            return manager;
        }



        public Parcel findParcelById(int id)
        {
            return FindParcelBy(p => p.Id == id && p.IsActive == true );
        }

        /// <summary>
        /// returns station by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>station</returns>
        public Station findStationBy(Predicate<Station> findBy)
        {

            Station stationDL = new Station();

            try
            {
                stationDL = (from station in DataSource.stations
                             where findBy(station)
                             select station).First();
            }
            catch (Exception ex)
            {
                throw new NotFoundException($" {ex}");
            }
            return stationDL;
        }

        public Station findStationById(int id)
        {
            return findStationBy(s => s.Id == id);
        }




        /// <summary>
        /// returns customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>customer</returns>


        public Customer findCustomerBy(Predicate<Customer> findBy)
        {

            Customer customerDL = new Customer();

            try
            {
                customerDL = (from customer in DataSource.customers
                              where findBy(customer)
                              select customer).First();
            }
            catch (Exception ex)
            {
                throw new NotFoundException($" {ex}");
            }
            return customerDL;
        }

        public Customer findCustomerById(int id)
        {
            return findCustomerBy(c => c.Id == id);
        }

        /// <summary>
        /// returns drone by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>drone</returns>


        public Drone findDroneBy(Predicate<Drone> findBy)
        {

            Drone droneDL = new Drone();

            try
            {
                droneDL = (from drone in DataSource.drones
                           where findBy(drone)
                           select drone).First();
            }
            catch (Exception ex)
            {
                throw new NotFoundException($" {ex}");
            }
            return droneDL;
        }

        public Drone findDroneById(int id)
        {
            return findDroneBy(d => d.Id == id);
        }




        /// <summary>
        /// returns droneCharge by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>droneCharge</returns>
        public DroneCharge findDroneChargeBy(Predicate<DroneCharge> findBy)
        {

            DroneCharge droneChargeDL = new DroneCharge();

            try
            {
                droneChargeDL = (from droneCharge in DataSource.droneCharges
                                 where findBy(droneCharge)
                                 select droneCharge).First();
            }
            catch (Exception ex)
            {
                throw new NotFoundException($" {ex}");
            }
            return droneChargeDL;
        }

        /// <summary>
        /// updates the drones list in the database
        /// </summary>
        /// <param name="drone"></param>
        public void updateDrone(Drone drone)
        {
            int index = DataSource.drones.FindIndex(d => d.Id == drone.Id);
            DataSource.drones[index] = drone;
        }

        /// <summary>
        /// updates the drones list in the database
        /// </summary>
        /// <param name="parcel"></param>
        public void updateParcel(Parcel parcel)
        {
            int index = DataSource.parcels.FindIndex(p => p.Id == parcel.Id);
            if (index == -1)
                throw new NotFoundException("parcel");
            DataSource.parcels[index] = parcel;

        }

        /// <summary>
        /// updates the drones list in the database
        /// </summary>
        /// <param name="customer"></param>
        public void updateCustomer(Customer customer)
        {
            int index = DataSource.customers.FindIndex(p => p.Id == customer.Id);
            DataSource.customers[index] = customer;

        }

        /// <summary>
        /// updates the stations list in the database
        /// </summary>
        /// <param name="station"></param>
        public void updateStation(Station station)
        {
            int index = DataSource.stations.FindIndex(p => p.Id == station.Id);
            DataSource.stations[index] = station;
        }

        /// <summary>
        /// updates the dronecharges list in the database
        /// </summary>
        /// <param name="dronecharge"></param>
        public void updateDronecharge(DroneCharge dronecharge)
        {
            int index = DataSource.droneCharges.FindIndex(p => p.DroneId == dronecharge.DroneId);
            DataSource.droneCharges[index] = dronecharge;

        }

        /// <summary>
        /// returns the array of the electricity use
        /// </summary>
        /// <returns>Electricity</returns>
        public double[] RequestElectricityUse()
        {
            double[] Electricity = { DataSource.Config.free, DataSource.Config.light, DataSource.Config.medium, DataSource.Config.heavy, DataSource.Config.rateChargePerHour };
            return Electricity;

        }

        /// <summary>
        /// removes droneCharge from dronecharges list in database
        /// </summary>
        /// <param name="id"></param>
        public void removeDroneCharge(int id)
        {
            DroneCharge droneChargeDL = new DroneCharge();
            try
            {
                droneChargeDL = findDroneChargeBy(i => i.DroneId == id);

            }
            catch (Exception ex)
            {
                throw new NotFoundException($"{ex}");
            }
            DataSource.droneCharges.Remove(droneChargeDL);
        }

        public void DeleteParcel(int id)
        {
            Parcel parcel = findParcelById(id);
            parcel.IsActive = false;
            updateParcel(parcel);
        }

        public IEnumerable<Parcel> GetParcelIdBy(Predicate<Parcel> findBy)
        {
            return from parcel in DataSource.parcels
                   where findBy(parcel)
                   select parcel;
        }

        public IEnumerable<Station> GetStationIdBy(Predicate<Station> findBy)
        {
            return from station in DataSource.stations
                   where findBy(station)
                   select station;
        }

    }

}





