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
using PO;
using System.Collections.ObjectModel;
using System.Windows.Threading;
//using PL.PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for Parcel.xaml
    /// </summary>
    /// 
    public partial class ParcelWindow : Window
    {
        public Action<BO.Parcel> ChangedParcelDelegate;
        userType currentUser;
        IBL bl;
        BO.Parcel parcel;
        PO.Parcel parcel_display = new PO.Parcel();

        #region Constructor
        public ParcelWindow()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            weightLabel.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            priorityLabel.ItemsSource = Enum.GetValues(typeof(Pritorities));
        }
        public ParcelWindow(IBL bl, userType currentUser) : this()
        {
            this.currentUser = currentUser;
            this.bl = bl;
            addParcelButton.Visibility = Visibility.Visible;
            OpenReciver.Visibility = Visibility.Hidden;
            parcel_display.ListChangedDelegate += new Action<BO.Parcel>(updateParcelList);



        }
        public ParcelWindow(IBL bl, BO.Parcel parcel, userType currentUser) : this()
        {
            this.currentUser = currentUser;
            addParcelButton.Visibility = Visibility.Hidden;
            this.parcel = parcel;
            parcel_display = new PO.Parcel()
            {
                ID = this.parcel.Id,
                CustomerAtParcelReciver = this.parcel.customerAtParcelReciver,
                IdDrone = this.parcel.droneAtParcel == null ? 0 : this.parcel.droneAtParcel.Id,
                CustomerAtParcelSender = this.parcel.customerAtParcelSender,
                Delivered = this.parcel.Delivered,
                PickedUp = this.parcel.PickedUp,
                Pritority = this.parcel.Pritority,
                Requested = this.parcel.Requested,
                Scheduled = this.parcel.Scheduled,
                Weight = this.parcel.Weight
            };

            this.bl = bl;
            weightLabel.IsEnabled = false;
            priorityLabel.IsEnabled = false;
            DataContext = parcel_display;
            currentUserTextBlock.Text = this.currentUser.ToString();
            parcel_display.ListChangedDelegate += new Action<BO.Parcel>(updateParcelList);

            if (parcel_display.IdDrone != 0 && this.parcel.Delivered == null) //if parcel is not deliverd yet,
                                                                              //the ability of watching the drone is open
            {
                openDroneButton.Visibility = Visibility.Visible;
            }

            if (parcel_display.Scheduled == null) //if the parcel is not assigned to a drone,
                                                  //the ability of deleing the drone is open
            {
                deleateParcelButton.Visibility = Visibility.Visible;
            }

            if (parcel_display.PickedUp == null && this.parcel.Scheduled != null) // if the parcel is assinged but no picked up,
                                                                                  // the ability of picking up the parcel is open
            {
                pickedUpCheckBox.Visibility = Visibility.Visible;
            }
            else if (parcel_display.Delivered == null && this.parcel.PickedUp != null) //if prcel is not supllied yet but was picked up already,
                                                                                       //the ability of supllying the parcel is open
            {
                deliveredChekBox.Visibility = Visibility.Visible;
            }

        }

        #endregion

        #region Parcel Function
        /// <summary>
        /// updates prcel in prarcel list window
        /// </summary>
        /// <param name="parcel"></param>
        public void updateParcelList(BO.Parcel parcel)
        {
            if (ChangedParcelDelegate != null)
            {
                ChangedParcelDelegate(parcel);
            }
        }

        /// <summary>
        /// adds parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Customer customerSender = bl.GetCustomerById(getIdSender());
                BO.Customer customerReceiver = bl.GetCustomerById(getIdReciver());
                CustomerAtParcel customerAtParcelSender1 = new CustomerAtParcel() { Id = customerSender.Id, Name = customerSender.Name };
                CustomerAtParcel customerAtParcelReciver1 = new CustomerAtParcel() { Id = customerReceiver.Id, Name = customerReceiver.Name };
                BO.Parcel parcel = new BO.Parcel()
                {
                    Weight = getMaxWeight(),
                    Pritority = getPritorities(),
                    customerAtParcelSender = customerAtParcelSender1,
                    customerAtParcelReciver = customerAtParcelReciver1,
                    Requested = DateTime.Now
                };
                int idParcel = bl.AddParcel(parcel);
                if (parcel_display.ListChangedDelegate != null)
                {
                    parcel_display.ListChangedDelegate(bl.GetParcelById(idParcel));
                }
                MessageBox.Show($"the parcel was added succesfuly!!! your Id's parcel is:{idParcel}");
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
        /// updates parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updatePrcel(object sender, RoutedEventArgs e)
        {
            try
            {
                WeightCategories weightCategories = getMaxWeight();
                Pritorities pritorities = getPritorities();
                parcel.Pritority = pritorities;
                parcel.Weight = weightCategories;
                bl.UpdateParcel(parcel);
                if (parcel_display.ListChangedDelegate != null)
                {
                    parcel_display.ListChangedDelegate(bl.GetParcelById(parcel_display.ID));
                }
            }
            catch (NotValidInput ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// supplies parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deliveredCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("agree Delivered", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                parcel.Delivered = DateTime.Now;
                bl.UpdateParcel(parcel);
                bl.SupplyParcelByDrone(bl.GetDroneById(parcel.droneAtParcel.Id).Id);
                DeliveredLabel.Text = $"{parcel.Delivered}";
                if (parcel_display.ListChangedDelegate != null)
                {
                    parcel_display.ListChangedDelegate(bl.GetParcelById(parcel.Id));
                }
            }

        }

        /// <summary>
        /// collects parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pickedUp_Checked(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("agree Pickup", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                parcel.PickedUp = DateTime.Now;
                bl.UpdateParcel(parcel);
                bl.CollectParcleByDrone(bl.GetDroneById(parcel.droneAtParcel.Id).Id);
                PickedUpLabel.Text = $"{parcel.PickedUp}";
                deliveredChekBox.Visibility = Visibility.Visible;
                if (parcel_display.ListChangedDelegate != null)
                {
                    parcel_display.ListChangedDelegate(bl.GetParcelById(parcel.Id));
                }
            }
        }

        /// <summary>
        /// deletes the parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteParcel(object sender, RoutedEventArgs e)
        {
            bl.DeleateParcel(parcel.Id);
            if (parcel_display.ListChangedDelegate != null)
            {
                parcel_display.ListChangedDelegate(parcel);
            }
            Close();
        }

        /// <summary>
        /// opens the drone window that is responsible of deliverig the parcel 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openDrone_Click(object sender, RoutedEventArgs e)
        {
            BO.Drone drone = bl.GetDroneById(parcel_display.IdDrone);
            new Drone(bl, drone).Show();
        }

        /// <summary>
        /// open the customer window of sender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openCustomerSender(object sender, RoutedEventArgs e)
        {
            BO.Customer customer = bl.GetCustomerById(parcel.customerAtParcelSender.Id);
            new CustomerWindow(bl, customer, currentUser).Show();
        }


        /// <summary>
        /// open the customer window of the reciver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openCustomerReciver(object sender, RoutedEventArgs e)
        {
            BO.Customer customer = bl.GetCustomerById(parcel.customerAtParcelReciver.Id);
            new CustomerWindow(bl, customer, currentUser).Show();
        }

        /// <summary>
        /// closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        #region Get Input
        /// <summary>
        /// gets input of pritority
        /// </summary>
        /// <returns>pritority</returns>
        private Pritorities getPritorities()
        {
            if (priorityLabel.SelectedItem == null)
                throw new NotValidInput("Pritorities");
            try
            {

                return (Pritorities)priorityLabel.SelectedItem;
            }
            catch (Exception)
            {
                throw new NotValidInput("Pritorities");
            }
        }

        /// <summary>
        /// gets input of max weight
        /// </summary>
        /// <returns>max weight</returns>
        private WeightCategories getMaxWeight()
        {
            if (weightLabel.SelectedItem == null)
                throw new NotValidInput("weight");
            try
            {
                return (WeightCategories)weightLabel.SelectedItem;
            }
            catch (Exception)
            {
                throw new NotValidInput("weight");
            }
        }

        /// <summary>
        /// gets input: id of sender
        /// </summary>
        /// <returns>id of sender</returns>
        private int getIdSender()
        {
            try
            {
                return Convert.ToInt32(customerAtParcelSenderLabel.Text);
            }
            catch (Exception)
            {
                throw new NotValidInput("Id Sender");
            }
        }

        /// <summary>
        /// gets input: id of reciver
        /// </summary>
        /// <returns>id of reciver</returns>
        private int getIdReciver()
        {
            try
            {
                return Convert.ToInt32(customerAtParcelReciverText.Text);
            }
            catch (Exception)
            {
                throw new NotValidInput("Id Sender");
            }
        }
        #endregion
    }
}