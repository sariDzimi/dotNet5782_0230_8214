using BO;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BL
{

    partial class BL : BlApi.IBL
    {
        internal static BL instance;


        private List<BO.Drone> dronesBL;
        private Random rand = new Random();
        private DalApi.IDal dalObject;

        private double ElectricityUseWhenFree = 0;
        private double ElectricityUseWhenLight = 0;
        private double ElectricityUseWhenMedium = 0;
        private double ElectricityUseWhenheavy = 0;
        private double RateOfCharching = 0.1;



        public BL()
        {
            //intlizing BL members
            dalObject = DAL.DalFactory.GetDal("DalObject");
            double[] ElectricityUse = dalObject.RequestElectricityUse();
            ElectricityUseWhenFree = ElectricityUse[0];
            ElectricityUseWhenLight = ElectricityUse[1];
            ElectricityUseWhenMedium = ElectricityUse[2];
            ElectricityUseWhenheavy = ElectricityUse[3];
            RateOfCharching = ElectricityUse[4];
            dronesBL = new List<BO.Drone>();
            List<DO.Parcel> parcelDLs = dalObject.GetParcel().ToList();

            foreach (var droneDL in dalObject.GetDrones())
            {
                BO.Drone droneBL = new BO.Drone() { Id = droneDL.Id, Model = droneDL.Model, MaxWeight = (BO.WeightCategories)droneDL.MaxWeight };
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
                    DO.DroneCharge droneChargeDL = new DO.DroneCharge() { DroneId = droneDL.Id, stationId = station.Id };
                    dalObject.addDronCharge(droneChargeDL);
                    droneBL.Location = station.Location;
                }

                else if (droneBL.DroneStatus == DroneStatus.Free)
                {
                    try
                    {
                        List<BO.Customer> customers = GetCustomersBy(c => c.parcelsSentedToCustomer.Any(p => p.parcelStatus == ParcelStatus.Delivered)).ToList();
                        double u = rand.Next(0, customers.Count);
                        Customer customer = customers[(int)rand.Next(0, customers.Count)];
                        droneBL.Location = customer.Location;
                    }
                    catch (Exception)
                    {
                        droneBL.Location = GetRandomStation().Location;
                    }
                    double electicityNeeded = CalculateElectricityWhenFree(droneBL.Location, closestStationToLoacation(droneBL.Location).Location);
                    droneBL.Battery = rand.Next((int)electicityNeeded, 100);

                }

                dronesBL.Add(droneBL);
            }
        }


        public static BL GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BL();
                }
                return instance;
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

            Parcel parcelBL = new Parcel() { customerAtParcelSender = customerAtParcelsendedr, customerAtParcelReciver = customerAtParcelreciver, Weight = (BO.WeightCategories)weigth, Pritority = (BO.Pritorities)prioty };

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
            Drone droneBL = FindDrone(idDrone);

            if (droneBL.DroneStatus != DroneStatus.Maintenance)
                throw new BO.DroneIsNotInCorrectStatus("drone is not in Maintenance ");

            droneBL.Battery += timeInCharging * RateOfCharching;
            droneBL.DroneStatus = DroneStatus.Free;
            updateDrone(droneBL);

