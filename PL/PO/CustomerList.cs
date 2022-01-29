using BlApi;
using BO;
using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO
{
    public class CustomerList : DependencyObject
    {
        IBL BL;
        public CustomerList()
        {

        }

        public ObservableCollection<Customer_p> Customers = new ObservableCollection<Customer_p>();
        public void AddeCustomer(CustomerToList customer)
        {
            Customers.Add(new Customer_p { Id = customer.Id, Name = customer.Name, Phone = customer.Phone, NumberOfParcelsInTheWayToCutemor = customer.NumberOfParcelsInTheWayToCutemor, NumberOfParcelsSendedAndNotProvided = customer.NumberOfParcelsSendedAndNotProvided, NumberOfParcelsSendedAndProvided = customer.NumberOfParcelsSendedAndProvided, NumberOfRecievedParcels = customer.NumberOfRecievedParcels });

        }
        public ObservableCollection<Customer_p> ConvertCustomerlBLToPL(List<CustomerToList> customersToLists)
        {
            foreach (var customer in customersToLists)
            {
                Customer_p customer1 = new Customer_p() { Id = customer.Id, Name = customer.Name, Phone = customer.Phone, NumberOfParcelsInTheWayToCutemor = customer.NumberOfParcelsInTheWayToCutemor, NumberOfParcelsSendedAndNotProvided = customer.NumberOfParcelsSendedAndNotProvided, NumberOfParcelsSendedAndProvided = customer.NumberOfParcelsSendedAndProvided, NumberOfRecievedParcels = customer.NumberOfRecievedParcels };
                Customers.Add(customer1);
            }
            return Customers;
        }

        public ObservableCollection<Customer_p> ClearCustomers()
        {
            Customers = new ObservableCollection<Customer_p>();
            return Customers;
        }


        public void UpdateList(Customer_p customer)
        {

            int index = Customers.IndexOf(Customers.Where(X => X.Id == customer.Id).FirstOrDefault());
            Customers[index] = customer;

        }
    }
}


