using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {


        public class StationToList
        {
            public int ID { get; set; }

            public int Name { get; set; }

            public int numberOfFreeChargeSlots { get; set; }

            public int numberOfUsedChargeSlots { get; set; }


            public StationToList()
            {

            }

        }
    }
}
