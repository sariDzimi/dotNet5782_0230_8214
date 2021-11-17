using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    public partial class BL
    {

       

        public List<CustomerBL> GetCustomerFromBL()
        {
            List<IDAL.DO.CustomerDL> customerDLs = dalObject.GetCustomersList();
            List<CustomerBL> customerBLs = new List<CustomerBL>();
            foreach (var e in customerDLs) {
                customerBLs.Add(convertToCustomerBL(e));
                    };

                return customerBLs;



        }

     



        }







    }
}
