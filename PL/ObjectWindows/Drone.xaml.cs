using BO;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        CurrentUser currentUser = new CurrentUser();

        IBL bL;
        BO.Drone drone;
        public bool boo = false;
        public Drone()
        {
            //WindowStyle = WindowStyle.None;
            drone = new BO.Drone() { };

            InitializeComponent();
        }
        public Drone(IBL bL1, CurrentUser currentUser1)
        {
            currentUser = currentUser1;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            DroneStatusDroneL.Visibility = Visibility.Hidden;

            bL = bL1;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            MaxWeight.Visibility = Visibility.Hidden;
            addButton.IsEnabled = true;
            
        }
        public Drone(IBL bL1, BO.Drone droneBL, CurrentUser currentUser1)
        {
            WindowStyle = WindowStyle.None;
            currentUser = currentUser1;
            drone = droneBL;
            InitializeComponent();
            bL = bL1;
            addButton.Visibility = Visibility.Hidden;
            updateBottun.IsEnabled = true;
           
            DroneStatusDroneL.Visibility = Visibility.Visible;
            //show relaed buttons
            switch (drone.DroneStatus)
            {
                case DroneStatus.Free:
                    sendDroneForDelivery.IsEnabled = true;
                    break;
                case DroneStatus.Maintenance:
                    releaseDroneFromCharging.IsEnabled = true;
                    break;
                case DroneStatus.Delivery:

                    Parcel parcelBL = bL.FindParcelBy(t => t.Id == drone.ParcelInDelivery.Id);

                    if (parcelBL.PickedUp == null)
                    {
                        colectParcel.IsEnabled = true;
                    }
                    else
                    {
                        //if (parcelBL.Delivered == null)

                    } 
                    
                    }
                    break;
              

            this.DataContext = drone;
            if (drone.ParcelInDelivery != null)
                ParcelInDelivery.Text = drone.ParcelInDelivery.ToString();
            Location.Text = drone.Location.ToString();
            MaxWeight.Visibility = Visibility.Visible;
            MaxWeight.Text = drone.MaxWeight.ToString();
            WeightSelector.Visibility = Visibility.Hidden;

        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bL.addDroneToBL(getId(), getMaxWeight(), getModel(), getNumberOfStation());
                MessageBox.Show("the drone was added succesfuly!!!");
                new DronesList(bL, currentUser).Show();
                Close();
            }
            catch (NotValidInput ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (IdAlreadyExist)
            {
                MessageBox.Show("id already exist");

            }
            catch (NotFound)
            {
                MessageBox.Show("station number not found");
            }

        }

        private void ButtonClick_Close(object sender, RoutedEventArgs e)
        {
            new DronesList(bL, currentUser).Show();
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
                sendDroneForDelivery.IsEnabled = false;
                releaseDroneFromCharging.IsEnabled = true;
     
                timeOfCharging.Text = "";
                DroneStatusDroneL.Text = $"{drone.DroneStatus}";
                ButteryDroneL.Text = $"{drone.Battery}%";
                MessageBox.Show("The drone was sent for charging successfully");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Button_Click_releaseDroneFromCharging(object sender, RoutedEventArgs e)
        {
            try
            {
                double time = getTime();
                bL.releaseDroneFromCharging(drone.Id, time);
                releaseDroneFromCharging.IsEnabled = false;
                sendDroneForDelivery.IsEnabled = true;
                timeOfCharging.Text = "";
                DroneStatusDroneL.Text = $"{drone.DroneStatus}";
                ButteryDroneL.Text = $"{drone.Battery}%";
                MessageBox.Show("Release the drone from charging successfully");
            }
            catch (OutOfRange)
            {
                MessageBox.Show("Time entered is too long");
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
                ButteryDroneL.Text = $"{drone.Battery}%";
                MessageBox.Show("Assign a drone to parcel successfully");
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
                ButteryDroneL.Text = $"{drone.Battery}%";


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
                //sendDroneToCharge.IsEnabled = true;
                DroneStatusDroneL.Text = $"{drone.DroneStatus}";
                MessageBox.Show("suplly a parcel by drone successfully");
                DroneStatusDroneL.Text = $"{drone.DroneStatus}";
                ParcelInDelivery.Text = $"{drone.ParcelInDelivery}";
                ButteryDroneL.Text = $"{drone.Battery}%";

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
                bL.updateDroneModel(getId(), getModel());
                MessageBox.Show($"the model drone updated successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new DronesList(bL, currentUser).Show();
            Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("hello");
        }

        private int getId()
        {
            try
            {
                return Convert.ToInt32(idDroneL.Text);
            }
            catch (Exception)
            {
                throw new NotValidInput("id");
            }
        }

        private int getMaxWeight()
        {
            if (WeightSelector.SelectedItem == null)
                throw new NotValidInput("Max Weight");
            try
            {

                return (int)(WeightCategories)WeightSelector.SelectedItem;
            }
            catch (Exception)
            {
                throw new NotValidInput("Max Weight");
            }
        }

        private int getNumberOfStation()
        {
            try
            {
                return Convert.ToInt32(numberOfStationInput.Text);
            }
            catch (Exception)
            {
                throw new NotValidInput("station number");
            }
        }

        private double getTime()
        {
            try
            {
                return Convert.ToDouble(timeOfCharging.Text);
            }
            catch (Exception)
            {
                throw new NotValidInput("time");
            }
        }

        private string getModel()
        {
            return modelDroneL.Text;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Parcel parcel = new Parcel();
            parcel = bL.FindParcel(drone.ParcelInDelivery.Id);
            new ParcelWindow(bL, parcel, currentUser).Show();
        }
    }
}

