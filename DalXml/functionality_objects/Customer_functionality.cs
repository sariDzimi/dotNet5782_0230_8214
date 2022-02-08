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
        #region Add Customer

        /// <summary>
        /// adds customer to customers xml file
        /// </summary>
        /// <param name="customer">customer</param>
        public void AddCustomer(Customer customer)
        {
            List<DO.Customer> customerList = GetCustomers().ToList();

            if (customerList.Any(p => p.Id == customer.Id))
            {
                throw new IdAlreadyExistException("customer", customer.Id);
            }

            customerList.Add(customer);

            XMLTools.SaveListToXMLSerializer<DO.Customer>(customerList, dir + customerFilePath);
        }

        #endregion

        #region Get Customer
        /// <summary>
        /// returns customers form customers xml file
        /// </summary>
        /// <param name="getBy">condition</param>
        /// <returns>customers that full-fill the conditon</returns>
        public IEnumerable<Customer> GetCustomers(Predicate<DO.Customer> getBy = null)
        {
            IEnumerable<Customer> customerList = XMLTools.LoadListFromXMLSerializer<DO.Customer>(dir + customerFilePath);

            getBy ??= ((st) => true);
            return from customer in customerList
                   where getBy(customer)
                   orderby customer.Id
                   select customer;

        }

        /// <summary>
        /// finds a customer by id
        /// </summary>
        /// <param name="id">id of customer</param>
        /// <returns>customer with the given id</returns>
        public DO.Customer GetCustomerById(int id)
        {
            try
            {
                return GetCustomers(c => c.Id == id).First();
            }
            catch (Exception)
            {
                throw new NotFoundException("customer", id);
            }

        }
        #endregion
        
        #region Update Customer
        /// <summary>
        /// updates the customer in the customers xml file
        /// </summary>
        /// <param name="customer">customer with updated details</param>
        public void UpdateCustomer(Customer customer)
        {
            List<DO.Customer> customerList = GetCustomers().ToList();

            int index = customerList.FindIndex(d => d.Id == customer.Id);

            if (index == -1)
                throw new NotFoundException("customer", customer.Id);

            customerList[index] = customer;

            XMLTools.SaveListToXMLSerializer(customerList, dir + customerFilePath);
        }
        #endregion

        #region Delete Customer
        /// <summary>
        /// deletes customer from customers xml file
        /// </summary>
        /// <param name="id">id of customer</param>
        public void DeleteCustomer(int id)
        {
            List<DO.Customer> customerList = GetCustomers().ToList();
            Customer customer;
            try
            {
                customer = GetCustomerById(id);
            }
            catch
            {
                throw new NotFoundException("customer", id);
            }

            customerList.Remove(customer);

            XMLTools.SaveListToXMLSerializer(customerList, dir + customerFilePath);
        }

        #endregion




    }
}