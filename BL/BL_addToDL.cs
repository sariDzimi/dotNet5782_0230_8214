using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    public partial class BL
    {
        public void addDroneToDL(DroneBL drone)
        {
            IDAL.DO.DroneDL droneDL = new IDAL.DO.DroneDL() { Id = drone.Id, MaxWeight = drone.MaxWeight, Model = drone.Model };
            dalObject.addDrone(droneDL);

        }

        public void addStationToDL(StationBL station)
        {
            IDAL.DO.StationDL stationDL = new IDAL.DO.StationDL() { Id = station.Id, Name = station.Name, Longitude = station.Location.Longitude, ChargeSlots = station.ChargeSlots, Latitude = station.Location.Latitude };
            dalObject.addStation(stationDL);

        }

        public void addParcelToDL(ParcelBL parcel)
        {
            IDAL.DO.ParcelDL parcelDL = new IDAL.DO.ParcelDL()
            {
                Id = parcel.Id,
                SenderId = parcel.customerAtParcelSender.Id,
                TargetId = parcel.customerAtParcelReciver.Id,
                Weight = parcel.Weight,
                Pritority = parcel.Pritority,

                Requested = parcel.Requested,
                DroneId = parcel.droneAtParcel.Id,
                Scheduled = parcel.Scheduled,
                Delivered = parcel.Delivered,
                PickedUp = parcel.PickedUp
            };
            dalObject.addParcel(parcelDL);

        }

        public void addCustomerToDL(CustomerBL customer)
        {
            IDAL.DO.CustomerDL customerDL = new IDAL.DO.CustomerDL() { Id = customer.Id, Name = customer.Name, Longitude = customer.Location.Longitude, Phone = customer.Phone, Latitude = customer.Location.Latitude };
            dalObject.addCustomer(customerDL);

        }

        public void addDroneChargeToDL(DroneChargeBL droneCharge)
        {
            IDAL.DO.DroneChargeDL droneChargeDL = new IDAL.DO.DroneChargeDL() { DroneId = droneCharge.DroneId, stationId = droneCharge.stationId };
            dalObject.addDronCharge(droneChargeDL);

        }
    }
}
