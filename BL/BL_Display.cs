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
        public StationBL DispleyStation(int id)
        {
            IDAL.DO.StationDL stationDL = new IDAL.DO.StationDL();
            stationDL = dalObject.findStation(id);
            StationBL stationBL = new StationBL();
            stationBL = convertToStationBL(stationDL);
            return stationBL;
        }

        public DroneBL DispleyDrone(int id)
        {
            DroneBL droneBL = dronesBL.Find(d => d.Id == id);
            return droneBL;
        }

        public CustomerBL DispleyCustomer(int id)
        {
            IDAL.DO.CustomerDL customerDL = new IDAL.DO.CustomerDL();
            customerDL = dalObject.findCustomer(id);
            CustomerBL customerBL = new CustomerBL();
            customerBL = convertToCustomerBL(customerDL);
            return customerBL;
        }

        public ParcelBL DispleyParcel(int id)
        {
            IDAL.DO.ParcelDL parcelDL = new IDAL.DO.ParcelDL();
            parcelDL = dalObject.findParcel(id);
            ParcelBL parcelBL = new ParcelBL();
            parcelBL = convertToParcelBL(parcelDL);
            return parcelBL;
        }

















    }
}
