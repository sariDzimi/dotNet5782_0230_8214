using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using IDAL.DO;


namespace BL
{
    public partial class BL : IBL.IBL
    {
        public List<Drone> dronesBL;
        Random rand = new Random();
        IDal.IDal dalObject;

        double ElectricityUseWhenFree = 0;
        double ElectricityUseWhenLight = 0;
        double ElectricityUseWhenMedium = 0;
        double ElectricityUseWhenheavy = 0;
        double RateOfCharching;
        public BL()
        {
            //intlizing BL members
            dalObject = new DalObject.DalObject();
            double[] ElectricityUse = dalObject.RequestElectricityUse();
            ElectricityUseWhenFree = ElectricityUse[0];
            ElectricityUseWhenLight = ElectricityUse[1];
            ElectricityUseWhenMedium = ElectricityUse[2];
            ElectricityUseWhenheavy = ElectricityUse[3];
            RateOfCharching = ElectricityUse[4];
            dronesBL = new List<Drone>();
            List<ParcelDL> parcelDLs = dalObject.GetParcel().ToList();

            foreach (var drone in dalObject.GetDrones())
            {
                Drone droneBL = new Drone() { Id = drone.Id, Model = drone.Model, MaxWeight = (IBL.BO.WeightCategories)drone.MaxWeight };
                ParcelDL parcel = parcelDLs.Find(p => p.DroneId == drone.Id);

                if (parcel.SenderId != 0 )
                {

                    IDAL.DO.ParcelDL parcelDL = dalObject.FindParcelBy(dr => dr.DroneId == droneBL.Id);
                    IDAL.DO.CustomerDL customerSernder = dalObject.findCustomerBy(c => c.Id == parcelDL.SenderId);
                    Location locationSender = convertToCustomerBL(customerSernder).Location;
                    IDAL.DO.CustomerDL customerReciver = dalObject.GetCustomer().ToList().Find(d => d.Id == parcelDL.TargetId);
                    Location locationReciver = convertToCustomerBL(customerReciver).Location;
                    parcel.Scheduled = DateTime.Now;
                    CustomerAtParcel customerAtParcelSender = new CustomerAtParcel();
                    customerAtParcelSender.Id = customerSernder.Id;
                    CustomerAtParcel customerAtParcelreciver = new CustomerAtParcel();
                    customerAtParcelreciver.Id = customerReciver.Id;
                    double distance1 = distanceBetweenTwoLocationds(new Location(customerSernder.Longitude, customerSernder.Latitude), new Location(customerReciver.Longitude, customerReciver.Latitude));
                    ParcelInDelivery parcelInDelivery = new ParcelInDelivery() { Id = parcelDL.Id, customerAtParcelTheSender = customerAtParcelSender, customerAtParcelTheReciver = customerAtParcelreciver, distance = distance1, locationCollect = new Location(customerSernder.Longitude, customerSernder.Latitude), locationTarget = new Location(customerReciver.Longitude, customerReciver.Latitude), pritorities = parcelDL.Pritority, weightCategories = parcelDL.Weight, isWating = false };
          
                    droneBL.ParcelInDelivery = parcelInDelivery;
                    droneBL.DroneStatus = DroneStatus.Delivery;
                    dalObject.updateParcel(parcel);
                    if (parcel.PickedUp < DateTime.Now)
                    {
                        int senderID = parcel.SenderId;
                        Location senderLocation = convertToCustomerBL(dalObject.findCustomerBy(t=> t.Id==senderID)).Location;
                        droneBL.Location = closestStationToLoacation(senderLocation).Location;
                    }
                    else
                    {
                        int reciverID = parcel.SenderId;
                        droneBL.Location = convertToCustomerBL(dalObject.findCustomerBy(t=> t.Id==reciverID)).Location;
                    }
                
                    double electicityNeeded = CalculateElectricity(droneBL.Location, FindCuatomerBy(t=> t.Id==parcel.TargetId).Location, parcel.Weight);
                    int r = rand.Next((int)electicityNeeded, 100);
                    droneBL.Battery = r;
                }
                else
                    droneBL.DroneStatus = (DroneStatus)rand.Next(0, 2);

                if (droneBL.DroneStatus == DroneStatus.Maintenance)
                {
                    int numOfStations = dalObject.GetStations().ToList().Count;
                    IDAL.DO.StationDL stationDL = dalObject.GetStations().ToList()[rand.Next(0, numOfStations)];
                    StationBL stationBL = convertToStationBL(stationDL);
                    droneBL.Location = stationBL.Location;
                    droneBL.Battery = rand.Next(0, 21);
                    IDAL.DO.DroneChargeDL droneChargeDL = new DroneChargeDL();
                    droneChargeDL.DroneId = drone.Id;
                    droneChargeDL.stationId = stationBL.Id;
                    dalObject.addDronCharge(droneChargeDL);
                    droneBL.Location = new Location(stationBL.Location.Longitude, stationBL.Location.Latitude);
                }

                if (droneBL.DroneStatus == DroneStatus.Free)
                {
                    List<ParcelDL> deliveredParcels = dalObject.GetParcel().ToList().FindAll(p => p.Delivered != new DateTime());
                    ParcelDL randomDeliveredParcel = deliveredParcels[rand.Next(0, deliveredParcels.Count)];
                    int recieverID = randomDeliveredParcel.TargetId;
                    CustomerDL reciver = dalObject.findCustomerBy(t=> t.Id==recieverID);
                    droneBL.Location = new Location(reciver.Longitude, reciver.Latitude);
                    double distance = distanceBetweenTwoLocationds(droneBL.Location, closestStationToLoacation(droneBL.Location).Location);
                    droneBL.Battery -= distance * ElectricityUseWhenFree;
                }
                dronesBL.Add(droneBL);
            }
        }

