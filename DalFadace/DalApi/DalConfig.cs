using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Text;

namespace DalApi
{
    /// <summary>
    /// holds the name and packages of the dal,
    /// The data relies on the configuration
    /// </summary>
    class DalConfig
    {
        internal static string DalName;
        internal static Dictionary<string, string> DalPackages;
        static DalConfig()
        {
            XElement dalConfig = XElement.Load(@"..\..\..\..\xml\dal-config.xml");
            DalName = dalConfig.Element("dal").Value;
            DalPackages = (from pkg in dalConfig.Element("dal-packages").Elements()
                           select pkg
                           ).ToDictionary(p => "" + p.Name, p => p.Value);
        }
    }

    /// <summary>
    /// exeptions of DalConfig Class
    /// </summary>
    public class DalConfigExeption : Exception
    {
        public DalConfigExeption(string msg) : base(msg) { }
        public DalConfigExeption(string msg, Exception ex) : base(msg, ex) { }

    }
}
