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
        public void ReleaseDroneFromCharging(int idDrone, double timeInCharging);
        public void CollectParcleByDrone(int idDrone);
        public void SendDroneToCharge(int droneId);
        public void AssignAParcelToADrone(int id);
        public void SupplyParcelByDrone(int DroneID);
       
        public void AddDrone(int id, WeightCategories weight, string model, int numberStaion);
        public IEnumerable<Station> GetStations();
        public IEnumerable<Parcel> GetParcels();
        public IEnumerable<Customer> GetCustomers();
        public IEnumerable<Drone> GetDrones();
        public void UpdateDroneModel(int id, string model);
        public void UpdateDataStation(int id, int name = -1, int totalChargeSlots = -1);
        public void UpdateDrone(Drone drone);
        public IEnumerable<DroneToList> GetDroneToLists();
        public IEnumerable<DroneToList> GetDroneToListsBy(Predicate<Drone> findBy);
        public Drone ConvertDroneToListToDrone(DroneToList droneToList);

        public IEnumerable<Drone> GetDronesBy(Predicate<Drone> findBy);

        public Drone GetDroneById(int id);

        public IEnumerable<Customer> GetCustomersBy(Predicate<Customer> findBy);

        public Customer GetCustomerById(int id);
        public IEnumerable<Station> GetStationsBy(Predicate<Station> findBy);

        public Station GetStationById(int id);

        public IEnumerable<Parcel> GetParcelsBy(Predicate<Parcel> findBy);
        public Parcel GetParcelById(int id);

        public IEnumerable<ParcelToList> GetParcelsToListBy(Predicate<ParcelToList> findBy);

        public IEnumerable<StationToList> GetStationToLists();
        public IEnumerable<ParcelToList> GetParcelToLists();

        public IEnumerable<CustomerToList> GetCustomerToLists();

        public CustomerToList ConvertCustomerToTypeOfCustomerToList(Customer customer);
        public ParcelToList convertParcelToTypeOfParcelToList(Parcel parcel);

        public Parcel ConvertParcelToTypeOfListToParcel(ParcelToList parcelToList);
        public IEnumerable<StationToList> GetStationToListBy(Predicate<StationToList> findBy);

        public int AddParcel(Parcel parcel);
        public void AddStation(Station station);

        public void UpdateStation(Station station);

        public void UpdateParcel(Parcel parcel);

        public void UpdateCustomer(Customer customer);

        public void AddCustomer(Customer customer);

        public bool IsValidMamager(Manager manager);

        public void DeleateParcel(int id);

        public void StartSimulation(Drone drone, Action<Drone, int> action, Func<bool> func);
        public double GetRateOFCharging();
        public DroneToList ConvertDroneToTypeOfDroneToList(Drone drone);
        public StationToList convertStationToTypeOfStationToList(Station station);


    }
}





