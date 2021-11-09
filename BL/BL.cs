using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using DalObject;




namespace BL
{
    public class BL 
    {
        public List<DroneBL> dronesBL;
        public void addDroneToDL(DroneBL drone)
        {
            IDAL.DO.DroneDL droneDL = new IDAL.DO.DroneDL() { Id = drone.Id, MaxWeight = drone.MaxWeight, Model = drone.Model };
            dalObject.addDrone(droneDL);

        }

        public void addStationToDL(StationBL station)
        {
            IDAL.DO.StationDL stationDL = new IDAL.DO.StationDL() { Id = station.Id, Name = station.Name, Longitude = station.Longitude, ChargeSlots = station.ChargeSlots, Latitude= station.Latitude };
            dalObject.addStation(stationDL);

        }

        public void addParcelToDL(ParcelBL parcel)
        {
            IDAL.DO.ParcelDL parcelDL = new IDAL.DO.ParcelDL() { Id = parcel.Id, SenderId = parcel.SenderId, TargetId = parcel.TargetId, Weight = parcel.Weight, Pritority = parcel.Pritority,
           
                Requested = parcel.Requested, DroneId =parcel.DroneId, Scheduled = parcel.Scheduled, Delivered = parcel.Delivered , PickedUp = parcel.PickedUp};
            dalObject.addParcel(parcelDL);

        }

        public void addCustomerToDL(CustomerBL station)
        {
            IDAL.DO.CustomerDL customerDL = new IDAL.DO.CustomerDL() { Id = station.Id, Name = station.Name, Longitude = station.Longitude, Phone = station.Phone, Latitude = station.Latitude };
            dalObject.addCustomer(customerDL);

        }


        public void addCustomerToDL(DroneChargeBL droneCharge)
        {
            IDAL.DO.DroneChargeDL droneChargeDL = new IDAL.DO.DroneChargeDL() { DroneId = droneCharge.DroneId, stationId = droneCharge.stationId };
            dalObject.addDronCharge(droneChargeDL);

        }

        public void addStationToBL(int id, int name, Location location)
        {
            StationBL stationBL = new StationBL() { Id = id, Name = name, LocationStation = location };
            List<DroneAtChargingBL> droneAtChargings = new List<DroneAtChargingBL>();
        }

        public void updateDroneModel(int id, string model)
        {
            
            IDAL.DO.DroneDL droneDL = dalObject.GetDronesList().First(d => d.Id == id);
            droneDL.Model = model;
            dalObject.updateDrone(droneDL);

            DroneBL droneBL = dronesBL.First(d => d.Id == id);
            droneBL.Model = model;
            updateDrone(droneBL);
        }

        public void updateDrone(DroneBL drone)
        {
            int index = dronesBL.FindIndex(d => d.Id == drone.Id);
            dronesBL[index] = drone;
        }




        public int DroneId { get; set; }
        public int stationId { get; set; }




        DalObject.DalObject dalObject;
        public BL()
        {
            IDal.IDal dalObject = new DalObject.DalObject();
            double[] ElectricityUse = dalObject.RequestElectricityUse();
            double ElectricityUseWhenFree = ElectricityUse[0];
            double ElectricityUseWhenLight = ElectricityUse[1];
            double ElectricityUseWhenMedium = ElectricityUse[2];
            double ElectricityUseWhenheavy = ElectricityUse[3];
            double RateOfCharching = ElectricityUse[4];

            //st<Drone> drones = dalObject.GetDrones();
            //drones.ForEach(e=> e.)

            List <IDAL.DO.DroneDL> dronesDL = dalObject.GetDronesList();

            dronesBL = new List<DroneBL>();

            Random rand = new Random();
            foreach (var drone in dronesDL)
           {
                DroneBL droneBL = new DroneBL();
                droneBL = convertToDroneBL(drone);

                //if(אם יש חבילהות שעוד לא סופקו אך הרחפן כבר שויך)
                //מצב הרחפן יהיה כמבצע משלוח ○
                /*            מיקום הרחפן יהיה כדלקמן: ○
                אם החבילה שויכה אך לא נאספה -מיקום יהיה ■
                בתחנה הקרובה לשולח
                אם החבילה נאספה אך עוד לא סופקה -מיקום ■
                הרחפן יהיה במיקום השולח
                מצב סוללה יוגרל בין טעינה מינימלית שתאפשר לרחפן ○
                לבצע את המשלוח ולהגיע לטעינה לתחנה הקרובה ליעד
                המשלוח לבין טעינה מלאה
                */
                //else
                droneBL.DroneStatus = (DroneStatus)rand.Next(0, 2);
                if(droneBL.DroneStatus == DroneStatus.Maintenance)
                {
                    int length = dalObject.GetStationsList().Count;
                    IDAL.DO.StationDL stationDL = dalObject.GetStationsList()[rand.Next(0, length)];
                    StationBL stationBL = convertToStationBL(stationDL);
                    droneBL.Location = stationBL.Location;
                    droneBL.Battery = rand.Next(0, 20);
                }
                if(droneBL.DroneStatus == DroneStatus.Free)
                {
                    //מיקומו יוגרל בין לקוחות שיש חבילות שסופקו להם//TODO

                }
                dronesBL.Add(droneBL);

            }

        }
        public DroneBL convertToDroneBL(IDAL.DO.DroneDL d)
        {
            DroneBL DroneBL = new DroneBL() { Id = d.Id, Model = d.Model, MaxWeight = d.MaxWeight};
            return DroneBL;
        }

