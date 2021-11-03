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

            }

            public int ID { get; set; }

            public IDAL.DO.Pritorities pritorities { get; set; }

            public CustomerAtDelivery customerAtDeliverySender { get; set; }
            public CustomerAtDelivery customerAtDeliveryReciver { get; set; }



        }

    }
}
