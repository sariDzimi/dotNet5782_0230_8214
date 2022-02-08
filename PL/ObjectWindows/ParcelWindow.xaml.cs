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
using PL.PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for Parcel.xaml
    /// </summary>
    /// 
    public partial class ParcelWindow : Window
    {
        public Action<BO.Parcel> ChangedParcelDelegate;

        CurrentUser currentUser = new CurrentUser();
        IBL bL1;
        Parcel parcel;
        Parcel_p Parcel_P= new Parcel_p();
        ParcelList parcelList= new ParcelList();
        CustomerList CustomerList = new CustomerList();
        //DroneList droneList = new DroneList();
        //Drone_p drone_P = new Drone_p();
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
            //this.parcelList.Parcels = parcel_PsA;
            Parcel_P.ListChangedDelegate += new Action<BO.Parcel>(UpdateParcelList);

        }

        public ParcelWindow(IBL bl, Parcel parcel1, CurrentUser currentUser1)
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
            CurrentUser.Text = currentUser.Type.ToString();
            Parcel_P.ListChangedDelegate += new Action<BO.Parcel>(UpdateParcelList);

            if (Parcel_P.IdDrone != 0 && parcel.Delivered == null)
            {
                OpenDrone.Visibility = Visibility.Visible;
               
            }

            if (Parcel_P.Scheduled == null)
            {
                DeleateParcel.Visibility = Visibility.Visible;
            }
            
            if (Parcel_P.PickedUp == null && parcel.Scheduled !=null)
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
                // BO.Parcel parcel = new Parcel();
                //parcel.Requested = DateTime.Now;
                Customer customerSender = bL1.GetCustomerById(getIdSender());
                Customer customerReceiver = bL1.GetCustomerById(getIdSender());
                CustomerAtParcel customerAtParcelSender1 = new CustomerAtParcel() { Id =customerSender.Id, Name  = customerSender.Name};
                CustomerAtParcel customerAtParcelReciver1 = new CustomerAtParcel() { Id = customerReceiver.Id, Name = customerReceiver.Name };
                bL1.AddParcel(new Parcel() { Id = getId(), Weight = getMaxWeight(), Pritority = getPritorities(), customerAtParcelSender = customerAtParcelSender1, customerAtParcelReciver = customerAtParcelReciver1, Requested = DateTime.Now });
                BO.Parcel parcel = bL1.GetParcelById(getId());
                if (Parcel_P.ListChangedDelegate != null)
                {
                    Parcel_P.ListChangedDelegate(parcel);
                }
                //parcelList.AddParcel(new ParcelToList() { ID = getId(), pritorities = getPritorities(), weightCategories = getMaxWeight(), NameOfCustomerReciver = customerAtParcelReciver1.Name, NameOfCustomerSended= customerAtParcelSender1.Name });
                MessageBox.Show("the parcel was added succesfuly!!!");
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
                WeightCategories weightCategories = getMaxWeight();
                Pritorities pritorities = getPritorities();
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
            /*ChangedParcelDelegate
                        bL1.DeleateParcel(parcel.Id);
                        parcelList.DeleateParcel(Parcel_P);
                        Close();*/
            ChangedParcelDelegate(parcel);
        }

        private void OpenDrone_Click(object sender, RoutedEventArgs e)
        {
            BO.Drone drone = bL1.GetDroneById(Parcel_P.IdDrone);
            new Drone(bL1, drone, currentUser).Show();
        }

        private void openCustomerSender(object sender, RoutedEventArgs e)
        {
            BO.Customer customer = bL1.GetCustomerById(parcel.customerAtParcelSender.Id);
            //Hide();
            new CustomerWindow(bL1, customer, currentUser, CustomerList).Show();
            //Show();
        }

        private void openCustomerReciver(object sender, RoutedEventArgs e)
        {
            BO.Customer customer = bL1.GetCustomerById(parcel.customerAtParcelReciver.Id);
            //Hide();
            new CustomerWindow(bL1, customer, currentUser, CustomerList).Show();
            //Show();
        }

        private void close_buuton(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}



