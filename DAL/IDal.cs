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


        public Parcel findParcel(int id);


        public Station findStation(int id);


        public Customer findCustomer(int id);


        public Drone findDrone(int id);



        public void updateDrone(Drone drone);

        public void updateParcel(Parcel parcel);

        public void updateCustomer(Customer customer);


        public void updateDronecharge(DroneCharge dronecharge);




        public void belongPacelToADrone(Parcel parcel);


        public void CollectAParcelByDrone(Parcel parcel);


        public void DeliverParcelToCustomer(Parcel parcel);


        public void SendDroneForCharging(Drone drone, Station station);

        public void ReleaseDroneFromCharging(Drone drone);
        

    }

}
