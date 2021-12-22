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
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        IBL bl;
        Station station;
        public StationWindow()
        {
            InitializeComponent();
        }
        public StationWindow(IBL blArg, Station stationArg)
        {
            InitializeComponent();
            bl = blArg;
            station = stationArg;
            DroneChargingListView.ItemsSource = station.droneAtChargings;
            updateStationLabel.Visibility = Visibility.Visible;
            DataContext = station;
            Location l = station.Location;
        }

        public StationWindow(IBL blArg)
        {
            InitializeComponent();
            bl = blArg;
            addStationLabel.Visibility = Visibility.Visible;
        }
    }
}
