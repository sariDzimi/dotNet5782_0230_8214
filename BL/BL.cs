using BO;
using DalApi;
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
        private DalApi.IDal dal;

        private double ElectricityUseWhenFree = 0;
        private double ElectricityUseWhenLight = 0;
        private double ElectricityUseWhenMedium = 0;
        private double ElectricityUseWhenheavy = 0;
        private double RateOfCharching = 0.1;



        public BL()
        {
            //intilizing BL members
            dal = DalFactory.GetDal();
            double[] ElectricityUse = dal.GetElectricityUse();
            ElectricityUseWhenFree = ElectricityUse[0];
            ElectricityUseWhenLight = ElectricityUse[1];
            ElectricityUseWhenMedium = ElectricityUse[2];
            ElectricityUseWhenheavy = ElectricityUse[3];
            RateOfCharching = ElectricityUse[4];
            dronesBL = new List<BO.Drone>();

            dal.DeleteAllDroneCharges();//we delete here all droneCharges, 
                                        //because when we loop over the drones we creat new droneCharges
                                        //for the drones that are in maintanace
            
            List<DO.Parcel> parcelDLs = dal.GetParcels().ToList();

            foreach (var droneDL in dal.GetDrones())
            {
                BO.Drone droneBL = new BO.Drone()
                {
                    Id = droneDL.Id,
                    Model = droneDL.Model,
                    MaxWeight = (BO.WeightCategories)droneDL.MaxWeight
                };

                //if the drone has any parcel that are assigned to him
                if (GetParcelsBy(p => p.droneAtParcel != null && p.droneAtParcel.Id == droneBL.Id).Any())
                {
                    Parcel parcel = GetParcelsBy(p => p.droneAtParcel != null && p.droneAtParcel.Id == droneBL.Id).First();

                    //if a parcel is assigned to a drone, but not delivered yet,
                    //it means that the drone is in a status of delivery
                    if (parcel.Delivered == null)
                    {
                        droneBL.DroneStatus = DroneStatus.Delivery;
                        Location locationSender = GetCustomerById(parcel.customerAtParcelSender.Id).Location;
                        Location locationReciver = GetCustomerById(parcel.customerAtParcelReciver.Id).Location;
                        parcel.Scheduled = DateTime.Now;
                        double distance1 = calculateDistanceBetweenTwoLocationds(locationSender, locationReciver);
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
                        UpdateParcel(parcel);

                        if (parcel.PickedUp.Equals(null))
                            droneBL.Location = closestStationToLoacationWithFreeChargeSlots(locationSender).Location;
                        else
                            droneBL.Location = locationReciver;

                        double electicityNeeded = calculateElectricity(droneBL.Location, locationReciver, parcel.Weight);
                        droneBL.Battery = rand.Next((int)electicityNeeded, 100);
                    }

                    //if drone is not in delivery he gets a random status
                    else
                        droneBL.DroneStatus = (DroneStatus)rand.Next(0, 2);
                }

                //if drone is not in delivery he gets a random status
                else
                    droneBL.DroneStatus = (DroneStatus)rand.Next(0, 2);

                //if drone is in maintanance we add a new droneCharge
                if (droneBL.DroneStatus == DroneStatus.Maintenance)
                {
                    Station station = getRandomStation();
                    droneBL.Location = station.Location;
                    droneBL.Battery = rand.Next(0, 21);
                    DO.DroneCharge droneChargeDL = new DO.DroneCharge() { DroneId = droneDL.Id, StationId = station.Id };
                    dal.AddDroneCharge(droneChargeDL);
                    droneBL.Location = station.Location;
                }

                //if the drone is free we assing his location to be in loacation of a random customer that has a dleivered parcel
                else if (droneBL.DroneStatus == DroneStatus.Free)
                {
                    try
                    {
                        //gets all customer with delivered parcels
                        List<BO.Customer> customers = GetCustomersBy(c => c.ParcelsSentedToCustomer.Any(p => p.ParcelStatus == ParcelStatus.Delivered)).ToList();
                        Customer customer = customers[(int)rand.Next(0, customers.Count)];
                        droneBL.Location = customer.Location;
                    }
                    catch (Exception)
                    {
                        droneBL.Location = getRandomStation().Location;
                    }

                    double electicityNeeded = calculateElectricityWhenFree(droneBL.Location, closestStationToLoacationWithFreeChargeSlots(droneBL.Location).Location);
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
        //public void ParcelToTransfor(int sendedId, int reciveId, int weigth, int prioty)
        //{
        //    CustomerAtParcel customerAtParcelsendedr = new CustomerAtParcel() { Id = sendedId };
        //    CustomerAtParcel customerAtParcelreciver = new CustomerAtParcel() { Id = reciveId };

        //    Parcel parcelBL = new Parcel() { customerAtParcelSender = customerAtParcelsendedr, customerAtParcelReciver = customerAtParcelreciver, Weight = (BO.WeightCategories)weigth, Pritority = (BO.Pritorities)prioty };

        //    parcelBL.Requested = DateTime.Now;
        //    parcelBL.droneAtParcel = null;
        //}

        public void StartSimulation(Drone drone, Action<Drone, int> action, Func<bool> func)
        {
            Simulation simulation = new Simulation(this, drone, action, func);
        }

        public double GetRateOFCharging()
        {
            return this.RateOfCharching;
        }
    }
}

