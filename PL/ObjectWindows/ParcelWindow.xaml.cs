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


namespace PL
{
    /// <summary>
    /// Interaction logic for Parcel.xaml
    /// </summary>
    /// 
    public partial class ParcelWindow : Window
    {
        CurrentUser currentUser = new CurrentUser();
        IBL bL1;
        Parcel parcel;
        Parcel_p Parcel_P;

        public ParcelWindow()
        {
            InitializeComponent();
        }

        public ParcelWindow(IBL bL, CurrentUser currentUser1)
        {
            currentUser = currentUser1;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            weightLabel.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            priorityLabel.ItemsSource = Enum.GetValues(typeof(Pritorities));
            bL1 = bL;
            AddParcelButton.Visibility = Visibility.Visible;
            OpenReciver.Visibility = Visibility.Hidden;

        }

        public ParcelWindow(IBL bl, Parcel parcel1, CurrentUser currentUser1)
        {
            currentUser = currentUser1;
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
            if (parcel.droneAtParcel != null && parcel.Delivered == null)
            {
                OpenDrone.Visibility = Visibility.Visible;
               
            }

            if (parcel.Scheduled == null)
            {
                DeleateParcel.Visibility = Visibility.Visible;
            }
            
            if (parcel.PickedUp == null && parcel.Scheduled !=null)
            {
                PickedUpC.Visibility = Visibility.Visible;
            }
            else if (parcel.Delivered == null)
            {
                DeliveredC.Visibility = Visibility.Visible;
            }

        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            new ParcelsList(bL1, currentUser).Show();
            Close();
        }

        private void AddParcel(object sender, RoutedEventArgs e)
        {
            try
            {
                // BO.Parcel parcel = new Parcel();
                //parcel.Requested = DateTime.Now;
                CustomerAtParcel customerAtParcelSender1 = new CustomerAtParcel() { Id = getIdSender() };
                CustomerAtParcel customerAtParcelReciver1 = new CustomerAtParcel() { Id = getIdReciver() };
                bL1.addParcelToDL(new Parcel() { Id = getId(), Weight = getMaxWeight(), Pritority = getPritorities(), customerAtParcelSender = customerAtParcelSender1, customerAtParcelReciver = customerAtParcelReciver1, Requested = DateTime.Now });
                MessageBox.Show("the parcel was added succesfuly!!!");
                new ParcelsList(bL1, currentUser).Show();
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
        private int getId()
        {
            try
            {
                return Convert.ToInt32(idParcelLabel.Text);
            }
            catch (Exception)
            {
                throw new NotValidInput("id");
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
                BO.WeightCategories weightCategories = getMaxWeight();
                BO.Pritorities pritorities = getPritorities();
                parcel.Pritority = pritorities;
                parcel.Weight = weightCategories;
                bL1.updateParcel(parcel);

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
                bL1.updateParcel(parcel);
                DeliveredLabel.Text = $"{parcel.Delivered}";

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
                bL1.updateParcel(parcel);
                PickedUpLabel.Text = $"{parcel.PickedUp}";
                DeliveredC.Visibility = Visibility.Visible;
            }
        }

        private void DeleteParcel(object sender, RoutedEventArgs e)
        {
            bL1.DeleateParcel(parcel);
            new ParcelsList(bL1, currentUser).Show();
            this.Close();
        }

        private void OpenDrone_Click(object sender, RoutedEventArgs e)
        {
            BO.Drone drone = bL1.FindDroneBy((p) => Parcel_P.IdDrone == p.Id);
            new Drone(bL1, drone, currentUser).Show();
        }

        private void openCustomerSender(object sender, RoutedEventArgs e)
        {
            BO.Customer customer = bL1.FindCustomerBy((c) => c.Id == parcel.customerAtParcelSender.Id);
            new CustomerWindow(bL1, customer, currentUser).Show();
        }

        private void openCustomerReciver(object sender, RoutedEventArgs e)
        {
            BO.Customer customer = bL1.FindCustomerBy((c) => c.Id == parcel.customerAtParcelReciver.Id);
            new CustomerWindow(bL1, customer, currentUser).Show();
        }

        private void close_buuton(object sender, RoutedEventArgs e)
        {
            new ParcelsList(bL1, currentUser).Show();
            Close();
        }
    }
}



