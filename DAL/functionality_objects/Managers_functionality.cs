using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal partial class DalObject : DalApi.IDal
    {
        public IEnumerable<Manager> GetManagers(Predicate<Manager> getBy = null)
        {
            getBy ??= (manager => true);
            return from manager in DataSource.Managers
                   where (getBy(manager))
                   select manager;
        }
    }
}