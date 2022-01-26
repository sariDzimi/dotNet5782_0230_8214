using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal partial class DalObject : DalApi.IDal
    {
        internal static DalObject Instance;

        public DalObject()
        {
            DataSource.Initialize();
        }

        public static DalObject GetInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new DalObject();
                return Instance;
            }
        }

        public double[] GetElectricityUse()
        {
            double[] Electricity = { DataSource.Config.free, DataSource.Config.light, DataSource.Config.medium, DataSource.Config.heavy, DataSource.Config.rateChargePerHour };
            return Electricity;

        }

    }
}





