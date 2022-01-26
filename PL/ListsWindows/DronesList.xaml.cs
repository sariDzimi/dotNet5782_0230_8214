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
using BO;
using BlApi;
using PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DronesList.xaml
    /// </summary>
    public partial class DronesList : Window
    {
        CollectionView view;
        public CurrentUser currentUser = new CurrentUser();
        List<DroneToList> items;
        DroneList droneList = new DroneList();
        private IBL bl;
        public DronesList()
        {
            WindowStyle = WindowStyle.None;

            InitializeComponent();

        }


        public DronesList(IBL bL1, CurrentUser currentUser1)
        {
            currentUser = currentUser1;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = bL1;
            items = bl.GetDroneToLists().ToList();
            droneList.Drone_Ps = droneList.ConvertDronelBLToPL(items);
            StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.DroneStatus));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            CurrentUser.Text = currentUser.Type.ToString();
            DronesListView.DataContext = droneList.Drone_Ps;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
        }

        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (BO.WeightCategories)MaxWeightSelector.SelectedItem;
            droneList.ClearDrones();
            droneList.ConvertDronelBLToPL(bl.GetDroneToListsBy((d) => d.MaxWeight == selected).ToList());
            DronesListView.DataContext = droneList.Drone_Ps;
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (BO.DroneStatus)StatusSelector.SelectedItem;
            droneList.ClearDrones();
            droneList.ConvertDronelBLToPL(bl.GetDroneToListsBy((d) => d.DroneStatus == selected).ToList());
            DronesListView.DataContext = droneList.Drone_Ps;
        }


        private void MouseDoubleClick_droneChoosen(object sender, MouseButtonEventArgs e)
        {
            Drone_p droneToList = (sender as ListView).SelectedValue as Drone_p;
            BO.Drone droneBL = bl.GetDroneById(droneToList.ID);
            new Drone(bl, droneBL, currentUser, droneList.Drone_Ps).Show();
            //Close();
        }

        private void addADrone_Click(object sender, RoutedEventArgs e)
        {
            new Drone(bl, currentUser, droneList.Drone_Ps).Show();
            //Close();

        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            //new ManegerWindow(bl, currentUser).Show();
            Close();
        }

    }
}
