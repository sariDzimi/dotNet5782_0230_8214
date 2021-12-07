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

namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        BL.BL bL;

        public Drone()
        {
            InitializeComponent();
        }
        public Drone(BL.BL bL1)
        {
            InitializeComponent();
            bL = bL1;
            AddDrone.Visibility = Visibility.Visible;
            Actions.Visibility = Visibility.Hidden;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
        }
        public Drone(BL.BL bL1, IBL.BO.DroneBL droneBL)
        {
            InitializeComponent();
            bL = bL1;
            
            AddDrone.Visibility = Visibility.Hidden;
            Actions.Visibility = Visibility.Visible;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var maxWeight = (int)(IBL.BO.WeightCategories)WeightSelector.SelectedItem;
            int id = Convert.ToInt32(IdInput.Text);
            string model = ModelInput.Text;
            int numOfStationForCharching = Convert.ToInt32(numberOfStationInput.Text);
            try
            {
                bL.addDroneToBL(id, maxWeight, model, numOfStationForCharching);
                MessageBox.Show("the drone was added succesfuly!!!");
                new DronesList(bL).Show();
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("couldn't add the drone");

                IdInput.Text = "";
                ModelInput.Text = "";
                numberOfStationInput.Text = "";
                WeightSelector.SelectedItem = null;
            }

        }

        private void ButtonClick_Close(object sender, RoutedEventArgs e)
        {
            new DronesList(bL).Show();
            Close();
        }

        private void numberOfStationInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_sendDroneForCharging(object sender, RoutedEventArgs e)
        {

        }
    }
}
