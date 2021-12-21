using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class ParcelToList

    {

        public ParcelToList()
        {

        }

        public int ID { get; set; }

        public string NameOfCustomerSended { get; set; }
        public string NameOfCustomerReciver { get; set; }

        public DO.WeightCategories weightCategories { get; set; }

        public DO.Pritorities pritorities { get; set; }

        public ParcelStatus parcelStatus { get; set; }

        public override string ToString()
        {
            return $"ID: {ID}, sender: {NameOfCustomerSended}, reciver:{NameOfCustomerReciver}, weight: {weightCategories}, priority:{pritorities}, parcel status:{parcelStatus}";
        }

    }
}