        /// <summary>
        /// Parcel To Transfor
        /// </summary>
        /// <param name="sendedId"></param>
        /// <param name="reciveId"></param>
        /// <param name="weigth"></param>
        /// <param name="prioty"></param>
        public void ParcelToTransfor(int sendedId, int reciveId, int weigth, int prioty)
        {
            CustomerAtParcel customerAtParcelsendedr = new CustomerAtParcel() { Id = sendedId };
            CustomerAtParcel customerAtParcelreciver = new CustomerAtParcel() { Id = reciveId };

            ParcelBL parcelBL = new ParcelBL() { customerAtParcelSender = customerAtParcelsendedr, customerAtParcelReciver = customerAtParcelreciver, Weight = (IDAL.DO.WeightCategories)weigth, Pritority = (IDAL.DO.Pritorities)prioty };

            parcelBL.Requested = DateTime.Now;
            parcelBL.droneAtParcel = null;
        }

        /// <summary>
        /// release Drone From Charging 
        /// </summary>
        /// <param name="idDrone"></param>
        /// <param name="timeInCharging"></param>
        public void releaseDroneFromCharging(int idDrone, double timeInCharging)
        {
            Drone droneBL = dronesBL.Find(d => d.Id == idDrone);
            if (droneBL == null)
            {
                throw new NotFound($" drone number {idDrone}");
            }
            if (droneBL.DroneStatus != DroneStatus.Maintenance)
                throw new IBL.BO.DroneIsNotInCorrectStatus("drone is not in Maintenance ");


            DroneDL droneDL = dalObject.findDroneBy(t=> t.Id==idDrone);
            droneBL.Battery += timeInCharging * RateOfCharching;
            droneBL.DroneStatus = DroneStatus.Free;
            updateDrone(droneBL);
            dalObject.updateDrone(droneDL);
            //TODO העלאת מספר עמדות טעינה פנויות ב1
            dalObject.removeDroneCharge(idDrone);
        }
        /// <summary>
        /// collect Parcle By Drone
        /// </summary>
        /// <param name="idDrone"></param>
        public void collectParcleByDrone(int idDrone)
        {

            Drone droneBL = dronesBL.Find(d => d.Id == idDrone);
            if (droneBL == null)
            {
                throw new NotFound($"drone number {idDrone}");
            }

            if (droneBL.DroneStatus != DroneStatus.Delivery)
                throw new IBL.BO.DroneIsNotInCorrectStatus("drone is not in Delivery  ");
            ;
            IDAL.DO.DroneDL droneDL = dalObject.findDroneBy(t=> t.Id==idDrone);
            IDAL.DO.ParcelDL parcelDL = dalObject.FindParcelBy(t => t.DroneId == droneBL.Id);
            IDAL.DO.CustomerDL customerSernder = dalObject.findCustomerById(parcelDL.SenderId); 
            Location locationSender = convertToCustomerBL(customerSernder).Location;
            IDAL.DO.WeightCategories weight = parcelDL.Weight;
            double useElectricity = CalculateElectricity(droneBL.Location, locationSender, weight);
            droneBL.Battery -= useElectricity;
            droneBL.Location = locationSender;
            updateDrone(droneBL);
            dalObject.updateDrone(droneDL);
            parcelDL.PickedUp = DateTime.Now;
            dalObject.updateParcel(parcelDL);

        }

