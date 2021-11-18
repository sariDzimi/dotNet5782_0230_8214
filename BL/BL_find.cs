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
        /// Find Station
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StationBL FindStation(int id)
        {
            StationBL stationBL = GetStations().ToList().Find(s => s.Id == id);
            if (stationBL==null)
            {
                throw new NotFound($"station number {id}");
            }
            return stationBL;
        }

        /// <summary>
        /// Find Drone
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DroneBL FindDrone(int id)
        {
            DroneBL droneBL = dronesBL.Find(d => d.Id == id);
            if (droneBL==null)
            {
                throw new NotFound($"drone number {id}");
            }
            return droneBL;
        }

        /// <summary>
        /// Find Cuatomer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CustomerBL FindCuatomer(int id)
        {
            List<CustomerBL> customerBLs = GetCustomers().ToList();
            CustomerBL customerBL = customerBLs.Find(d => d.Id == id);
            if (customerBL==null)
            {
                throw new NotFound($"customer number {id}");
            }
            //Console.WriteLine(customerBL);
            return customerBL;
        }

        /// <summary>
        /// Find Parcel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ParcelBL FindParcel(int id)
        {
            ParcelBL parcelBL = GetParcels().ToList().Find(d => d.Id == id);
            if (parcelBL ==null)
            {
                throw new NotFound($"parcel number {id}");
            }
            return parcelBL;
        }
    }
}