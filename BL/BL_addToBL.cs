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
        /// add Station To BL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="slots"></param>
        public void addStationToBL(int id, int name, Location location, int slots)
        {
            foreach (var item in dalObject.GetStations().ToList())
            {
                if (item.Id == id)
                {
                    throw new IdAlreadyExist(id);
                }
            }


            StationBL stationBL = new StationBL() { Id = id, Name = name, Location = location, ChargeSlots = slots };
            List<DroneAtChargingBL> droneAtChargings = new List<DroneAtChargingBL>();
            IDAL.DO.StationDL stationDL = new IDAL.DO.StationDL() { Id = id, Name = name, ChargeSlots = slots, Latitude = location.Latitude, Longitude = location.Longitude };
            dalObject.addStation(stationDL);
        }

        /// <summary>
        /// add Drone To BL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="model"></param>
        /// <param name="numberStaion"></param>
        public void addDroneToBL(int id, int status, string model, int numberStaion)
        {
            if(status <1 || status > 3)
            {
                throw new OutOfRange("status");
            }
            foreach (var item in dalObject.GetDrones())
            {
                if (item.Id == id)
                {
                    throw new IdAlreadyExist(id);
                }
            }
            DroneBL droneBL = new DroneBL() { Id = id, MaxWeight = (IDAL.DO.WeightCategories)status, Model = model };

            StationBL stationBL = GetStations().ToList().Find(p => p.Id == numberStaion);
            if (stationBL==null)
            {
                throw new NotFound($"station number {numberStaion} to put the drone");
            }

            if (stationBL.ChargeSlots == 0)
            {
                throw new NotFound($"space in the station number {numberStaion} to put the drone");
            }
            else
            {
                DroneAtChargingBL droneAtChargingBL = new DroneAtChargingBL() { ID = id, Battery = droneBL.Battery };
                stationBL.droneAtChargings.Add(droneAtChargingBL);
                stationBL.ChargeSlots -= 1;
            }
           

            droneBL.DroneStatus = DroneStatus.Maintenance;


            droneBL.Battery = rand.Next(20, 40);
            IDAL.DO.StationDL stationDL = new IDAL.DO.StationDL();
            stationDL = dalObject.findStation(numberStaion);
            stationDL.ChargeSlots -= 1;
            Location location = new Location(stationDL.Longitude, stationDL.Latitude);
            droneBL.Location = location;
            IDAL.DO.DroneDL drone = new IDAL.DO.DroneDL() { Id = id, MaxWeight = (IDAL.DO.WeightCategories)status, Model = model };
            dalObject.addDrone(drone);
            dronesBL.Add(droneBL);
            dalObject.updateStation(stationDL);
        }

        /// <summary>
        /// add Customer To BL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="location"></param>
        public void addCustomerToBL(int id, string name, string phone, Location location)

        {
            foreach (var item in dalObject.GetCustomer().ToList())
            {
                if (item.Id == id)
                {
                    throw new IdAlreadyExist(id);
                }
            }
            CustomerBL customerBL = new CustomerBL() { Id = id, Name = name, Phone = phone, Location = location };
            IDAL.DO.CustomerDL customer = new IDAL.DO.CustomerDL() { Id = id, Latitude = location.Latitude, Longitude = location.Longitude, Name = name, Phone = phone };
            dalObject.addCustomer(customer);
        }

        /// <summary>
        /// add Parcel To BL
        /// </summary>
        /// <param name="SenderId"></param>
        /// <param name="reciverId"></param>
        /// <param name="weight"></param>
        /// <param name="prionity"></param>
        public int addParcelToBL(int SenderId, int reciverId, int weight, int prionity)
        {
            if(weight <1 || weight > 3)
            {
                throw new OutOfRange("weight");
            }

            if (prionity < 1 || prionity > 3)
            {
                throw new OutOfRange("Pritorities");
            }

            bool flag;
            int id;
            do
            {
                flag = false;
                id = rand.Next() % ((dalObject.GetParcel().ToList()).Count + 10);
                foreach (var item in dalObject.GetParcel().ToList())
                {
                    if (item.Id == id)
                    {
                        flag = true;
                        break;
                    }

                }
            } while (flag == true);


           

            CustomerAtParcel customerAtParcelSender = new CustomerAtParcel() { Id = SenderId };
            CustomerAtParcel customerAtParcelReciver = new CustomerAtParcel() { Id = reciverId };
            ParcelBL parcelBL = new ParcelBL() {Id=id, customerAtParcelSender = customerAtParcelSender, customerAtParcelReciver = customerAtParcelReciver, Weight = (IDAL.DO.WeightCategories)weight, Pritority = (IDAL.DO.Pritorities)prionity };
            IDAL.DO.ParcelDL parcelDL = new IDAL.DO.ParcelDL() {Id= id, SenderId = SenderId, TargetId = reciverId, Weight = (IDAL.DO.WeightCategories)weight, Pritority = (IDAL.DO.Pritorities)prionity };
            dalObject.addParcel(parcelDL);
            return id;
        }
    }
}
