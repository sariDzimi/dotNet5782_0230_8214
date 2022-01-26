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

        #region Station
        public IEnumerable<DO.Station> GetStations(Predicate<Station> predicate = null)
        {

            XElement elements = XMLTools.LoadData(dir + stationFilePath);
            IEnumerable<DO.Station> StationList;
            try
            {
                StationList = (from p in elements.Elements()
                               select new DO.Station()
                               {
                                   Id = Convert.ToInt32(p.Element("Id").Value),
                                   Name = Convert.ToInt32(p.Element("Name").Value),
                                   ChargeSlots = Convert.ToInt32(p.Element("ChargeSlots").Value),
                                   Latitude = Convert.ToDouble(p.Element("Latitude").Value),
                                   Longitude = Convert.ToDouble(p.Element("Longitude").Value)
                               });
            }
            catch
            {
                throw new ListEmpty("stationList");
            }

            predicate ??= ((st) => true);

            return from station in StationList
                   where predicate(station)
                   select station;
        }
        public DO.Station GetStationById(int id)
        {
            XElement elements = XMLTools.LoadData(dir + stationFilePath);
            DO.Station station = new DO.Station();
            try
            {
                station = (from p in elements.Elements()
                           where Convert.ToInt32(p.Element("Id").Value) == id
                           select new DO.Station()
                           {
                               Id = Convert.ToInt32(p.Element("Id").Value),
                               Name = Convert.ToInt32(p.Element("Name").Value),
                               ChargeSlots = Convert.ToInt32(p.Element("ChargeSlots").Value),
                               Latitude = Convert.ToDouble(p.Element("Latitude").Value),
                               Longitude = Convert.ToDouble(p.Element("Longitude").Value)
                           }).First();
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("station");
            }
            return station;
        }
        public void AddStation(DO.Station station)
        {
            XElement elements = XMLTools.LoadData(dir + stationFilePath);
            XElement element = (from p in elements.Elements()
                                where Convert.ToInt32(p.Element("Id").Value) == station.Id
                                select p).FirstOrDefault();
            if (element == null)
            {
                XElement Id = new XElement("Id", station.Id);
                XElement Name = new XElement("Name", station.Name);
                XElement ChargeSlots = new XElement("ChargeSlots", station.ChargeSlots);
                XElement Latitude = new XElement("Latitude", station.Latitude);
                XElement Longitude = new XElement("Longitude", station.Longitude);
                elements.Add(new XElement("station", Id, Name, ChargeSlots, Latitude, Longitude));
                elements.Save(dir + stationFilePath);
            }
            else
            {
                throw new IdAlreadyExist(station.Id);
            }
        }
        public void DeleteStation(int id)
        {
            XElement element = XMLTools.LoadData(dir + stationFilePath);
            XElement elements;
            try
            {
                elements = (from p in element.Elements()
                            where Convert.ToInt32(p.Element("Id").Value) == id
                            select p).First();
            }
            catch
            {
                throw new NotFoundException("Station");
            }

            elements.Remove();
            element.Save(dir + stationFilePath);
        }
        public void UpdateStation(DO.Station station)
        {
            XElement elements = XMLTools.LoadData(dir + stationFilePath);
            XElement element;
            try
            {
                element = (from p in elements.Elements()
                           where Convert.ToInt32(p.Element("Id").Value) == station.Id
                           select p).First();
            }
            catch
            {
                throw new NotFoundException("station");
            }
            element.Element("Id").Value = station.Id.ToString();
            element.Element("Name").Value = station.Name.ToString();
            element.Element("ChargeSlots").Value = station.ChargeSlots.ToString();
            element.Element("Latitude").Value = station.Latitude.ToString();
            element.Element("Longitude").Value = station.Longitude.ToString();
            elements.Save(dir + stationFilePath);
        }

        #endregion
    }
}