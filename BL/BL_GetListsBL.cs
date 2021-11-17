using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;


namespace BL
{
    public partial class BL
    {

       

        public List<CustomerBL> GetCustomerFromBL()
        {
            List<CustomerDL> customerDLs = dalObject.GetCustomersList();
            List<CustomerBL> customerBLs = new List<CustomerBL>();
            foreach (var e in customerDLs) {
                customerBLs.Add(convertToCustomerBL(e));
                    };

                return customerBLs;



        }

     



        }








/*        public List<StationBL> GetStationFromBL()
        {
            List<StationDL> stationDLs = dalObject.GetStations().ToList();
            List<StationBL> stationBLs = new List<StationBL>();
            foreach (var station in stationDLs)
            {
                stationBLs.Add(convertToStationBL(station));
            };

            return stationBLs;
        }

        public List<ParcelBL> GetSParcelFromBL()
        {
            List<ParcelDL> ParcelDLs = dalObject.GetParcel().ToList();
            List<ParcelBL> ParcelBLs = new List<ParcelBL>();
            foreach (var Parcel in ParcelDLs)
            {
                ParcelBLs.Add(convertToParcelBL(Parcel));
            };

            return ParcelBLs;
        }*/

    }
}
