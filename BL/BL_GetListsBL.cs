using IBL.BO;
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
        public IEnumerable<IBL.BO.Station> GetStations()
        {
            return from station in dalObject.GetStations()
                   select ConvertToStationBL(station);
           
        }

        /// <summary>
        /// Get Parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IBL.BO.Parcel> GetParcels()
        {

            return from parcel in dalObject.GetParcel()
                   select convertToParcelBL(parcel);
            
        }

        /// <summary>
        /// Get Customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IBL.BO.Customer> GetCustomers()
        {

            return from customer in dalObject.GetCustomer()
                   select convertToCustomerBL(customer);

           
        }

        /// <summary>
        /// Get Drones
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IBL.BO.Drone> GetDrones()
        {
            return from drone in dronesBL
                   select drone;

        }


        public IEnumerable<IBL.BO.DroneCharge> GetDronesCharges()
        {
            return from drone in dalObject.GetDroneCharges()
                   select ConvertToDroneChargeBL(drone);

        }

        public IEnumerable<DroneToList> GetDroneToLists()
        {
            return from drone in GetDrones()
                   select ConvertDroneToDroneToList(drone);
            
        }
    }
}



