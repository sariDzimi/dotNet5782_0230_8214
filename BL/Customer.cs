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
        public class Customer
        {
            IDAL.DO.Customer customer;
            public Customer()
            {
                 customer = new IDAL.DO.Customer();
            }

            List<ParcelAtTransfor> parcelsSentedByCustomer = new List<ParcelAtTransfor>();
            List<ParcelAtTransfor> parcelsSentedToCustomer = new List<ParcelAtTransfor>();

            Location location { get; set; }


            public override string ToString()
            {
                return $"customer {customer.Name} : {customer.Id}";
            }

            
        }
    }
}
