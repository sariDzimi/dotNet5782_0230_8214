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
        IBL bl;
        Station station;
        Station_p station_P;

        public StationWindow()
        {
            InitializeComponent();
        }

        public StationWindow(IBL blArg, Station stationArg) 
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blArg;
            station = stationArg;
            station_P = new Station_p() { ID = station.Id, Name = station.Name, Longitude = station.Location.Longitude,Latitude= station.Location.Latitude, FreeChargeSlots = station.FreeChargeSlots };
            DroneChargingListView.ItemsSource = station.DroneAtChargings;
            updateStationLabel.Visibility = Visibility.Visible;
            DataContext = station_P;
        }

        public StationWindow(IBL blArg)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blArg;
            addStationLabel.Visibility = Visibility.Visible;
        }

        private void DroneChargingListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DroneChargingListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.DroneAtCharging droneAtCharging = (sender as ListView).SelectedValue as DroneAtCharging;
            new Drone(bl, bl.GetDroneById(droneAtCharging.Id)).Show();
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
                if (station_P.ListChangedDelegate != null)
                {
                    station_P.ListChangedDelegate(bl.GetStationById(getId()));
                }
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
