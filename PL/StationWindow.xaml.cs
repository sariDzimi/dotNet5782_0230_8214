﻿using System;
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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        CurrentUser currentUser = new CurrentUser();
        IBL bl;
        Station station;
        public StationWindow()
        {
           // InitializeComponent();
        }
        public StationWindow(IBL blArg, Station stationArg, CurrentUser currentUser1)
        {
            currentUser = currentUser1;
            //InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blArg;
            station = stationArg;
            DroneChargingListView.ItemsSource = station.droneAtChargings;
            updateStationLabel.Visibility = Visibility.Visible;
            DataContext = station;
            laditudeTextBox.DataContext = station.Location;
            longitudeTextBox.DataContext = station.Location;
        }

        public StationWindow(IBL blArg, CurrentUser currentUser1)
      {
            currentUser = currentUser1;
            //InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blArg;
            addStationLabel.Visibility = Visibility.Visible;
        }

        private void DroneChargingListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DroneChargingListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.DroneAtCharging droneAtCharging = (sender as ListView).SelectedValue as DroneAtCharging;

            Hide();
            new Drone(bl, bl.FindDrone(droneAtCharging.ID), currentUser).ShowDialog();
            Show();

        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.updateStation(new Station()
                {
                    Id = getId(),
                    Name = getName(),
                    droneAtChargings = null,
                    Location = getLocation(),
                    FreeChargeSlots = getChargeSlots()
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.addStationToDL(new Station()
                {
                    Id = getId(),
                    Name = getName(),
                    droneAtChargings = null,
                    Location = getLocation(),
                    FreeChargeSlots = getChargeSlots()
                });

                Hide();
                new StationsList(bl, currentUser).ShowDialog();
                Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private Location getLocation()
        {
            try
            {
                double longitude = Convert.ToDouble(longitudeTextBox.Text);
                double latitude = Convert.ToDouble(laditudeTextBox.Text);
                return new Location(longitude, latitude);
            }
            catch
            {
                throw new NotValidInput("Location");
            }
        }



        private int getId()
        {
            try
            {
                return Convert.ToInt32(idTextBox.Text);
            }
            catch
            {
                throw new NotValidInput("longitude");
            }
        }

        private int getName()
        {
            try
            {
                return Convert.ToInt32(nameTextBox.Text);
            }
            catch
            {
                throw new NotValidInput("name");
            }
        }

        private int getChargeSlots()
        {
            try
            {
                return Convert.ToInt32(ChargeStolsTextBox.Text);
            }
            catch
            {
                throw new NotValidInput("charge slots");
            }
        }
    }
}
