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
        IBL bL1;
        Parcel parcel;
        Parcel_p Parcel_P = new Parcel_p();
        //ParcelList parcelList= new ParcelList();
        //CustomerList CustomerList = new CustomerList();
        //DroneList droneList = new DroneList();
        //Drone_p drone_P = new Drone_p();
        public ParcelWindow()
        {
            InitializeComponent();
        }

        public ParcelWindow(IBL bL, userType currentUser1)
        {
            currentUser = currentUser1;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            weightLabel.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            priorityLabel.ItemsSource = Enum.GetValues(typeof(Pritorities));
            bL1 = bL;
            AddParcelButton.Visibility = Visibility.Visible;
            OpenReciver.Visibility = Visibility.Hidden;
            Parcel_P.ListChangedDelegate += new Action<BO.Parcel>(UpdateParcelList);

        }

        public ParcelWindow(IBL bl, Parcel parcel1, userType currentUser1)
        {
            currentUser = currentUser1;
            //CustomerList = customerList;
            InitializeComponent();
            AddParcelButton.Visibility = Visibility.Hidden;
            parcel = parcel1;
            Parcel_P = new Parcel_p() { ID = parcel.Id, CustomerAtParcelReciver = parcel.customerAtParcelReciver, IdDrone = parcel.droneAtParcel == null ? 0 : parcel.droneAtParcel.Id, CustomerAtParcelSender = parcel.customerAtParcelSender, Delivered = parcel.Delivered, PickedUp = parcel.PickedUp, Pritority = parcel.Pritority, Requested = parcel.Requested, Scheduled = parcel.Scheduled, Weight = parcel.Weight };
            bL1 = bl;
            weightLabel.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            priorityLabel.ItemsSource = Enum.GetValues(typeof(Pritorities));
            weightLabel.IsEnabled = false;
            priorityLabel.IsEnabled = false;
            DataContext = Parcel_P;
            currentUserTextBlock.Text = currentUser.ToString();
            Parcel_P.ListChangedDelegate += new Action<BO.Parcel>(UpdateParcelList);

            if (Parcel_P.IdDrone != 0 && parcel.Delivered == null)
            {
                OpenDrone.Visibility = Visibility.Visible;

            }

            if (Parcel_P.Scheduled == null)
            {
                DeleateParcel.Visibility = Visibility.Visible;
                // Parcel_P.ListChangedDelegate +=DeleteParcelToList;

            }

            if (Parcel_P.PickedUp == null && parcel.Scheduled != null)
            {
                PickedUpC.Visibility = Visibility.Visible;
            }
            else if (Parcel_P.Delivered == null && parcel.PickedUp != null)
            {
                DeliveredC.Visibility = Visibility.Visible;
            }

        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void UpdateParcelList(BO.Parcel parcel)
        {
            if (ChangedParcelDelegate != null)
            {
                ChangedParcelDelegate(parcel);
            }
        }

        private void AddParcel(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer customerSender = bL1.GetCustomerById(getIdSender());
                Customer customerReceiver = bL1.GetCustomerById(getIdReciver());
                CustomerAtParcel customerAtParcelSender1 = new CustomerAtParcel() { Id = customerSender.Id, Name = customerSender.Name };
                CustomerAtParcel customerAtParcelReciver1 = new CustomerAtParcel() { Id = customerReceiver.Id, Name = customerReceiver.Name };
                BO.Parcel parcel = new Parcel()
                {
                    Weight = getMaxWeight(),
                    Pritority = getPritorities(),
                    customerAtParcelSender = customerAtParcelSender1,
                    customerAtParcelReciver = customerAtParcelReciver1,
                    Requested = DateTime.Now
                };
                int idParcel = bL1.AddParcel(parcel);
                if (Parcel_P.ListChangedDelegate != null)
                {
                    Parcel_P.ListChangedDelegate(bL1.GetParcelById(idParcel));
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

        private void UpdatePrcel(object sender, RoutedEventArgs e)
        {
            try
            {
                WeightCategories weightCategories = getMaxWeight();
                Pritorities pritorities = getPritorities();
                parcel.Pritority = pritorities;
                parcel.Weight = weightCategories;
                bL1.UpdateParcel(parcel);
                if (Parcel_P.ListChangedDelegate != null)
                {
                    Parcel_P.ListChangedDelegate(bL1.GetParcelById(Parcel_P.ID));
                }
            }
            catch (NotValidInput ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("agree Delivered", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
            }
            else
            {
                parcel.Delivered = DateTime.Now;
                bL1.UpdateParcel(parcel);
                DeliveredLabel.Text = $"{parcel.Delivered}";
                if (Parcel_P.ListChangedDelegate != null)
                {
                    Parcel_P.ListChangedDelegate(bL1.GetParcelById(parcel.Id));
                }
            }

        }

        private void PickedUpC_Checked(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("agree Pickup", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
            }
            else
            {
                parcel.PickedUp = DateTime.Now;
                bL1.UpdateParcel(parcel);
                PickedUpLabel.Text = $"{parcel.PickedUp}";
                DeliveredC.Visibility = Visibility.Visible;
                if (Parcel_P.ListChangedDelegate != null)
                {
                    Parcel_P.ListChangedDelegate(bL1.GetParcelById(parcel.Id));
                }
            }
        }

        private void DeleteParcel(object sender, RoutedEventArgs e)
        {
            // Parcel_P.ListChangedDelegate += new Changed<BO.Parcel>(UpdateParcelList);

            bL1.DeleateParcel(parcel.Id);
            // BO.Parcel parcelCorrent = bL1.GetParcelById(parcel.Id);
            if (Parcel_P.ListChangedDelegate != null)
            {
                Parcel_P.ListChangedDelegate(parcel);
            }
            //parcelList.DeleateParcel(Parcel_P);
            Close();
        }

        private void OpenDrone_Click(object sender, RoutedEventArgs e)
        {
            BO.Drone drone = bL1.GetDroneById(Parcel_P.IdDrone);
            new Drone(bL1, drone).Show();
        }

        private void openCustomerSender(object sender, RoutedEventArgs e)
        {
            BO.Customer customer = bL1.GetCustomerById(parcel.customerAtParcelSender.Id);
            //Hide();
            new CustomerWindow(bL1, customer, currentUser).Show();
            //Show();
        }

        private void openCustomerReciver(object sender, RoutedEventArgs e)
        {
            BO.Customer customer = bL1.GetCustomerById(parcel.customerAtParcelReciver.Id);
            //Hide();
            new CustomerWindow(bL1, customer, currentUser).Show();
            //Show();
        }

        private void close_buuton(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}



