using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BlApi;
using BO;
using PL.PO;
using PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerList.xaml
    /// </summary>
    public partial class CustomersList : Window
    {
        CurrentUser currentUser = new CurrentUser();
        private IBL bl;
        CustomerList customerList = new CustomerList();
        public CustomersList()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
        }

        public CustomersList(IBL blArg, CurrentUser currentUser1)
        {

            currentUser = currentUser1;
            InitializeComponent();
            bl = blArg;
            customerList.Customers = customerList.ConvertCustomerlBLToPL(bl.GetCustomerToLists().ToList());
            customersListView.DataContext = customerList.Customers;
            WindowStyle = WindowStyle.None;
            CurrentUser.Text = currentUser.Type.ToString();

        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void customeList_MouseDoubleList(object sender, MouseButtonEventArgs e)
        {
            Customer_p  customerToList = (sender as ListView).SelectedValue as Customer_p;
            BO.Customer customer = bl.GetCustomerById(customerToList.Id);
            new CustomerWindow(bl, customer, currentUser, customerList).Show();
        
    }

        private void addCustomer_click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow(bl, currentUser, customerList).Show();

        }
    }
}
