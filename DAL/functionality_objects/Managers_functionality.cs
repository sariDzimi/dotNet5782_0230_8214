using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using DO;
using System.Runtime.CompilerServices;

namespace Dal
{
    internal partial class DalObject : DalApi.IDal
    {
        #region Get Managers

        /// <summary>
        /// returns managers form datasource
        /// </summary>
        /// <param name="getBy">condition</param>
        /// <returns>managers that full-fill the conditon</returns>
        public IEnumerable<Manager> GetManagers(Predicate<Manager> getBy = null)
        {
            getBy ??= (manager => true);
            return from manager in DataSource.Managers
                   where (getBy(manager))
                   select manager;
        }

        #endregion
    }
}