using System;
using System.Collections.Generic;
using System.IO;
using DO;
using System.Linq;
using System.Xml.Linq;
using DalApi;

namespace DalXml
{
    public partial class DalXml : IDal
    {
        static string dir = @"..\..\..\..\xmlData\";
        static DalXml()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        string stationFilePath = @"StationList.xml";
        string customerFilePath = @"CustomerList.xml";
        string parcelFilePath = @"ParcelList.xml";
        string droneFilePath = @"DroneList.xml";
        string droneChargeFilePath = @"DroneChargeList.xml";

        public DalXml()
        {
            DS.DataSource.Initialize();
            if (!File.Exists(dir + customerFilePath))
                XMLTools.SaveListToXMLSerializer<DO.Customer>(DS.DataSource.customers, dir + customerFilePath);

            if (!File.Exists(dir + parcelFilePath))
                XMLTools.SaveListToXMLSerializer<DO.Parcel>(DS.DataSource.parcels, dir + parcelFilePath);

            if (!File.Exists(dir + droneFilePath))
                XMLTools.SaveListToXMLSerializer<DO.Drone>(DS.DataSource.drones, dir + droneFilePath);

            if (!File.Exists(dir + stationFilePath))
                XMLTools.SaveListToXMLSerializer<DO.Station>(DS.DataSource.stations, dir + stationFilePath);

            if (!File.Exists(dir + droneChargeFilePath))
                XMLTools.SaveListToXMLSerializer<DO.DroneCharge>(DS.DataSource.droneCharges, dir + droneChargeFilePath);

        }

        #region Drone
        public void AddDrone(Drone drone)
        {
            IEnumerable<DO.Drone> droneList = GetDrones();

            if (droneList.Any(d => d.Id == drone.Id))
            {
                throw new IdAlreadyExist(drone.Id);
            }

            droneList.ToList().Add(drone);

            XMLTools.SaveListToXMLSerializer(droneList, dir + droneFilePath);
        }
        public void DeleteDrone(int id)
        {
            IEnumerable<DO.Drone> droneList = GetDrones();
            Drone drone;
            try
            {
                drone = GetDroneById(id);
            }
            catch
            {
                throw new NotFoundException("drone");
            }

            droneList.ToList().Remove(drone);

            XMLTools.SaveListToXMLSerializer(droneList, dir + droneFilePath);
        }
        public void UpdateDrone(Drone drone)
        {
            List<DO.Drone> droneList = GetDrones().ToList();

            int index = droneList.FindIndex(d => d.Id == drone.Id);

            if (index == -1)
                throw new NotFoundException("drone");

            droneList[index] = drone;

            XMLTools.SaveListToXMLSerializer(droneList, dir + droneFilePath);
        }
        public DO.Drone GetDroneById(int id)
        {
            try
            {
                return GetDrones(d => d.Id == id).First();
            }
            catch (Exception)
            {
                throw new NotFoundException("drone");
            }
        }
        public IEnumerable<DO.Drone> GetDrones(Predicate<DO.Drone> predicat = null)
        {
            IEnumerable<DO.Drone> droneList = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dir + droneFilePath);

            predicat ??= ((st) => true);
            return from drone in droneList
                   where predicat(drone)
                   orderby drone.Id
                   select drone;

        }


        #endregion

        #region Parcel
        public void AddParcel(Parcel parcel)
        {
            IEnumerable<DO.Parcel> parcelList = GetParcels();
            if (parcelList.Any(p => p.Id == parcel.Id))
            {
                throw new IdAlreadyExist(parcel.Id);
            }

            parcelList.ToList().Add(parcel);

            XMLTools.SaveListToXMLSerializer<DO.Parcel>(parcelList, dir + parcelFilePath);
        }
        public void DeleteParcel(int id)
        {
            Parcel parcel;
            try
            {
                parcel = GetParcelById(id);
            }
            catch
            {
                throw new NotFoundException("parcel");
            }
            parcel.IsActive = false;
            UpdateParcel(parcel);
        }
        public void UpdateParcel(Parcel parcel)
        {
            List<DO.Parcel> parcelList = GetParcels().ToList();

            int index = parcelList.FindIndex(d => d.Id == parcel.Id);

            if (index == -1)
                throw new NotFoundException("parcel");

            parcelList[index] = parcel;

            XMLTools.SaveListToXMLSerializer<DO.Parcel>(parcelList, dir + parcelFilePath);
        }
        public DO.Parcel GetParcelById(int id)
        {
            try
            {
                return GetParcels(p => p.Id == id).First();
            }
            catch (Exception)
            {
                throw new NotFoundException("parcel");
            }

        }
        public IEnumerable<DO.Parcel> GetParcels(Predicate<DO.Parcel> predicat = null)
        {
            IEnumerable<DO.Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<DO.Parcel>(dir + parcelFilePath);

            predicat ??= ((st) => true);
            return from parcel in parcelList
                   where predicat(parcel)
                   orderby parcel.Id
                   select parcel;
        }


