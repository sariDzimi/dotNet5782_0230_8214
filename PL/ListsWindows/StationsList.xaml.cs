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
        StationWindow OpenWindow;
        private IBL bL;
        CollectionView view;
        public StationsList()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <returns></returns>
        public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="currentUser1"></param>
        public StationsList(IBL bl)
        {
            WindowStyle = WindowStyle.None;
            InitializeComponent();
            bL = bl;
            Stations = Convert<StationToList>(bl.GetStationToLists());
            DataContext = Stations;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cheked_onlyStationsWithFreeSlots(object sender, RoutedEventArgs e)
        {
            clearListView();
            Stations=Convert<StationToList>(bL.GetStationToListBy(s => s.NumberOfFreeChargeSlots != 0));

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_groupByNumberOfFree(object sender, RoutedEventArgs e)
        {
            clearListView();
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("NumberOfFreeChargeSlots");
            view.GroupDescriptions.Add(groupDescription);
        }
        /// <summary>
        /// 
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
        private void Button_Click_ClearGroupBy(object sender, RoutedEventArgs e)
        {
            clearListView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void unChecked_onlyFreeChargeSlots(object sender, RoutedEventArgs e)
        {
            clearListView();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDoubleClick_stationChoosen(object sender, MouseButtonEventArgs e)
        {
            StationToList stationToList = (sender as ListView).SelectedValue as StationToList;
            BO.Station station = bL.GetStationById(stationToList.Id);
            OpenWindow = new StationWindow(bL, station);
            OpenWindow.ChangedParcelDelegate += UpdateInList;
            OpenWindow.Show();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="station"></param>
        private void UpdateInList(BO.Station station)
        {
            StationToList stationToList = Stations.First((d) => d.Id == station.Id);
            int index = Stations.IndexOf(stationToList);
            Stations[index] = bL.convertStationToTypeOfStationToList(station);


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="station"></param>
        private void AddStationToLst(BO.Station station)
        {
            Stations.Add(new StationToList() { Id = station.Id, Name = station.Name, NumberOfFreeChargeSlots = station.FreeChargeSlots });
            Stations.Add(bL.convertStationToTypeOfStationToList(station));


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void adddStation_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow = new StationWindow(bL);
            OpenWindow.ChangedParcelDelegate += AddStationToLst;
            OpenWindow.Show();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
