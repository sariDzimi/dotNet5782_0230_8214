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
        public void addDrone(Drone drone);

        public void addCustomer(Customer customer);

        public void addParcel(Parcel parcel);

        public void addStation(Station station);

        public void addDronCharge(DroneCharge droneCharge);

        public IEnumerable<Station> GetStations(Predicate<Station> getBy = null);


        public IEnumerable<Drone> GetDrones(Predicate<Drone> getBy = null);


        public IEnumerable<Customer> GetCustomers(Predicate<Customer> getBy = null);


        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> getBy = null);

        public IEnumerable<DroneCharge> GetDroneCharges(Predicate<DroneCharge> getBy = null);

        public Station GetStationById(int id);

        public Customer GetCustomerById( int id);

        public Drone GetDroneById(int id);
        public DroneCharge GetDroneChargeById(int droneId);

        public void updateDrone(Drone drone);

        public void updateParcel(Parcel parcel);
        public void updateStation(Station parcel);

        public void updateCustomer(Customer customer);

        public void updateDronecharge(DroneCharge dronecharge);

        public void DeleteDroneCharge(int id);

        public double[] RequestElectricityUse();
        public IEnumerable<Manager> GetManagers(Predicate<Manager> findBy);

    }

}
