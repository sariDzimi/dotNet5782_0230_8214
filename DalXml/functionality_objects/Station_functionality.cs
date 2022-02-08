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

        #region Add Station

        /// <summary>
        /// adds station to stations xml file
        /// </summary>
        /// <param name="station"></param>
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
                throw new IdAlreadyExistException("station", station.Id);
            }
        }

        #endregion

        #region Get Station

        /// <summary>
        /// returns stations form stations xml file
        /// </summary>
        /// <param name="getBy">condition</param>
        /// <returns>stations that full-fill the conditon</returns>
        public IEnumerable<DO.Station> GetStations(Predicate<Station> getBy = null)
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
                throw new ListIsEmptyException("stationList");
            }

            getBy ??= ((st) => true);

            return from station in StationList
                   where getBy(station)
                   select station;
        }

        /// <summary>
        /// finds a station by id
        /// </summary>
        /// <param name="id">id of station</param>
        /// <returns>station with the given id</returns>
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
                throw new NotFoundException("station",id);
            }
            return station;
        }

        #endregion

        #region Update Station

        /// <summary>
        /// updates the station in the stations xml file
        /// </summary>
        /// <param name="station">station with updated details</param>
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
                throw new NotFoundException("station", station.Id);
            }
            element.Element("Id").Value = station.Id.ToString();
            element.Element("Name").Value = station.Name.ToString();
            element.Element("ChargeSlots").Value = station.ChargeSlots.ToString();
            element.Element("Latitude").Value = station.Latitude.ToString();
            element.Element("Longitude").Value = station.Longitude.ToString();
            elements.Save(dir + stationFilePath);
        }
        #endregion

        #region Delete Station

        /// <summary>
        /// deletes station from stations xml file
        /// </summary>
        /// <param name="id">id of station</param>
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
                throw new NotFoundException("Station", id);
            }

            elements.Remove();
            element.Save(dir + stationFilePath);
        }

        #endregion
    }
}