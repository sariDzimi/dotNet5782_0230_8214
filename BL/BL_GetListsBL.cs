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



        public IEnumerable<StationBL> GetStations()
        {
            foreach (var station in dalObject.GetStations())
            {
                yield return convertToStationBL(station);
            }
        }

        public IEnumerable<ParcelBL> GetParcels()
        {
            foreach (var parcel in dalObject.GetParcel())
            {
                yield return  convertToParcelBL(parcel);
            }
        }

        public IEnumerable<CustomerBL> GetCustomers()
        {
            foreach (var customer in dalObject.GetCustomer())
            {
                yield return convertToCustomerBL(customer);
            }
        }

        public IEnumerable<DroneBL> GetDrones()
        {
            foreach (var drone in dronesBL)
            {
                yield return drone;
            }
        }
    }
}
