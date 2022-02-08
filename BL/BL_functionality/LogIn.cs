using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace BL
{
    partial class BL
    {
        /// <summary>
        /// cheks if manager exist in the system
        /// </summary>
        /// <param name="manager">manager</param>
        /// <returns>does manager exist</returns>
        public bool IsValidMamager(Manager manager)
        {
            DO.Manager manager1 = null;
            try
            {
                lock (dal)
                {
                    manager1 = dal.GetManagers((M) => ((M.Password == manager.Password) && (M.UserName == manager.UserName))).First();
                }
            }
            catch(Exception)
            {
                throw new NotFound("Manager");
            }


            if (manager != null)
                return true;
            return false;  //data secureity: Denided by default
        }



    }
}
