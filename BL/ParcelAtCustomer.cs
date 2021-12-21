using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace BO {

        public class ParcelAtCustomer
        {

            public ParcelAtCustomer()
            {

            }


            public int ID { get; set; }

            public WeightCategories weightCategories { get; set; }

            public Pritorities pritorities { get; set; }

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

