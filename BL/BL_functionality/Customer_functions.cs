using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace BL
{
    partial class BL
    {
        #region Add Customer

        /// <summary>
        /// adds customer to dal
        /// </summary>
        /// <param name="customer">customer</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer customer)
        {
            DO.Customer customerDL = convertCustomerFromBlToDal(customer);

            try
            {
                lock (dal)
                {
                    dal.AddCustomer(customerDL);
                }

            }
            catch (DalApi.IdAlreadyExistException)
            {
                throw new IdAlreadyExist("customer", customer.Id);
            }
        }

        #endregion

        #region Get Customer

        /// <summary>
        /// gets customers frone Dal
        /// </summary>
        /// <returns>customers</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomers()
        {
            lock (dal)
            {
                return from customer in dal.GetCustomers()
                       select convertCustomerFromDalToBl(customer);
            }
        }

        /// <summary>
        /// gets customers form dal that full-fill the condition
        /// </summary>
        /// <param name="findBy">condition</param>
        /// <returns>customers that full-fill the condition</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomersBy(Predicate<Customer> findBy)
        {
            return from customer in GetCustomers()
                   where findBy(customer)
                   select customer;
        }

        /// <summary>
        /// finds a customer with the given id
        /// </summary>
        /// <param name="id">id of customoer</param>
        /// <returns>customer with the given id</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomerById(int id)
        {
            try
            {
                lock (dal)
                {
                    return convertCustomerFromDalToBl(dal.GetCustomerById(id));
                }

            }
            catch (DalApi.NotFoundException)
            {
                throw new NotFound("customer", id);
            }
        }

        /// <summary>
        /// get customers as CustomerToList type
        /// </summary>
        /// <returns>customers as CustomerToList type</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> GetCustomerToLists()
        {
            return from customer in GetCustomers()
                   select ConvertCustomerToTypeOfCustomerToList(customer);
        }

        #endregion

        #region Update Customer

        /// <summary>
        /// updates customer at dal
        /// </summary>
        /// <param name="customer"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(Customer customer)
        {
            try
            {
                lock (dal)
                {
                    dal.UpdateCustomer(convertCustomerFromBlToDal(customer));
                }
            }
            catch (DalApi.NotFoundException)
            {
                throw new NotFound("customer", customer.Id);
            }
        }

        #endregion

        #region Convert Customer

        /// <summary>
        /// converts customer of type BL to type DAL
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        private DO.Customer convertCustomerFromBlToDal(Customer customer)
        {
            return new DO.Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Longitude = customer.Location.Longitude,
                Phone = customer.Phone,
                Latitude = customer.Location.Latitude
            };
        }

        /// <summary>
        /// converts a customer and parcel to ParcelAtCustomer entity
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="parcel"></param>
        /// <returns></returns>
        private ParcelAtCustomer convertParcelAtCustomer(Customer customer, Parcel parcel)
        {
            return new ParcelAtCustomer()
            {
                Id = parcel.Id,
                CustomerAtParcel = new CustomerAtParcel() { Id = customer.Id, Name = customer.Name },
                WeightCategories = parcel.Weight,
                Pritorities = parcel.Pritority,
                ParcelStatus = calculateParcelsStatus(parcel)
            };
        }

        /// <summary>
        /// convert customer of type Dal to customer of type BL
        /// </summary>
        /// <param name="customer">customer of type </param>
        /// <returns></returns>
        private Customer convertCustomerFromDalToBl(DO.Customer customer)
        {
            Customer CustomerBL = new Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Location = new Location() { Longitude = customer.Longitude , Latitude = customer.Latitude},
                Phone = customer.Phone
            };


            foreach (var parcel in GetParcels())
            {
                if (parcel.customerAtParcelSender.Id == CustomerBL.Id)
                {
                    ParcelAtCustomer parcelAtCustomer = convertParcelAtCustomer(CustomerBL, parcel);
                    CustomerBL.ParcelsSentedByCustomer ??= new List<ParcelAtCustomer>();
                    CustomerBL.ParcelsSentedByCustomer.Add(parcelAtCustomer);
                }
                if (parcel.customerAtParcelReciver.Id == CustomerBL.Id)
                {
                    ParcelAtCustomer parcelAtCustomer = convertParcelAtCustomer(CustomerBL, parcel);
                    CustomerBL.ParcelsSentedToCustomer ??= new List<ParcelAtCustomer>();
                    CustomerBL.ParcelsSentedToCustomer.Add(parcelAtCustomer);
                }
            }
            return CustomerBL;
        }

        /// <summary>
        /// convert customer to type of customerToList
        /// </summary>
        /// <param name="customer">customer</param>
        /// <returns>customerToList</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public CustomerToList ConvertCustomerToTypeOfCustomerToList(Customer customer)
        {
            return new CustomerToList()
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                NumberOfParcelsInTheWayToCutemor = customer.ParcelsSentedToCustomer ==null? 0: customer.ParcelsSentedToCustomer.Count(p => p.ParcelStatus == ParcelStatus.PickedUp),
                NumberOfRecievedParcels = customer.ParcelsSentedToCustomer ==null? 0: customer.ParcelsSentedToCustomer.Count(p => p.ParcelStatus == ParcelStatus.Delivered),
                NumberOfParcelsSendedAndNotProvided = customer.ParcelsSentedByCustomer==null?0: customer.ParcelsSentedByCustomer.Count(p => p.ParcelStatus == ParcelStatus.PickedUp),
                NumberOfParcelsSendedAndProvided = customer.ParcelsSentedByCustomer==null?0: customer.ParcelsSentedByCustomer.Count(p => p.ParcelStatus == ParcelStatus.Delivered)
            };
        }

        #endregion
    }

}


