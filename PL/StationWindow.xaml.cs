using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BlApi;
using BO;
using PO;
namespace PL
{
    /// <summary>
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        public Action<BO.Station> ChangedParcelDelegate;

        CurrentUser currentUser = new CurrentUser();
        IBL bl;
        Station station;
        Station_p station_P;
        //StationList StationsList = new StationList();

        public StationWindow()
        {
            InitializeComponent();
        }
        public StationWindow(IBL blArg, Station stationArg, CurrentUser currentUser1) //StationList Stations)
        {
            currentUser = currentUser1;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blArg;
            //this.StationsList = Stations;
            station = stationArg;
            station_P = new Station_p() { ID = station.Id, Name = station.Name, Longitude = station.Location.Longitude,Latitude= station.Location.Latitude, FreeChargeSlots = station.FreeChargeSlots };
            DroneChargingListView.ItemsSource = station.DroneAtChargings;
            updateStationLabel.Visibility = Visibility.Visible;
            DataContext = station_P;
            CurrentUser.Text = currentUser.Type.ToString();
        }

        public StationWindow(IBL blArg, CurrentUser currentUser1)
        {
            currentUser = currentUser1;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blArg;
            //StationsList = Stations;
            addStationLabel.Visibility = Visibility.Visible;
            CurrentUser.Text = currentUser.Type.ToString();
        }

        private void DroneChargingListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DroneChargingListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.DroneAtCharging droneAtCharging = (sender as ListView).SelectedValue as DroneAtCharging;

            //Hide();
            new Drone(bl, bl.GetDroneById(droneAtCharging.Id), currentUser).Show();
            //Show();

        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateStation(new Station()
                {
                    Id = getId(),
                    Name = getName(),
                    DroneAtChargings = null,
                    Location = getLocation(),
                    FreeChargeSlots = getChargeSlots()
                });
                Station station = bl.GetStationById(getId());
                //StationsList.UpdateListStations(new Station_p() { ID = getId(), FreeChargeSlots = getChargeSlots(), Name = getName(), Latitude  = getLocation().Latitude, Longitude  = getLocation().Longitude });
            }

            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateStationList(BO.Station station)
        {
            if (ChangedParcelDelegate != null)
            {
                ChangedParcelDelegate(station);
            }
        }
        //

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddStation(new Station()
                {
                    Id = getId(),
                    Name = getName(),
                    DroneAtChargings = null,
                    Location = getLocation(),
                    FreeChargeSlots = getChargeSlots()
                });
                //Station station = bl.GetStationById(getId());
                if (station_P.ListChangedDelegate != null)
                {
                    station_P.ListChangedDelegate(bl.GetStationById(getId()));
                }
                //StationsList.AddStation(new StationToList() { ID = getId(), numberOfFreeChargeSlots = getChargeSlots(), Name = getName()});
                MessageBox.Show("the Station added!!");
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private Location getLocation()
        {
            try
            {
                double longitude = Convert.ToDouble(longitudeTextBox.Text);
                double latitude = Convert.ToDouble(laditudeTextBox.Text);
                return new Location() { Longitude = longitude, Latitude = latitude};
            }
            catch
            {
                throw new NotValidInput("Location");
            }
        }



        private int getId()
        {
            try
            {
                return Convert.ToInt32(idTextBox.Text);
            }
            catch
            {
                throw new NotValidInput("longitude");
            }
        }

        private int getName()
        {
            try
            {
                return Convert.ToInt32(nameTextBox.Text);
            }
            catch
            {
                throw new NotValidInput("name");
            }
        }

        private int getChargeSlots()
        {
            try
            {
                return Convert.ToInt32(ChargeStolsTextBox.Text);
            }
            catch
            {
                throw new NotValidInput("charge slots");
            }
        }
    }
}
