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
            StationsListView.ItemsSource = bL.GetStationToLists();
            items = bl.GetStationToLists().ToList();
            view = (CollectionView)CollectionViewSource.GetDefaultView(bL.GetStationToLists());
        }

        private void Cheked_onlyStationsWithFreeSlots(object sender, RoutedEventArgs e)
        {
            StationsListView.ItemsSource = bL.GetStationToListBy(s => s.numberOfFreeChargeSlots != 0);
        }

        private void Button_Click_groupByNumberOfFree(object sender, RoutedEventArgs e)
        {
            clearListView();
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("numberOfFreeChargeSlots");
            view.GroupDescriptions.Add(groupDescription);
        }

        private void clearListView()
        {
            items = bL.GetStationToLists().ToList();
            StationsListView.ItemsSource = items;
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
            //Hide();
            new StationWindow(bL, station, currentUser).Show();
            //Show();
        }

        private void adddStation_Click(object sender, RoutedEventArgs e)
        {
            //Hide();
            new StationWindow(bL, currentUser).Show();
            //Show();
        }

        private void closeButton_click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
