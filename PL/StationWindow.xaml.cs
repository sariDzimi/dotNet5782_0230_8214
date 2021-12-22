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
        public StationWindow(IBL blArg, Station station)
        {
            InitializeComponent();
            bl = blArg;
            DroneChargingListView.ItemsSource = station.droneAtChargings;
        }

    }
}
