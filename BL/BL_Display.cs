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
/*        public StationBL DisplayStation(int id)
        {
            return FindStation(id);
        }

        public DroneBL DisplayDrone(int id)
        { 
            return FindDrone(id);
        }

        public CustomerBL DisplayCuatomer(int id)
        {
            return FindCuatomer(id);
        }

        public ParcelBL DisplayParcel(int id)
        {
            return FindParcel(id);
        }*/

        public IEnumerable<ParcelBL> GetNotAsignedParcels()
        {
            foreach(var parcel in GetParcels())
            {
                if (parcel.droneAtParcel.Equals(null))
                    yield return parcel;
            }
        }

        public IEnumerable<StationBL> StationsWithEmptyChargeSlots()
        {
            foreach (var station in GetStations())
            {
                if (station.ChargeSlots > 0)
                    yield return station;
            }
        }


















    }
}
