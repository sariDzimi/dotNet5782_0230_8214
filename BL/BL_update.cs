using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    public partial class BL
    {
        public void updateDroneModel(int id, string model)
        {

            IDAL.DO.DroneDL droneDL = dalObject.GetDronesList().First(d => d.Id == id);
            droneDL.Model = model;
            dalObject.updateDrone(droneDL);

            DroneBL droneBL = dronesBL.First(d => d.Id == id);
            droneBL.Model = model;
            updateDrone(droneBL);
        }

        public void updateDataStation(int id, int name = -1, int totalChargeSlots = -1)
        {
            IDAL.DO.StationDL station = dalObject.findStation(id);
            if (name != -1)
                station.Name = name;
            if (totalChargeSlots != -1)
                station.ChargeSlots = totalChargeSlots;
            dalObject.updateStation(station);
        }
        public void updateDataCustomer(int id, string name = null, string phone = null)
        {
            IDAL.DO.CustomerDL customer = dalObject.findCustomer(id);
            if (name != null)
                customer.Name = name;
            if (phone != null)
                customer.Phone = phone;
            dalObject.updateCustomer(customer);
        }

        public void updateDrone(DroneBL drone)
        {
            int index = dronesBL.FindIndex(d => d.Id == drone.Id);
            dronesBL[index] = drone;
        }
    }
}
