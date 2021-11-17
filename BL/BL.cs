﻿using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using IDAL.DO;


namespace BL
{
    public partial class BL
    {
        public List<DroneBL> dronesBL;
        Random rand = new Random();
        DalObject.DalObject dalObject;

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
            dronesBL = new List<DroneBL>();
            List<ParcelDL> parcelDLs = dalObject.GetParcel().ToList();

            foreach (var drone in dalObject.GetDrones())
            {
                DroneBL droneBL = new DroneBL() { Id = drone.Id, Model = drone.Model, MaxWeight = drone.MaxWeight };
                ParcelDL parcel = parcelDLs.Find(p => p.DroneId == drone.Id);
                if (!parcel.Equals(null))
                {
                    droneBL.DroneStatus = DroneStatus.Delivery;
                    ParcelBL parcelBL = convertToParcelBL(parcel);
                    if (parcel.PickedUp < DateTime.Now)
                    {
                        int senderID = parcelBL.customerAtParcelSender.Id;
                        Location senderLocation = convertToCustomerBL(dalObject.findCustomer(senderID)).Location;
                        droneBL.Location = closestStationToLoacation(senderLocation).Location;
                    }
                    else
                    {
                        int reciverID = parcelBL.customerAtParcelSender.Id;
                        droneBL.Location = convertToCustomerBL(dalObject.findCustomer(reciverID)).Location;
                    }
                    double electicityNeeded = CalculateElectricity(droneBL.Location, convertToCustomerBL(dalObject.findCustomer(parcelBL.customerAtParcelReciver.Id)).Location, parcelBL.Weight);
                    droneBL.Battery = rand.Next((int)electicityNeeded, 100);
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
                }

                if (droneBL.DroneStatus == DroneStatus.Free)
                {
                    List<ParcelDL> deliveredParcels = dalObject.GetParcel().ToList().FindAll(p => !p.Delivered.Equals(null));
                    ParcelBL randomDeliveredParcel = convertToParcelBL(deliveredParcels[rand.Next(0, deliveredParcels.Count)]);
                    int recieverID = randomDeliveredParcel.customerAtParcelReciver.Id;
                    droneBL.Location = convertToCustomerBL(dalObject.findCustomer(recieverID)).Location;
                    double distance = distanceBetweenTwoLocationds(droneBL.Location, closestStationToLoacation(droneBL.Location).Location);
                    droneBL.Battery -= distance * ElectricityUseWhenFree;
                }
                dronesBL.Add(droneBL);
            }

        }

        public void ParcelToTransfor(int sendedId, int reciveId, int weigth, int prioty)
        {
            CustomerAtParcel customerAtParcelsendedr = new CustomerAtParcel() { Id = sendedId };
            CustomerAtParcel customerAtParcelreciver = new CustomerAtParcel() { Id = reciveId };

            ParcelBL parcelBL = new ParcelBL() { customerAtParcelSender = customerAtParcelsendedr, customerAtParcelReciver = customerAtParcelreciver, Weight = (IDAL.DO.WeightCategories)weigth, Pritority = (IDAL.DO.Pritorities)prioty };

            parcelBL.Requested = DateTime.Now;
            parcelBL.droneAtParcel = null;
        }

        public void releaseDroneFromCharging(int idDrone, double timeInCharging)
        {
            DroneBL droneBL = dronesBL.Find(d => d.Id == idDrone);
            if (droneBL.DroneStatus != DroneStatus.Free)
                //TODO - throw exeption
                ;
            DroneDL droneDL = dalObject.findDrone(idDrone);
            droneDL.Battery += timeInCharging * RateOfCharching;
            droneBL.Battery += timeInCharging * RateOfCharching;
            droneBL.DroneStatus = DroneStatus.Free;
            updateDrone(droneBL);
            dalObject.updateDrone(droneDL);
            //TODO העלאת מספר עמדות טעינה פנויות ב1
            dalObject.removeDroneCharge(idDrone);
        }

        public void collectParcleByDrone(int idDrone)
        {
            DroneBL droneBL = dronesBL.Find(d => d.Id == idDrone);
            if (droneBL.DroneStatus != DroneStatus.Delivery)
                //TODO - throw exeption
                ;
            IDAL.DO.DroneDL droneDL = dalObject.findDrone(idDrone);
            IDAL.DO.CustomerDL customerSernder = dalObject.GetCustomer().ToList().Find(d => d.Id == droneBL.ParcelAtTransfor.customerAtDeliverySender.Id);
            Location locationSender = convertToCustomerBL(customerSernder).Location;
            //double distance = distanceBetweenTwoLocationds(locationSender, locationReciver);
            IDAL.DO.ParcelDL parcelDL = dalObject.findParcel(droneBL.ParcelAtTransfor.ID);
            IDAL.DO.WeightCategories weight = parcelDL.Weight;
            double useElectricity = CalculateElectricity(droneBL.Location, locationSender, weight);
            droneBL.Battery -= useElectricity;
            droneDL.Battery -= useElectricity;
            droneBL.Location = locationSender;
            updateDrone(droneBL);
            dalObject.updateDrone(droneDL);
            parcelDL.PickedUp = DateTime.Now;
            dalObject.updateParcel(parcelDL);

        }



