using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DAL;


namespace BL
{
    partial class BL
    {
        public Parcel ConvertParcelToListToParcel(ParcelToList parcelToList)
        {
            return GetParcelById(parcelToList.ID);
        }
        public IEnumerable<ParcelToList> GetParcelToLists()
        {
            return from parcel in GetParcels()
                   select convertParcelToParcelToList(parcel);

        }

        public IEnumerable<ParcelToList> GetParcelsToListBy(Predicate<ParcelToList> findBy)
        {
            return from parcel in GetParcelToLists()
                   where findBy(parcel)
                   select parcel;
        }

    }
}

