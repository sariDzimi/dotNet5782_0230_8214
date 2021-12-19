using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO{

        public class ParcelInDelivery
        {
            public ParcelInDelivery()
            {

            }

            public int Id { get; set; } 

            public bool IsWating { get; set; }

            public DO.WeightCategories weightCategories { get; set; }

            public DO.Pritorities pritorities { get; set; }

           public CustomerAtParcel customerAtParcelTheSender { get; set; }
           public CustomerAtParcel customerAtParcelTheReciver { get; set; }


            public Location locationCollect { get; set; }
            public Location locationTarget { get; set; }

            public double distance { get; set; }

            public override string ToString()
            {
                return $"ID  : {Id}, " +
                    $" pritorities: {pritorities}, customerAtDeliverySender: {customerAtParcelTheSender}," +
                    $" customerAtDeliveryReciver: {customerAtParcelTheReciver}, weightCategories {weightCategories}," +
                    $" locationCollect {locationCollect}, locationTarget {locationTarget}, distance {distance}   "


                ;
            }


        }
    }

}
