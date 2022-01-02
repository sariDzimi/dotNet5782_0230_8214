using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;



namespace BlApi
{
    public interface IBL
    {
        
        public void releaseDroneFromCharging(int idDrone, double timeInCharging);
        public void collectParcleByDrone(int idDrone);
        public void sendDroneToCharge(int droneId);
        public void AssignAParcelToADrone(int id);
        public void supplyParcelByDrone(int DroneID);
       
        public void addDroneToBL(int id, int status, string model, int numberStaion);
        public IEnumerable<Station> GetStations();
        public IEnumerable<Parcel> GetParcels();
        public IEnumerable<Customer> GetCustomers();
        public IEnumerable<Drone> GetDrones();
        public void updateDroneModel(int id, string model);
        public void updateDataStation(int id, int name = -1, int totalChargeSlots = -1);
        //public void updateDataCustomer(int id, string name = null, string phone = null);
        public void updateDrone(Drone drone);
        public IEnumerable<DroneToList> GetDroneToLists();
        public IEnumerable<DroneToList> GetDroneToListsBy(Predicate<Drone> findBy);
        public Drone ConvertDroneToListToDrone(DroneToList droneToList);

        public Station FindStationBy(Predicate<Station> findBy);
        public Station FindStation(int id);
        public Drone FindDrone(int id);
        public Drone FindDroneBy(Predicate<Drone> findBy);
        public Customer FindCustomerBy(Predicate<Customer> findBy);
        public Parcel FindParcelBy(Predicate<Parcel> findBy);
        public Parcel FindParcel(int id);
        public DroneCharge FindDroneCharge(int droneId);
        public IEnumerable<StationToList> GetStationToLists();
        public IEnumerable<Parcel> GetParcelsBy(Predicate<Parcel> findBy);
        public IEnumerable<ParcelToList> GetParcelToLists();

        public IEnumerable<ParcelToList> GetParcelsToListBy(Predicate<ParcelToList> findBy);
        public IEnumerable<CustomerToList> GetCustomerToLists();

        public ParcelToList convertParcelToParcelToList(Parcel parcel);

        public Parcel ConvertParcelToListToParcel(ParcelToList parcelToList);
        public IEnumerable<StationToList> GetStationToListBy(Predicate<StationToList> findBy);

        public void addParcelToDL(Parcel parcel);
        public void addStationToDL(Station station);

        public void updateStation(Station station);

        public void updateParcel(Parcel parcel);

        public void updateCustomer(Customer customer);

        public Customer FindCustomer(int id);

        public void addCustomerToDL(Customer customer);

        public bool CheckWorkerIfExixst(Manager manager);

    }
}





