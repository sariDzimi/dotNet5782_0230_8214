using System;
using System.Collections.Generic;
using System.IO;
using DO;
using System.Linq;
using System.Xml.Linq;
using DalApi; 

namespace Dal
{

    /// <summary>
    /// Data layer, based on xml files
    /// </summary>
    public partial class DalXml : IDal
    {
        static string dir = @"..\..\..\..\xmlData\";
        static DalXml()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
        public DalXml()
        {
            DataSource.Initialize();
            if (!File.Exists(dir + customerFilePath))
                XMLTools.SaveListToXMLSerializer(DataSource.customers, dir + customerFilePath);

            if (!File.Exists(dir + parcelFilePath))
                XMLTools.SaveListToXMLSerializer(DataSource.parcels, dir + parcelFilePath);

            if (!File.Exists(dir + droneFilePath))
                XMLTools.SaveListToXMLSerializer(DataSource.drones, dir + droneFilePath);

            if (!File.Exists(dir + stationFilePath))
                XMLTools.SaveListToXMLSerializer(DataSource.stations, dir + stationFilePath);

            if (!File.Exists(dir + droneChargeFilePath))
                XMLTools.SaveListToXMLSerializer(DataSource.droneCharges, dir + droneChargeFilePath);

        }

        #region File Path
        string stationFilePath = @"StationList.xml";
        string customerFilePath = @"CustomerList.xml";
        string parcelFilePath = @"ParcelList.xml";
        string droneFilePath = @"DroneList.xml";
        string droneChargeFilePath = @"DroneChargeList.xml";
        string configFilePath = @"Config.xml";
        string managersFilePath = @"Managers.xml";
        #endregion

        internal static DalXml Instance;

        /// <summary>
        /// returns instance of DalXml
        /// </summary>
        public static DalXml GetInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new DalXml();
                return Instance;
            }
        }

        /// <summary>
        /// gets array of electricity use from config file
        /// </summary>
        /// <returns>array of electricity use</returns>
        double[] IDal.GetElectricityUse()
        {

            XElement elements = XMLTools.LoadData(dir + configFilePath);
            double[] Electricity = new double[5];
            try
            {
                foreach(var electricity in elements.Elements())
                {
                    Electricity[0] = Convert.ToDouble(electricity.Value);
                    Electricity[1] = Convert.ToDouble(electricity.Value);
                    Electricity[2] = Convert.ToDouble(electricity.Value);
                    Electricity[3] = Convert.ToDouble(electricity.Value);
                    Electricity[4] = Convert.ToDouble(electricity.Value);
                   
                }
            }
            catch
            {
                throw new ListIsEmptyException("electricity use");
            }

            return Electricity;
        }

    }
}
