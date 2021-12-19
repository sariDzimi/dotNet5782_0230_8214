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

namespace PL
{
    /// <summary>
    /// Interaction logic for DronesList.xaml
    /// </summary>
    public partial class DronesList : Window
    {
        private BL.BL bl;
        public DronesList()
        {
            WindowStyle = WindowStyle.None;

            InitializeComponent();
            
        }

        public DronesList(BL.BL bL1)
        {
            WindowStyle = WindowStyle.None;

            InitializeComponent();
            bl = bL1;
            DronesListView.ItemsSource = bl.GetDroneToLists();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(BlApi.BO.DroneStatus));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(BlApi.BO.WeightCategories)); 
        }

        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (BlApi.BO.WeightCategories)MaxWeightSelector.SelectedItem;
            DronesListView.ItemsSource = bl.GetDroneToListsBy((d) => d.MaxWeight == selected);
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (BlApi.BO.DroneStatus)StatusSelector.SelectedItem;
            DronesListView.ItemsSource = bl.GetDroneToListsBy((d) => d.DroneStatus == selected);
        }


        private void MouseDoubleClick_droneChoosen(object sender, MouseButtonEventArgs e)
        {
            BlApi.BO.DroneToList droneToList = (sender as ListView).SelectedValue as BlApi.BO.DroneToList;
            BlApi.BO.Drone droneBL = bl.ConvertDroneToListToDrone(droneToList);
            new Drone(bl, droneBL).Show();
            Close();
        }

        private void addADrone_Click(object sender, RoutedEventArgs e)
        {
            new Drone(bl).Show();
            Close();

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow(bl).Show();
            Close();
        }

        private void DronesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
