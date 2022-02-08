using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    /// <summary>
    /// details of parcel when apears in list
    /// </summary>
    public class ParcelToList
    {
        public int Id { get; set;}
        public string NameOfCustomerSended { get; set; }
        public string NameOfCustomerReciver { get; set; }

        public WeightCategories weightCategories { get; set; }

        public Pritorities pritorities { get; set; }

        public ParcelStatus parcelStatus { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, sender: {NameOfCustomerSended}, reciver:{NameOfCustomerReciver}, weight: {weightCategories}, priority:{pritorities}, parcel status:{parcelStatus}";
        }

    }
}