        public StationBL convertToStationBL(IDAL.DO.StationDL s)
        {
            StationBL StationBL = new StationBL() { Id = s.Id, Name = s.Name, Location = new Location(s.Longitude, s.Latitude)};
            return StationBL;
        }

        public CustomerBL convertToCustomerBL(IDAL.DO.CustomerDL c)
        {
            CustomerBL CustomerBL = new CustomerBL() { Id = c.Id, Name = c.Name, Location = new Location(c.Latitude, c.Longitude), Phone = c.Phone };
            return CustomerBL;
        }

        public ParcelBL convertToParcelBL(IDAL.DO.ParcelDL p)
        {
            ParcelBL ParcelBL = new ParcelBL() {Id = p.Id, Delivered = p.Delivered, PickedUp = p.PickedUp, DroneId = p.DroneId, Pritority = p.Pritority, Requested = p.Requested, Scheduled = p.Scheduled, SenderId = p.SenderId, TargetId = p.TargetId, Weight = p.Weight};
            return ParcelBL;
        }










        //public void belongPacelToADrone(Parcel parcel)
        //{
        //    Parcel parcel1 = new Parcel();
        //    parcel1 = parcel;
        //   var drone = DataSource.drones.Find(c => c.Status == DroneStatus.Free);
        //    int indexDrone = DataSource.drones.FindIndex(c => c.Id == parcel.Id);
        //    parcel1.DroneId = drone.Id;
        //    drone.Status = DroneStatus.Delivery;
        //    dalObject.updateDrone(drone);
        //    dalObject.updateParcel(parcel1);


        //}

        //public void CollectAParcelByDrone(Parcel parcel)
        //{
        //    parcel.PickedUp = DateTime.Now;
        //    dalObject.updateParcel(parcel);
        //}

        //public void DeliverParcelToCustomer(Parcel parcel)
        //{
        //    parcel.Delivered = DateTime.Now;
        //    dalObject.updateParcel(parcel);
        //}

        //public void SendDroneForCharging(Drone drone, Station station)
        //{
        //    drone.Status = DroneStatus.Maintenance;
        //    DroneCharge droneCharge = new DroneCharge();
        //    droneCharge.DroneId = drone.Id;
        //    droneCharge.stationId = station.Id;

        //    dalObject.addDronCharge(droneCharge);
        //}
        //public void ReleaseDroneFromCharging(Drone drone)
        //{
        //    int index = 0;
        //    drone.Status = DroneStatus.Free;
        //    for (int i = 0; i < DataSource.droneCharges.Count; i++)
        //    {
        //        if (DataSource.droneCharges[i].DroneId == drone.Id)
        //        {
        //            index = i;
        //            break;
        //        }


        //    }

        //    for (int i = index; i < DataSource.droneCharges.Count - 1; i++)
        //    {
        //        DataSource.droneCharges[i] = DataSource.droneCharges[i + 1];
        //    }

        //}



















    }
}
