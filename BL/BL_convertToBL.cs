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

        /// <summary>
        /// convert To Station BL
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public Station convertToStationBL(IDAL.DO.StationDL s)
        {
            Station StationBL = new Station() { Id = s.Id, Name = s.Name, Location = new Location(s.Longitude, s.Latitude) };
            StationBL.ChargeSlots = calculateFreeChargeSlotsInStation(s.Id);
            return StationBL;
        }

        /// <summary>
        /// convert To Customer BL
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public Customer convertToCustomerBL(IDAL.DO.CustomerDL c)
        {
            Customer CustomerBL = new Customer() { Id = c.Id, Name = c.Name, Location = new Location(c.Latitude, c.Longitude), Phone = c.Phone };
            foreach (var p in dalObject.GetParcel().ToList())
            {
                if (p.SenderId == CustomerBL.Id)
                {
                    ParcelAtCustomer parcelAtCustomer = new ParcelAtCustomer() { ID = p.Id, customerAtParcel = new CustomerAtParcel() { Id = CustomerBL.Id, Name = CustomerBL.Name }, weightCategories = p.Weight, pritorities = p.Pritority, parcelStatus = ParcelStatus.created };
                    CustomerBL.parcelsSentedByCustomer.Add(parcelAtCustomer);
                }
                if (p.TargetId == CustomerBL.Id)
                {
                    ParcelAtCustomer parcelAtCustomer = new ParcelAtCustomer() { ID = p.Id, customerAtParcel = new CustomerAtParcel() { Id = CustomerBL.Id, Name = CustomerBL.Name }, weightCategories = p.Weight, pritorities = p.Pritority, parcelStatus = ParcelStatus.Collected };
                    CustomerBL.parcelsSentedToCustomer.Add(parcelAtCustomer);
                }
            }
            return CustomerBL;
        }


        /// <summary>
        /// convert To Parcel BL
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Parcel convertToParcelBL(IDAL.DO.ParcelDL p)
        {
            Drone droneBL = new Drone();
            DroneAtParcel droneAtParcel = new DroneAtParcel() { Id = p.DroneId, Battery = droneBL.Battery, Location = droneBL.Location };
            CustomerAtParcel customerAtParcelsender1 = new CustomerAtParcel() { Id = p.SenderId, Name = dalObject.findCustomerById(p.SenderId).Name };
            CustomerAtParcel customerAtParcelreciver1 = new CustomerAtParcel() { Id = p.TargetId, Name = dalObject.findCustomerById(p.TargetId).Name };

            Parcel ParcelBL = new Parcel() { Id = p.Id, Delivered = p.Delivered, PickedUp = p.PickedUp, droneAtParcel = droneAtParcel, Pritority = p.Pritority, Requested = p.Requested, Scheduled = p.Scheduled, customerAtParcelSender = customerAtParcelsender1, customerAtParcelReciver = customerAtParcelreciver1, Weight = p.Weight };
            return ParcelBL;
        }

        /// <summary>
        /// calculate Free ChargeSlots In Station
        /// </summary>
        /// <param name="statioinID"></param>
        /// <returns></returns>
        public int calculateFreeChargeSlotsInStation(int statioinID)
        {
            int total = dalObject.findStationById(statioinID).ChargeSlots;
            foreach (var chargeDrone in dalObject.GetDroneCharges())
            {
                if (chargeDrone.stationId == statioinID)
                    total--;
            }
            return total;
        }

        public DroneToList ConvertDroneToDroneToList(Drone drone)
        {
            DroneToList droneToList = new DroneToList() { Id = drone.Id, Battery = drone.Battery, DroneStatus = drone.DroneStatus, Location = drone.Location, Model = drone.Model };
            if (!drone.ParcelInDelivery.Equals(null))
                droneToList.NumberOfSendedParcel = drone.ParcelInDelivery.Id;
            else
                droneToList.NumberOfSendedParcel = 0;
            return droneToList;

        }

        public Drone ConvertDroneToListToDrone(DroneToList droneToList)
        {
            return FindDrone(droneToList.Id);
        }
    }


}
