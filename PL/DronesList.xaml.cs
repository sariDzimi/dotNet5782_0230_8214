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

namespace PL
{
    /// <summary>
    /// Interaction logic for DronesList.xaml
    /// </summary>
    public partial class DronesList : Window
    {
        private BL.BL bl;
        public DronesList()
        {
            WindowStyle = WindowStyle.None;

            InitializeComponent();
            
        }

        public DronesList(BL.BL bL1)
        {
            WindowStyle = WindowStyle.None;

            InitializeComponent();
            bl = bL1;
            DronesListView.ItemsSource = bl.GetDrones();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatus));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories)); 
        }

        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (IBL.BO.WeightCategories)MaxWeightSelector.SelectedItem;
            DronesListView.ItemsSource = bl.GetDronesBy((d) => d.MaxWeight == selected);
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (IBL.BO.DroneStatus)StatusSelector.SelectedItem;
            DronesListView.ItemsSource = bl.GetDronesBy((d) => d.DroneStatus == selected);
        }


        private void MouseDoubleClick_droneChoosen(object sender, MouseButtonEventArgs e)
        {
            IBL.BO.DroneBL droneBL = (sender as ListView).SelectedValue as IBL.BO.DroneBL;
           
            new Drone(bl, droneBL).Show();
            Close();
        }

        private void addADrone_Click(object sender, RoutedEventArgs e)
        {
            new Drone(bl).Show();
            Close();

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DronesList(bl).Show();
            Close();
        }
    }
}
