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
        public StationBL convertToStationBL(IDAL.DO.StationDL s)
        {
            StationBL StationBL = new StationBL() { Id = s.Id, Name = s.Name, Location = new Location(s.Longitude, s.Latitude) };
            StationBL.ChargeSlots = calculateFreeChargeSlotsInStation(s.Id);
            return StationBL;
        }

        /// <summary>
        /// convert To Customer BL
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public CustomerBL convertToCustomerBL(IDAL.DO.CustomerDL c)
        {
            CustomerBL CustomerBL = new CustomerBL() { Id = c.Id, Name = c.Name, Location = new Location(c.Latitude, c.Longitude), Phone = c.Phone  };
            return CustomerBL;
        }


        /// <summary>
        /// convert To Parcel BL
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public ParcelBL convertToParcelBL(IDAL.DO.ParcelDL p)
        {
            DroneBL droneBL = new DroneBL();
            try
            { 
                droneBL = dronesBL.First(d => d.Id == p.DroneId);
                if (droneBL == null)
                {
                    throw new Exception("cant convert");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
          
          
            DroneAtParcel droneAtParcel = new DroneAtParcel() { Id = p.DroneId, Battery = droneBL.Battery, Location = droneBL.Location};
            CustomerAtParcel customerAtParcelsender1 = new CustomerAtParcel() { Id = p.SenderId, Name = dalObject.findCustomer(p.SenderId).Name };
            CustomerAtParcel customerAtParcelreciver1 = new CustomerAtParcel() { Id = p.TargetId, Name = dalObject.findCustomer(p.TargetId).Name };

            ParcelBL ParcelBL = new ParcelBL() { Id = p.Id, Delivered = p.Delivered, PickedUp = p.PickedUp, droneAtParcel = droneAtParcel, Pritority = p.Pritority, Requested = p.Requested, Scheduled = p.Scheduled, customerAtParcelSender = customerAtParcelsender1, customerAtParcelReciver = customerAtParcelreciver1, Weight = p.Weight };
            return ParcelBL;
        }

        /// <summary>
        /// calculate Free ChargeSlots In Station
        /// </summary>
        /// <param name="statioinID"></param>
        /// <returns></returns>
        public int calculateFreeChargeSlotsInStation(int statioinID)
        {
            int total = dalObject.findStation(statioinID).ChargeSlots;
            foreach (var chargeDrone in dalObject.GetDroneCharges())
            {
                if (chargeDrone.stationId == statioinID)
                    total--;
            }
            return total;
        }
    }
}