        #endregion

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

        #region Customer
        public void AddCustomer(Customer customer)
        {
            IEnumerable<DO.Customer> customerList = GetCustomers();
            if (customerList.Any(p => p.Id == customer.Id))
            {
                throw new IdAlreadyExist(customer.Id);
            }

            customerList.ToList().Add(customer);

            XMLTools.SaveListToXMLSerializer<DO.Customer>(customerList, dir + customerFilePath);
        }
        public void DeleteCustomer(int id)
        {
            IEnumerable<DO.Customer> customerList = GetCustomers();
            Customer customer;
            try
            {
                customer = GetCustomerById(id);
            }
            catch
            {
                throw new NotFoundException("customer");
            }

            customerList.ToList().Remove(customer);

            XMLTools.SaveListToXMLSerializer(customerList, dir + customerFilePath);
        }
        public void UpdateCustomer(Customer customer)
        {
            List<DO.Customer> customerList = GetCustomers().ToList();

            int index = customerList.FindIndex(d => d.Id == customer.Id);

            if (index == -1)
                throw new NotFoundException("customer");

            customerList[index] = customer;

            XMLTools.SaveListToXMLSerializer(customerList, dir + customerFilePath);
        }
        public DO.Customer GetCustomerById(int id)
        {
            try
            {
                return GetCustomers(c => c.Id == id).First();
            }
            catch (Exception)
            {
                throw new NotFoundException("customer");
            }

        }
        public IEnumerable<Customer> GetCustomers(Predicate<DO.Customer> predicat = null)
        {
            IEnumerable<Customer> customerList = XMLTools.LoadListFromXMLSerializer<DO.Customer>(dir + customerFilePath);

            predicat ??= ((st) => true);
            return from customer in customerList
                   where predicat(customer)
                   orderby customer.Id
                   select customer;

        }


        #endregion

        #region DroneCharge
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            IEnumerable<DroneCharge> droneChargeList = GetDroneCharges();
            if (droneChargeList.Any(dc => dc.DroneId == droneCharge.DroneId))
            {
                throw new IdAlreadyExist(droneCharge.DroneId);
            }

            droneChargeList.ToList().Add(droneCharge);

            XMLTools.SaveListToXMLSerializer(droneChargeList, dir + droneChargeFilePath);
        }
        public void DeleteDroneCharge(int id)
        {
            IEnumerable<DO.DroneCharge> droneChargeList = GetDroneCharges();
            DroneCharge droneCharge;
            try
            {
                droneCharge = GetDroneChargeById(id);
            }
            catch
            {
                throw new NotFoundException("droneCharge");
            }

            droneChargeList.ToList().Remove(droneCharge);

            XMLTools.SaveListToXMLSerializer(droneChargeList, dir + droneChargeFilePath);
        }
        public DO.DroneCharge GetDroneChargeById(int droneId)
        {
            try
            {
                return GetDroneCharges(c => c.DroneId == droneId).First();
            }
            catch (Exception)
            {
                throw new NotFoundException("droneCharge");
            }

        }
        public IEnumerable<DroneCharge> GetDroneCharges(Predicate<DO.DroneCharge> predicat = null)
        {
            IEnumerable<DroneCharge> droneChargeList = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dir + droneChargeFilePath);

            predicat ??= ((st) => true);
            return from droneCharge in droneChargeList
                   where predicat(droneCharge)
                   orderby droneCharge.DroneId
                   select droneCharge;

        }

        #endregion
        double[] IDal.RequestElectricityUse()
        {
            throw new NotImplementedException();
        }
        IEnumerable<Manager> IDal.GetManagers(Predicate<Manager> findBy)
        {
            throw new NotImplementedException();
        }
    }
}
