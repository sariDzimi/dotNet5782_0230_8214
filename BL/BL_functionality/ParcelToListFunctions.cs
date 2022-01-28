using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace BL
{
    partial class BL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel ConvertParcelToListToParcel(ParcelToList parcelToList)
        {
            return GetParcelById(parcelToList.ID);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetParcelToLists()
        {
            return from parcel in GetParcels()
                   select convertParcelToParcelToList(parcel);

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetParcelsToListBy(Predicate<ParcelToList> findBy)
        {
            return from parcel in GetParcelToLists()
                   where findBy(parcel)
                   select parcel;
        }

    }
}

