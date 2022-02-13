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
        private IBL bl;
        ObservableCollection<DroneToList> drones = new ObservableCollection<DroneToList>();

        public DronesList()
        {
            WindowStyle = WindowStyle.None;
            InitializeComponent();
        }
        public DronesList(IBL bL1) : this()
        {
            bl = bL1;
            drones = Convert(bl.GetDroneToLists());
            StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.DroneStatus));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            DataContext = drones;
        }

        /// <summary>
        /// converts collection of type IEnumerable to observable collection
        /// <typeparam name="T"></typeparam>
        /// <param name="original">collection of type IEnumerable</param>
        /// <returns>collection of type ObservableCollection</returns>
        public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }

        /// <summary>
        /// shows only drones with selected max weight
        /// </summary>
        /// <param name="sender">max weight</param>
        /// <param name="e"></param>
        private void maxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (BO.WeightCategories)MaxWeightSelector.SelectedItem;
            drones = Convert<DroneToList>(bl.GetDroneToListsBy((d) => d.MaxWeight == selected));
            DataContext = drones;
        }

        /// <summary>
        /// shows only drones with selected status
        /// </summary>
        /// <param name="sender">status</param>
        /// <param name="e"></param>
        private void statusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (BO.DroneStatus)StatusSelector.SelectedItem;
            drones = Convert<DroneToList>(bl.GetDroneToListsBy((d) => d.DroneStatus == selected));
            DataContext = drones;
        }

        /// <summary>
        /// opens drone Window of the chosen station
        /// </summary>
        /// <param name="sender">station</param>
        /// <param name="e"></param>
        private void droneChoosen_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DroneToList droneToList = (sender as ListView).SelectedValue as DroneToList;
            BO.Drone droneBL = bl.GetDroneById(droneToList.Id);
            Drone OpenWindow = new Drone(bl, droneBL);
            OpenWindow.ChangedDroneDelegate += UpdateInList;
            OpenWindow.Show();
        }

        /// <summary>
        /// updates the drone in the observable collection
        /// </summary>
        /// <param name="drone"></param>
        public void UpdateInList(BO.Drone drone)
        {
            DroneToList droneToList = drones.First((d) => d.Id == drone.Id);
            int index = drones.IndexOf(droneToList);
            drones[index] = new DroneToList() { Id = drone.Id, Battery = drone.Battery, DroneStatus = drone.DroneStatus, Model = drone.Model, Location = drone.Location };
        }

        /// <summary>
        /// adds the drone to the observable collection
        /// </summary>
        /// <param name="drone"></param>
        public void AddDrone(BO.Drone drone)
        {
            drones.Add(bl.ConvertDroneToTypeOfDroneToList(drone));
        }

        /// <summary>
        /// adds drone
        /// </summary>
        /// <param name="sender">drone</param>
        /// <param name="e"></param>
        private void addADrone_Click(object sender, RoutedEventArgs e)
        {
            Drone OpenWindow = new Drone(bl);
            OpenWindow.ChangedDroneDelegate += AddDrone;
            OpenWindow.Show();

        }

        /// <summary>
        /// closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
