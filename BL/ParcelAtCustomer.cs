using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO {

        public class ParcelAtCustomer
        {

            public ParcelAtCustomer()
            {

            }


            public int ID { get; set; }

            public DO.WeightCategories weightCategories { get; set; }

            public DO.Pritorities pritorities { get; set; }

            public ParcelStatus parcelStatus { get; set; }

            public CustomerAtParcel customerAtParcel { get; set; }


            public override string ToString()
            {
                return $"ParcelAtCustomer  : {ID}, " +
                    $" , weightCategories:{weightCategories} {pritorities}, pritorities: " +
                    $" parcelStatus {parcelStatus}, " +
                    $"customerAtParcel : {customerAtParcel},";
                ;
            }



        }

    }
}
