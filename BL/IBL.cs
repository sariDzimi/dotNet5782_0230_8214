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
        public DroneToList ConvertDroneToDroneToList(Drone drone);
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
        public void addStationToDL(Station station);
        public void addParcelToDL(Parcel parcel);
        public void addCustomerToDL(Customer customer);
        public void addDroneChargeToDL(DroneCharge droneCharge);
        public Station convertToStationBL(IDAL.DO.Station s);
        public Customer convertToCustomerBL(IDAL.DO.Customer c);
        public Parcel convertToParcelBL(IDAL.DO.Parcel p);
        public int calculateFreeChargeSlotsInStation(int statioinID);
        public IEnumerable<Parcel> GetNotAsignedParcels();
        public IEnumerable<Station> GetStationsWithEmptyChargeSlots();
        public Station FindStationBy(Predicate<Station> predicate);
        public Drone FindDroneBy(Predicate<Drone> predicate);
        public Customer FindCustomerBy(Predicate<Customer> predicate);
        public Parcel FindParcelBy(Predicate<Parcel> predicate);
        public IEnumerable<Station> GetStations();
        public IEnumerable<Parcel> GetParcels();
        public IEnumerable<Customer> GetCustomers();
        public IEnumerable<Drone> GetDrones();
        public void updateDroneModel(int id, string model);
        public void updateDataStation(int id, int name = -1, int totalChargeSlots = -1);
        public void updateDataCustomer(int id, string name = null, string phone = null);
        public void updateDrone(Drone drone);

    }
}