        /// <summary>
        /// send Drone To Charge
        /// </summary>
        /// <param name="droneId"></param>
        public void sendDroneToCharge(int droneId)
        {
            Drone drone = dronesBL.Find(d => d.Id == droneId);
            if (drone == null)
            {
                throw new NotFound($"drone number {droneId}");
            }
            if (drone.DroneStatus == 0)
            {
                StationBL stationMini = closestStationToLoacation(drone.Location);
               
                if (dalObject.RequestElectricityUse()[0] * distanceBetweenTwoLocationds(stationMini.Location, drone.Location) < drone.Battery)
                {
                    IDAL.DO.StationDL stationDL = dalObject.GetStations().ToList().Find(s => s.Id == stationMini.Id);
                    IDAL.DO.DroneDL droneDL = dalObject.GetDrones().ToList().Find(s => s.Id == stationMini.Id);
                    //BL
                    drone.Battery -= dalObject.RequestElectricityUse()[0] * distanceBetweenTwoLocationds(stationMini.Location, drone.Location);
                    drone.Location = stationMini.Location;
                    drone.DroneStatus = (DroneStatus)1;
                    updateDrone(drone);
                    stationMini.ChargeSlots = stationMini.ChargeSlots - 1;

                    //DL

                    IDAL.DO.DroneChargeDL droneChargeDL = new IDAL.DO.DroneChargeDL();
                    droneChargeDL.DroneId = droneId;
                    dalObject.addDronCharge(droneChargeDL);
                }
                else
                {
                    throw new IBL.BO.DroneDoesNotHaveEnoughBattery();
                }
            }
            else
            {
                throw new IBL.BO.DroneIsNotInCorrectStatus("drone is not free");
            }
        }

