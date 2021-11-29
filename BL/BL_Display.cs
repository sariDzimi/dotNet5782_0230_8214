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
            foreach(var p in dalObject.GetParcelIdBy((x) => x.DroneId == 0))
            {
                yield return convertToParcelBL(p);
            }
        }


        /// <summary>
        /// GetS tations With Empty ChargeSlots
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StationBL> GetStationsWithEmptyChargeSlots()
        {
            foreach (var station in dalObject.GetStationIdBy((x)=> x.ChargeSlots > 0))
            {
                yield return convertToStationBL(station);
            }
        }

       
    }
}