        public void sendDroneToCharge(int droneId)
        {
            var drone = dronesBL.Find(d => d.Id == droneId);
            if (drone.DroneStatus == 0)
            {
                StationBL stationMini = closestStationToLoacation(drone.Location);
                if (dalObject.RequestElectricityUse()[0] * distanceBetweenTwoLocationds(stationMini.Location, drone.Location) > drone.Battery)
                {
                    IDAL.DO.StationDL stationDL = dalObject.GetStations().ToList().Find(s => s.Id == stationMini.Id);
                    IDAL.DO.DroneDL droneDL = dalObject.GetDrones().ToList().Find(s => s.Id == stationMini.Id);
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


        public double distanceBetweenTwoLocationds(Location location1, Location location2)
        {
            return Math.Sqrt(Math.Pow(location1.Longitude - location2.Longitude, 2)
                + Math.Pow(location1.Latitude - location2.Latitude, 2) * 1.0);


        }




        public void AssignAParcelToADrone(int id)
        {
            StationBL stationBL = new StationBL();
            ParcelBL parcel = new ParcelBL();
            ParcelBL parcel1 = new ParcelBL();
            CustomerBL customerBLsender = new CustomerBL();
            IDAL.DO.CustomerDL customerParcel = new IDAL.DO.CustomerDL();
            CustomerBL customerBLreciver = new CustomerBL();
            IDAL.DO.ParcelDL parcelDL = new IDAL.DO.ParcelDL();
            List<CustomerBL> customerBLs = GetCustomers().ToList();
            List<IDAL.DO.ParcelDL> parcelDLs = new List<IDAL.DO.ParcelDL>();
            IDAL.DO.DroneDL droneDL = dalObject.findDrone(id);
            DroneBL droneBL = new DroneBL();
            droneBL = dronesBL.Find(s => s.Id == id);
            if (droneBL.DroneStatus != DroneStatus.Free)
            {
                //throw Exception
            }


            foreach (var p in parcelDLs)
            {


                parcel = convertToParcelBL(p);
                customerParcel = dalObject.findCustomer(parcelDL.SenderId);
                customerBLsender = customerBLs.Find(e => e.Id == parcel.customerAtParcelSender.Id);
                customerBLreciver = customerBLs.Find(e => e.Id == parcel.customerAtParcelReciver.Id);
                stationBL = minimumDistanceFromStation(customerBLreciver.Location);
                double electricidy1 = CalculateElectricity(customerBLsender.Location, customerBLreciver.Location, parcel.Weight) + distanceBetweenTwoLocationds(stationBL.Location, droneBL.Location) * this.ElectricityUseWhenFree + distanceBetweenTwoLocationds(droneBL.Location, customerBLsender.Location) * this.ElectricityUseWhenFree;
                double distanceToCharging1 = distanceBetweenTwoLocationds(customerBLreciver.Location, stationBL.Location);
                if (electricidy1 > droneBL.Battery)
                {
                    if (parcelDL.Pritority < p.Pritority)
                    {
                        parcelDL = p;

                foreach (IDAL.DO.ParcelDL p in dalObject.GetParcel())
                {
                    parcel = convertToParcelBL(p);
                    customerBLsender = customerBLs.Find(e => e.Id == parcel.customerAtParcelSender.Id);
                    customerBLreciver = customerBLs.Find(e => e.Id == parcel.customerAtParcelReciver.Id);
                    stationBL = closestStationToLoacation(customerBLreciver.Location);
                    //Calculate Electricity if the drone have enaph battery to do it.
                    if (CalculateElectricity(customerBLsender.Location, customerBLreciver.Location, parcel.Weight) + distanceBetweenTwoLocationds(stationBL.Location, droneBL.Location) * this.ElectricityUseWhenFree + distanceBetweenTwoLocationds(droneBL.Location, customerBLsender.Location) * this.ElectricityUseWhenFree < droneBL.Battery)
                    {
                        parcelDL.Remove(p);
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

                    }

                    droneBL.DroneStatus = DroneStatus.Delivery;
                    updateDrone(droneBL);
                    parcelDL.DroneId = id;
                    parcelDL.Scheduled = DateTime.Now;
                    dalObject.updateParcel(parcelDL);
                }
                IDAL.DO.Pritorities pritority = parcelDLs.Max(s => s.Pritority);
                parcelDL = parcelDLs.FindAll(s => s.Pritority == pritority);
                parcelDL = parcelDL.FindAll(s => s.Weight < droneBL.MaxWeight);
                IDAL.DO.WeightCategories weightCategories = parcelDL.Max(s => s.Weight);
                parcelDL = parcelDL.FindAll(s => s.Weight == weightCategories);

            }
        }

                }

        public void supllyParcelByDrone(int DroneID)
        {
            DroneBL droneBL = dronesBL.Find(d => d.Id == DroneID);
            IDAL.DO.DroneDL droneDL = dalObject.findDrone(DroneID);
            if (droneBL.DroneStatus != DroneStatus.Delivery)
                //TODO throw exception
                ;
            IDAL.DO.CustomerDL customerSernder = dalObject.GetCustomer().ToList().Find(d => d.Id == droneBL.ParcelAtTransfor.customerAtDeliverySender.Id);
            Location locationSender = convertToCustomerBL(customerSernder).Location;
            IDAL.DO.CustomerDL customerReciver = dalObject.GetCustomer().ToList().Find(d => d.Id == droneBL.ParcelAtTransfor.customerAtDeliveryReciver.Id);
            Location locationReciver = convertToCustomerBL(customerReciver).Location;
            //double distance = distanceBetweenTwoLocationds(locationSender, locationReciver);
            IDAL.DO.ParcelDL parcelDL = dalObject.findParcel(droneBL.ParcelAtTransfor.ID);
            IDAL.DO.WeightCategories weight = parcelDL.Weight;
            double useElectricity = CalculateElectricity(locationSender, locationReciver, weight);
            droneBL.Battery -= useElectricity;
            droneDL.Battery -= useElectricity;
            droneBL.Location = locationReciver;
            updateDrone(droneBL);
            dalObject.updateDrone(droneDL);
            parcelDL.Delivered = DateTime.Now;
            dalObject.updateParcel(parcelDL);

            }
        }

        }



           







            //List<IDAL.DO.ParcelDL> parcelDLs = new List<IDAL.DO.ParcelDL>();
            //List<IDAL.DO.ParcelDL> parcelDL = new List<IDAL.DO.ParcelDL>();
            //List<ParcelBL> parcelBLs = new List<ParcelBL>();
            //List<CustomerBL> customerBLs = GetCustomerFromBL();
            //CustomerBL customerBLsender = new CustomerBL();
            //CustomerBL customerBLreciver = new CustomerBL();
            //StationBL stationBL = new StationBL();

            //ParcelBL parcel = new ParcelBL();
            //DroneBL droneBL = new DroneBL();
            //IDAL.DO.DroneDL droneDL = dalObject.findDrone(id);
            //droneBL = dronesBL.Find(s => s.Id == id);
            //if (droneBL.DroneStatus == DroneStatus.Free)
            //{

            //    foreach (IDAL.DO.ParcelDL p in  dalObject.GetParcel())
            //    {
            //        parcel = convertToParcelBL(p);
            //        customerBLsender = customerBLs.Find(e => e.Id == parcel.customerAtParcelSender.Id);
            //        customerBLreciver = customerBLs.Find(e => e.Id == parcel.customerAtParcelReciver.Id);
            //        stationBL = minimumDistanceFromStation(customerBLreciver.Location);
            //        //Calculate Electricity if the drone have enaph battery to do it.
            //        if (CalculateElectricity(customerBLsender.Location, customerBLreciver.Location, parcel.Weight)+ distanceBetweenTwoLocationds(stationBL.Location,droneBL.Location)*this.ElectricityUseWhenFree+ distanceBetweenTwoLocationds(droneBL.Location, customerBLsender.Location)*this.ElectricityUseWhenFree < droneBL.Battery)
            //        {
            //            parcelDL.Remove(p);
            //        }

            //    }
            //    IDAL.DO.Pritorities pritority = parcelDLs.Max(s => s.Pritority);
            //    parcelDL = parcelDLs.FindAll(s => s.Pritority == pritority);
            //    parcelDL = parcelDL.FindAll(s => s.Weight < droneBL.MaxWeight);
            //    IDAL.DO.WeightCategories weightCategories = parcelDL.Max(s => s.Weight);
            //    parcelDL = parcelDL.FindAll(s => s.Weight == weightCategories);




    
                



            public double CalculateElectricity(Location location1 , Location location2, IDAL.DO.WeightCategories weight)
            {
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
