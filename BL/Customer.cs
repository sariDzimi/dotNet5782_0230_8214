using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class CustomerBL
        {


            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }

            public Location Location { get; set; }
            public override string ToString()
            {
                return $"customer {Name} : {Id}";
            }


            List<ParcelAtCustomer> parcelsSentedByCustomer = new List<ParcelAtCustomer>();
            List<ParcelAtCustomer> parcelsSentedToCustomer = new List<ParcelAtCustomer>();

          


           
            
        }
    }
}
