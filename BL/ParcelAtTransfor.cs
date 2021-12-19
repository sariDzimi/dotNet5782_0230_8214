using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO {

        public class ParcelAtTransfor
        {
            public ParcelAtTransfor()
            {
                CustomerAtDeliverySender = new CustomerAtParcel();
                CustomerAtDeliveryReciver = new CustomerAtParcel();
            }

            public int ID { get; set; }

            public DO.Pritorities Pritorities { get; set; }

            public CustomerAtParcel CustomerAtDeliverySender { get; set; }
            public CustomerAtParcel CustomerAtDeliveryReciver { get; set; }

            public override string ToString()
            {
                return $"ID  : {ID}, " +
                    $" pritorities: {Pritorities}, customerAtDeliverySender: {CustomerAtDeliverySender}," +
                    $" customerAtDeliveryReciver: {CustomerAtDeliveryReciver}, " 
                 

                ;
            }


        }
    }
}
