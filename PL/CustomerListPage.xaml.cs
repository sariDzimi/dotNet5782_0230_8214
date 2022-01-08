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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerListPage.xaml
    /// </summary>
    public partial class CustomerListPage : Page
    {
        public CustomerListPage()
        {
            InitializeComponent();
        }
        CurrentUser currentUser = new CurrentUser();
        private IBL bl;

        public CustomerListPage(IBL blArg, CurrentUser currentUser1)
        {
            currentUser = currentUser1;
            InitializeComponent();
            bl = blArg;
            customersListView.ItemsSource = bl.GetCustomerToLists();

        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            new ManegerWindow(bl, currentUser).Show();
            Window.GetWindow(this).Close();
        }

        private void customeList_MouseDoubleList(object sender, MouseButtonEventArgs e)
        {
            CustomerToList customerToList = (sender as ListView).SelectedValue as CustomerToList;
            BO.Customer customer = bl.FindCustomer(customerToList.Id);
            Window.GetWindow(this).Content = new CustomerPage(bl, customer, currentUser, this);
        }

        private void addCustomer_click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new CustomerPage(bl, currentUser, this);
        }
    }
}
