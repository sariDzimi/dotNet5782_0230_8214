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
        public void addDrone(Drone drone);

        public void addCustomer(Customer customer);

        public void addParcel(Parcel parcel);

        public void addStation(Station station);

        public void addDronCharge(DroneCharge droneCharge);

        public IEnumerable<Station> GetStations();


        public IEnumerable<Drone> GetDrones();


        public IEnumerable<Customer> GetCustomer();


        public IEnumerable<Parcel> GetParcel();

        public IEnumerable<DroneCharge> GetDroneCharges();


        public Parcel FindParcelBy(Predicate<Parcel> predicate);


        public Station findStationBy( Predicate<Station> predicate);
        public Station findStationById( int id);


        public Customer findCustomerBy( Predicate<Customer> predicate);
        public Customer findCustomerById( int id);


        public Drone findDroneBy( Predicate<Drone> predicate);
        public Drone findDroneById(int id);

        

        public void updateDrone(Drone drone);

        public void updateParcel(Parcel parcel);
        public void updateStation(Station parcel);
        public IEnumerable<Parcel> GetParcelIdBy( Predicate<Parcel> predicate);
        public IEnumerable<Station> GetStationIdBy(Predicate<Station> predicate);

        public void updateCustomer(Customer customer);


        public void updateDronecharge(DroneCharge dronecharge);

        public void removeDroneCharge(int id);

        public double[] RequestElectricityUse();
        public IEnumerable<Manager> GetManeger();
        public Manager findManegerBy(Predicate<Manager> findBy);

    }

}
