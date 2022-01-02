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
        public void DeleateParcel(Parcel parcel)
        {
            DO.Parcel parcel1 = dalObject.FindParcelBy((p) => p.Id == parcel.Id);
            parcel1.IsActive = false;
            dalObject.updateParcel(parcel1);
            
        }
        /// <summary>
        /// Get Not Asigned Parcels
        /// </summary>
        /// <returns></returns>
/*        public IEnumerable<Parcel> GetNotAsignedParcels()
        {
            foreach(var p in dalObject.GetParcelIdBy((x) => x.DroneId == 0))
            {
                yield return convertToParcelBL(p);
            }
        }*/


        /// <summary>
        /// GetS tations With Empty ChargeSlots
        /// </summary>
        /// <returns></returns>
/*        public IEnumerable<Station> GetStationsWithEmptyChargeSlots()
        {
            foreach (var station in dalObject.GetStationIdBy((x)=> x.ChargeSlots > 0))
            {
                yield return ConvertToStationBL(station);
            }
        }*/

       
    }
}
