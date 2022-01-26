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
        #region Customer
        public void AddCustomer(Customer customer)
        {
            List<DO.Customer> customerList = GetCustomers().ToList();
            if (customerList.Any(p => p.Id == customer.Id))
            {
                throw new IdAlreadyExist(customer.Id);
            }

            customerList.Add(customer);

            XMLTools.SaveListToXMLSerializer<DO.Customer>(customerList, dir + customerFilePath);
        }
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
                throw new NotFoundException("customer");
            }

            customerList.Remove(customer);

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
    }
}