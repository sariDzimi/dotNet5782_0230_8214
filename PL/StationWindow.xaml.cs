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
        BO.Station station;
        PO.Station station_display;

        public StationWindow()
        {
            InitializeComponent();
        }

        public StationWindow(IBL bl, BO.Station station) 
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            this.bl = bl;
            this.station = station;
            station_display = new PO.Station() { ID = this.station.Id, Name = this.station.Name, Longitude = this.station.Location.Longitude, Latitude = this.station.Location.Latitude, FreeChargeSlots = this.station.FreeChargeSlots };
            droneChargingListView.ItemsSource = this.station.DroneAtChargings;
            updateStationLabel.Visibility = Visibility.Visible;
            DataContext = station_display;
        }

        public StationWindow(IBL bl)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            this.bl = bl;
            addStationLabel.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// opens drone window of the choosen droneCharge
        /// </summary>
        /// <param name="sender">droneCharge</param>
        /// <param name="e"></param>
        private void droneChargingListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.DroneAtCharging droneAtCharging = (sender as ListView).SelectedValue as DroneAtCharging;
            new Drone(bl, bl.GetDroneById(droneAtCharging.Id)).Show();
        }

        /// <summary>
        /// updates station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateStation(new BO.Station()
                {
                    Id = getId(),
                    Name = getName(),
                    DroneAtChargings = null,
                    Location = getLocation(),
                    FreeChargeSlots = getChargeSlots()
                });
                BO.Station station = bl.GetStationById(getId());
                updateStationList(station);
            }

            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// updates station in station list window
        /// </summary>
        /// <param name="station">updated station</param>
        public void updateStationList(BO.Station station)
        {
            if (ChangedParcelDelegate != null)
            {
                ChangedParcelDelegate(station);
            }
        }

        /// <summary>
        /// adds stations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddStation(new BO.Station()
                {
                    Id = getId(),
                    Name = getName(),
                    DroneAtChargings = null,
                    Location = getLocation(),
                    FreeChargeSlots = getChargeSlots()
                });
                if (station_display.ListChangedDelegate != null)
                {
                    station_display.ListChangedDelegate(bl.GetStationById(getId()));
                }
                MessageBox.Show("the Station added!!");
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// closes window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// gets input of location
        /// </summary>
        /// <returns>location</returns>
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

        /// <summary>
        /// gets input of id
        /// </summary>
        /// <returns>id of station</returns>
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

        /// <summary>
        /// gets input on name
        /// </summary>
        /// <returns>name of station</returns>
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

        /// <summary>
        /// gets input: number of charge slots
        /// </summary>
        /// <returns>number of charge slots of station</returns>
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
