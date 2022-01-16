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
        void DeleteParcel(int id);
        void AddDrone(Drone drone);

        void AddCustomer(Customer customer);

        void AddParcel(Parcel parcel);

        void AddStation(Station station);

        void AddDroneCharge(DroneCharge droneCharge);

        IEnumerable<Station> GetStations(Predicate<Station> getBy = null);


        IEnumerable<Drone> GetDrones(Predicate<Drone> getBy = null);


        IEnumerable<Customer> GetCustomers(Predicate<Customer> getBy = null);


        IEnumerable<Parcel> GetParcels(Predicate<Parcel> getBy = null);

        IEnumerable<DroneCharge> GetDroneCharges(Predicate<DroneCharge> getBy = null);

        Station GetStationById(int id);

        Customer GetCustomerById( int id);

        Parcel GetParcelById(int id);
        Drone GetDroneById(int id);
        DroneCharge GetDroneChargeById(int droneId);

        void UpdateDrone(Drone drone);

        void UpdateParcel(Parcel parcel);
        void UpdateStation(Station parcel);

        void UpdateCustomer(Customer customer);

        void DeleteDroneCharge(int id);

        double[] RequestElectricityUse();
        IEnumerable<Manager> GetManagers(Predicate<Manager> findBy);

    }

}
