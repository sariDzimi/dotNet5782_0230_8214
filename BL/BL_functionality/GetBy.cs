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
        public IEnumerable<Drone> GetDronesBy(Predicate<Drone> findBy)
        {
            return from drone in GetDrones()
                   where findBy(drone)
                   select drone;
        }

        public IEnumerable<Customer> GetCustomersBy(Predicate<Customer> findBy)
        {
            return from customer in GetCustomers()
                   where findBy(customer)
                   select customer;
        }

        public Customer GetCustomerById(int id)
        {
            return GetCustomersBy(c => c.Id == id).ToList().First();
        }
        public IEnumerable<DroneToList> GetDroneToListsBy(Predicate<Drone> findBy)
        {
            return from drone in GetDrones()
                   where findBy(drone)
                   select ConvertDroneToDroneToList(drone);
        }
        public IEnumerable<Parcel> GetParcelsBy(Predicate<Parcel> findBy)
        {
            return from parcel in GetParcels()
                   where findBy(parcel)
                   select parcel;
        }

        public IEnumerable<ParcelToList> GetParcelsToListBy(Predicate<ParcelToList> findBy)
        {
            return from parcel in GetParcelToLists()
                   where findBy(parcel)
                   select parcel;
        }



    }
}
