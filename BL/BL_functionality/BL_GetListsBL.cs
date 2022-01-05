using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    partial class BL
    {

        /// <summary>
        /// Get Stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> GetStations()
        {
            return from station in dalObject.GetStations()
                   select ConvertToStationBL(station);
           
        }

        /// <summary>
        /// Get Parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> GetParcels()
        {

            return from parcel in dalObject.GetParcels()
                   select convertToParcelBL(parcel);
            
        }

        /// <summary>
        /// Get Customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomers()
        {

            return from customer in dalObject.GetCustomers()
                   select convertToCustomerBL(customer);

           
        }

        /// <summary>
        /// Get Drones
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Drone> GetDrones()
        {
            return from drone in dronesBL
                   select drone;

        }


        public IEnumerable<DroneCharge> GetDronesCharges()
        {
            return from drone in dalObject.GetDroneCharges()
                   select ConvertToDroneChargeBL(drone);

        }

        public IEnumerable<DroneToList> GetDroneToLists()
        {
            return from drone in GetDrones()
                   select ConvertDroneToDroneToList(drone);
            
        }

        public IEnumerable<StationToList> GetStationToLists()
        {
            return from station in GetStations()
                   select convertStationToStationToList(station);

        }

        public IEnumerable<StationToList> GetStationToListBy(Predicate<StationToList> findBy)
        {
            return from station in GetStationToLists()
                   where findBy(station)
                   select station;
                   
        }

        public IEnumerable<ParcelToList> GetParcelToLists()
        {
            return from parcel in GetParcels()
                   select convertParcelToParcelToList(parcel);

        }
        public IEnumerable<CustomerToList> GetCustomerToLists()
        {
            return from customer in GetCustomers()
                   select convertCustomerToCustomerToList(customer);

        }
    }
}



