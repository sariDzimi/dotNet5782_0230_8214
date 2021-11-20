﻿using System;
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
                customerAtDeliverySender = new CustomerAtParcel();
                customerAtDeliveryReciver = new CustomerAtParcel();
            }

            public int ID { get; set; }

            public IDAL.DO.Pritorities pritorities { get; set; }

            public CustomerAtParcel customerAtDeliverySender { get; set; }
            public CustomerAtParcel customerAtDeliveryReciver { get; set; }

            public override string ToString()
            {
                return $"ID  : {ID}, " +
                    $" pritorities: {pritorities}, customerAtDeliverySender: {customerAtDeliverySender}," +
                    $" customerAtDeliveryReciver: {customerAtDeliveryReciver}, " 
                 

                ;
            }


        }
    }
}
