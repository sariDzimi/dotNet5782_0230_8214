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
using System.ComponentModel;
using BlApi;
using PO;
using System.Collections.ObjectModel;

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
        Drone_p drone_P;
        DroneList DroneP = new DroneList();
        public bool boo = false;
        BackgroundWorker worker = new BackgroundWorker();
        public Drone()
        {
            //WindowStyle = WindowStyle.None;
            drone = new BO.Drone() { };

            InitializeComponent();
        }
        public Drone(IBL bL1, CurrentUser currentUser1, ObservableCollection<Drone_p> drone_Ps)
        {
            currentUser = currentUser1;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            DroneStatusDroneL.Visibility = Visibility.Hidden;
            DroneP.Drone_Ps = drone_Ps;
            bL = bL1;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            addButton.IsEnabled = true;
            CurrentUser.Text = currentUser.Type;

        }
      
        public Drone(IBL bL1, BO.Drone droneBL, CurrentUser currentUser1, ObservableCollection<Drone_p> drone_Ps)
        {
            WindowStyle = WindowStyle.None;
            currentUser = currentUser1;
            drone = droneBL;
            DroneP.Drone_Ps = drone_Ps;
            InitializeComponent();
            ParcelInDelivery parcelInDelivery = new ParcelInDelivery();
            drone_P = new Drone_p() { Battery = drone.Battery, ID = drone.Id, DroneStatus = drone.DroneStatus, Location = drone.Location, MaxWeight = drone.MaxWeight, Model = drone.Model, ParcelInDelivery = drone.ParcelInDelivery == null  ? new BO.ParcelInDelivery() : drone.ParcelInDelivery  }; 
            bL = bL1;
            DataContext = drone_P;
            addButton.Visibility = Visibility.Hidden;
            updateBottun.IsEnabled = true;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
           
            switch (drone.DroneStatus)
            {
                case DroneStatus.Free:
                    sendDroneForDelivery.IsEnabled = true;
                    //OpaenDrone.Visibility = Visibility.Hidden;
                    break;
                case DroneStatus.Maintenance:
                    releaseDroneFromCharging.IsEnabled = true;
                   // OpaenDrone.Visibility = Visibility.Hidden;
                    break;
                case DroneStatus.Delivery:

                    Parcel parcelBL = bL.GetParcelById(drone_P.ParcelInDelivery.Id);

                    if (parcelBL.PickedUp == null)
                    {
                        colectParcel.IsEnabled = true;
                        OpaenDrone.Visibility = Visibility.Visible;                    }
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

        //TODO.....
        public Drone(IBL bL1, BO.Drone droneBL, CurrentUser currentUser1)
        {
            WindowStyle = WindowStyle.None;
            currentUser = currentUser1;
            drone = droneBL;
            InitializeComponent();
            ParcelInDelivery parcelInDelivery = new ParcelInDelivery();
            drone_P = new Drone_p() { Battery = drone.Battery, ID = drone.Id, DroneStatus = drone.DroneStatus, Location = drone.Location, MaxWeight = drone.MaxWeight, Model = drone.Model, ParcelInDelivery = drone.ParcelInDelivery == null ? new BO.ParcelInDelivery() : drone.ParcelInDelivery };
            bL = bL1;
            DataContext = drone_P;
            addButton.Visibility = Visibility.Hidden;
            updateBottun.IsEnabled = true;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));

            switch (drone.DroneStatus)
            {
                case DroneStatus.Free:
                    sendDroneForDelivery.IsEnabled = true;
                    //OpaenDrone.Visibility = Visibility.Hidden;
                    break;
                case DroneStatus.Maintenance:
                    releaseDroneFromCharging.IsEnabled = true;
                    // OpaenDrone.Visibility = Visibility.Hidden;
                    break;
                case DroneStatus.Delivery:

                    Parcel parcelBL = bL.GetParcelById(drone_P.ParcelInDelivery.Id);

                    if (parcelBL.PickedUp == null)
                    {
                        colectParcel.IsEnabled = true;
                        OpaenDrone.Visibility = Visibility.Visible;
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
                bL.addDroneToBL(getId(), getMaxWeight(), getModel(), getNumberOfStation());
                BO.Drone drone = bL.GetDroneById(getId());
                DroneP.AddDrone(new DroneToList() { Id = getId(), Model = getModel(), NumberOfSendedParcel = getNumberOfStation(), Battery = drone.Battery , Location  = drone.Location, DroneStatus = drone.DroneStatus });
                MessageBox.Show("the drone was added succesfuly!!!");
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
            Close();
        }


        private void numberOfStationInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_sendDroneToCharge(object sender, RoutedEventArgs e)
        {
            try
            {
                bL.sendDroneToCharge(drone_P.ID);
                sendDroneForDelivery.IsEnabled = false;
                releaseDroneFromCharging.IsEnabled = true;
                timeOfCharging.Text = "";
                drone_P.Update(drone);
                DroneP.UpdateList(drone_P);
                MessageBox.Show("The drone was sent for charging successfully");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Button_Click_releaseDroneFromCharging(object sender, RoutedEventArgs e)
        {
            try
            {
                double time = getTime();
                bL.releaseDroneFromCharging(drone_P.ID, time);
                releaseDroneFromCharging.IsEnabled = false;
                sendDroneForDelivery.IsEnabled = true;
                timeOfCharging.Text = "";
                drone_P.Update(drone);
                DroneP.UpdateList(drone_P);
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
                bL.AssignAParcelToADrone(drone_P.ID);
                sendDroneForDelivery.IsEnabled = false;
                colectParcel.IsEnabled = true;
                drone_P.Update(drone);
                DroneP.UpdateList(drone_P);
                MessageBox.Show("Assign a drone to parcel successfully");
                OpaenDrone.Visibility = Visibility.Visible;
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
                bL.collectParcleByDrone(drone_P.ID);
                colectParcel.IsEnabled = false;
                supllyParcel.IsEnabled = true;
                MessageBox.Show("collect a parcel by drone successfully");
                drone_P.Update(drone);
                DroneP.UpdateList(drone_P);



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
                bL.supplyParcelByDrone(drone_P.ID);
                supllyParcel.IsEnabled = false;
                sendDroneForDelivery.IsEnabled = true;
                MessageBox.Show("suplly a parcel by drone successfully");
                drone_P.Update(drone);
               

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
           
            Parcel parcel = bL.GetParcelById(drone.ParcelInDelivery.Id);
            //Hide();
            new ParcelWindow(bL, parcel, currentUser).Show();
            //Show();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void simulation_Click(object sender, RoutedEventArgs e)
        {
            worker.DoWork += (object? sender, DoWorkEventArgs e) =>
            {
                bL.StartSimulation(
                    drone,
                    (d, i) =>
                    {
                        worker.ReportProgress(i);
                    },
                    () => worker.CancellationPending
                    );

            };

            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += (object? sender, ProgressChangedEventArgs e) =>
            {
                drone_P.Update(drone);
                DroneP.UpdateList(drone_P);
            };
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
        }
    }
}

