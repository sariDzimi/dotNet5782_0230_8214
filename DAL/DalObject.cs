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
        public ParcelDL findParcel(int id)
        {
            ParcelDL parcel;
            try
            {
                parcel = DataSource.parcels.First(parcel => parcel.Id == id);
            }
            catch (Exception)
            {
                throw new NotFoundException($"parcel number{id}");
            }

            return parcel;
        }

        /// <summary>
        /// returns station by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>station</returns>
        public StationDL findStation(int id)
        {

            StationDL station;
            try
            {
                station = DataSource.stations.First(sat => sat.Id == id);
            }
            catch (Exception)
            {
                throw new NotFoundException($"starion number{ id }");
            }
            return station;
        }

        /// <summary>
        /// returns customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>customer</returns>
        public CustomerDL findCustomer(int id)
        {

            CustomerDL customer;
            try
            {
                customer = DataSource.customers.Find(customer => customer.Id == id);
            }
            catch (Exception)
            {

                throw new NotFoundException($"customer number {id}");
            }
            return customer;
        }

        /// <summary>
        /// returns drone by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>drone</returns>
        public DroneDL findDrone(int id)
        {
            DroneDL drone;
            try
            {
                drone = DataSource.drones.First(drone => drone.Id == id);
            }
            catch (Exception)
            {
                throw new NotFoundException($"drone number{id} ");
            }
            return drone;
        }

        /// <summary>
        /// returns droneCharge by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>droneCharge</returns>
        public DroneChargeDL findDroneCharge(int id)
        {

            DroneChargeDL droneCharge;
            try
            {
                droneCharge = DataSource.droneCharges.First(droneCahrge => droneCahrge.DroneId == id);
            }
            catch (Exception)
            {
                throw new NotFoundException($"droneCharge number{id}");
            }
            return droneCharge;
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
            DataSource.droneCharges.Remove(findDroneCharge(id));
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





