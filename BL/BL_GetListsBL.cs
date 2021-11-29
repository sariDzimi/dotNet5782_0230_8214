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
        public IEnumerable<StationBL> GetStations()
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
        public IEnumerable<ParcelBL> GetParcels()
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
        public IEnumerable<CustomerBL> GetCustomers()
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
        public IEnumerable<DroneBL> GetDrones()
        {
            foreach (var drone in dronesBL)
            {
                yield return drone;
            }
        }
    }
}

   

