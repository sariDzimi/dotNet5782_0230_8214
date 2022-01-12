using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    public partial class BL
    {
        //public IEnumerable<Drone> GetDronesBy(Predicate<Drone> findBy)
        //{
        //    return from drone in GetDrones()
        //           where findBy(drone)
        //           select drone;
        //}

        //public Drone GetDroneById(int id)
        //{
        //    try
        //    {
        //        return GetDronesBy(c => c.Id == id).First();
        //    }
        //    catch
        //    {
        //        throw new NotFound($"couldn't find Drone ${id}");
        //    }
        //}

        //public IEnumerable<Customer> GetCustomersBy(Predicate<Customer> findBy)
        //{
        //    return from customer in GetCustomers()
        //           where findBy(customer)
        //           select customer;
        //}

        //public Customer GetCustomerById(int id)
        //{
        //    try
        //    {
        //        return GetCustomersBy(c => c.Id == id).First();
        //    }
        //    catch
        //    {
        //        throw new NotFound($"couldn't find Customer ${id}");
        //    }
        //}
        //public IEnumerable<Station> GetStationsBy(Predicate<Station> findBy)
        //{
        //    return from station in GetStations()
        //           where findBy(station)
        //           select station;
        //}

        //public Station GetStationById(int id)
        //{
        //    try
        //    {
        //        return GetStationsBy(c => c.Id == id).First();
        //    }
        //    catch
        //    {
        //        throw new NotFound($"couldn't find Station ${id}");
        //    }
        //}

        //public IEnumerable<Parcel> GetParcelsBy(Predicate<Parcel> findBy)
        //{
        //    return from parcel in GetParcels()
        //           where findBy(parcel)
        //           select parcel;
        //}
        //public Parcel GetParcelById(int id)
        //{
        //    try
        //    {
        //        return GetParcelsBy(c => c.Id == id).First();
        //    }
        //    catch
        //    {
        //        throw new NotFound($"couldn't find Parcel ${id}");
        //    }
        //}
        //public IEnumerable<DroneToList> GetDroneToListsBy(Predicate<Drone> findBy)
        //{
        //    return from drone in GetDrones()
        //           where findBy(drone)
        //           select ConvertDroneToDroneToList(drone);
        //}

        //public IEnumerable<ParcelToList> GetParcelsToListBy(Predicate<ParcelToList> findBy)
        //{
        //    return from parcel in GetParcelToLists()
        //           where findBy(parcel)
        //           select parcel;
        //}
    }
}
