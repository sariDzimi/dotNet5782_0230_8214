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
    /// Interaction logic for StationsList.xaml
    /// </summary>
    public partial class StationsList : Window
    {
        ObservableCollection<StationToList> Stations = new ObservableCollection<StationToList>();
        StationWindow OpenWindow;

        CurrentUser currentUser = new CurrentUser();
        private IBL bL;
        CollectionView view;
        //List<StationToList> items;
        //StationList stationList = new StationList();
        public StationsList()
        {
            InitializeComponent();
        }

        public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }
        public StationsList(IBL bl, CurrentUser currentUser1)
        {
            currentUser = currentUser1;
            WindowStyle = WindowStyle.None;
            InitializeComponent();
            bL = bl;
            //items = bl.GetStationToLists().ToList();
            Stations = Convert<StationToList>(bl.GetStationToLists());

            //stationList.Stations = stationList.ConvertStationBLToPL(items);
            DataContext = Stations;
            //view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
            CurrentUser.Text = currentUser.Type.ToString();
        }

        private void Cheked_onlyStationsWithFreeSlots(object sender, RoutedEventArgs e)
        {
            clearListView();
            //stationList.ClearStations();
            Stations=Convert<StationToList>(bL.GetStationToListBy(s => s.numberOfFreeChargeSlots != 0));
            // StationsListView.DataContext = stationList.Stations;

        }

        private void Button_Click_groupByNumberOfFree(object sender, RoutedEventArgs e)
        {
            clearListView();
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("numberOfFreeChargeSlots");
            view.GroupDescriptions.Add(groupDescription);
        }

        private void clearListView()
        {
            //stationList.Stations = stationList.ClearStations();
            //stationList.Stations = stationList.ConvertStationBLToPL(bL.GetStationToLists().ToList());
            Stations = new ObservableCollection<StationToList>();
            Stations = Convert<StationToList>(bL.GetStationToLists());
            //parcelsList.Parcels = parcelsList.ClearParcels();
            //parcelsList.Parcels = parcelsList.ConvertParcelBLToPL(bl.GetParcelToLists().ToList());
            DataContext = Stations;
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
            StationToList stationToList = (sender as ListView).SelectedValue as StationToList;
            BO.Station station = bL.GetStationById(stationToList.ID);
            OpenWindow = new StationWindow(bL, station, currentUser);
            OpenWindow.ChangedParcelDelegate += UpdateInList;
            OpenWindow.Show();

        }

        private void UpdateInList(BO.Station station)
        {
            StationToList stationToList = Stations.First((d) => d.ID == station.Id);
            int index = Stations.IndexOf(stationToList);
            Stations[index] = new StationToList() { ID = station.Id, Name = station.Name, numberOfFreeChargeSlots = station.FreeChargeSlots };


        }

        private void AddStationToLst(BO.Station station)
        {
            Stations.Add(new StationToList() { ID = station.Id, Name = station.Name, numberOfFreeChargeSlots = station.FreeChargeSlots });
        }


        private void adddStation_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow = new StationWindow(bL, currentUser);
            OpenWindow.ChangedParcelDelegate += AddStationToLst;
            OpenWindow.Show();
            //new StationWindow(bL, currentUser).Show();
        }

        private void closeButton_click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
