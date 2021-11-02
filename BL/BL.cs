using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using DalObject;




namespace BL
{
    public class BL 
    {
        public BL()
        {
            IDal.IDal dalObject = new DalObject.DalObject();
        }

        DalObject.DalObject dalObject;

        public void belongPacelToADrone(Parcel parcel)
        {
            Parcel parcel1 = new Parcel();
            parcel1 = parcel;
           var drone = DataSource.drones.Find(c => c.Status == DroneStatus.Free);
            int indexDrone = DataSource.drones.FindIndex(c => c.Id == parcel.Id);
            parcel1.DroneId = drone.Id;
            drone.Status = DroneStatus.Delivery;
            dalObject.updateDrone(drone);
            dalObject.updateParcel(parcel1);

            
        }

        public void CollectAParcelByDrone(Parcel parcel)
        {
            parcel.PickedUp = DateTime.Now;
            dalObject.updateParcel(parcel);
        }

        public void DeliverParcelToCustomer(Parcel parcel)
        {
            parcel.Delivered = DateTime.Now;
            dalObject.updateParcel(parcel);
        }

        public void SendDroneForCharging(Drone drone, Station station)
        {
            drone.Status = DroneStatus.Maintenance;
            DroneCharge droneCharge = new DroneCharge();
            droneCharge.DroneId = drone.Id;
            droneCharge.stationId = station.Id;

            dalObject.addDronCharge(droneCharge);
        }
        public void ReleaseDroneFromCharging(Drone drone)
        {
            int index = 0;
            drone.Status = DroneStatus.Free;
            for (int i = 0; i < DataSource.droneCharges.Count; i++)
            {
                if (DataSource.droneCharges[i].DroneId == drone.Id)
                {
                    index = i;
                    break;
                }


            }
         
            for (int i = index; i < DataSource.droneCharges.Count - 1; i++)
            {
                DataSource.droneCharges[i] = DataSource.droneCharges[i + 1];
            }
           
        }



















    }
}
