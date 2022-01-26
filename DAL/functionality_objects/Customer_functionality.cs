using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal partial class DalObject : DalApi.IDal
    {
        #region Customer
        /// <summary>
        /// adds the customer to the customers list in the DataSource
        ///  If the ID alredy exist the function will throw exception
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            if (DataSource.customers.Any(cs => cs.Id == customer.Id))
            {
                throw new IdAlreadyExist(customer.Id);
            }
            DataSource.customers.Add(customer);
        }

        /// <summary>
        /// returns customers form datasource
        /// </summary>
        /// <returns>DataSource.customers</returns>
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> getBy = null)
        {
            getBy ??= (customer => true);
            return from customer in DataSource.customers
                   where (getBy(customer))
                   select customer;
        }

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