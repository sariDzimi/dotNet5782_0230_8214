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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        IBL bl;
        Customer customer;
        public CustomerWindow()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
        }
        public CustomerWindow(IBL BL)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = BL;
            updateButton.Visibility = Visibility.Collapsed;
        }

        public CustomerWindow(IBL BL, Customer customerArg)
        {
            InitializeComponent();
            customer = customerArg;
            bl = BL;
            DataContext = customer;
            ParcelByCustomerListView.ItemsSource = customer.parcelsSentedByCustomer;
            ParcelToCustomerListView.ItemsSource = customer.parcelsSentedToCustomer;
            addButton.Visibility = Visibility.Collapsed;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            new CustomersList(bl).Show();
            Close();
        }

        private void addButton_click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.addCustomerToDL(new Customer { Id = getId(), Name = getName(),Phone = getPhone(), Location = getLocation() });
                new CustomersList(bl).Show();
                Close();

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
                bl.updateCustomer(new Customer() { Id = getId(), Name = getName(), Phone = getPhone(), Location = getLocation() });
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
                return new Location(longitude, latitude);
            }
            catch
            {
                throw new NotValidInput("Location");
            }
        }

    }
}
