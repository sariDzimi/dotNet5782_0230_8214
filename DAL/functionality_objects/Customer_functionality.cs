using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal partial class DalObject : DalApi.IDal
    {
        #region Add Customer

        /// <summary>
        /// adds customer to DataSource
        /// </summary>
        /// <param name="customer">Customer</param>
        public void AddCustomer(Customer customer)
        {
            if (DataSource.customers.Any(cs => cs.Id == customer.Id))
            {
                throw new IdAlreadyExist(customer.Id);
            }
            DataSource.customers.Add(customer);
        }

        #endregion

        #region Get Customer

        /// <summary>
        /// returns customers form datasource
        /// </summary>
        /// <param name="getBy">condition</param>
        /// <returns>customers that full-fill the conditon</returns>
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> getBy = null)
        {
            getBy ??= (customer => true);
            return from customer in DataSource.customers
                   where (getBy(customer))
                   select customer;
        }

        /// <summary>
        /// finds a customer by id
        /// </summary>
        /// <param name="id">id of customer</param>
        /// <returns>customer with the given id</returns>
        public Customer GetCustomerById(int id)
        {
            try
            {

                return GetCustomers(c => c.Id == id).First();
            }
            catch
            {
                throw new NotFoundException("customer");
            }
        }

        #endregion

        #region Update Customer

        /// <summary>
        /// update customer in the DataSource
        /// </summary>
        /// <param name="customer">customer with updated details</param>
        public void UpdateCustomer(Customer customer)
        {
            int index = DataSource.customers.FindIndex(p => p.Id == customer.Id);
            if (index == -1)
                throw new NotFoundException("customer");
            DataSource.customers[index] = customer;

        }

        #endregion

    }
}