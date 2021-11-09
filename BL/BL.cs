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

        public void addStationToBL(int id, int name, Location location, int slots)
        {
            StationBL stationBL = new StationBL() { Id = id, Name = name, LocationStation = location , ChargeSlots=slots};
            List<DroneAtChargingBL> droneAtChargings = new List<DroneAtChargingBL>();
        }

        public void addDroneToBL(int id,int status, string model , int numberStaion)
        {
            DroneBL droneBL = new DroneBL() { Id = id, MaxWeight = (IDAL.DO.WeightCategories)status, Model = model };
            //TODO numberStation.
            droneBL.droneStatus = DroneStatus.Maintenance;

            
            droneBL.Battery= rand.Next(20, 40);
            IDAL.DO.StationDL stationDL = new IDAL.DO.StationDL();
            stationDL = dalObject.findStation(numberStaion);
            Location location = new Location(stationDL.Longitude, stationDL.Latitude);
            droneBL.location = location;



        }

        public void addCustomerToBL(int id, string name, string phone, Location location)
        {
            CustomerBL customerBL = new CustomerBL() { Id = id, Name = name, Phone = phone, Location = location };

        }


        public void ParcelAtTransfor(int sendedId, int reciveId, int weigth, int prioty)
        {
            ParcelBL parcelBL = new ParcelBL() { Id = sendedId, TargetId = reciveId, Weight = (IDAL.DO.WeightCategories)weigth, Pritority = (IDAL.DO.Pritorities)prioty };
            parcelBL.Requested = DateTime.Now;
            parcelBL.droneAtParcel = null;




        }









        Random rand = new Random();
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

            List <IDAL.DO.DroneDL> drones = dalObject.GetDronesList();

            List<Drone> dronesBL = new List<Drone>();

           
            foreach (var drone in dronesDL)
           {
                Drone droneBL = new Drone();
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
                droneBL.droneStatus = (DroneStatus)rand.Next(0, 2);
                if(droneBL.droneStatus == DroneStatus.Maintenance)
                {
                    int length = dalObject.GetStationsList().Count;
                    IDAL.DO.Station stationDL = dalObject.GetStationsList()[rand.Next(0, length)];
                    Station stationBL = 
                }

           }

        }
        public Drone convertToDroneBL(IDAL.DO.Drone drone)
        {
            Drone DroneBL = new Drone() { Id = drone.Id, Model = drone.Model, MaxWeight = drone.MaxWeight };
            return DroneBL;
        }

        public Station convertToStationBL(IDAL.DO.Station Station)
        {
            Station StationBL = new Station() { Id = Station.Id, Name = Station.Name,};
            return StationBL;
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
