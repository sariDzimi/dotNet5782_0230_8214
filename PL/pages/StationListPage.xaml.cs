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

namespace PL
{
    /// <summary>
    /// Interaction logic for StationListPage.xaml
    /// </summary>
    public partial class StationListPage : Page
    {
        CurrentUser currentUser = new CurrentUser();
        private IBL bL;
        CollectionView view;
        List<StationToList> items;
        public StationListPage()
        {
            InitializeComponent();
        }

        public StationListPage(IBL bl, CurrentUser currentUser1 )
        {
            currentUser = currentUser1;
            InitializeComponent();
            bL = bl;
            StationsListView.ItemsSource = bL.GetStationToLists();
            items = bl.GetStationToLists().ToList();
            view = (CollectionView)CollectionViewSource.GetDefaultView(bL.GetStationToLists());
        }

        private void Cheked_onlyStationsWithFreeSlots(object sender, RoutedEventArgs e)
        {
            StationsListView.ItemsSource = bL.GetStationToListBy(s => s.NumberOfFreeChargeSlots != 0);
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
            BO.Station station = bL.GetStationById(stationToList.Id);
            Window.GetWindow(this).Content = new StationPage(bL, station, currentUser, this);
        }

        private void adddStation_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new StationPage(bL, currentUser, this);
        }

        private void closeButton_click(object sender, RoutedEventArgs e)
        {
            new ManegerWindow(bL, currentUser).Show();
            Window.GetWindow(this).Close();
        }
    }
}
