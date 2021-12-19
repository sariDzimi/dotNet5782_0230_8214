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
        public void updateDataCustomer(int id, string name = null, string phone = null);
        public void updateDrone(Drone drone);

    }
}





