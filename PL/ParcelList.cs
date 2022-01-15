using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Diagnostics;
using System.Windows;
using System.Collections.ObjectModel;
using BlApi;

namespace PO
{
     public class ParcelList: DependencyObject
    {

        public ParcelList()
        {
            
        }

        public ObservableCollection<Parcel_p> Parcels = new ObservableCollection<Parcel_p>();

        public void AddParcel(ParcelToList parcel)
        {
            Parcels.Add(new Parcel_p { ID = parcel.ID, Pritority = parcel.pritorities, Weight = parcel.weightCategories });
        }
         public ObservableCollection<Parcel_p> ConvertParcelBLToPL(List<ParcelToList> parcelsBL)
        {

          foreach(var parcel in parcelsBL)
            {
                Parcel_p parcel_P = new Parcel_p() { ID = parcel.ID, Pritority = parcel.pritorities,   Weight = parcel.weightCategories };
                Parcels.Add(parcel_P);
            }
            return Parcels;
        }

    }
}
