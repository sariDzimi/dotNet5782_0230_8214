/*using System;
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
        IBL BL;
        public ParcelList()
        {
            
        }

        public ObservableCollection<Parcel_p> Parcels = new ObservableCollection<Parcel_p>();

        public void AddParcel(ParcelToList parcel)
        {
            Parcels.Add(new Parcel_p { ID = parcel.Id, Pritority = parcel.pritorities, Weight = parcel.weightCategories, NameOfReciver = parcel.NameOfCustomerReciver, NameOfSender =parcel.NameOfCustomerSended, ParcelStatus = parcel.parcelStatus });
        }
         public ObservableCollection<Parcel_p> ConvertParcelBLToPL(List<ParcelToList> parcelsBL)
        {

          foreach(var parcel in parcelsBL)
            {
                Parcel_p parcel_P = new Parcel_p() { ID = parcel.Id, Pritority = parcel.pritorities,   Weight = parcel.weightCategories, NameOfReciver =parcel.NameOfCustomerReciver, NameOfSender= parcel.NameOfCustomerSended, ParcelStatus = parcel.parcelStatus  };
                Parcels.Add(parcel_P);
            }
            return Parcels;
        }

        public ObservableCollection<Parcel_p> ClearParcels()
        {
            //foreach (var parcel in Parcels)
            //{
            //    Parcels.Remove(parcel);
            //}
            Parcels = new ObservableCollection<Parcel_p>();
            return Parcels;
        }
        public void UpdateListParcels(Parcel_p parcel_P )
        {
            if (Parcels.Count == 0)
            {
                this.Parcels = this.ConvertParcelBLToPL(BL.GetParcelToLists().ToList());
            }

            int index = Parcels.IndexOf(Parcels.Where(X => X.ID == parcel_P.ID).FirstOrDefault());
            Parcels[index] = parcel_P;

        }
         public void DeleateParcel(Parcel_p parcel_P)
        {
            Parcels.Remove(parcel_P);
        }
    }
}
*/