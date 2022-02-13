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
    public partial class StationsList : Window
    {
        ObservableCollection<StationToList> Stations = new ObservableCollection<StationToList>();
        StationWindow stationWindow;
        private IBL bL;
        CollectionView view;
        public StationsList()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="currentUser1"></param>
        public StationsList(IBL bl) : this()
        {
            bL = bl;
            Stations = Convert<StationToList>(bl.GetStationToLists());
            DataContext = Stations;
        }

        /// <summary>
        /// converts collection of type IEnumerable to observable collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original">collection of type IEnumerable</param>
        /// <returns>collection of type observable collection</returns>
        public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }

        /// <summary>
        /// shows only stations with free charge slots
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showOnlyStationsWithFreeSlots_Checked(object sender, RoutedEventArgs e)
        {
            clearListView();
            Stations = Convert<StationToList>(bL.GetStationToListBy(s => s.NumberOfFreeChargeSlots != 0));

        }

        /// <summary>
        /// groups stations by number of free charge slots
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupByNumberOfFreeChargeSlots_Click(object sender, RoutedEventArgs e)
        {
            clearListView();
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("NumberOfFreeChargeSlots");
            view.GroupDescriptions.Add(groupDescription);
        }
        /// <summary>
        /// clears grouped of selcted view to default view
        /// </summary>
        private void clearListView()
        {
            Stations = new ObservableCollection<StationToList>();
            Stations = Convert<StationToList>(bL.GetStationToLists());
            DataContext = Stations;
            view = (CollectionView)CollectionViewSource.GetDefaultView(StationsListView.ItemsSource);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearGroupBy_Click(object sender, RoutedEventArgs e)
        {
            clearListView();
        }
        
        /// <summary>
        /// shows all stations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dontShowOnlyFreeChargeSlots_Unchecked(object sender, RoutedEventArgs e)
        {
            clearListView();
        }
        
        /// <summary>
        /// opens station window of choosen station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stationChoosen_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            StationToList stationToList = (sender as ListView).SelectedValue as StationToList;
            BO.Station station = bL.GetStationById(stationToList.Id);
            stationWindow = new StationWindow(bL, station);
            stationWindow.ChangedParcelDelegate += updateInList;
            stationWindow.Show();

        }

        /// <summary>
        /// updates station in the observable collection
        /// </summary>
        /// <param name="station"></param>
        private void updateInList(BO.Station station)
        {
            StationToList stationToList = Stations.First((d) => d.Id == station.Id);
            int index = Stations.IndexOf(stationToList);
            Stations[index] = bL.convertStationToTypeOfStationToList(station);


        }

        /// <summary>
        /// adds station to the observable collection
        /// </summary>
        /// <param name="station"></param>
        private void addStationToList(BO.Station station)
        {
            Stations.Add(new StationToList() { Id = station.Id, Name = station.Name, NumberOfFreeChargeSlots = station.FreeChargeSlots });
            Stations.Add(bL.convertStationToTypeOfStationToList(station));
        }

        /// <summary>
        /// adds station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addStation_Click(object sender, RoutedEventArgs e)
        {
            stationWindow = new StationWindow(bL);
            stationWindow.ChangedParcelDelegate += addStationToList;
            stationWindow.Show();
        }

        /// <summary>
        /// closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeWindow_click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
