using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;

namespace DalApi
{
    /// <summary>
    /// define object of dal
    /// </summary>
    public static class DalFactory
    {
        /// <summary>
        /// creats an object of DaL,
        /// based on the type of DalCongfig
        /// </summary>
        /// <returns>instance of dal</returns>
        public static IDal GetDal()
        {
            string dalType = DalConfig.DalName;
            string dalPkg = DalConfig.DalPackages[dalType];
            if (dalPkg == null) throw new DalConfigExeption($"Package {dalType} is not found in packages list in dal-config.xml");
            try { Assembly.Load(dalPkg); }
            catch (Exception) { throw new DalConfigExeption("Failed to load the dal-config.xml file"); }
            Type type = Type.GetType($"Dal.{dalPkg}, {dalPkg}");
            if (type == null) throw new DalConfigExeption($"class {dalPkg} was not found in the {dalPkg}.dll");
            IDal dal = (IDal)type.GetProperty("GetInstance",
                BindingFlags.Public | BindingFlags.Static).GetValue(null);
            if (dal == null) throw new DalConfigExeption($"Class {dalPkg} is not a singleton or worng propertry name for Instance");
            return dal;
        }
    }
}
