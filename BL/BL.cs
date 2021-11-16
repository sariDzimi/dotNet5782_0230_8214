using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;




namespace BL
{
    public partial class BL 
    {
        public List<DroneBL> dronesBL;
        Random rand = new Random();
        DalObject.DalObject dalObject;

        double ElectricityUseWhenFree=0;
        double ElectricityUseWhenLight=0;
        double ElectricityUseWhenMedium=0;
        double ElectricityUseWhenheavy=0;
        public BL()
        {
            //intlizing BL members
            IDal.IDal dalObject = new DalObject.DalObject();
            double[] ElectricityUse = dalObject.RequestElectricityUse();
            double ElectricityUseWhenFree = ElectricityUse[0];
            double ElectricityUseWhenLight = ElectricityUse[1];
            double ElectricityUseWhenMedium = ElectricityUse[2];
            double ElectricityUseWhenheavy = ElectricityUse[3];
            double RateOfCharching = ElectricityUse[4];
            dronesBL = new List<DroneBL>();

            foreach (var drone in dalObject.GetDrones())
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
                if (droneBL.DroneStatus == DroneStatus.Maintenance)
                {
                    int length = dalObject.GetStationsList().Count;
                    IDAL.DO.StationDL stationDL = dalObject.GetStationsList()[rand.Next(0, length)];
                    StationBL stationBL = convertToStationBL(stationDL);
                    droneBL.Location = stationBL.Location;
                    droneBL.Battery = rand.Next(0, 20);
                }
                if (droneBL.DroneStatus == DroneStatus.Free)
                {
                    //מיקומו יוגרל בין לקוחות שיש חבילות שסופקו להם//TODO

                }
                dronesBL.Add(droneBL);

            }

        }






        public void ParcelToTransfor(int sendedId, int reciveId, int weigth, int prioty)
        {
            CustomerAtParcel customerAtParcelsendedr = new CustomerAtParcel() { Id = sendedId };
            CustomerAtParcel customerAtParcelreciver = new CustomerAtParcel() { Id = reciveId };

            ParcelBL parcelBL = new ParcelBL() { customerAtParcelSender= customerAtParcelsendedr, customerAtParcelReciver= customerAtParcelreciver,   Weight = (IDAL.DO.WeightCategories)weigth, Pritority = (IDAL.DO.Pritorities)prioty };

            parcelBL.Requested = DateTime.Now;
            parcelBL.droneAtParcel = null;
        }




        public void  sendDroneToCharge(int droneId)
        {
            var drone = dronesBL.Find(d => d.Id == droneId);
            if(drone.DroneStatus == 0)
            {
                StationBL stationMini = minimumDistanceFromStation(drone.Location);
               if( dalObject.RequestElectricityUse()[0]* distanceBetweenTwoLocationds(stationMini.Location, drone.Location)>drone.Battery)
                {
                    IDAL.DO.StationDL stationDL = dalObject.GetStationsList().Find(s=> s.Id == stationMini.Id);
                    IDAL.DO.DroneDL droneDL = dalObject.GetDronesList().Find(s => s.Id == stationMini.Id);
                    //BL
                    drone.Battery -= dalObject.RequestElectricityUse()[0] * distanceBetweenTwoLocationds(stationMini.Location, drone.Location);
                    drone.Location = stationMini.Location;
                    drone.DroneStatus = (DroneStatus)1;
                    stationMini.ChargeSlots = stationMini.ChargeSlots - 1;

                    //DL
                     droneDL.Battery -= dalObject.RequestElectricityUse()[0] * distanceBetweenTwoLocationds(stationMini.Location, drone.Location);
                    IDAL.DO.DroneChargeDL droneChargeDL = new IDAL.DO.DroneChargeDL();  
                    dalObject.addDronCharge(droneChargeDL);
                }
                else
                {
                    //TODO throw exception
                }
            }
            else
            {
                //TODO throw exception
            }
        }
        


      

        public StationBL minimumDistanceFromStation(Location location)
        {
            StationBL stationMini;
            StationBL station;


            stationMini = convertToStationBL(dalObject.GetStationsList()[0]);
            
                foreach (IDAL.DO.StationDL s in dalObject.GetStations())
                {
                    station = convertToStationBL(s);
                    double distance1 = distanceBetweenTwoLocationds(station.Location, location);
                    double distance2 = distanceBetweenTwoLocationds(stationMini.Location, location);
                    if (distance1 < distance2 && station.ChargeSlots < 0)
                    {
                        stationMini = station;
                    }
                }

            return stationMini; 


        }


        public double distanceBetweenTwoLocationds(Location location1, Location location2)
        {
            return Math.Sqrt(Math.Pow(location1.Longitude - location2.Longitude, 2) 
                + Math.Pow(location1.Latitude - location2.Latitude, 2)*1.0);


        }




        public void AssignAParcelToADrone(int id)
        {
            List<IDAL.DO.ParcelDL> parcelDLs = new List<IDAL.DO.ParcelDL>();
            List<IDAL.DO.ParcelDL> parcelDL = new List<IDAL.DO.ParcelDL>();
            List<ParcelBL> parcelBLs = new List<ParcelBL>();
            List<CustomerBL> customerBLs = GetCustomerFromBL();


            ParcelBL parcel = new ParcelBL();
            DroneBL droneBL = new DroneBL();
            IDAL.DO.DroneDL droneDL = dalObject.findDrone(id);
            droneBL = dronesBL.Find(s => s.Id == id);
            if (droneBL.DroneStatus == DroneStatus.Free)
            {

                foreach (IDAL.DO.ParcelDL p in  dalObject.GetParcel())
                {
                    parcel = convertToParcelBL(p);
                    customerBLs.Find(e => e.Id == parcel.customerAtParcelSender.id);
                }
                IDAL.DO.Pritorities pritority = parcelDLs.Max(s => s.Pritority);
                parcelDL = parcelDLs.FindAll(s => s.Pritority == pritority);
                parcelDL = parcelDL.FindAll(s => s.Weight < droneBL.MaxWeight);
                IDAL.DO.WeightCategories weightCategories = parcelDL.Max(s => s.Weight);
                parcelDL = parcelDL.FindAll(s => s.Weight == weightCategories);
                



            }
        }

            public double CalculateElectricity(Location location1 , Location location2, IDAL.DO.WeightCategories weight)
            {

            double distance=  distanceBetweenTwoLocationds(location1, location2);
            switch (weight) {
                case IDAL.DO.WeightCategories.Light:
                    return (distance* this.ElectricityUseWhenLight);
                case IDAL.DO.WeightCategories.Medium:
                    return (distance * this.ElectricityUseWhenMedium);
                case IDAL.DO.WeightCategories.Heavy:
                    return (distance * this.ElectricityUseWhenheavy);

                default:
                    return 0;


            }
 }
















        




        public int DroneId { get; set; }
        public int stationId { get; set; }







 /*       public IDAL.DO.StationDL convertToStationDL(StationBL station)
        {
            IDAL.DO.StationDL stationDL = new IDAL.DO.StationDL() { Id = station.Id, Name = station.Name, Longitude = station.Location.Longitude, ChargeSlots = station.ChargeSlots, Latitude = station.Location.Latitude };
            return stationDL;
        }

        public IDAL.DO.CustomerDL convertToCustomerDL(CustomerBL station)
        {
            IDAL.DO.CustomerDL customerDL = new IDAL.DO.CustomerDL() { Id = station.Id, Name = station.Name, Longitude = station.Location.Longitude, Phone = station.Phone, Latitude = station.Location.Latitude };
            return customerDL;
        }*/








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
