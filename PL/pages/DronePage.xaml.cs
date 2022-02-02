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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using BO;
using PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DronePage.xaml
    /// </summary>
    public partial class DronePage : Page
    {
        public DronePage()
        {
            InitializeComponent();
        }
        CurrentUser currentUser = new CurrentUser();

        IBL bL;
        BO.Drone drone;
        Drone_p drone_P;
        public bool boo = false;
        Page previeusPage;


        public DronePage(IBL bL1, CurrentUser currentUser1,Page previeusPageArg)
        {
            currentUser = currentUser1;
            previeusPage = previeusPageArg;
            InitializeComponent();
           /// WindowStyle = WindowStyle.None;
            DroneStatusDroneL.Visibility = Visibility.Hidden;

            bL = bL1;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            addButton.IsEnabled = true;

        }
        public DronePage(IBL bL1, BO.Drone droneBL, CurrentUser currentUser1, Page previeusPageArg)
        {
            //WindowStyle = WindowStyle.None;
            previeusPage = previeusPageArg;
            currentUser = currentUser1;
            drone = droneBL;
            InitializeComponent();
            drone_P = new Drone_p() { Battery = drone.Battery, ID = drone.Id, DroneStatus = drone.DroneStatus, Location = drone.Location, MaxWeight = drone.MaxWeight, Model = drone.Model, /*ParcelInDelivery = drone.ParcelInDelivery = drone.ParcelInDelivery*/ };
            bL = bL1;
            DataContext = drone_P;
            addButton.Visibility = Visibility.Hidden;
            updateBottun.IsEnabled = true;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));

            switch (drone.DroneStatus)
            {
                case DroneStatus.Free:
                    sendDroneForDelivery.IsEnabled = true;
                    break;
                case DroneStatus.Maintenance:
                    releaseDroneFromCharging.IsEnabled = true;
                    break;
                case DroneStatus.Delivery:

                    List<Parcel> parcels = bL.GetParcels().ToList();

                    Parcel parcelBL = bL.GetParcelById(drone.ParcelInDelivery.Id);

                    if (parcelBL.PickedUp == null)
                    {
                        colectParcel.IsEnabled = true;
                    }
                    else
                    {
                        //if (parcelBL.Delivered == null)

                    }
                    break;
            }


            if (drone.ParcelInDelivery != null)
                ParcelInDelivery.Text = drone.ParcelInDelivery.ToString();
            WeightSelector.IsEnabled = false;


        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bL.AddDrone(getId(), getMaxWeight(), getModel(), getNumberOfStation());
                MessageBox.Show("the drone was added succesfuly!!!");
                Window.GetWindow(this).Content = new DroneListPage(bL, currentUser);
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

/*        private void ButtonClick_Close(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = previeusPage;
        }*/


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
                drone_P.DroneStatus = drone.DroneStatus;
                drone_P.Battery = drone.Battery;
                drone_P.ParcelInDelivery = drone.ParcelInDelivery;
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
                drone_P.DroneStatus = drone.DroneStatus;
                drone_P.Battery = drone.Battery;
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
                drone_P.DroneStatus = drone.DroneStatus;
                drone_P.Battery = drone.Battery;
                drone_P.ParcelInDelivery = drone.ParcelInDelivery;
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
                MessageBox.Show("collect a parcel by drone successfully");
                drone_P.DroneStatus = drone.DroneStatus;
                drone_P.Battery = drone.Battery;


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
                MessageBox.Show("suplly a parcel by drone successfully");
                drone_P.DroneStatus = drone.DroneStatus;
                drone_P.Battery = drone.Battery;
                drone_P.ParcelInDelivery = drone.ParcelInDelivery;

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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("hello");
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

        private WeightCategories getMaxWeight()
        {
            if (WeightSelector.SelectedItem == null)
                throw new NotValidInput("Max Weight");
            try
            {

                return (WeightCategories)WeightSelector.SelectedItem;
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

            Parcel parcel = bL.GetParcelById(drone.ParcelInDelivery.Id);
            Window.GetWindow(this).Content = new ParcelPage(bL, parcel, currentUser, this);  //new ParcelWindow(bL, parcel, currentUser).ShowDialog();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = previeusPage;
        }
    }
}
