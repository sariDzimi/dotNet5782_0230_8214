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
//using PL.PO;
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

        CurrentUser currentUser = new CurrentUser();
        private IBL bl;
        //CustomerList customerList = new CustomerList();
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
            Customers = Convert<CustomerToList>(bl.GetCustomerToLists());
            DataContext = Customers;
            WindowStyle = WindowStyle.None;
            CurrentUser.Text = currentUser.Type.ToString();

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
            CustomerWindow OpenWindow = new CustomerWindow(bl, bl.GetCustomerById(customerToList.Id), currentUser);
            OpenWindow.ChangedParcelDelegate += UpdateInList;
            OpenWindow.Show();

        }
        private void UpdateInList(BO.Customer customer) {
            CustomerToList parcelToList =Customers.First((d) => d.Id == customer.Id);
            int index = Customers.IndexOf(parcelToList);
            Customers[index] = bl.convertCustomerToTypeOfCustomerToList(customer);
        }

        public void AddCustomerToLst(BO.Customer parcel)
        {
            Customers.Add(bl.convertCustomerToTypeOfCustomerToList(parcel));
        }

        private void addCustomer_click(object sender, RoutedEventArgs e)
        {
            OpenWindow = new CustomerWindow(bl, currentUser);
            OpenWindow.ChangedParcelDelegate += AddCustomerToLst;
            OpenWindow.Show();

        }
    }
}
