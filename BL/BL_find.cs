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
            CustomerBL customerBL = GetCustomers().ToList().Find(d => d.Id == id);
            return customerBL;
        }

        public ParcelBL FindParcel(int id)
        {
            ParcelBL parcelBL = GetParcels().ToList().Find(d => d.Id == id);
            return parcelBL;
        }

    }
}