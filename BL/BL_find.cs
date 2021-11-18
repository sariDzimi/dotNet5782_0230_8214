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
        public StationBL FindStation(int id)
        {
            StationBL stationBL = GetStations().ToList().Find(s => s.Id == id);
            return stationBL;
        }

        public DroneBL FindDrone(int id)
        {
            DroneBL droneBL = dronesBL.Find(d => d.Id == id);
            return droneBL;
        }

        public CustomerBL FindCuatomer(int id)
        {
            List<CustomerBL> customerBLs = GetCustomers().ToList();
            CustomerBL customerBL = customerBLs.Find(d => d.Id == id);
            //Console.WriteLine(customerBL);
            return customerBL;
        }

        public ParcelBL FindParcel(int id)
        {
            ParcelBL parcelBL = GetParcels().ToList().Find(d => d.Id == id);
            return parcelBL;
        }

    }
}