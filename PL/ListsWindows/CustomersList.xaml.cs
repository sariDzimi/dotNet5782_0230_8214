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
        ObservableCollection<CustomerToList> Customers = new ObservableCollection<CustomerToList>();
        CustomerWindow OpenWindow;

        private IBL bl;
        public CustomersList()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
        }

        public CustomersList(IBL blArg)
        {
            InitializeComponent();
            bl = blArg;
            Customers = Convert<CustomerToList>(bl.GetCustomerToLists());
            DataContext = Customers;
            WindowStyle = WindowStyle.None;

        }

        public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void customeList_MouseDoubleList(object sender, MouseButtonEventArgs e)
        {
            CustomerToList customerToList = (sender as ListView).SelectedValue as CustomerToList;
            CustomerWindow OpenWindow = new CustomerWindow(bl, bl.GetCustomerById(customerToList.Id), userType.manager);
            OpenWindow.ChangedParcelDelegate += UpdateInList;
            OpenWindow.Show();

        }
        private void UpdateInList(BO.Customer customer) {
            CustomerToList parcelToList =Customers.First((d) => d.Id == customer.Id);
            int index = Customers.IndexOf(parcelToList);
            Customers[index] = bl.ConvertCustomerToTypeOfCustomerToList(customer);
        }

        public void AddCustomerToLst(BO.Customer parcel)
        {
            Customers.Add(bl.ConvertCustomerToTypeOfCustomerToList(parcel));
        }

        private void addCustomer_click(object sender, RoutedEventArgs e)
        {
            OpenWindow = new CustomerWindow(bl, userType.cutomer);
            OpenWindow.ChangedParcelDelegate += AddCustomerToLst;
            OpenWindow.Show();

        }
    }
}
