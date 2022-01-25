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

        ///// <summary>
        ///// convert To Station BL
        ///// </summary>
        ///// <param name="s"></param>
        ///// <returns></returns>
        //private Station ConvertToStationBL(DO.Station s)
        //{
        //    Station StationBL = new Station() { Id = s.Id, Name = s.Name, Location = new Location(s.Longitude, s.Latitude) };
        //    StationBL.FreeChargeSlots = calculateFreeChargeSlotsInStation(s.Id);
        //    foreach (var dronecharge in dalObject.GetDroneCharges())
        //    {
        //        if (dronecharge.stationId == s.Id)
        //            StationBL.droneAtChargings.Add(new DroneAtCharging() { ID = dronecharge.DroneId, Battery = GetDroneById(dronecharge.DroneId).Battery });
        //    }
        //    return StationBL;
        //}

        ///// <summary>
        ///// convert To Customer BL
        ///// </summary>
        ///// <param name="c"></param>
        ///// <returns></returns>
        //private Customer convertToCustomerBL(DO.Customer c)
        //{
        //    Customer CustomerBL = new Customer() { Id = c.Id, Name = c.Name, Location = new Location(c.Latitude, c.Longitude), Phone = c.Phone };

        //    foreach (var p in GetParcels())
        //    {
        //        if (p.customerAtParcelSender.Id == CustomerBL.Id)
        //        {
        //            ParcelAtCustomer parcelAtCustomer = new ParcelAtCustomer() { ID = p.Id, CustomerAtParcel = new CustomerAtParcel() { Id = CustomerBL.Id, Name = CustomerBL.Name }, WeightCategories = p.Weight, Pritorities = p.Pritority, ParcelStatus = ParcelsStatus(p) };
        //            CustomerBL.parcelsSentedByCustomer.Add(parcelAtCustomer);
        //        }
        //        if (p.customerAtParcelReciver.Id == CustomerBL.Id)
        //        {
        //            ParcelAtCustomer parcelAtCustomer = new ParcelAtCustomer() { ID = p.Id, CustomerAtParcel = new CustomerAtParcel() { Id = CustomerBL.Id, Name = CustomerBL.Name }, WeightCategories = p.Weight, Pritorities = p.Pritority, ParcelStatus = ParcelsStatus(p) };
        //            CustomerBL.parcelsSentedToCustomer.Add(parcelAtCustomer);
        //        }
        //    }
        //    return CustomerBL;
        //}

        ///// <summary>
        ///// convert To Parcel BL
        ///// </summary>
        ///// <param name="p"></param>
        ///// <returns></returns>
        //private Parcel convertToParcelBL(DO.Parcel p)
        //{
        //    Drone droneBL = new Drone();
        //    DroneAtParcel droneAtParcel;
        //    if (p.DroneId == 0)
        //    {
        //        droneAtParcel = null;
        //    }
        //    else
        //    {
        //        droneAtParcel = new DroneAtParcel() { Id = p.DroneId };
        //    }
        //    //DroneAtParcel droneAtParcel = new DroneAtParcel() { Id = p.DroneId, Battery = droneBL.Battery, Location = droneBL.Location };
        //    CustomerAtParcel customerAtParcelsender1 = new CustomerAtParcel() { Id = p.SenderId, Name = dalObject.GetCustomerById(p.SenderId).Name };
        //    CustomerAtParcel customerAtParcelreciver1 = new CustomerAtParcel() { Id = p.TargetId, Name = dalObject.GetCustomerById(p.TargetId).Name };

        //    Parcel ParcelBL = new Parcel() { Id = p.Id, Delivered = p.Delivered, PickedUp = p.PickedUp, droneAtParcel = droneAtParcel, Pritority = (BO.Pritorities)p.Pritority, Requested = p.Requested, Scheduled = p.Scheduled, customerAtParcelSender = customerAtParcelsender1, customerAtParcelReciver = customerAtParcelreciver1, Weight = (BO.WeightCategories)p.Weight };
        //    return ParcelBL;
        //}

        /// <summary>
        /// calculate Free ChargeSlots In Station
        /// </summary>
        /// <param name="statioinID"></param>
        /// <returns></returns>
        private int calculateFreeChargeSlotsInStation(int statioinID)
        {
            int total = dal.GetStationById(statioinID).ChargeSlots;
            foreach (var chargeDrone in dal.GetDroneCharges())
            {
                if (chargeDrone.stationId == statioinID)
                    total--;
            }
            return total;
        }

        //public DroneToList ConvertDroneToDroneToList(Drone drone)
        //{
        //    DroneToList droneToList = new DroneToList() { Id = drone.Id, Battery = drone.Battery, DroneStatus = drone.DroneStatus, Location = drone.Location, Model = drone.Model };
        //    if (!(drone.ParcelInDelivery == null))
        //        droneToList.NumberOfSendedParcel = drone.ParcelInDelivery.Id;
        //    else
        //        droneToList.NumberOfSendedParcel = 0;
        //    return droneToList;

        //}

        //public Drone ConvertDroneToListToDrone(DroneToList droneToList)
        //{
        //    return GetDroneById(droneToList.Id);
        //}

        //public Parcel ConvertParcelToListToParcel(ParcelToList parcelToList)
        //{
        //    return GetParcelById(parcelToList.ID);
        //}

        //private StationToList convertStationToStationToList(Station station)
        //{
        //    StationToList stationToList = new StationToList()
        //    {
        //        ID = station.Id,
        //        Name = station.Name,
        //        numberOfFreeChargeSlots = station.FreeChargeSlots,
        //        numberOfUsedChargeSlots = dalObject.GetStationById(station.Id).ChargeSlots - station.FreeChargeSlots
        //    };
        //    return stationToList;
        //}

        private ParcelStatus ParcelsStatus(Parcel parcel)
        {
            ParcelStatus parcelStatus = ParcelStatus.Requested;
            if (parcel.Requested != null)
                parcelStatus = ParcelStatus.Requested;
            if (parcel.Scheduled != null)
                parcelStatus = ParcelStatus.Scheduled;
            if (parcel.PickedUp != null)
                parcelStatus = ParcelStatus.PickedUp;
            if (parcel.Delivered != null)
                parcelStatus = ParcelStatus.Delivered;
            return parcelStatus;

        }

        //private DroneCharge ConvertToDroneChargeBL(DO.DroneCharge droneChargeDL)
        //{
        //    return new DroneCharge(droneChargeDL.DroneId, droneChargeDL.stationId);
        //}

        //public ParcelToList convertParcelToParcelToList(Parcel parcel)
        //{
        //    return new ParcelToList()
        //    {
        //        ID = parcel.Id,
        //        NameOfCustomerReciver = parcel.customerAtParcelReciver.Name,
        //        NameOfCustomerSended = parcel.customerAtParcelSender.Name,
        //        parcelStatus = ParcelsStatus(parcel),
        //        pritorities = parcel.Pritority,
        //        weightCategories = parcel.Weight
        //    };
        //}
        //public CustomerToList convertCustomerToCustomerToList(Customer customer)
        //{
        //    return new CustomerToList()
        //    {
        //        Id = customer.Id,
        //        Name = customer.Name,
        //        Phone = customer.Phone,
        //        NumberOfParcelsInTheWayToCutemor = customer.parcelsSentedToCustomer.Count(p => p.ParcelStatus == ParcelStatus.PickedUp),
        //        NumberOfRecievedParcels = customer.parcelsSentedToCustomer.Count(p => p.ParcelStatus == ParcelStatus.Delivered),
        //        NumberOfParcelsSendedAndNotProvided = customer.parcelsSentedByCustomer.Count(p => p.ParcelStatus == ParcelStatus.PickedUp),
        //        NumberOfParcelsSendedAndProvided = customer.parcelsSentedByCustomer.Count(p => p.ParcelStatus == ParcelStatus.Delivered)
        //    };
        //}
    }


}
