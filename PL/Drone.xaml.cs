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
        IBL.BO.Drone drone;
        public bool boo = false;
        public Drone()
        {
            WindowStyle = WindowStyle.None;
            drone = new IBL.BO.Drone() { };

            InitializeComponent();
        }
        public Drone(BL.BL bL1)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            timeCharging.Visibility = Visibility.Hidden;
            timeOfCharging.Visibility = Visibility.Hidden;

            bL = bL1;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
            MaxWeight.Visibility = Visibility.Hidden;
            numberOfStationInput.Visibility = Visibility.Visible;
            numberOfStationLabel.Visibility = Visibility.Visible;
            addButton.IsEnabled = true;
        }
        public Drone(BL.BL bL1, IBL.BO.Drone droneBL)
        {
            WindowStyle = WindowStyle.None;

            drone = droneBL;
            InitializeComponent();
            bL = bL1;
            updateBottun.IsEnabled = true;
            idDroneL.IsReadOnly = true;
            WeightSelector.Visibility = Visibility.Hidden;
            numberOfStationInput.Visibility = Visibility.Hidden;

            //show relaed buttons
            switch (drone.DroneStatus)
            {
                case DroneStatus.Free:
                    sendDroneForDelivery.IsEnabled = true;
                    sendDroneToCharge.IsEnabled = true;
                    break;
                case DroneStatus.Maintenance:
                    releaseDroneFromCharging.IsEnabled = true;
                    timeCharging.IsEnabled = true;
                    timeOfCharging.IsEnabled = true;
                    break;
                case DroneStatus.Delivery:

                    IBL.BO.Parcel parcelBL = bL.FindParcelBy(t => t.Id == drone.ParcelInDelivery.Id);

                    if (parcelBL.PickedUp == null)
                    {
                        colectParcel.IsEnabled = true;
                    }
                    else
                    {
                        if (parcelBL.Delivered == null)

                            supllyParcel.IsEnabled = true;
                    }
                    break;
            }

            this.DataContext = drone;
            ParcelInDelivery.Text = drone.ParcelInDelivery.ToString();
            Location.Text = drone.Location.ToString();
        }

        


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var maxWeight = (int)(IBL.BO.WeightCategories)WeightSelector.SelectedItem;
            int id = Convert.ToInt32(idDroneL.Text);
            string model = modelDroneL.Text;
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
                sendDroneToCharge.IsEnabled = false;
                sendDroneForDelivery.IsEnabled = false;
                releaseDroneFromCharging.IsEnabled = true;
                timeOfCharging.IsEnabled = true;
                timeCharging.IsEnabled = true;
                timeOfCharging.Visibility = Visibility.Visible;
                timeCharging.Visibility = Visibility.Visible;
                timeOfCharging.Text = "";
                DroneStatusDroneL.Text = $"{drone.DroneStatus}";
                ButteryDroneL.Text = $"{drone.Battery}";
                MessageBox.Show("The drone was sent for charging successfully");


            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }


        }

        private void Button_Click_releaseDroneFromCharging(object sender, RoutedEventArgs e)
        {
            try
            {

                double time = Convert.ToDouble(timeOfCharging.Text);
                bL.releaseDroneFromCharging(drone.Id, time);
                releaseDroneFromCharging.IsEnabled = false;
                sendDroneForDelivery.IsEnabled = true;
                sendDroneToCharge.IsEnabled = true;
                timeCharging.Visibility = Visibility.Hidden;
                timeOfCharging.Visibility = Visibility.Hidden;
                DroneStatusDroneL.Text = $"{drone.DroneStatus}";
                ButteryDroneL.Text = $"{drone.Battery}";
                MessageBox.Show("Release the drone from charging successfully");
            }
            catch (Exception)
            {
                MessageBox.Show("please enter time of charging");
            }
        }

        private void Button_Click_sendDroneForDelivery(object sender, RoutedEventArgs e)
        {
            try
            {
                bL.AssignAParcelToADrone(drone.Id);
                sendDroneForDelivery.IsEnabled = false;
                colectParcel.IsEnabled = true;
                DroneStatusDroneL.Text = $"{drone.DroneStatus}";
                ParcelInDelivery.Text = Convert.ToString(drone.ParcelInDelivery);
                ButteryDroneL.Text = $"{drone.Battery}";
                MessageBox.Show("Assign a drone to parcel successfully");
                sendDroneToCharge.IsEnabled = false;
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
                colectParcel.IsEnabled = false;
                supllyParcel.IsEnabled = true;
                DroneStatusDroneL.Text = $"{drone.DroneStatus}";
                MessageBox.Show("collect a parcel by drone successfully");
                ButteryDroneL.Text = $"{drone.Battery}";


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
                supllyParcel.IsEnabled = false;
                sendDroneForDelivery.IsEnabled = true;
                sendDroneToCharge.IsEnabled = true;
                DroneStatusDroneL.Text = $"{drone.DroneStatus}";
                MessageBox.Show("suplly a parcel by drone successfully");
                DroneStatusDroneL.Text = $"{drone.DroneStatus}";
                ParcelInDelivery.Text = $"{drone.ParcelInDelivery}";
                ButteryDroneL.Text = $"{drone.Battery}";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string newModel = modelDroneL.Text;
                int i = Convert.ToInt32(idDroneL.Text);
                bL.updateDroneModel(i, newModel);
                MessageBox.Show($"the model drone : {newModel} updated successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new DronesList(bL).Show();
            Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("djfdl");
        }
    }
}

