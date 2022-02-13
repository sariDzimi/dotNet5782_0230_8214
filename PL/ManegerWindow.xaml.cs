using System.Windows;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for ManegerWindow.xaml
    /// </summary>
    public partial class ManegerWindow : Window
    {

        public IBL bL1;
        userType currentUser;

        public ManegerWindow()
        {
            InitializeComponent();
        }

        public ManegerWindow(IBL bL, userType currentUser1)
        {
            bL1 = bL;
            currentUser = currentUser1;
            InitializeComponent();
            CurrentUser.Text = currentUser1.ToString();
        }

        private void openDroneList_Click(object sender, RoutedEventArgs e)
        {
            new DronesList(bL1).Show();
        }

        private void openParcelList_Click(object sender, RoutedEventArgs e)
        {
            new ParcelsList(bL1).Show();
        }
        private void openStationsList_Click(object sender, RoutedEventArgs e)
        {
            new StationsList(bL1).Show();
        }

        private void openCustomerList_Click(object sender, RoutedEventArgs e)
        {
            new CustomersList(bL1).Show();
        }

        private void showListsButtons_Click(object sender, RoutedEventArgs e)
        {
           DronesList.Visibility = DronesList.Visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
        }
    }
}

