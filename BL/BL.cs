using BlApi.BO;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BL
{
     partial class BL : BlApi.IBL
    {
        private List<BO.Drone> dronesBL;
        private Random rand = new Random();
        private IDal.IDal dalObject;

        private double ElectricityUseWhenFree = 0;
        private double ElectricityUseWhenLight = 0;
        private double ElectricityUseWhenMedium = 0;
        private double ElectricityUseWhenheavy = 0;
        private double RateOfCharching = 0.1;
        


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
            dronesBL = new List<BlApi.BO.Drone>();
            List<IDAL.DO.Parcel> parcelDLs = dalObject.GetParcel().ToList();

            foreach (var droneDL in dalObject.GetDrones())
            {
                Drone droneBL = new Drone() { Id = droneDL.Id, Model = droneDL.Model, MaxWeight = (BlApi.BO.WeightCategories)droneDL.MaxWeight };
                if (GetParcelsBy(p => (p.droneAtParcel != null && p.droneAtParcel.Id == droneBL.Id)).Any())
                {
                    Parcel parcel = GetParcelsBy(p => (p.droneAtParcel != null && p.droneAtParcel.Id == droneBL.Id)).First();
                    if (parcel.Delivered == null)
                    {
                        droneBL.DroneStatus = DroneStatus.Delivery;
                        Location locationSender = FindCustomerBy(c => c.Id == parcel.customerAtParcelSender.Id).Location;
                        Location locationReciver = FindCustomerBy(c => c.Id == parcel.customerAtParcelReciver.Id).Location;
                        parcel.Scheduled = DateTime.Now;
                        double distance1 = distanceBetweenTwoLocationds(locationSender, locationReciver);
                        ParcelInDelivery parcelInDelivery = new ParcelInDelivery()
                        {
                            Id = parcel.Id,
                            customerAtParcelTheSender = parcel.customerAtParcelSender,
                            customerAtParcelTheReciver = parcel.customerAtParcelReciver,
                            distance = distance1,
                            locationCollect = locationSender,
                            locationTarget = locationReciver,
                            pritorities = parcel.Pritority,
                            weightCategories = parcel.Weight,
                            IsWating = false
                        };
                        droneBL.ParcelInDelivery = parcelInDelivery;
                        updateParcel(parcel);

                        if (parcel.PickedUp.Equals(null))
                            droneBL.Location = closestStationToLoacation(locationSender).Location;
                        else
                            droneBL.Location = locationReciver;

                        double electicityNeeded = CalculateElectricity(droneBL.Location, locationReciver, parcel.Weight);
                        droneBL.Battery = rand.Next((int)electicityNeeded, 100);
                    }
                    else
                        droneBL.DroneStatus = (DroneStatus)rand.Next(0, 2);
                }
                else
                    droneBL.DroneStatus = (DroneStatus)rand.Next(0, 2);

                if (droneBL.DroneStatus == DroneStatus.Maintenance)
                {
                    Station station = GetRandomStation();
                    droneBL.Location = station.Location;
                    droneBL.Battery = rand.Next(0, 21);
                    IDAL.DO.DroneCharge droneChargeDL = new IDAL.DO.DroneCharge() { DroneId = droneDL.Id, stationId = station.Id };
                    dalObject.addDronCharge(droneChargeDL);
                    droneBL.Location = station.Location;
                }

                else if (droneBL.DroneStatus == DroneStatus.Free)
                {
                    try
                    {
                        List<BlApi.BO.Customer> customers = GetCustomersBy(c => c.parcelsSentedToCustomer.Any(p => p.parcelStatus == ParcelStatus.Provided)).ToList();
                        double u = rand.Next(0, customers.Count);
                        Customer customer = customers[(int)rand.Next(0, customers.Count)];
                        droneBL.Location = customer.Location;
                    }
                    catch (Exception )
                    {
                        droneBL.Location = GetRandomStation().Location;
                    }
                    double electicityNeeded = distanceBetweenTwoLocationds(droneBL.Location, closestStationToLoacation(droneBL.Location).Location) * ElectricityUseWhenFree;
                    droneBL.Battery = rand.Next((int)electicityNeeded, 100);

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

            Parcel parcelBL = new BlApi.BO.Parcel() { customerAtParcelSender = customerAtParcelsendedr, customerAtParcelReciver = customerAtParcelreciver, Weight = (IDAL.DO.WeightCategories)weigth, Pritority = (IDAL.DO.Pritorities)prioty };

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
            BlApi.BO.Drone droneBL = FindDrone(idDrone);

            if (droneBL.DroneStatus != DroneStatus.Maintenance)
                throw new BlApi.BO.DroneIsNotInCorrectStatus("drone is not in Maintenance ");

            droneBL.Battery += timeInCharging * RateOfCharching;
            droneBL.DroneStatus = DroneStatus.Free;
            updateDrone(droneBL);

            Station station = FindStation(FindDroneCharge(droneBL.Id).stationId);
            station.ChargeSlots += 1;
            updateStation(station);

            dalObject.removeDroneCharge(idDrone);
        }

        /// <summary>
        /// collect Parcle By Drone
        /// </summary>
        /// <param name="idDrone"></param>
        public void collectParcleByDrone(int idDrone)
        {
            BlApi.BO.Drone droneBL = FindDrone(idDrone);
            Parcel parcel = FindParcel(droneBL.ParcelInDelivery.Id);
            if (droneBL.DroneStatus != DroneStatus.Delivery || droneBL.ParcelInDelivery == null)
                throw new BlApi.BO.DroneIsNotInCorrectStatus("drone is not in Delivery");

            Location locationSender = droneBL.ParcelInDelivery.locationCollect;
            double useElectricity = CalculateElectricity(droneBL.Location, locationSender, droneBL.ParcelInDelivery.weightCategories);
            droneBL.Battery -= useElectricity;
            droneBL.Location = locationSender;
            updateDrone(droneBL);

            //Parcel parcel = FindParcel(droneBL.ParcelInDelivery.Id);
            parcel.PickedUp = DateTime.Now;
            updateParcel(parcel);

        }

        /// <summary>
        /// send Drone To Charge
        /// </summary>
        /// <param name="droneId"></param>
        public void sendDroneToCharge(int droneId)
        {
            Drone drone = FindDrone(droneId);
            if (drone.DroneStatus == DroneStatus.Free)
            {
                Station closestStation = closestStationToLoacation(drone.Location);

                if (dalObject.RequestElectricityUse()[0] * distanceBetweenTwoLocationds(closestStation.Location, drone.Location) < drone.Battery)
                {
                    //IDAL.DO.Station stationDL = dalObject.GetStations().ToList().Find(s => s.Id == closestStation.Id);
                    //IDAL.DO.Drone droneDL = dalObject.GetDrones().ToList().Find(s => s.Id == closestStation.Id);
                    //BL
                    drone.Battery -= dalObject.RequestElectricityUse()[0] * distanceBetweenTwoLocationds(closestStation.Location, drone.Location);
                    drone.Location = closestStation.Location;
                    drone.DroneStatus = DroneStatus.Maintenance;
                    updateDrone(drone);
                    closestStation.ChargeSlots = closestStation.ChargeSlots - 1;
                    updateStation(closestStation);
                    //DL
                    DroneCharge droneCharge = new DroneCharge(droneId, closestStation.Id);
                    AddDroneCharge(droneCharge);
                }
                else
                {
                    throw new BlApi.BO.DroneDoesNotHaveEnoughBattery();
                }
            }
            else
            {
                throw new BlApi.BO.DroneIsNotInCorrectStatus("drone is not free");
            }
        }

        /// <summary>
        /// closest Station To Loacation
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        private BlApi.BO.Station closestStationToLoacation(Location location)
        {
            BlApi.BO.Station closestStation;

            closestStation = GetStations().ToList().First();

            

            foreach (BlApi.BO.Station station in GetStations())
            {
                double distance1 = distanceBetweenTwoLocationds(station.Location, location);
                double distance2 = distanceBetweenTwoLocationds(closestStation.Location, location);
                if (distance1 < distance2 && station.ChargeSlots > 0)
                {
                    closestStation = station;
                }
            }

            return closestStation;
        }

        /// <summary>
        /// distance Between Two Locationds
        /// </summary>
        /// <param name="location1"></param>
        /// <param name="location2"></param>
        /// <returns></returns>
        private double distanceBetweenTwoLocationds(Location location1, Location location2)
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
            BlApi.BO.Station stationBL = new BlApi.BO.Station();
            BlApi.BO.Parcel parcel = new BlApi.BO.Parcel();
            BlApi.BO.Parcel parcel1 = new BlApi.BO.Parcel();
            BlApi.BO.Customer customerBLsender = new BlApi.BO.Customer();
            IDAL.DO.Customer customerParcel = new IDAL.DO.Customer();
            BlApi.BO.Customer customerBLreciver = new BlApi.BO.Customer();
            List<IDAL.DO.Parcel> parcels= dalObject.GetParcel().ToList();
            IDAL.DO.Parcel parcelDL = dalObject.GetParcel().ToList().First(t => t.Scheduled == null);
            List<BlApi.BO.Customer> customerBLs = Enumerable.ToList<BlApi.BO.Customer>(GetCustomers());
            List<IDAL.DO.Parcel> parcelDLs = dalObject.GetParcel().ToList();
            BlApi.BO.Drone droneBL = dronesBL.Find(s => s.Id == id);
            if (droneBL == null)
            {
                throw new NotFound($"drone number {id}");
            }
            if (droneBL.DroneStatus != DroneStatus.Free)
            {
                throw new BlApi.BO.DroneIsNotInCorrectStatus("drone is not free");
            }
            foreach (var p in parcelDLs)
            {
                parcel = convertToParcelBL(p);
                if (parcel.Scheduled != null)
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
            ParcelInDelivery parcelInDelivery = new ParcelInDelivery() { Id = parcelDL.Id, customerAtParcelTheSender = customerAtParcelSender, customerAtParcelTheReciver = customerAtParcelReciver, distance = distance1, locationCollect = customerBLsender.Location, locationTarget = customerBLsender.Location, IsWating = false, pritorities = parcelDL.Pritority, weightCategories = parcelDL.Weight };
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

            BlApi.BO.Drone droneBL = FindDrone(DroneID);

            if (droneBL.DroneStatus != DroneStatus.Delivery)
                throw new BlApi.BO.DroneIsNotInCorrectStatus("drone is not in delivery");
            if (droneBL.ParcelInDelivery == null)
                throw new NotFound("parcel in drone");

            Location locationSender = droneBL.ParcelInDelivery.locationCollect;
            Location locationReciver = droneBL.ParcelInDelivery.locationTarget;
            double electricityUsed = CalculateElectricity(locationSender, locationReciver, droneBL.ParcelInDelivery.weightCategories);
            droneBL.Battery -= electricityUsed;
            droneBL.Location = locationReciver;
            droneBL.DroneStatus = DroneStatus.Free;
            droneBL.ParcelInDelivery = null;
            updateDrone(droneBL);
            BlApi.BO.Parcel parcel = FindParcel(droneBL.Id);
            parcel.Delivered = DateTime.Now;
            updateParcel(parcel);

        }

        /// <summary>
        /// Calculate Electricity
        /// </summary>
        /// <param name="location1"></param>
        /// <param name="location2"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        private double CalculateElectricity(Location location1, Location location2, IDAL.DO.WeightCategories weight)
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

        private BlApi.BO.Station GetRandomStation()
        {
            int numOfStations = GetStations().ToList().Count;
            BlApi.BO.Station station = GetStations().ToList()[rand.Next(0, numOfStations)];
            return station;
        }
    }
}

