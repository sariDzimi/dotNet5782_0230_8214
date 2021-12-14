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
        public Drone()
        {
            WindowStyle = WindowStyle.None;

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
           
            //hide all buttons

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
                    if (parcelBL.Delivered != null & parcelBL.PickedUp != null)
                        MessageBox.Show("the parcel was supplied");
                    break;


            }

            ShowDroneDetales(drone);

        }

        public void ShowDroneDetales(IBL.BO.Drone droneBL)
        {

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
            double time = Convert.ToDouble(timeOfCharging.Text);

            try
            {
                bL.releaseDroneFromCharging(drone.Id, time);
                releaseDroneFromCharging.IsEnabled = false;
                sendDroneForDelivery.IsEnabled = true;
                sendDroneToCharge.IsEnabled= true;
                timeCharging.Visibility = Visibility.Hidden;
                timeOfCharging.Visibility = Visibility.Hidden;
                DroneStatusDroneL.Text = $"{drone.DroneStatus}";
                ButteryDroneL.Text = $"{drone.Battery}";
                MessageBox.Show("Release the drone from charging successfully");


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
                sendDroneForDelivery.IsEnabled = false;
                colectParcel.IsEnabled = true;
                DroneStatusDroneL.Text = $"{drone.DroneStatus}";
                ParcelInDelivery.Text = Convert.ToString(drone.ParcelInDelivery);
                ButteryDroneL.Text = $"{drone.Battery}";
                MessageBox.Show("Assign a drone to parcel successfully");
                sendDroneToCharge.IsEnabled= false;
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

    }
}








































//using IBL.BO;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

//namespace PL
//{
//    /// <summary>
//    /// Interaction logic for Drone.xaml
//    /// </summary>
//    public partial class Drone : Window
//    {
//        BL.BL bL;
//        DroneBL drone;
//        public Drone()
//        {
//            WindowStyle = WindowStyle.None;

//            InitializeComponent();
//        }
//        public Drone(BL.BL bL1)
//        {
//            InitializeComponent();
//            WindowStyle = WindowStyle.None;

//            bL = bL1;
//            AddDrone.Visibility = Visibility.Visible;
//            Actions.Visibility = Visibility.Hidden;
//            WeightSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
//        }
//        public Drone(BL.BL bL1, IBL.BO.DroneBL droneBL)
//        {
//            WindowStyle = WindowStyle.None;

//            drone = droneBL;
//            InitializeComponent();
//            bL = bL1;
//            AddDrone.Visibility = Visibility.Hidden;
//            Actions.Visibility = Visibility.Visible;

//            //hide all buttons
//            sendDroneForDelivery.Visibility = Visibility.Hidden;
//            colectParcel.Visibility = Visibility.Hidden;
//            supllyParcel.Visibility = Visibility.Hidden;
//            sendDroneToCharge.Visibility = Visibility.Hidden;
//            releaseDroneFromCharging.Visibility = Visibility.Hidden;

//            //show relaed buttons
//            switch (drone.DroneStatus)
//            {
//                case DroneStatus.Free:
//                    sendDroneForDelivery.Visibility = Visibility.Visible;
//                    sendDroneToCharge.Visibility = Visibility.Visible;
//                    break;
//                case DroneStatus.Maintenance:
//                    releaseDroneFromCharging.Visibility = Visibility.Visible;
//                    timeCharging.Visibility = Visibility.Visible;
//                    timeOfCharging.Visibility = Visibility.Visible;
//                    break;
//                case DroneStatus.Delivery:
//                    // if (bL.FindParcel(drone.ParcelAtTransfor.ID).PickedUp == null)
//                    IBL.BO.ParcelBL parcelBL = bL.FindParcelBy(t=> t.Id== drone.ParcelInDelivery.Id);

//                    if (parcelBL.PickedUp == null)
//                    {
//                        colectParcel.Visibility = Visibility.Visible;
//                    }
//                    else
//                    {
//                        if (parcelBL.Delivered == null)

//                            supllyParcel.Visibility = Visibility.Visible;
//                    }
//                    if(parcelBL.Delivered!=null & parcelBL.PickedUp!= null)
//                        MessageBox.Show("the parcel was supplied");
//                    break;


//            }
//            ButteryDroneL.Text = $"{droneBL.Battery}";
//            idDroneL.Text = $"{droneBL.Id}";
//            modelDroneL.Text = $"{droneBL.Model}";
//            MaxWeight.Text = $"{droneBL.MaxWeight}";
//            DroneStatusDroneL.Text = $"{droneBL.DroneStatus}";
//            Location.Text = $"{droneBL.Location}";
//            ParcelInDelivery.Text = $"{droneBL.ParcelInDelivery}";

//        }


