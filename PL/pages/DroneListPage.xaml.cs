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
    /// Interaction logic for DroneListPage.xaml
    /// </summary>
    public partial class DroneListPage : Page
    {
        public DroneListPage()
        {
            InitializeComponent();
        }
        public CurrentUser currentUser = new CurrentUser();

        private IBL bl;

        public DroneListPage(IBL bL1, CurrentUser currentUser1)

        {
            currentUser = currentUser1;
            InitializeComponent();
            //WindowStyle = WindowStyle.None;
            bl = bL1;
            DronesListView.ItemsSource = bl.GetDroneToLists();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.DroneStatus));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
        }

        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (BO.WeightCategories)MaxWeightSelector.SelectedItem;
            DronesListView.ItemsSource = bl.GetDroneToListsBy((d) => d.MaxWeight == selected);
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (BO.DroneStatus)StatusSelector.SelectedItem;
            DronesListView.ItemsSource = bl.GetDroneToListsBy((d) => d.DroneStatus == selected);
        }


        private void MouseDoubleClick_droneChoosen(object sender, MouseButtonEventArgs e)
        {
            DroneToList droneToList = (sender as ListView).SelectedValue as DroneToList;
            BO.Drone droneBL = bl.GetDroneById(droneToList.Id);
            Window.GetWindow(this).Content = new DronePage(bl, droneBL, currentUser, this);
        }

        private void addADrone_Click(object sender, RoutedEventArgs e)
        {
           // Window.GetWindow(this).Content = new DronePage(bl, currentUser, this); new Drone(bl, currentUser, Drone_Ps).Show();

        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            new ManegerWindow(bl, currentUser).Show();
            Window.GetWindow(this).Close();
        }

       
    }
}
