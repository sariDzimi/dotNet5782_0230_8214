using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using DO;

namespace Dal
{
    /// <summary>
    /// Data layer, based on DataSource (lists)
    /// </summary>
    internal partial class DalObject : DalApi.IDal
    {
        internal static DalObject Instance;

        public DalObject()
        {
            DataSource.Initialize();
        }

        /// <summary>
        /// returns instance of DalObject
        /// </summary>
        public static DalObject GetInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new DalObject();
                return Instance;
            }
        }

        /// <summary>
        /// gets array of electricity use from config
        /// </summary>
        /// <returns>array of electricity use</returns>
        public double[] GetElectricityUse()
        {
            double[] Electricity = { DataSource.Config.free, DataSource.Config.light, DataSource.Config.medium, DataSource.Config.heavy, DataSource.Config.rateChargePerHour };
            return Electricity;

        }

    }
}





