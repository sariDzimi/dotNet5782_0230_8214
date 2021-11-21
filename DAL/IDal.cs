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

        public void removeDroneCharge(int id);

        public double[] RequestElectricityUse();
    }

}
