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
        public Action<BO.Drone> ChangedDroneDelegate;
        IBL bL;
        BO.Drone drone;
        Drone_p drone_P = new Drone_p();
        public bool boo = false;
        BackgroundWorker worker = new BackgroundWorker();
        public Drone()
        {
            InitializeComponent();
        }
        public Drone(IBL bL1)
        {
            InitializeComponent();
            DroneStatusDroneL.Visibility = Visibility.Hidden;
            bL = bL1;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            addButton.IsEnabled = true;
            drone_P.ListChangedDelegate += new Action<BO.Drone>(UpdateDroneList);
        }

        public Drone(IBL bL1, BO.Drone droneBL) : this(bL1)
        {
            drone = droneBL;
            drone_P = new Drone_p()
            {
                Battery = drone.Battery,
                ID = drone.Id,
                DroneStatus = drone.DroneStatus,
                Location = drone.Location,
                MaxWeight = drone.MaxWeight,
                Model = drone.Model,
                //ParcelInDelivery = drone.ParcelInDelivery == null ? new BO.ParcelInDelivery() : drone.ParcelInDelivery
               ParcelInDelivery = drone.ParcelInDelivery
            };

            DataContext = drone_P;
            addButton.Visibility = Visibility.Hidden;
            updateBottun.IsEnabled = true;
            SwitchDroneStatus();
            if (drone.ParcelInDelivery != null)
                ParcelInDelivery.Text = drone.ParcelInDelivery.ToString();
            WeightSelector.IsEnabled = false;


        }
        public void UpdateDroneList(BO.Drone drone)
        {
            if (ChangedDroneDelegate != null)
            {
                ChangedDroneDelegate(drone);
            }
        }
        private void SwitchDroneStatus()
        {
            switch (drone.DroneStatus)
            {
                case DroneStatus.Free:
                    sendDroneForDelivery.IsEnabled = true;
                    break;
                case DroneStatus.Maintenance:
                    releaseDroneFromCharging.IsEnabled = true;
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
                        if (parcelBL.Delivered == null)
                        {
                            OpaenDrone.Visibility = Visibility.Visible;
                            supllyParcel.IsEnabled = true;
                        }

                    }
                    break;
            }
        }


        private void addDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bL.AddDrone(getId(), getMaxWeight(), getModel(), getNumberOfStation());
                BO.Drone drone = bL.GetDroneById(getId());
                if (drone_P.ListChangedDelegate != null)
                {
                    drone_P.ListChangedDelegate(drone);
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
        private void numberOfStationInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void sendDroneToCharge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bL.SendDroneToCharge(drone_P.ID);
                sendDroneForDelivery.IsEnabled = false;
                releaseDroneFromCharging.IsEnabled = true;
                timeOfCharging.Text = "";
                drone_P.Update(drone);
                MessageBox.Show("The drone was sent for charging successfully");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void releaseDroneFromCharging_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double time = getTime();
                bL.ReleaseDroneFromCharging(drone_P.ID, time);
                releaseDroneFromCharging.IsEnabled = false;
                sendDroneForDelivery.IsEnabled = true;
                timeOfCharging.Text = "";
                drone_P.Update(drone);
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

        private void sendDroneForDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bL.AssignAParcelToADrone(drone_P.ID);
                sendDroneForDelivery.IsEnabled = false;
                colectParcel.IsEnabled = true;
                drone_P.Update(drone);
                MessageBox.Show("Assign a drone to parcel successfully");
                OpaenDrone.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void colectParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bL.CollectParcleByDrone(drone_P.ID);
                colectParcel.IsEnabled = false;
                supllyParcel.IsEnabled = true;
                MessageBox.Show("collect a parcel by drone successfully");
                drone_P.Update(drone);



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void supllyParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bL.SupplyParcelByDrone(drone_P.ID);
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
        /// gets input of drone id
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

        private void openParcelInDelivery_Click(object sender, RoutedEventArgs e)
        {

            BO.Parcel parcel = bL.GetParcelById(drone.ParcelInDelivery.Id);
            ParcelWindow parcelWindow = new ParcelWindow(bL, parcel, userType.manager);
            parcelWindow.Show();
        }


        /// <summary>
        /// dis-enables actions buttons
        /// </summary>
        private void DisenableButtons()
        {
            Buttons.Visibility = Visibility.Hidden;
            simulation.IsEnabled = false;
            stopSimulation.IsEnabled = true;
        }

        /// <summary>
        /// enables actions buttons
        /// </summary>
        private void EnableButtons()
        {
            Buttons.Visibility = Visibility.Visible;
            simulation.IsEnabled = true;
            stopSimulation.IsEnabled = false;
            this.SwitchDroneStatus();

        }

        #region simulation
        /// <summary>
        /// activates the simulation
        /// </summary>
        /// <param name="sender">current drone</param>
        /// <param name="e"></param>
        private void simulation_Click(object sender, RoutedEventArgs e)
        {
            DisenableButtons();
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
            this.EnableButtons();
        }
        #endregion
    }
}

