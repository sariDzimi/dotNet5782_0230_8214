using System.Windows;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for ManegerWindow.xaml
    /// </summary>
    public partial class ManegerWindow : Window
    {

        public IBL bl;
        userType currentUser;

        public ManegerWindow()
        {
            InitializeComponent();
        }

        public ManegerWindow(IBL bl, userType currentUser)
        {
            this.bl = bl;
            this.currentUser = currentUser;
            InitializeComponent();
            CurrentUser.Text = currentUser.ToString();
        }

        /// <summary>
        /// opens window of drones list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openDroneList_Click(object sender, RoutedEventArgs e)
        {
            new DronesList(bl).Show();
        }

        /// <summary>
        /// opens window of parcels list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openParcelList_Click(object sender, RoutedEventArgs e)
        {
            new ParcelsList(bl).Show();
        }

        /// <summary>
        /// opens window of stations list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openStationsList_Click(object sender, RoutedEventArgs e)
        {
            new StationsList(bl).Show();
        }

        /// <summary>
        /// opens window of customers list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openCustomerList_Click(object sender, RoutedEventArgs e)
        {
            new CustomersList(bl).Show();
        }

        /// <summary>
        /// shows and hides lists buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showAndHideListsButtons_Click(object sender, RoutedEventArgs e)
        {
           DronesList.Visibility = DronesList.Visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
        }
    }
}

