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
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for DronesList.xaml
    /// </summary>
    public partial class DronesList : Window
    {
        ObservableCollection<DroneToList> Drones = new ObservableCollection<DroneToList>();
        public CurrentUser currentUser = new CurrentUser();
        private IBL bl;
        public DronesList()
        {
            WindowStyle = WindowStyle.None;
            InitializeComponent();

        }


        public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }



        public DronesList(IBL bL1, CurrentUser currentUser1)
        {
            currentUser = currentUser1;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = bL1;
            Drones = Convert<DroneToList>(bl.GetDroneToLists());
            StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.DroneStatus));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            CurrentUser.Text = currentUser.Type.ToString();
            DataContext = Drones;
        }

        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (BO.WeightCategories)MaxWeightSelector.SelectedItem;
            Drones = Convert<DroneToList>(bl.GetDroneToListsBy((d) => d.MaxWeight == selected));
            DataContext = Drones;


        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (BO.DroneStatus)StatusSelector.SelectedItem;
            Drones = Convert<DroneToList>(bl.GetDroneToListsBy((d) => d.DroneStatus == selected));
            DataContext = Drones;

        }


        private void MouseDoubleClick_droneChoosen(object sender, MouseButtonEventArgs e)
        {
            DroneToList droneToList = (sender as ListView).SelectedValue as DroneToList;
            BO.Drone droneBL = bl.GetDroneById(droneToList.Id);
            Drone OpenWindow = new Drone(bl, droneBL, currentUser);
            OpenWindow.ChangedDroneDelegate += UpdateInList;
            OpenWindow.Show();

        }

        public void UpdateInList(BO.Drone drone)
        {
            DroneToList droneToList = Drones.First((d) => d.Id == drone.Id);
            int index = Drones.IndexOf(droneToList);
            Drones[index] = new DroneToList() {Id = drone.Id, Battery = drone.Battery, DroneStatus = drone.DroneStatus, Model = drone.Model,Location = drone.Location  };


        }
        public void AddDrone(BO.Drone drone)
        {
            Drones.Add(new DroneToList() {Id = drone.Id, Battery = drone.Battery, DroneStatus = drone.DroneStatus, Location =  drone.Location, Model  = drone.Model});
            Drones.Add(bl.ConvertDroneToTypeOfDroneToList(drone));

        }
        

        private void addADrone_Click(object sender, RoutedEventArgs e)
        {
            Drone OpenWindow = new Drone(bl,currentUser);
            OpenWindow.ChangedDroneDelegate += AddDrone;
            OpenWindow.Show();

        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
