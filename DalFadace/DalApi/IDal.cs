using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;



namespace DalApi
{
    public interface IDal
    {
        #region Drone
        void AddDrone(Drone drone);
        IEnumerable<Drone> GetDrones(Predicate<Drone> getBy = null);
        Drone GetDroneById(int id);
        void UpdateDrone(Drone drone);
        #endregion

        #region Parcel
        int AddParcel(Parcel parcel);
        IEnumerable<Parcel> GetParcels(Predicate<Parcel> getBy = null);
        Parcel GetParcelById(int id);
        void UpdateParcel(Parcel parcel);
        void DeleteParcel(int id);
        #endregion

        #region Customer
        void AddCustomer(Customer customer);
        IEnumerable<Customer> GetCustomers(Predicate<Customer> getBy = null);
        Customer GetCustomerById( int id);
        void UpdateCustomer(Customer customer);
        #endregion

        #region Station
        void AddStation(Station station);
        IEnumerable<Station> GetStations(Predicate<Station> getBy = null);
        Station GetStationById(int id);
        void UpdateStation(Station parcel);
        #endregion

        #region DroneCharge
        void AddDroneCharge(DroneCharge droneCharge);
        IEnumerable<DroneCharge> GetDroneCharges(Predicate<DroneCharge> getBy = null);
        DroneCharge GetDroneChargeById(int droneId);
        void DeleteDroneCharge(int id);
        void DeleteAllDroneCharges();
        #endregion

        #region Managers
        IEnumerable<Manager> GetManagers(Predicate<Manager> findBy);
        #endregion

        double[] GetElectricityUse();

    }

}
