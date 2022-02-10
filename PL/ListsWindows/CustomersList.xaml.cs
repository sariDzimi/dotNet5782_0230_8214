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
            BO.Customer customer = bl.GetCustomerById(customerToList.Id);
            CustomerWindow OpenWindow = new CustomerWindow(bl, customer, currentUser);

        
    }

        private void addCustomer_click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow(bl, currentUser).Show();

        }
    }
}
