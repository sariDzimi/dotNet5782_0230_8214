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
        /// Get Not Asigned Parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ParcelBL> GetNotAsignedParcels()
        {
            foreach(var parcel in GetParcels())
            {
                if (parcel.droneAtParcel.Id==0)
                    yield return parcel;
            }
        }

        /// <summary>
        /// GetS tations With Empty ChargeSlots
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StationBL> GetStationsWithEmptyChargeSlots()
        {
            foreach (var station in GetStations())
            {
                if (station.ChargeSlots > 0)
                    yield return station;
            }
        }


















    }
}
