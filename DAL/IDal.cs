using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using DO;



namespace DalApi
{
    public interface IDal
    {
        public void DeleteParcel(int id);
        public void AddDrone(Drone drone);

        public void AddCustomer(Customer customer);

        public void AddParcel(Parcel parcel);

        public void AddStation(Station station);

        public void AddDroneCharge(DroneCharge droneCharge);

        public IEnumerable<Station> GetStations(Predicate<Station> getBy = null);


        public IEnumerable<Drone> GetDrones(Predicate<Drone> getBy = null);


        public IEnumerable<Customer> GetCustomers(Predicate<Customer> getBy = null);


        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> getBy = null);

        public IEnumerable<DroneCharge> GetDroneCharges(Predicate<DroneCharge> getBy = null);

        public Station GetStationById(int id);

        public Customer GetCustomerById( int id);

        public Parcel GetParcelById(int id);
        public Drone GetDroneById(int id);
        public DroneCharge GetDroneChargeById(int droneId);

        public void UpdateDrone(Drone drone);

        public void UpdateParcel(Parcel parcel);
        public void UpdateStation(Station parcel);

        public void UpdateCustomer(Customer customer);

        public void DeleteDroneCharge(int id);

        public double[] RequestElectricityUse();
        public IEnumerable<Manager> GetManagers(Predicate<Manager> findBy);

    }

}
