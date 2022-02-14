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
using System.Windows.Threading;
using BlApi;
using BO;
using PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public Action<BO.Customer> ChangedParcelDelegate;
        userType currentUser;
        IBL bl;
        BO.Customer customer;
        PO.Customer customer_display;

        #region CustomerWindow constructors
        public CustomerWindow()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
        }

        public CustomerWindow(IBL bl, userType currentUser)
        {
            this.currentUser = currentUser;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            this.bl = bl;
            updateButton.Visibility = Visibility.Collapsed;
            CurrentUser.Text = this.currentUser.ToString();
            customer_display.ListChangedDelegate += new Action<BO.Customer>(updateCustomerList);

        }

        public CustomerWindow(IBL bl, BO.Customer customer, userType currentUser)
        {
            this.currentUser = currentUser;
            InitializeComponent();
            this.customer = customer;
            customer_display = new PO.Customer()
            {
                Id = this.customer.Id,
                Name = this.customer.Name,
                Longitude = this.customer.Location.Longitude,
                Latitude = this.customer.Location.Latitude,
                Phone = this.customer.Phone,
                ParcelsSentedByCustomer = this.customer.ParcelsSentedByCustomer,
                ParcelsSentedToCustomer = this.customer.ParcelsSentedToCustomer
            };
            this.bl = bl;
            DataContext = customer_display;
            ParcelByCustomerListView.ItemsSource = this.customer.ParcelsSentedByCustomer;
            ParcelToCustomerListView.ItemsSource = this.customer.ParcelsSentedToCustomer;
            addButton.Visibility = Visibility.Collapsed;
            CurrentUser.Text = this.currentUser.ToString();
            customer_display.ListChangedDelegate += new Action<BO.Customer>(updateCustomerList);

        }

        /// <summary>
        /// updates customer in customers list window
        /// </summary>
        /// <param name="customer">updated customer</param>
        public void updateCustomerList(BO.Customer customer)
        {
            if (ChangedParcelDelegate != null)
            {
                ChangedParcelDelegate(customer);
            }
        }

        #endregion

        #region buttons functionality


        /// <summary>
        /// opens parcel window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addParcel_Click(object sender, RoutedEventArgs e)
        {
            ParcelWindow parcelWindow = new ParcelWindow(bl, currentUser);
            parcelWindow.ChangedParcelDelegate = updateInList;
            parcelWindow.Show();

        }

        /// <summary>
        /// closes window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// adds customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddCustomer(new BO.Customer { Id = getId(), Name = getName(), Phone = getPhone(), Location = getLocation() });
                BO.Customer customer = bl.GetCustomerById(getId());
                if (customer_display.ListChangedDelegate != null)
                {
                    customer_display.ListChangedDelegate(customer);
                }
                Close();
                MessageBox.Show("The customer was added succesfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// updates customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateCustomer(new BO.Customer() { Id = getId(), Name = getName(), Phone = getPhone(), Location = getLocation() });
                MessageBox.Show("The customer Updeted");
                CustomerToList customer = bl.GetCustomerToLists().First((c) => c.Id == customer_display.Id);
                customer_display.UpdateFromBL(bl.GetCustomerById(getId()));
                customer_display.UpdateFromToList(customer);
                if (customer_display.ListChangedDelegate != null)
                {
                    customer_display.ListChangedDelegate(bl.GetCustomerById(customer.Id));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region get input from TextBoxes

        /// <summary>
        /// gets input of id
        /// </summary>
        /// <returns>id of customer</returns>
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

        /// <summary>
        /// gets input of name
        /// </summary>
        /// <returns>name of customer</returns>
        private string getName() { return nameTextBox.Text; }

        /// <summary>
        /// gets input of phone
        /// </summary>
        /// <returns>phone of customer</returns>
        private string getPhone() { return phoneTextBox.Text; }

        /// <summary>
        /// gets input of location
        /// </summary>
        /// <returns>location of customer</returns>
        private Location getLocation()
        {
            try
            {
                double longitude = Convert.ToDouble(longitudeTextBox.Text);
                double latitude = Convert.ToDouble(latitudeTextBox.Text);
                return new Location() { Longitude = longitude, Latitude = latitude };
            }
            catch
            {
                throw new NotValidInput("Location");
            }
        }

        

        #endregion

        #region ParcelsList MouseDoubleClick

        /// <summary>
        /// opens parcel window of choosen parcel
        /// </summary>
        /// <param name="sender">parcel</param>
        /// <param name="e"></param>
        private void parcelsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ParcelAtCustomer parcelAtCustomer = (sender as ListView).SelectedValue as ParcelAtCustomer;
            BO.Parcel parcel = bl.GetParcelById(parcelAtCustomer.Id);
            ParcelWindow OpaenParcel = new ParcelWindow(bl, parcel, currentUser);
            OpaenParcel.ChangedParcelDelegate += updateInList;
            OpaenParcel.Show();
        }

        /// <summary>
        /// updates the parcels lists
        /// </summary>
        /// <param name="parcel"></param>
        public void updateInList(BO.Parcel parcel)
        {
            ParcelByCustomerListView.ItemsSource = customer.ParcelsSentedByCustomer;
            ParcelToCustomerListView.ItemsSource = customer.ParcelsSentedToCustomer;
        }
        #endregion


    }
}
