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
        public void addStationToBL(int id, int name, Location location, int slots)
        {
            foreach (var item in dalObject.GetStations())
            {
                if (item.Id == id)
                {
                    throw new IdAlreadyExist(id);
                }
            }

            StationBL stationBL = new StationBL() { Id = id, Name = name, Location = location, ChargeSlots = slots };
            List<DroneAtChargingBL> droneAtChargings = new List<DroneAtChargingBL>();
        }

        public void addDroneToBL(int id, int status, string model, int numberStaion)
        {

            foreach (var item in dalObject.GetDrones())
            {
                if (item.Id == id)
                {
                    throw new IdAlreadyExist(id);
                }
            }
            DroneBL droneBL = new DroneBL() { Id = id, MaxWeight = (IDAL.DO.WeightCategories)status, Model = model };
            //TODO numberStation.
            droneBL.DroneStatus = DroneStatus.Maintenance;


            droneBL.Battery = rand.Next(20, 40);
            IDAL.DO.StationDL stationDL = new IDAL.DO.StationDL();
            stationDL = dalObject.findStation(numberStaion);
            Location location = new Location(stationDL.Longitude, stationDL.Latitude);
            droneBL.Location = location;
        }

        public void addCustomerToBL(int id, string name, string phone, Location location)

        {
            foreach (var item in dalObject.GetCustomer())
            {
                if (item.Id == id)
                {
                    throw new IdAlreadyExist(id);
                }
            }
            CustomerBL customerBL = new CustomerBL() { Id = id, Name = name, Phone = phone, Location = location };
        }

        public void addParcelToBL( int SenderId,int  reciverId, int weight, int prionity)
        {
            foreach (var item in dalObject.GetParcel())
            {
                if (item.Id == SenderId)
                {
                    throw new IdAlreadyExist(SenderId);
                }
            }

            CustomerAtParcel customerAtParcelSender = new CustomerAtParcel() { Id = SenderId };
            CustomerAtParcel customerAtParcelReciver = new CustomerAtParcel() { Id = reciverId };
            ParcelBL parcelBL = new ParcelBL() { customerAtParcelSender = customerAtParcelSender, customerAtParcelReciver = customerAtParcelReciver, Weight = (IDAL.DO.WeightCategories)weight, Pritority = (IDAL.DO.Pritorities) prionity };
        }
    }
}