        /// <summary>
        /// closest Station To Loacation
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public StationBL closestStationToLoacation(Location location)
        {
            StationBL stationMini;
            StationBL station;


            stationMini = convertToStationBL(dalObject.GetStations().ToList()[0]);

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

        /// <summary>
        /// distance Between Two Locationds
        /// </summary>
        /// <param name="location1"></param>
        /// <param name="location2"></param>
        /// <returns></returns>
        public double distanceBetweenTwoLocationds(Location location1, Location location2)
        {
            return Math.Sqrt(Math.Pow(location1.Longitude - location2.Longitude, 2)
                + Math.Pow(location1.Latitude - location2.Latitude, 2) * 1.0);


        }

        /// <summary>
        /// Assign AParcel To A Drone
        /// </summary>
        /// <param name="id"></param>
        public void AssignAParcelToADrone(int id)
        {
            StationBL stationBL = new StationBL();
            ParcelBL parcel = new ParcelBL();
            ParcelBL parcel1 = new ParcelBL();
            CustomerBL customerBLsender = new CustomerBL();
            IDAL.DO.CustomerDL customerParcel = new IDAL.DO.CustomerDL();
            CustomerBL customerBLreciver = new CustomerBL();
            IDAL.DO.ParcelDL parcelDL = dalObject.GetParcel().ToList().First(t => t.Scheduled == null);
            List<CustomerBL> customerBLs = GetCustomers().ToList();
            List<IDAL.DO.ParcelDL> parcelDLs = dalObject.GetParcel().ToList();
            Drone droneBL = dronesBL.Find(s => s.Id == id);
            if (droneBL == null)
            {
                throw new NotFound($"drone number {id}");
            }
            if (droneBL.DroneStatus != DroneStatus.Free)
            {
                throw new IBL.BO.DroneIsNotInCorrectStatus("drone is not free");
            }
            foreach (var p in parcelDLs)
            {
                parcel = convertToParcelBL(p);
                if(parcel.Scheduled != null)
                {
                    continue;
                }
                customerParcel = dalObject.findCustomerBy(t => t.Id == parcelDL.SenderId);
                customerBLsender = customerBLs.Find(e => e.Id == parcel.customerAtParcelSender.Id);
                customerBLreciver = customerBLs.Find(e => e.Id == parcel.customerAtParcelReciver.Id);
                stationBL = closestStationToLoacation(customerBLreciver.Location);
                double electricidy1 = CalculateElectricity(customerBLsender.Location, customerBLreciver.Location, parcel.Weight) + distanceBetweenTwoLocationds(stationBL.Location, droneBL.Location) * this.ElectricityUseWhenFree + distanceBetweenTwoLocationds(droneBL.Location, customerBLsender.Location) * this.ElectricityUseWhenFree;
                double distanceToCharging1 = distanceBetweenTwoLocationds(customerBLreciver.Location, stationBL.Location);
                if (electricidy1 < droneBL.Battery)
                {
                    if (parcelDL.Pritority < p.Pritority)
                    {
                        parcelDL = p;

                        parcel = convertToParcelBL(p);
                        customerBLsender = customerBLs.Find(e => e.Id == parcel.customerAtParcelSender.Id);
                        customerBLreciver = customerBLs.Find(e => e.Id == parcel.customerAtParcelReciver.Id);
                        stationBL = closestStationToLoacation(customerBLreciver.Location);
                    }
                    else if (parcelDL.Pritority == p.Pritority)
                    {
                        if (parcelDL.Weight < p.Weight)
                        {
                            parcelDL = p;
                        }
                    }
                    else if (parcelDL.Weight == p.Weight)
                    {
                        if (distanceToCharging1 < distanceBetweenTwoLocationds(droneBL.Location, new Location(customerParcel.Longitude, customerParcel.Latitude)))
                        {
                            parcelDL = p;
                        }
                    }
                    if (parcelDL.Weight == 0)
                    {
                        throw new NotFound("not found  parcel");
                    }

                }
            }
            CustomerAtParcel customerAtParcelSender = new CustomerAtParcel() { Id = customerBLsender.Id, Name = customerBLsender.Name };
            CustomerAtParcel customerAtParcelReciver = new CustomerAtParcel() { Id = customerBLreciver.Id, Name = customerBLreciver.Name };
            droneBL.DroneStatus = DroneStatus.Delivery;
            double distance1 = distanceBetweenTwoLocationds(customerBLsender.Location, customerBLreciver.Location);
            ParcelInDelivery parcelInDelivery = new ParcelInDelivery() { Id = parcelDL.Id, customerAtParcelTheSender = customerAtParcelSender, customerAtParcelTheReciver = customerAtParcelReciver, distance = distance1, locationCollect = customerBLsender.Location, locationTarget = customerBLsender.Location, isWating = false, pritorities = parcelDL.Pritority, weightCategories = parcelDL.Weight };
            droneBL.ParcelInDelivery = parcelInDelivery;
            updateDrone(droneBL);
            parcelDL.DroneId = id;
            parcelDL.Scheduled = DateTime.Now;
            dalObject.updateParcel(parcelDL);
            
        }

        /// <summary>
        /// supply Parcel By Drone
        /// </summary>
        /// <param name="DroneID"></param>

        public void supplyParcelByDrone(int DroneID)
        {

            Drone droneBL = dronesBL.Find(d => d.Id == DroneID);
            if (droneBL == null)
            {
                throw new NotFound($"drone number {DroneID}");
            }
            IDAL.DO.DroneDL droneDL = dalObject.findDroneBy(i => i.Id == DroneID);
            if (droneBL.DroneStatus != DroneStatus.Delivery)
                throw new IBL.BO.DroneIsNotInCorrectStatus("drone is not in delivery");
            IDAL.DO.ParcelDL parcelDL = dalObject.FindParcelBy(dr => dr.DroneId == droneBL.Id);
            IDAL.DO.CustomerDL customerSernder = dalObject.findCustomerBy(c => c.Id== parcelDL.SenderId);
            Location locationSender = convertToCustomerBL(customerSernder).Location;
            IDAL.DO.CustomerDL customerReciver = dalObject.GetCustomer().ToList().Find(d => d.Id == parcelDL.TargetId);
            Location locationReciver = convertToCustomerBL(customerReciver).Location;
          
            CustomerAtParcel customerAtParcelSender = new CustomerAtParcel();
            customerAtParcelSender.Id = customerSernder.Id;
            CustomerAtParcel customerAtParcelreciver = new CustomerAtParcel();
            customerAtParcelreciver.Id = customerReciver.Id;
            double distance1 = distanceBetweenTwoLocationds(new Location(customerSernder.Longitude, customerSernder.Latitude), new Location(customerReciver.Longitude, customerReciver.Latitude));
            //ParcelInDelivery parcelInDelivery = new ParcelInDelivery() { Id = parcelDL.Id, customerAtParcelTheSender = customerAtParcelSender, customerAtParcelTheReciver = customerAtParcelreciver, distance = distance1, locationCollect = new Location(customerSernder.Longitude, customerSernder.Latitude), locationTarget = new Location(customerReciver.Longitude, customerReciver.Latitude), pritorities = parcelDL.Pritority, weightCategories= parcelDL.Weight,isWating= false };
            ParcelInDelivery parcelInDelivery = new ParcelInDelivery();
            IDAL.DO.WeightCategories weight = parcelDL.Weight;
            double useElectricity = CalculateElectricity(locationSender, locationReciver, weight);
            droneBL.Battery -= useElectricity;
            droneBL.Location = locationReciver;
            droneBL.DroneStatus = DroneStatus.Free;
            droneBL.ParcelInDelivery = parcelInDelivery;
            updateDrone(droneBL);
            dalObject.updateDrone(droneDL);
            parcelDL.Delivered = DateTime.Now;
            dalObject.updateParcel(parcelDL);

        }

        /// <summary>
        /// Calculate Electricity
        /// </summary>
        /// <param name="location1"></param>
        /// <param name="location2"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public double CalculateElectricity(Location location1, Location location2, IDAL.DO.WeightCategories weight)
        {

            double distance = distanceBetweenTwoLocationds(location1, location2);
            switch (weight)
            {
                case IDAL.DO.WeightCategories.Light:
                    return (distance * this.ElectricityUseWhenLight);
                case IDAL.DO.WeightCategories.Medium:
                    return (distance * this.ElectricityUseWhenMedium);
                case IDAL.DO.WeightCategories.Heavy:
                    return (distance * this.ElectricityUseWhenheavy);

                default:
                    return 0;


            }
        }
    }
}

