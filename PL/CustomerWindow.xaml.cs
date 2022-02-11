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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        CurrentUser currentUser;
        IBL bl;
        Customer customer;
        Customer_p Customer_P;
        CustomerList Customers = new CustomerList();
        #region CustomerWindow constructors

        public CustomerWindow()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
        }

        public CustomerWindow(IBL BL, CurrentUser currentUser1, CustomerList customerList)
        {
            currentUser = currentUser1;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = BL;
            updateButton.Visibility = Visibility.Collapsed;
            CurrentUser.Text = currentUser.Type.ToString();
            this.Customers = customerList;
        }

        public CustomerWindow(IBL BL, Customer customerArg, CurrentUser currentUser1, CustomerList customerList)
        {
            currentUser = currentUser1;
            InitializeComponent();
            customer = customerArg;
            Customer_P = new Customer_p() { Id = customer.Id, Name = customer.Name, Longitude = customer.Location.Longitude, Latitude= customer.Location.Latitude, Phone = customer.Phone, ParcelsSentedByCustomer = customer.ParcelsSentedByCustomer, ParcelsSentedToCustomer = customer.ParcelsSentedToCustomer };
            bl = BL;
            DataContext = Customer_P;
            Customers = customerList;
            ParcelByCustomerListView.ItemsSource = customer.ParcelsSentedByCustomer;
            ParcelToCustomerListView.ItemsSource = customer.ParcelsSentedToCustomer;
            addButton.Visibility = Visibility.Collapsed;
            CurrentUser.Text = currentUser.Type.ToString();
        }

        #endregion

        #region buttons functionality
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addButton_click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddCustomer(new Customer { Id = getId(), Name = getName(),Phone = getPhone(), Location = getLocation() });
                Close();
                MessageBox.Show("The customer added");
                Customers.AddeCustomer(bl.ConvertCustomerToTypeOfCustomerToList(bl.GetCustomerById(getId())));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void updateButton_click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateCustomer(new Customer() { Id = getId(), Name = getName(), Phone = getPhone(), Location = getLocation() });
                MessageBox.Show("The customer Updeted");
                CustomerToList customer = bl.GetCustomerToLists().First((c) => c.Id == Customer_P.Id);
                this.Customer_P.UpdateFromBL(bl.GetCustomerById(getId()));
                this.Customer_P.UpdateFromToList(customer);
                Customers.UpdateList(Customer_P);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region get input from TextBoxes
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
                return new Location(){ Longitude = longitude, Latitude = latitude};
            }
            catch
            {
                throw new NotValidInput("Location");
            }
        }

        #endregion

        #region ParcelsList MouseDoubleClick

        private void ParcelsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ParcelAtCustomer parcelAtCustomer = (sender as ListView).SelectedValue as ParcelAtCustomer;
            Parcel parcel = bl.GetParcelById(parcelAtCustomer.Id);
            new ParcelWindow(bl, parcel, currentUser).Show();
        }

        #endregion

        private void addParcelButton_Click(object sender, RoutedEventArgs e)
        {
            //MIRIAM-TODO
            //new ParcelWindow()
        }

    }
}