/*            Station station = FindStation(FindDroneCharge(droneBL.Id).stationId);
            station.FreeChargeSlots += 1;
            updateStation(station);
*/
            dalObject.removeDroneCharge(idDrone);
        }

        /// <summary>
        /// collect Parcle By Drone
        /// </summary>
        /// <param name="idDrone"></param>
        public void collectParcleByDrone(int idDrone)
        {
            Drone droneBL = FindDrone(idDrone);
            Parcel parcel = FindParcel(droneBL.ParcelInDelivery.Id);
            if (droneBL.DroneStatus != DroneStatus.Delivery || droneBL.ParcelInDelivery == null)
                throw new DroneIsNotInCorrectStatus("drone is not in Delivery");

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
                    updateDrone(drone);/*
                    closestStation.FreeChargeSlots = closestStation.FreeChargeSlots - 1;
                    updateStation(closestStation);*/
                    //DL
                    DroneCharge droneCharge = new DroneCharge(droneId, closestStation.Id);
                    AddDroneCharge(droneCharge);
                }
                else
                {
                    throw new DroneDoesNotHaveEnoughBattery();
                }
            }
            else
            {
                throw new DroneIsNotInCorrectStatus("drone is not free");
            }
        }

        /// <summary>
        /// closest Station To Loacation
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        private Station closestStationToLoacation(Location location)
        {
            Station closestStation;

            closestStation = GetStations().ToList().First();



            foreach (Station station in GetStations())
            {
                double distance1 = distanceBetweenTwoLocationds(station.Location, location);
                double distance2 = distanceBetweenTwoLocationds(closestStation.Location, location);
                if (distance1 < distance2 && station.FreeChargeSlots > 0)
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
            Parcel bestParcel = new Parcel();
            try
            {
                bestParcel = FindParcelBy(t => t.Scheduled == null);
            }
            catch (Exception)
            {
                throw new NotFound("parcel");
            }
            Drone droneBL = FindDrone(id);

            foreach (var parcel in GetParcels())
            {
                if (parcel.droneAtParcel != null || parcel.Scheduled != null) continue;
                Location CurrentSenderLocation = FindCustomer(parcel.customerAtParcelSender.Id).Location;
                Location CurrentReciverLocation = FindCustomer(parcel.customerAtParcelReciver.Id).Location;
                Location CloslestStationLocation = closestStationToLoacation(CurrentReciverLocation).Location;
                double electricityNeeded = CalculateElectricity(CurrentSenderLocation, CurrentReciverLocation, parcel.Weight)
                    + CalculateElectricityWhenFree(CurrentReciverLocation, CloslestStationLocation)
                    + CalculateElectricityWhenFree(droneBL.Location, CurrentSenderLocation);
                if (electricityNeeded < droneBL.Battery)
                {
                    if (bestParcel.Pritority < parcel.Pritority)
                    {
                        bestParcel = parcel;
                    }
                    else if (bestParcel.Pritority == parcel.Pritority)
                    {
                        if (bestParcel.Weight < parcel.Weight)
                        {
                            bestParcel = parcel;
                        }
                        else if (bestParcel.Weight == parcel.Weight)
                        {
                            Location bestParcelLocaion = FindCustomer(bestParcel.customerAtParcelSender.Id).Location;
                            if (distanceBetweenTwoLocationds(droneBL.Location, CurrentSenderLocation) < distanceBetweenTwoLocationds(droneBL.Location, bestParcelLocaion)) ;
                            {
                                bestParcel = parcel;
                            }
                        }
                    }
                }
            }

            Location SenderLocation = FindCustomer(bestParcel.customerAtParcelSender.Id).Location;
            Location ReciverLocation = FindCustomer(bestParcel.customerAtParcelReciver.Id).Location;
            droneBL.DroneStatus = DroneStatus.Delivery;
            double distance1 = distanceBetweenTwoLocationds(SenderLocation, ReciverLocation);

            droneBL.ParcelInDelivery = new ParcelInDelivery()
            {
                customerAtParcelTheReciver = bestParcel.customerAtParcelReciver,
                customerAtParcelTheSender = bestParcel.customerAtParcelSender,
                Id = bestParcel.Id,
                IsWating = false,
                locationCollect = SenderLocation,
                locationTarget = ReciverLocation,
                distance = distance1,
                pritorities = bestParcel.Pritority,
                weightCategories = bestParcel.Weight
            };
            updateDrone(droneBL);
            bestParcel.droneAtParcel = new DroneAtParcel { Id = droneBL.Id, Battery = droneBL.Id, Location = droneBL.Location };
            bestParcel.Scheduled = DateTime.Now;
            updateParcel(bestParcel);

        }
        /// <summary>
        /// supply Parcel By Drone
        /// </summary>
        /// <param name="DroneID"></param>

        public void supplyParcelByDrone(int DroneID)
        {

            Drone droneBL = FindDrone(DroneID);

            if (droneBL.DroneStatus != DroneStatus.Delivery)
                throw new DroneIsNotInCorrectStatus("drone is not in delivery");
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
            Parcel parcel = FindParcel(droneBL.Id);
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
        private double CalculateElectricity(Location location1, Location location2, WeightCategories weight)
        {

            double distance = distanceBetweenTwoLocationds(location1, location2);
            switch (weight)
            {
                case WeightCategories.Light:
                    return (distance * this.ElectricityUseWhenLight);
                case WeightCategories.Medium:
                    return (distance * this.ElectricityUseWhenMedium);
                case WeightCategories.Heavy:
                    return (distance * this.ElectricityUseWhenheavy);

                default:
                    return 0;


            }
        }

        private double CalculateElectricityWhenFree(Location location1, Location location2)
        {

            double distance = distanceBetweenTwoLocationds(location1, location2);

            return distance * ElectricityUseWhenLight;

        }

        private Station GetRandomStation()
        {
            int numOfStations = GetStations().ToList().Count;
            Station station = GetStations().ToList()[rand.Next(0, numOfStations)];
            return station;
        }
    }
}

