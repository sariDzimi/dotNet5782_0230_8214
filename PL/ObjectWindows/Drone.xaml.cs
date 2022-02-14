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
using System.Windows.Threading;

namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        public Action<BO.Drone> ChangedDroneDelegate;
        IBL bL;
        BO.Drone drone;
        PO.Drone drone_display = new PO.Drone();
        BackgroundWorker worker;

        public Drone()
        {
            InitializeComponent();
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }

        public Drone(IBL bL) : this()
        {
            this.bL = bL;
            DroneStatusDroneL.Visibility = Visibility.Hidden;
            addButton.IsEnabled = true;
            drone_display.ListChangedDelegate += new Action<BO.Drone>(updateDroneListWindow);

        }

        public Drone(IBL bL1, BO.Drone droneBL) : this()
        {
           
            drone = droneBL;
            ParcelInDelivery parcelInDelivery = new ParcelInDelivery();
            drone_display = new PO.Drone()
            {
                Battery = drone.Battery,
                Id = drone.Id,
                DroneStatus = drone.DroneStatus,
                Location = drone.Location,
                MaxWeight = drone.MaxWeight,
                Model = drone.Model,
                ParcelInDelivery = drone.ParcelInDelivery
            };

            bL = bL1;
            DataContext = drone_display;
            addButton.Visibility = Visibility.Hidden;
            updateBottun.IsEnabled = true;
            showButtonsAccordingToDroneStatus();
            if (drone.ParcelInDelivery != null)
                ParcelInDelivery.Text = drone.ParcelInDelivery.ToString();
            WeightSelector.IsEnabled = false;
            drone_display.ListChangedDelegate += new Action<BO.Drone>(updateDroneListWindow);

        }

        void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToLongTimeString();
        }
        /// <summary>
        /// updates drone in the drone list window
        /// </summary>
        /// <param name="drone"></param>
        public void updateDroneListWindow(BO.Drone drone)
        {
            if (ChangedDroneDelegate != null)
            {
                ChangedDroneDelegate(drone);
            }
        }

        /// <summary>
        /// shows only action buttons that are related to the drone status
        /// </summary>
        private void showButtonsAccordingToDroneStatus()
        {
            sendDroneForDelivery.IsEnabled = false;
            releaseDroneFromCharging.IsEnabled = false;
            colectParcel.IsEnabled = false;
            supllyParcel.IsEnabled = false;

            switch (drone.DroneStatus)
            {
                case DroneStatus.Free:
                    sendDroneForDelivery.IsEnabled = true;
                    break;
                case DroneStatus.Maintenance:
                    releaseDroneFromCharging.IsEnabled = true;
                    break;
                case DroneStatus.Delivery:
                    if (drone.ParcelInDelivery.IsWating)
                    {
                        colectParcel.IsEnabled = true;
                    }
                    else
                    {
                        supllyParcel.IsEnabled = true;
                    }
                    OpaenDrone.Visibility = Visibility.Visible;
                    break;
            }
        }

        /// <summary>
        /// actives the drones with the action
        /// </summary>
        /// <param name="action">action</param>
        /// <param name="id">id of drone</param>
        /// <param name="message">message if the action is done succesfully</param>
        private void activeDrone(Action<int> action, int id, string message)
        {
            try
            {
                action(id);
                showButtonsAccordingToDroneStatus();
                drone_display.updateDisplayObject(drone);
                MessageBox.Show(message);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        /// <summary>
        /// adds drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bL.AddDrone(getId(), getMaxWeight(), getModel(), getNumberOfStation());
                BO.Drone drone = bL.GetDroneById(getId());
                if (drone_display.ListChangedDelegate != null)
                {
                    drone_display.ListChangedDelegate(drone);
                }
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

        /// <summary>
        /// sends drone for charging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendDroneToCharge_Click(object sender, RoutedEventArgs e)
        {
            activeDrone(bL.SendDroneToCharge, drone.Id, "The drone was sent for charging successfully");
        }

        /// <summary>
        /// releases drone from charging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void releaseDroneFromCharging_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double time = getTime();
                bL.ReleaseDroneFromCharging(drone_display.Id, time);
                releaseDroneFromCharging.IsEnabled = false;
                sendDroneForDelivery.IsEnabled = true;
                timeOfCharging.Text = "";
                drone_display.updateDisplayObject(drone);
                MessageBox.Show("Released drone from charging successfully");
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

        /// <summary>
        /// sends drone for a delivery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void assignParcelToDrone_Click(object sender, RoutedEventArgs e)
        {
            activeDrone(bL.AssignParcelToDrone, drone_display.Id, "Assign a drone to parcel successfully");
            OpaenDrone.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// collect parcel from sender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colectParcel_Click(object sender, RoutedEventArgs e)
        {
            activeDrone(bL.CollectParcleByDrone, drone_display.Id, "collect a parcel by drone successfully");
        }

        /// <summary>
        /// supllies parcel to the reciver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void supllyParcel_Click(object sender, RoutedEventArgs e)
        {
            activeDrone(bL.SupplyParcelByDrone, drone.Id, "suplly a parcel by drone successfully");
        }

        /// <summary>
        /// updates drone model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateDroneModel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bL.UpdateDroneModel(getId(), getModel());
                MessageBox.Show($"the model drone updated successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// gets input of drne id
        /// </summary>
        /// <returns>id of drone</returns>
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

        /// <summary>
        /// gets input of maxWeight of drone
        /// </summary>
        /// <returns>max weight</returns>
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

        /// <summary>
        /// gets input of the station number where the drone is charged
        /// </summary>
        /// <returns>station number where the drone is charged</returns>
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

        /// <summary>
        /// gets input of time that the drone was charged
        /// </summary>
        /// <returns>time that the drone was charged</returns>
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

        /// <summary>
        /// gets input of drones model
        /// </summary>
        /// <returns>model of drone</returns>
        private string getModel()
        {
            return modelDroneL.Text;
        }

        /// <summary>
        /// opens window of the parcel in delivery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openParcelInDelivery_Click(object sender, RoutedEventArgs e)
        {

            BO.Parcel parcel = bL.GetParcelById(drone.ParcelInDelivery.Id);
            ParcelWindow parcelWindow = new ParcelWindow(bL, parcel, userType.manager);
            parcelWindow.Show();
        }

        /// <summary>
        /// dis-enables action buttons when simulaions is active
        /// </summary>
        private void disenableButtonsWhileSimulates()
        {
            Buttons.Visibility = Visibility.Hidden;
            simulation.IsEnabled = false;
            stopSimulation.IsEnabled = true;
        }

        /// <summary>
        /// enables action buttons when simulation stops
        /// </summary>
        private void enableButtonWhileSimulates()
        {
            Buttons.Visibility = Visibility.Visible;
            simulation.IsEnabled = true;
            stopSimulation.IsEnabled = false;
            this.showButtonsAccordingToDroneStatus();

        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if(worker != null)
                worker.CancelAsync();
        }

        #region simulation
        /// <summary>
        /// activates the simulation
        /// </summary>
        /// <param name="sender">current drone</param>
        /// <param name="e"></param>
        private void simulation_Click(object sender, RoutedEventArgs e)
        {
            worker = new BackgroundWorker();
            disenableButtonsWhileSimulates();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
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
                drone_display.updateDisplayObject(drone);
            };
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// stops the simulation
        /// </summary>
        /// <param name="sender">current drone</param>
        /// <param name="e"></param>
        private void stopSimulation_Click(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
            this.enableButtonWhileSimulates();
        }
        #endregion
    }
}

