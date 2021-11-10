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
            StationBL stationBL = new StationBL() { Id = id, Name = name, Location = location, ChargeSlots = slots };
            List<DroneAtChargingBL> droneAtChargings = new List<DroneAtChargingBL>();
        }

        public void addDroneToBL(int id, int status, string model, int numberStaion)
        {
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
            CustomerBL customerBL = new CustomerBL() { Id = id, Name = name, Phone = phone, Location = location };

        }
    }
}
