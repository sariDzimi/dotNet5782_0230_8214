using System;
using System.Collections.Generic;
using System.IO;
using DO;
using System.Linq;
using System.Xml.Linq;
using DalApi;

namespace Dal
{
    partial class DalXml : IDal
    {
        #region Get Managers
        /// <summary>
        /// returns managers form managers xml file
        /// </summary>
        /// <param name="getBy">condition</param>
        /// <returns>managers that full-fill the conditon</returns>
        IEnumerable<Manager> IDal.GetManagers(Predicate<Manager> getBy)
        {
            XElement elements = XMLTools.LoadData(dir + managersFilePath);
            IEnumerable<Manager> managers;
            try
            {

                managers = (from m in elements.Elements()
                            select new Manager()
                            {
                                UserName = m.Element("UserName").Value,
                                Password = Convert.ToInt32(m.Element("Password").Value)
                            });
            }
            catch
            {
                throw new ListIsEmptyException("managers");
            }
            getBy ??= ((st) => true);

            return from manager in managers
                   where getBy(manager)
                   select manager;

        }

        #endregion
    }
}