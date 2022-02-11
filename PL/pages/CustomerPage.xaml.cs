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
using PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        public CustomerPage()
        {
            InitializeComponent();
        }

        CurrentUser currentUser = new CurrentUser();
        IBL bl;
        Customer customer;
        Customer_p Customer_P;
        Page previousPage;
        public CustomerPage(IBL BL, CurrentUser currentUser1, Page previousPageArg)
        {
            previousPage = previousPageArg;
            currentUser = currentUser1;
            InitializeComponent();
            bl = BL;
            updateButton.Visibility = Visibility.Collapsed;
        }

        public CustomerPage(IBL BL, Customer customerArg, CurrentUser currentUser1, Page previousPageArg)
        {
            previousPage = previousPageArg;
            currentUser = currentUser1;
            InitializeComponent();
            customer = customerArg;
            Customer_P = new Customer_p() { Id = customer.Id, Name = customer.Name, Longitude = customer.Location.Longitude, Latitude = customer.Location.Latitude, Phone = customer.Phone, ParcelsSentedByCustomer = customer.ParcelsSentedByCustomer, ParcelsSentedToCustomer = customer.ParcelsSentedToCustomer };
            bl = BL;
            DataContext = Customer_P;
            ParcelByCustomerListView.ItemsSource = customer.ParcelsSentedByCustomer;
            ParcelToCustomerListView.ItemsSource = customer.ParcelsSentedToCustomer;
            addButton.Visibility = Visibility.Collapsed;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = previousPage;
        }

        private void addButton_click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddCustomer(new Customer { Id = getId(), Name = getName(), Phone = getPhone(), Location = getLocation() });
                MessageBox.Show("customer ids added succesfuly!");
                Window.GetWindow(this).Content = new CustomerListPage(bl, currentUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void updateButton_click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateCustomer(new Customer() { Id = getId(), Name = getName(), Phone = getPhone(), Location = getLocation() });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int getId()
        {
            try
            {
                return Convert.ToInt32(idTextBox.Text);
            }
            catch
            {
                throw new NotValidInput("id");
            }
        }

        private string getName() { return nameTextBox.Text; }

        private string getPhone() { return phoneTextBox.Text; }

        private Location getLocation()
        {
            try
            {
                double longitude = Convert.ToDouble(longitudeTextBox.Text);
                double latitude = Convert.ToDouble(latitudeTextBox.Text);
                return new Location() { Longitude = longitude, Latitude = latitude};
            }
            catch
            {
                throw new NotValidInput("Location");
            }
        }

        private void ParcelsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ParcelAtCustomer parcelAtCustomer = (sender as ListView).SelectedValue as ParcelAtCustomer;
            Parcel parcel = bl.GetParcelById(parcelAtCustomer.Id);
            Window.GetWindow(this).Content = new ParcelPage(bl, parcel, currentUser, this);
        }


    }
}
