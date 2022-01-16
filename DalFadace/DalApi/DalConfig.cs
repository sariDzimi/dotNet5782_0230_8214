﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Text;

namespace DalApi
{
    class DalConfig
    {
        internal static string DalName;
        internal static Dictionary<string, string> DalPackages;
        static DalConfig()
        {
            XElement dalConfig = XElement.Load(@"C:\Users\yebsc\source\repos\dotNet5782_0230_8214\dal-config.xml");
            DalName = dalConfig.Element("dal").Value;
            DalPackages = (from pkg in dalConfig.Element("dal-packages").Elements()
                           select pkg
                           ).ToDictionary(p => "" + p.Name, p => p.Value);
        }
    }
    public class DalConfigExeption : Exception
    {
        public DalConfigExeption(string msg) : base(msg) { }
        public DalConfigExeption(string msg, Exception ex) : base(msg, ex) { }

    }
}
