using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;


namespace BL
{
    public partial class BL
    {


        /// <summary>
        /// Get Stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> GetStations()
        {
            foreach (var station in dalObject.GetStations())
            {
                yield return convertToStationBL(station);
            }
        }

        /// <summary>
        /// Get Parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> GetParcels()
        {
            foreach (var parcel in dalObject.GetParcel())
            {
                yield return convertToParcelBL(parcel);
            }
        }

        /// <summary>
        /// Get Customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomers()
        {
            foreach (var customer in dalObject.GetCustomer())
            {
                yield return convertToCustomerBL(customer);
            }
        }

        /// <summary>
        /// Get Drones
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Drone> GetDrones()
        {
            foreach (var drone in dronesBL)
            {
                yield return drone;
            }
        }

        public IEnumerable<DroneToList> GetDroneToLists()
        {
            foreach (var drone in GetDrones())
            {
                yield return ConvertDroneToDroneToList(drone);
            }
        }
    }
}

   

