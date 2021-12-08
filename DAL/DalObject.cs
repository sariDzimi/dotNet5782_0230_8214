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


        /// <summary>
        /// returns stations form datasource
        /// </summary>
        /// <returns>DataSource.stations</returns>
        public IEnumerable<StationDL> GetStations()
        {
            foreach (var station in DataSource.stations)
            {
                yield return station;
            }
        }

        /// <summary>
        /// returns drones form datasource
        /// </summary>
        /// <returns>DataSource.drones</returns>
        public IEnumerable<DroneDL> GetDrones()
        {
            foreach (var drone in DataSource.drones)
            {
                yield return drone;
            }
        }

        /// <summary>
        /// returns customers form datasource
        /// </summary>
        /// <returns>DataSource.customers</returns>
        public IEnumerable<CustomerDL> GetCustomer()
        {
            foreach (var customer in DataSource.customers)
            {
                yield return customer;
            }
        }

        /// <summary>
        /// returns customers form datasource
        /// </summary>
        /// <returns>DataSource.customers</returns>
        public IEnumerable<ParcelDL> GetParcel()
        {
            foreach (var parcel in DataSource.parcels)
            {
                yield return parcel;
            }
        }

        /// <summary>
        /// returns droneCharges form datasource
        /// </summary>
        /// <returns>DataSource.droneCharges</returns>
        public IEnumerable<DroneChargeDL> GetDroneCharges()
        {
            foreach (var droneCharge in DataSource.droneCharges)
            {
                yield return droneCharge;
            }
        }

        /// <summary>
        /// returns parcel by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>parcel</returns>
        /// 


        public ParcelDL FindParcelBy(Predicate<ParcelDL> findBy)
        {

            ParcelDL parcelDL = new ParcelDL();

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

        public ParcelDL findParcelById(int id)
        {
            return FindParcelBy(p => p.Id == id);
        }

        /// <summary>
        /// returns station by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>station</returns>
        public StationDL findStationBy(Predicate<StationDL> findBy)
        {

            StationDL stationDL = new StationDL();

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

        public StationDL findStationById(int id)
        {
            return findStationBy(s => s.Id == id);
        }




        /// <summary>
        /// returns customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>customer</returns>


        public CustomerDL findCustomerBy(Predicate<CustomerDL> findBy)
        {

            CustomerDL customerDL = new CustomerDL();

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

        public CustomerDL findCustomerById(int id)
        {
            return findCustomerBy(c => c.Id == id);
        }

        /// <summary>
        /// returns drone by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>drone</returns>


        public DroneDL findDroneBy(Predicate<DroneDL> findBy)
        {

            DroneDL droneDL = new DroneDL();

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

        public DroneDL findDroneById(int id)
        {
            return findDroneBy(d => d.Id == id);
        }




        /// <summary>
        /// returns droneCharge by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>droneCharge</returns>
        public DroneChargeDL findDroneChargeBy(Predicate<DroneChargeDL> findBy)
        {

            DroneChargeDL droneChargeDL = new DroneChargeDL();

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
        public void updateDrone(DroneDL drone)
        {
            int index = DataSource.drones.FindIndex(d => d.Id == drone.Id);
            DataSource.drones[index] = drone;
        }

        /// <summary>
        /// updates the drones list in the database
        /// </summary>
        /// <param name="parcel"></param>
        public void updateParcel(ParcelDL parcel)
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
        public void updateCustomer(CustomerDL customer)
        {
            int index = DataSource.customers.FindIndex(p => p.Id == customer.Id);
            DataSource.customers[index] = customer;

        }

        /// <summary>
        /// updates the stations list in the database
        /// </summary>
        /// <param name="station"></param>
        public void updateStation(StationDL station)
        {
            int index = DataSource.stations.FindIndex(p => p.Id == station.Id);
            DataSource.stations[index] = station;
        }

        /// <summary>
        /// updates the dronecharges list in the database
        /// </summary>
        /// <param name="dronecharge"></param>
        public void updateDronecharge(DroneChargeDL dronecharge)
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
            double[] Electricity = { DataSource.Config.free, DataSource.Config.light, DataSource.Config.medium,DataSource.Config.heavy, DataSource.Config.rateChargePerHour };
            return Electricity;

        }

        /// <summary>
        /// removes droneCharge from dronecharges list in database
        /// </summary>
        /// <param name="id"></param>
        public void removeDroneCharge(int id)
        {
            DroneChargeDL droneChargeDL= new DroneChargeDL();
            try
            {
                droneChargeDL = findDroneChargeBy(i => i.DroneId == id);

            }
            catch(Exception ex)
            {
                throw new NotFoundException($"{ex}");
            }
            DataSource.droneCharges.Remove(droneChargeDL);
        }



        public IEnumerable<ParcelDL> GetParcelIdBy(Predicate<ParcelDL> findBy)
        {
            return from parcel in DataSource.parcels
                   where findBy(parcel)
                   select parcel;
        }

        public IEnumerable<StationDL> GetStationIdBy(Predicate<StationDL> findBy)
        {
            return from station in DataSource.stations
                   where findBy(station)
                   select station;
        }

    }

}





