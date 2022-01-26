using System;
using System.Collections.Generic;
using System.IO;
using DO;
using System.Linq;
using System.Xml.Linq;
using DalApi; 

namespace Dal
{
    public partial class DalXml : IDal
    {
        internal static DalXml Instance;

        static string dir = @"..\..\..\..\xmlData\";
        static DalXml()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public static DalXml GetInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new DalXml();
                return Instance;
            }
        }

        string stationFilePath = @"StationList.xml";
        string customerFilePath = @"CustomerList.xml";
        string parcelFilePath = @"ParcelList.xml";
        string droneFilePath = @"DroneList.xml";
        string droneChargeFilePath = @"DroneChargeList.xml";
        string configFilePath = @"Config.xml";
        string managersFilePath = @"Managers.xml";

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

        

      

       

       

       
        double[] IDal.GetElectricityUse()
        {
            //IEnumerable<double> electricityUseList = XMLTools.LoadListFromXMLSerializer<double>(dir + configFilePath);
            //return electricityUseList.ToArray();

            XElement elements = XMLTools.LoadData(dir + configFilePath);
            double[] Electricity = new double[5];
            try
            {
                //double light = elements.Elements()
                foreach(var electricity in elements.Elements())
                {
                    Electricity[0] = Convert.ToDouble(electricity.Value);
                    Electricity[1] = Convert.ToDouble(electricity.Value);
                    Electricity[2] = Convert.ToDouble(electricity.Value);
                    Electricity[3] = Convert.ToDouble(electricity.Value);
                    Electricity[4] = Convert.ToDouble(electricity.Value);
                   
                }
                //StationList = (from p in elements.Elements()
                //               select new DO.Station()
                //               {
                //                   Id = Convert.ToInt32(p.Element("Id").Value),
                //                   Name = Convert.ToInt32(p.Element("Name").Value),
                //                   ChargeSlots = Convert.ToInt32(p.Element("ChargeSlots").Value),
                //                   Latitude = Convert.ToDouble(p.Element("Latitude").Value),
                //                   Longitude = Convert.ToDouble(p.Element("Longitude").Value)
                //               });
            }
            catch
            {
                throw new ListEmpty("stationList");
            }

            return Electricity;
        }
        IEnumerable<Manager> IDal.GetManagers(Predicate<Manager> findBy)
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
                throw new ListEmpty("managers");
            }
            findBy ??= ((st) => true);

            return from manager in managers
                   where findBy(manager)
                   select manager;

        }
    }
}
