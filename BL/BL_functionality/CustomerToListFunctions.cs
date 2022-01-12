using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DAL;


namespace BL
{
    partial class BL
    {
        public IEnumerable<CustomerToList> GetCustomerToLists()
        {
            return from customer in GetCustomers()
                   select convertCustomerToCustomerToList(customer);

        }
    }
}


