using IBL.BO;
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
        DroneBL drone;
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
            drone = droneBL;
            InitializeComponent();
            bL = bL1;
            AddDrone.Visibility = Visibility.Hidden;
            Actions.Visibility = Visibility.Visible;

            //hide all buttons
            sendDroneForDelivery.Visibility = Visibility.Hidden;
            colectParcel.Visibility = Visibility.Hidden;
            supllyParcel.Visibility = Visibility.Hidden;
            sendDroneToCharge.Visibility = Visibility.Hidden;
            releaseDroneFromCharging.Visibility = Visibility.Hidden;

            //show relaed buttons
            switch (drone.DroneStatus)
            {
                case DroneStatus.Free:
                    sendDroneForDelivery.Visibility = Visibility.Visible;
                    sendDroneToCharge.Visibility = Visibility.Visible;
                    break;
                case DroneStatus.Maintenance:
                    releaseDroneFromCharging.Visibility = Visibility.Visible;
                    timeCharging.Visibility = Visibility.Visible;
                    timeOfCharging.Visibility = Visibility.Visible;
                    break;
                case DroneStatus.Delivery:
                    // if (bL.FindParcel(drone.ParcelAtTransfor.ID).PickedUp == null)
                    IBL.BO.ParcelBL parcelBL = bL.FindParcelBy(t=> t.Id== drone.Id);
                    if (parcelBL.PickedUp == null) 
                        colectParcel.Visibility = Visibility.Visible;
                    else
                        supllyParcel.Visibility = Visibility.Visible;
                    break;


            }
            ButteryDroneL.Text = $"{droneBL.Battery}";
            idDroneL.Text = $"{droneBL.Id}";
            modelDroneL.Text = $"{droneBL.Model}";
            MaxWeight.Text = $"{droneBL.MaxWeight}";
            DroneStatusDroneL.Text = $"{droneBL.DroneStatus}";
            Location.Text = $"{droneBL.Location}";
            ParcelInDelivery.Text = $"{droneBL.ParcelInDelivery}";
    
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

        private void Button_Click_sendDroneToCharge(object sender, RoutedEventArgs e)
        {
            try
            {
                bL.sendDroneToCharge(drone.Id);
                sendDroneToCharge.Visibility = Visibility.Hidden;
                releaseDroneFromCharging.Visibility = Visibility.Visible;
                timeOfCharging.Visibility = Visibility.Visible;
                timeCharging.Visibility = Visibility.Visible;
                DroneStatusDroneL.Text = $"{drone.DroneStatus}";


            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }

           
        }

        private void Button_Click_releaseDroneFromCharging(object sender, RoutedEventArgs e)
        {
            double time = Convert.ToDouble( timeOfCharging.Text);

            try
            {
                bL.releaseDroneFromCharging(drone.Id, time);
                releaseDroneFromCharging.Visibility = Visibility.Hidden;
                sendDroneForDelivery.Visibility = Visibility.Visible;
                timeCharging.Visibility = Visibility.Hidden;
                timeOfCharging.Visibility = Visibility.Hidden;
                DroneStatusDroneL.Text = $"{drone.DroneStatus}";



            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           




        }

        private void Button_Click_sendDroneForDelivery(object sender, RoutedEventArgs e)
        {
            try
            {
                bL.AssignAParcelToADrone(drone.Id);
                sendDroneForDelivery.Visibility = Visibility.Hidden;
                colectParcel.Visibility = Visibility.Visible;
                DroneStatusDroneL.Text = $"{drone.DroneStatus}";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
     
           
        }

        private void Button_Click_colectParcel(object sender, RoutedEventArgs e)
        {
            try
            {
                bL.collectParcleByDrone(drone.Id);
                colectParcel.Visibility = Visibility.Hidden;
                supllyParcel.Visibility = Visibility.Visible;
                DroneStatusDroneL.Text = $"{drone.DroneStatus}";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
           
        }

        private void Button_Click_supllyParcel(object sender, RoutedEventArgs e)
        {
            try
            {
                bL.supplyParcelByDrone(drone.Id);
                supllyParcel.Visibility = Visibility.Hidden;
                sendDroneForDelivery.Visibility = Visibility.Visible;
                sendDroneToCharge.Visibility = Visibility.Visible;
                DroneStatusDroneL.Text = $"{drone.DroneStatus}";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string newModel = modelDroneL.Text;
            int i = Convert.ToInt32(idDroneL.Text);
            bL.updateDroneModel(i,newModel);
           
        }
    }
}
