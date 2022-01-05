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
        /// add Drone To DL
        /// </summary>
        /// <param name="drone"></param>
        /*  public void addDroneToDL(Drone drone)
          {
              foreach (var item in dalObject.GetStations().ToList())
              {
                  if (item.Id == drone.Id)
                  {
                      throw new IdAlreadyExist(drone.Id);
                  }
              }


              IDAL.DO.Drone droneDL = new IDAL.DO.Drone() { Id = drone.Id, MaxWeight = (IDAL.DO.WeightCategories)drone.MaxWeight, Model = drone.Model };
              dalObject.addDrone(droneDL);
  */

        /// <summary>
        /// add Station To DL
        /// </summary>
        /// <param name="station"></param>
        public void addStationToDL(Station station)
        {
            DO.Station stationDL = new DO.Station()
            {
                Id = station.Id,
                Name = station.Name,
                ChargeSlots = station.FreeChargeSlots,
                Longitude = station.Location.Longitude,
                Latitude = station.Location.Latitude
            };
            try
            {
                dalObject.AddStation(stationDL);
            }
            catch (DAL.IdAlreadyExist)
            {
                throw new IdAlreadyExist(station.Id);
            }

        }

        /// <summary>
        /// add Parcel To DL
        /// </summary>
        /// <param name="parcel"></param>
        public void addParcelToDL(Parcel parcel)
        {
            DO.Parcel parcelDL = new DO.Parcel()
            {
                Id = parcel.Id,
                SenderId = parcel.customerAtParcelSender.Id,
                TargetId = parcel.customerAtParcelReciver.Id,
                Weight = (DO.WeightCategories)parcel.Weight,
                Pritority = (DO.Pritorities)parcel.Pritority,
                Requested = parcel.Requested == null ? null : parcel.Requested,
                DroneId = parcel.droneAtParcel == null ? 0 : parcel.droneAtParcel.Id,
                Scheduled = parcel.Scheduled == null ? null : parcel.Scheduled,
                Delivered = parcel.Delivered == null ? null : parcel.Delivered,
                PickedUp = parcel.PickedUp == null ? null : parcel.PickedUp
            };
            try
            {
                dalObject.AddParcel(parcelDL);
            }
            catch (DAL.IdAlreadyExist)
            {
                throw new IdAlreadyExist(parcel.Id);
            }

        }


        /// <summary>
        /// add Customer To DL
        /// </summary>
        /// <param name="customer"></param>
        public void addCustomerToDL(Customer customer)
        {
            DO.Customer customerDL = new DO.Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Longitude = customer.Location.Longitude,
                Phone = customer.Phone,
                Latitude = customer.Location.Latitude
            };
            try
            {
                dalObject.AddCustomer(customerDL);
            }
            catch (DAL.IdAlreadyExist)
            {
                throw new IdAlreadyExist(customer.Id);
            }
        }
    }

    /// <summary>
    /// add DroneCharge To DL
    /// </summary>
    /// <param name="droneCharge"></param>
    /*        public void addDroneChargeToDL(DroneCharge droneCharge)
            {

                IDAL.DO.DroneCharge droneChargeDL = new IDAL.DO.DroneCharge() { DroneId = droneCharge.DroneId, stationId = droneCharge.stationId };
                dalObject.addDronCharge(droneChargeDL);

            }*/
}

