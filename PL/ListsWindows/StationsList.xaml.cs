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
    /// Interaction logic for StationsList.xaml
    /// </summary>
    public partial class StationsList : Window
    {
        CurrentUser currentUser = new CurrentUser();
        private IBL bL;
        CollectionView view;
        List<StationToList> items;
        StationList stationList = new StationList();
        public StationsList()
        {
            InitializeComponent();
        }

        public StationsList(IBL bl, CurrentUser currentUser1)
        {
            currentUser = currentUser1;
            WindowStyle = WindowStyle.None;
            InitializeComponent();
            bL = bl;
            items = bl.GetStationToLists().ToList();
            stationList.Stations = stationList.ConvertStationBLToPL(items);
            StationsListView.DataContext = stationList.Stations;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
            CurrentUser.Text = currentUser.Type.ToString();
        }

        private void Cheked_onlyStationsWithFreeSlots(object sender, RoutedEventArgs e)
        {
            stationList.ClearStations();
            stationList.ConvertStationBLToPL(bL.GetStationToListBy(s => s.numberOfFreeChargeSlots != 0).ToList());
            StationsListView.DataContext = stationList.Stations;

        }

        private void Button_Click_groupByNumberOfFree(object sender, RoutedEventArgs e)
        {
            clearListView();
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("numberOfFreeChargeSlots");
            view.GroupDescriptions.Add(groupDescription);
        }

        private void clearListView()
        {
            stationList.Stations = stationList.ClearStations();
            stationList.Stations = stationList.ConvertStationBLToPL(bL.GetStationToLists().ToList());
            StationsListView.DataContext = stationList.Stations;
            view = (CollectionView)CollectionViewSource.GetDefaultView(StationsListView.ItemsSource);
        }

        private void Button_Click_ClearGroupBy(object sender, RoutedEventArgs e)
        {
            clearListView();
        }

        private void unChecked_onlyFreeChargeSlots(object sender, RoutedEventArgs e)
        {
            clearListView();
        }

        private void MouseDoubleClick_stationChoosen(object sender, MouseButtonEventArgs e)
        {
            Station_p stationToList = (sender as ListView).SelectedValue as Station_p;
            BO.Station station = bL.GetStationById(stationToList.ID);
            new StationWindow(bL, station, currentUser, stationList).Show();
            
        }

        private void adddStation_Click(object sender, RoutedEventArgs e)
        {
            new StationWindow(bL, currentUser, stationList).Show();
        }

        private void closeButton_click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
