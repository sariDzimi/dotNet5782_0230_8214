using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Diagnostics;
using System.Windows;
using System.Collections.ObjectModel;

namespace PO
{
    class ParcelList
    {

        public ObservableCollection<Parcel_p> Parcels = new ObservableCollection<Parcel_p>();

        public void AddStudent(BO.Parcel parcel)
        {
            Parcels.Add(new Parcel_p {ID = parcel.Id, CustomerAtParcelReciver= parcel.customerAtParcelReciver, CustomerAtParcelSender= parcel.customerAtParcelSender });
        }


    }
}
