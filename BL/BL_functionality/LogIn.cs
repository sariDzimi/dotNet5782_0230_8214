using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;


namespace BL
{
    partial class BL
    {

        public bool CheckWorkerIfExixst(Manager manager)
        {
            try
            {
                DO.Manager manager1 = dalObject.GetManagers((M) => ((M.Password == manager.Password) && (M.UserName == manager.UserName))).First();

            }
            catch(Exception)
            {
                throw new NotFound("Msneger");
            }


            return true;
        }



    }
}
