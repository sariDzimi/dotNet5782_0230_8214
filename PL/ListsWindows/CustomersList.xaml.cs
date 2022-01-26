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
namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerList.xaml
    /// </summary>
    public partial class CustomersList : Window
    {
        CurrentUser currentUser = new CurrentUser();
        private IBL bl;
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
            WindowStyle = WindowStyle.None;
            customersListView.ItemsSource = bl.GetCustomerToLists();
            CurrentUser.Text = currentUser.Type;

        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            //new ManegerWindow(bl, currentUser).Show();
            Close();
        }

        private void customeList_MouseDoubleList(object sender, MouseButtonEventArgs e)
        {
            CustomerToList customerToList = (sender as ListView).SelectedValue as CustomerToList;
            BO.Customer customer = bl.GetCustomerById(customerToList.Id);

            //Hide();
            new CustomerWindow(bl, customer, currentUser).Show();
            //Show();
        }

        private void addCustomer_click(object sender, RoutedEventArgs e)
        {


            //var win = new CustomerWindow(bl, currentUser);
            //Hide();
            new CustomerWindow(bl, currentUser).Show();
            //Show();

        }
    }
}