//        private void Button_Click(object sender, RoutedEventArgs e)
//        {
//            var maxWeight = (int)(IBL.BO.WeightCategories)WeightSelector.SelectedItem;
//            int id = Convert.ToInt32(IdInput.Text);
//            string model = ModelInput.Text;
//            int numOfStationForCharching = Convert.ToInt32(numberOfStationInput.Text);
//            try
//            {
//                bL.addDroneToBL(id, maxWeight, model, numOfStationForCharching);
//                MessageBox.Show("the drone was added succesfuly!!!");
//                new DronesList(bL).Show();
//                Close();
//            }
//            catch (Exception)
//            {
//                MessageBox.Show("couldn't add the drone");

//                IdInput.Text = "";
//                ModelInput.Text = "";
//                numberOfStationInput.Text = "";
//                WeightSelector.SelectedItem = null;
//            }

//        }

//        private void ButtonClick_Close(object sender, RoutedEventArgs e)
//        {
//            new DronesList(bL).Show();
//            Close();
//        }


//        private void numberOfStationInput_TextChanged(object sender, TextChangedEventArgs e)
//        {

//        }

//        private void Button_Click_sendDroneToCharge(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                bL.sendDroneToCharge(drone.Id);
//                sendDroneToCharge.Visibility = Visibility.Hidden;
//                sendDroneForDelivery.Visibility = Visibility.Hidden;
//                releaseDroneFromCharging.Visibility = Visibility.Visible;
//                timeOfCharging.Visibility = Visibility.Visible;
//                timeCharging.Visibility = Visibility.Visible;
//                DroneStatusDroneL.Text = $"{drone.DroneStatus}";
//                ButteryDroneL.Text = $"{drone.Battery}";
//                MessageBox.Show("The drone was sent for charging successfully");


//            }

//            catch (Exception ex) { MessageBox.Show(ex.Message); }


//        }

//        private void Button_Click_releaseDroneFromCharging(object sender, RoutedEventArgs e)
//        {
//            double time = Convert.ToDouble( timeOfCharging.Text);

//            try
//            {
//                bL.releaseDroneFromCharging(drone.Id, time);
//                releaseDroneFromCharging.Visibility = Visibility.Hidden;
//                sendDroneForDelivery.Visibility = Visibility.Visible;
//                sendDroneToCharge.Visibility = Visibility.Visible;
//                timeCharging.Visibility = Visibility.Hidden;
//                timeOfCharging.Visibility = Visibility.Hidden;
//                DroneStatusDroneL.Text = $"{drone.DroneStatus}";
//                ButteryDroneL.Text = $"{drone.Battery}";
//                MessageBox.Show("Release the drone from charging successfully");


//            }


//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }





//        }

//        private void Button_Click_sendDroneForDelivery(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                bL.AssignAParcelToADrone(drone.Id);
//                sendDroneForDelivery.Visibility = Visibility.Hidden;
//                colectParcel.Visibility = Visibility.Visible;
//                DroneStatusDroneL.Text = $"{drone.DroneStatus}";
//                ParcelInDelivery.Text = Convert.ToString( drone.ParcelInDelivery);
//                ButteryDroneL.Text = $"{drone.Battery}";
//                MessageBox.Show("Assign a drone to parcel successfully");

//            }



//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }


//        }

//        private void Button_Click_colectParcel(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                bL.collectParcleByDrone(drone.Id);
//                colectParcel.Visibility = Visibility.Hidden;
//                supllyParcel.Visibility = Visibility.Visible;
//                DroneStatusDroneL.Text = $"{drone.DroneStatus}";
//                MessageBox.Show("collect a parcel by drone successfully");
//                ButteryDroneL.Text = $"{drone.Battery}";


//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }


//        }

//        private void Button_Click_supllyParcel(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                bL.supplyParcelByDrone(drone.Id);
//                supllyParcel.Visibility = Visibility.Hidden;
//                sendDroneForDelivery.Visibility = Visibility.Visible;
//                sendDroneToCharge.Visibility = Visibility.Visible;
//                DroneStatusDroneL.Text = $"{drone.DroneStatus}";
//                MessageBox.Show("suplly a parcel by drone successfully");
//                DroneStatusDroneL.Text = $"{drone.DroneStatus}";
//                ParcelInDelivery.Text = $"{drone.ParcelInDelivery}";
//                ButteryDroneL.Text = $"{drone.Battery}";

//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }


//        }

//        private void Button_Click_1(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                string newModel = modelDroneL.Text;
//                int i = Convert.ToInt32(idDroneL.Text);
//                bL.updateDroneModel(i, newModel);
//                MessageBox.Show($"the model drone : {newModel} updated successfully");
//            }
//            catch(Exception ex)
//            {
//                MessageBox.Show($"{ex}");
//            }


//        }

//        private void Button_Click_2(object sender, RoutedEventArgs e)
//        {
//            new DronesList(bL).Show();
//            Close();
//        }
//    }
//}
