using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerList.xaml
    /// </summary>
    public partial class CustomersList : Window
    {

        private IBL bl;
        ObservableCollection<CustomerToList> customers = new ObservableCollection<CustomerToList>();
        CustomerWindow customerWindow;

        public CustomersList()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
        }
        public CustomersList(IBL bl) : this()
        {
            this.bl = bl;
            customers = Convert(this.bl.GetCustomerToLists());
            DataContext = customers;
        }

        /// <summary>
        /// opens CustomerWindow of the customer that is choosen
        /// </summary>
        /// <param name="sender">choosen customer</param>
        /// <param name="e"></param>
        private void customerChoosen_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CustomerToList customerToList = (sender as ListView).SelectedValue as CustomerToList;
            CustomerWindow OpenWindow = new CustomerWindow(bl, bl.GetCustomerById(customerToList.Id), userType.manager);
            OpenWindow.ChangedParcelDelegate += updateInList;
            OpenWindow.Show();
        }

        /// <summary>
        /// adds customer
        /// </summary>
        /// <param name="sender">customer</param>
        /// <param name="e"></param>
        private void addCustomer_Click(object sender, RoutedEventArgs e)
        {
            customerWindow = new CustomerWindow(bl, userType.cutomer);
            customerWindow.ChangedParcelDelegate += addCustomerToList;
            customerWindow.Show();
        }

        /// <summary>
        /// closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #region Binding
        /// <summary>
        /// converts collection of type IEnumerable to observable collection
        /// <typeparam name="T"></typeparam>
        /// <param name="original">collection of type IEnumerable</param>
        /// <returns>collection of type ObservableCollection</returns>
        private void updateInList(BO.Customer customer)
        {
            CustomerToList parcelToList = customers.First((d) => d.Id == customer.Id);
            int index = customers.IndexOf(parcelToList);
            customers[index] = bl.ConvertCustomerToTypeOfCustomerToList(customer);
        }

        /// <summary>
        /// adds the customer to the observable collection
        /// </summary>
        /// <param name="customer">customer</param>
        public void addCustomerToList(BO.Customer customer)
        {
            customers.Add(bl.ConvertCustomerToTypeOfCustomerToList(customer));
        }

        /// <summary>
        /// converts collection of type IEnumerable to observable collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <returns></returns>
        public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }
        #endregion
    }
}
