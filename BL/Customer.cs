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


            List<ParcelAtTransfor> parcelsSentedByCustomer = new List<ParcelAtTransfor>();
            List<ParcelAtTransfor> parcelsSentedToCustomer = new List<ParcelAtTransfor>();

            Location location { get; set; }


           
            
        }
    }
}
