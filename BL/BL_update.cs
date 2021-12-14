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
        /// <summary>
        /// updat eDrone Model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public void updateDroneModel(int id, string model)
        {
            IDAL.DO.DroneDL droneDL = dalObject.findDroneById(id);
            droneDL.Model = model;
            dalObject.updateDrone(droneDL);

            Drone droneBL = dronesBL.First(d => d.Id == id);
            droneBL.Model = model;
            updateDrone(droneBL);
        }

        /// <summary>
        /// update Data Station
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="totalChargeSlots"></param>
        public void updateDataStation(int id, int name = -1, int totalChargeSlots = -1)
        {
            IDAL.DO.StationDL station = dalObject.findStationById(id);
            if (name != -1)
                station.Name = name;
            if (totalChargeSlots != -1)
                station.ChargeSlots = totalChargeSlots;
            dalObject.updateStation(station);
        }

        /// <summary>
        /// update Data Customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        public void updateDataCustomer(int id, string name = null, string phone = null)
        {
            IDAL.DO.CustomerDL customer = dalObject.findCustomerById(id);
            if (name != null)
                customer.Name = name;
            if (phone != null)
                customer.Phone = phone;
            dalObject.updateCustomer(customer);
        }

        /// <summary>
        /// update Drone
        /// </summary>
        /// <param name="drone"></param>
        public void updateDrone(Drone drone)
        {
            int index = dronesBL.FindIndex(d => d.Id == drone.Id);
            dronesBL[index] = drone;
        }
    }
}
