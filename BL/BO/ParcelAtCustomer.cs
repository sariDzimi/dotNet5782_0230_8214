using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    /// <summary>
    /// details of parcel that apears in customer
    /// </summary>
    public class ParcelAtCustomer
    {
        public int Id { get; set; }

        public WeightCategories WeightCategories { get; set; }

        public Pritorities Pritorities { get; set; }

        public ParcelStatus ParcelStatus { get; set; }

        public CustomerAtParcel CustomerAtParcel { get; set; }


        public override string ToString()
        {
            return $"ParcelAtCustomer  : {Id}, " +
                $" , weightCategories:{WeightCategories} {Pritorities}, pritorities: " +
                $" parcelStatus {ParcelStatus}, " +
                $"customerAtParcel : {CustomerAtParcel},";
        }
    }

}

