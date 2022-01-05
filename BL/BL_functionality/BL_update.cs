using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
     partial class BL
    {
        /// <summary>
        /// updat eDrone Model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public void updateDroneModel(int id, string model)
        {
            DO.Drone droneDL = dalObject.GetDroneById(id);
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
            DO.Station station = dalObject.GetStationById(id);
            if (name != -1)
                station.Name = name;
            if (totalChargeSlots != -1)
                station.ChargeSlots = totalChargeSlots;
            dalObject.updateStation(station);
        }

        public void updateParcel(Parcel parcel)
        {
            dalObject.updateParcel(new DO.Parcel()
            {
                Id = parcel.Id,
                Delivered = parcel.Delivered,
                DroneId = parcel.droneAtParcel == null ? 0 : parcel.droneAtParcel.Id,
                PickedUp = parcel.PickedUp,
                Pritority =  (DO.Pritorities)parcel.Pritority,
                Requested = parcel.Requested,
                Scheduled = parcel.Scheduled,
                SenderId = parcel.customerAtParcelSender.Id,
                TargetId = parcel.customerAtParcelReciver.Id,
                Weight = (DO.WeightCategories)parcel.Weight
            });
        }

        /// <summary>
        /// update Data Customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
/*        public void updateDataCustomer(int id, string name = null, string phone = null)
        {
            DO.Customer customer = dalObject.findCustomerById(id);
            if (name != null)
                customer.Name = name;
            if (phone != null)
                customer.Phone = phone;
            dalObject.updateCustomer(customer);
        }*/

        public void updateCustomer(Customer customer)
        {
            dalObject.updateCustomer(new DO.Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Longitude = customer.Location.Longitude,
                Latitude = customer.Location.Latitude
            });
        }

        /// <summary>
        /// update Drone
        /// </summary>
        /// <param name="drone"></param>
        public void updateDrone(Drone drone)
        {
            int index = dronesBL.FindIndex(d => d.Id == drone.Id);
            dronesBL[index] = drone;
            dalObject.updateDrone(new DO.Drone() { Id = drone.Id, MaxWeight = (DO.WeightCategories)drone.MaxWeight, Model = drone.Model });
        }

        public void updateStation(Station station)
        {
            dalObject.updateStation(new DO.Station()
            {
                ChargeSlots = station.FreeChargeSlots,
                Id = station.Id,
                Latitude = station.Location.Latitude,
                Longitude = station.Location.Longitude,
                Name = station.Name
            });
        }
    }
}
