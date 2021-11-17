using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;


namespace IDal
{
    public interface IDal
    {
        public void addDrone(DroneDL drone);

        public void addCustomer(CustomerDL customer);

        public void addParcel(ParcelDL parcel);

        public void addStation(StationDL station);

        public void addDronCharge(DroneChargeDL droneCharge);

        public IEnumerable<StationDL> GetStations();


        public IEnumerable<DroneDL> GetDrones();


        public IEnumerable<CustomerDL> GetCustomer();


        public IEnumerable<ParcelDL> GetParcel();

        public IEnumerable<DroneChargeDL> GetDroneCharges();


        public ParcelDL findParcel(int id);


        public StationDL findStation(int id);


        public CustomerDL findCustomer(int id);


        public DroneDL findDrone(int id);



        public void updateDrone(DroneDL drone);

        public void updateParcel(ParcelDL parcel);

        public void updateCustomer(CustomerDL customer);


        public void updateDronecharge(DroneChargeDL dronecharge);

        public IEnumerable<StationDL> GetStationsWithEmptyChargingSlots();

        public IEnumerable<ParcelDL> GetNotBelongedParcels();


        public void belongPacelToADrone(ParcelDL parcel);


        public void CollectAParcelByDrone(ParcelDL parcel);


        public void DeliverParcelToCustomer(ParcelDL parcel);


        public void SendDroneForCharging(DroneDL drone, StationDL station);

        public void ReleaseDroneFromCharging(DroneDL drone);

        public void removeDroneCharge(int id);

        public double[] RequestElectricityUse();
    }

}
