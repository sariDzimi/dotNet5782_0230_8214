using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DAL;


namespace BL
{
    partial class BL
    {
        /// <summary>
        /// add Customer To DL
        /// </summary>
        /// <param name="customer"></param>
        public void addCustomerToDL(Customer customer)
        {
            DO.Customer customerDL = new DO.Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Longitude = customer.Location.Longitude,
                Phone = customer.Phone,
                Latitude = customer.Location.Latitude
            };
            try
            {
                dalObject.AddCustomer(customerDL);
            }
            catch (DalApi.IdAlreadyExist)
            {
                throw new IdAlreadyExist(customer.Id);
            }
        }

        /// <summary>
        /// convert To Customer BL
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private Customer convertToCustomerBL(DO.Customer c)
        {
            Customer CustomerBL = new Customer() { Id = c.Id, Name = c.Name, Location = new Location(c.Latitude, c.Longitude), Phone = c.Phone };

            foreach (var p in GetParcels())
            {
                if (p.customerAtParcelSender.Id == CustomerBL.Id)
                {
                    ParcelAtCustomer parcelAtCustomer = new ParcelAtCustomer() { ID = p.Id, CustomerAtParcel = new CustomerAtParcel() { Id = CustomerBL.Id, Name = CustomerBL.Name }, WeightCategories = p.Weight, Pritorities = p.Pritority, ParcelStatus = ParcelsStatus(p) };
                    CustomerBL.parcelsSentedByCustomer.Add(parcelAtCustomer);
                }
                if (p.customerAtParcelReciver.Id == CustomerBL.Id)
                {
                    ParcelAtCustomer parcelAtCustomer = new ParcelAtCustomer() { ID = p.Id, CustomerAtParcel = new CustomerAtParcel() { Id = CustomerBL.Id, Name = CustomerBL.Name }, WeightCategories = p.Weight, Pritorities = p.Pritority, ParcelStatus = ParcelsStatus(p) };
                    CustomerBL.parcelsSentedToCustomer.Add(parcelAtCustomer);
                }
            }
            return CustomerBL;
        }

        public void updateCustomer(Customer customer)
        {
            dalObject.UpdateCustomer(new DO.Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Longitude = customer.Location.Longitude,
                Latitude = customer.Location.Latitude
            });
        }
        public IEnumerable<Customer> GetCustomersBy(Predicate<Customer> findBy)
        {
            return from customer in GetCustomers()
                   where findBy(customer)
                   select customer;
        }

        public Customer GetCustomerById(int id)
        {
            try
            {
                return GetCustomersBy(c => c.Id == id).First();
            }
            catch
            {
                throw new NotFound($"couldn't find Customer ${id}");
            }
        }
        /// <summary>
        /// Get Customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomers()
        {

            return from customer in dalObject.GetCustomers()
                   select convertToCustomerBL(customer);


        }
        public CustomerToList convertCustomerToCustomerToList(Customer customer)
        {
            return new CustomerToList()
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                NumberOfParcelsInTheWayToCutemor = customer.parcelsSentedToCustomer.Count(p => p.ParcelStatus == ParcelStatus.PickedUp),
                NumberOfRecievedParcels = customer.parcelsSentedToCustomer.Count(p => p.ParcelStatus == ParcelStatus.Delivered),
                NumberOfParcelsSendedAndNotProvided = customer.parcelsSentedByCustomer.Count(p => p.ParcelStatus == ParcelStatus.PickedUp),
                NumberOfParcelsSendedAndProvided = customer.parcelsSentedByCustomer.Count(p => p.ParcelStatus == ParcelStatus.Delivered)
            };
        }
    }

}


