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
/*        public DroneBL convertToDroneBL(IDAL.DO.DroneDL d)
        {
            DroneBL DroneBL = new DroneBL() { Id = d.Id, Model = d.Model, MaxWeight = d.MaxWeight };
            return DroneBL;
        }*/

        public StationBL convertToStationBL(IDAL.DO.StationDL s)
        {
            StationBL StationBL = new StationBL() { Id = s.Id, Name = s.Name, Location = new Location(s.Longitude, s.Latitude) };
            StationBL.ChargeSlots = calculateFreeChargeSlotsInStation(s.Id);
            return StationBL;
        }

        public CustomerBL convertToCustomerBL(IDAL.DO.CustomerDL c)
        {
            CustomerBL CustomerBL = new CustomerBL() { Id = c.Id, Name = c.Name, Location = new Location(c.Latitude, c.Longitude), Phone = c.Phone };
            return CustomerBL;
        }

        public ParcelBL convertToParcelBL(IDAL.DO.ParcelDL p)
        {
            DroneBL droneBL = dronesBL.Find(d => d.Id == p.DroneId);
            DroneAtParcel droneAtParcel = new DroneAtParcel() { Id = p.DroneId, Battery = droneBL.Battery, Location = droneBL.Location};
            CustomerAtParcel customerAtParcelsender = new CustomerAtParcel() { Id = p.SenderId, Name = dalObject.GetCustomersList().Find(c => c.Id == p.SenderId).Name };
            CustomerAtParcel customerAtParcelreciver = new CustomerAtParcel() { Id = p.TargetId, Name = dalObject.GetCustomersList().Find(c => c.Id == p.TargetId).Name };

            ParcelBL ParcelBL = new ParcelBL() { Id = p.Id, Delivered = p.Delivered, PickedUp = p.PickedUp, droneAtParcel = droneAtParcel, Pritority = p.Pritority, Requested = p.Requested, Scheduled = p.Scheduled, customerAtParcelSender = customerAtParcelsender, customerAtParcelReciver = customerAtParcelreciver, Weight = p.Weight };
            return ParcelBL;
        }

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
