using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;



namespace IBL
{
    public interface IBL
    {
        public void ParcelToTransfor(int sendedId, int reciveId, int weigth, int prioty);
        public void releaseDroneFromCharging(int idDrone, double timeInCharging);
        public void collectParcleByDrone(int idDrone);
        public void sendDroneToCharge(int droneId);
        public void AssignAParcelToADrone(int id);
        public void supplyParcelByDrone(int DroneID);
        public void addStationToBL(int id, int name, Location location, int slots);
        public void addDroneToBL(int id, int status, string model, int numberStaion);
        public void addCustomerToBL(int id, string name, string phone, Location location);
        public int addParcelToBL(int SenderId, int reciverId, int weight, int prionity);
        public void addStationToDL(StationBL station);
        public void addParcelToDL(ParcelBL parcel);
        public void addCustomerToDL(CustomerBL customer);
        public void addDroneChargeToDL(DroneChargeBL droneCharge);
        public StationBL convertToStationBL(IDAL.DO.StationDL s);
        public CustomerBL convertToCustomerBL(IDAL.DO.CustomerDL c);
        public ParcelBL convertToParcelBL(IDAL.DO.ParcelDL p);
        public int calculateFreeChargeSlotsInStation(int statioinID);
        public IEnumerable<ParcelBL> GetNotAsignedParcels();
        public IEnumerable<StationBL> GetStationsWithEmptyChargeSlots();
        public StationBL FindStation(int id);
        public DroneBL FindDrone(int id);
        public CustomerBL FindCuatomer(int id);
        public ParcelBL FindParcel(int id);
        public IEnumerable<StationBL> GetStations();
        public IEnumerable<ParcelBL> GetParcels();
        public IEnumerable<CustomerBL> GetCustomers();
        public IEnumerable<DroneBL> GetDrones();
        public void updateDroneModel(int id, string model);
        public void updateDataStation(int id, int name = -1, int totalChargeSlots = -1);
        public void updateDataCustomer(int id, string name = null, string phone = null);
        public void updateDrone(DroneBL drone);

    }
}





