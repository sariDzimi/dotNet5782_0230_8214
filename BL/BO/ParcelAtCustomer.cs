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

            public WeightCategories WeightCategories { get; set; }

            public Pritorities Pritorities { get; set; }

            public ParcelStatus ParcelStatus { get; set; }

            public CustomerAtParcel CustomerAtParcel { get; set; }


            public override string ToString()
            {
                return $"ParcelAtCustomer  : {ID}, " +
                    $" , weightCategories:{WeightCategories} {Pritorities}, pritorities: " +
                    $" parcelStatus {ParcelStatus}, " +
                    $"customerAtParcel : {CustomerAtParcel},";
                ;
            }



        }

    }

