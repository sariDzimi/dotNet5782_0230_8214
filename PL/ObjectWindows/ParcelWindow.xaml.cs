﻿using BO;
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
    /// Interaction logic for Parcel.xaml
    /// </summary>
    /// 
    public partial class ParcelWindow : Window
    {
        CurrentUser currentUser = new CurrentUser();
        IBL bL1;
        Parcel parcel;
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

        public ParcelWindow( IBL bl, Parcel parcel1 , CurrentUser currentUser1)
        {
            currentUser = currentUser1;
            InitializeComponent();
            AddParcelButton.Visibility = Visibility.Hidden;
            parcel = parcel1;
            bL1 = bl;
            idParcelLabel.Text = $"{parcel.Id}";
            if (parcel.droneAtParcel != null)
            {
                iddroneLabel.Text = $"{parcel.droneAtParcel.Id}";
                if(parcel.Delivered == null)
                {
                    OpenDrone.Visibility = Visibility.Visible;
                }
                
            }
            if (parcel.Scheduled == null)
            {
                DeleateParcel.Visibility = Visibility.Visible;
            }
           
            weightLabel.Text = $"{parcel.Weight}";
            priorityLabel.Text = $"{parcel.Pritority}";
            RequestedLabel.Text = $"{parcel.Requested}";
            ScheduledLabel.Text = $"{parcel.Scheduled}";
            PickedUpLabel.Text = $"{parcel.PickedUp}";
            DeliveredLabel.Text = $"{parcel.Delivered}";
            customerAtParcelSenderLabel.Text = $"{parcel.customerAtParcelSender}";
            customerAtParcelReciverText.Text = $"{parcel.customerAtParcelReciver}";
            weightLabel.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            priorityLabel.ItemsSource = Enum.GetValues(typeof(Pritorities));
            weightLabel.IsEditable = true;
            weightLabel.Text = $"{parcel.Weight}";
            priorityLabel.IsEditable = true;
            priorityLabel.Text = $"{parcel.Pritority}";

            if (parcel.PickedUp == null)
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
            Close();
        }

        private void AddParcel(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomerAtParcel customerAtParcelSender1 = new CustomerAtParcel() { Id = getIdSender() };
                CustomerAtParcel customerAtParcelReciver1 = new CustomerAtParcel() { Id = getIdReciver() };
                bL1.addParcelToDL( new Parcel() { Id = getId(), Weight = getMaxWeight(),Pritority=getPritorities(), customerAtParcelSender= customerAtParcelSender1, customerAtParcelReciver= customerAtParcelReciver1, Requested= DateTime.Now });
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
            bL1.DeleateParcel(parcel);
            Close();
        }

        private void OpenDrone_Click(object sender, RoutedEventArgs e)
        {
            BO.Drone drone = bL1.FindDroneBy((p) =>  parcel.droneAtParcel.Id== p.Id );
            Hide();
            new Drone(bL1, drone, currentUser).ShowDialog();
            Show();
        }

        private void openCustomerSender(object sender, RoutedEventArgs e)
        {
            BO.Customer customer = bL1.FindCustomerBy((c) => c.Id == parcel.customerAtParcelSender.Id);
            Hide();
            new CustomerWindow(bL1, customer, currentUser).ShowDialog();
            Show();
        }

        private void openCustomerReciver(object sender, RoutedEventArgs e)
        {
            BO.Customer customer = bL1.FindCustomerBy((c) => c.Id == parcel.customerAtParcelReciver.Id);
            Hide();
            new CustomerWindow(bL1, customer, currentUser).ShowDialog();
            Show();
        }

        private void close_buuton(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}



                                                                                                                                                                                                       