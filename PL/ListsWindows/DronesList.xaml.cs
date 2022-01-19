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
using BO;
using BlApi;
using PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DronesList.xaml
    /// </summary>
    public partial class DronesList : Window
    {
        public CurrentUser currentUser = new CurrentUser();
        List<DroneToList> items;
        DroneList droneList = new DroneList();
        private IBL bl;
        public DronesList()
        {
            WindowStyle = WindowStyle.None;

            InitializeComponent();

        }


        public DronesList(IBL bL1, CurrentUser currentUser1)
        {
            currentUser = currentUser1;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = bL1;
            items = bl.GetDroneToLists().ToList();
            droneList.Drone_Ps = droneList.ConvertDronelBLToPL(items);
            DataContext = droneList.Drone_Ps;
            DronesListView.ItemsSource = droneList.Drone_Ps;
            StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.DroneStatus));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            CurrentUser.Text = currentUser.Type;

        }

        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (BO.WeightCategories)MaxWeightSelector.SelectedItem;
            DronesListView.ItemsSource = bl.GetDroneToListsBy((d) => d.MaxWeight == selected);
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (BO.DroneStatus)StatusSelector.SelectedItem;
            //  DronesListView.ItemsSource = droneList.Drone_Ps;
            droneList.ClearDrones();
            droneList.ConvertDronelBLToPL(bl.GetDroneToListsBy((d) => d.DroneStatus == selected).ToList());
           // DataContext = droneList.Drone_Ps;

            //droneList.Drone_Ps = droneList.ConvertDronelBLToPL(items);
            //DataContext = droneList.Drone_Ps;
            DronesListView.ItemsSource = droneList.Drone_Ps;
            
            //bl.GetDroneToListsBy((d) => d.DroneStatus == selected).ToList();
        }


        private void MouseDoubleClick_droneChoosen(object sender, MouseButtonEventArgs e)
        {
            Drone_p droneToList = (sender as ListView).SelectedValue as Drone_p;
            BO.Drone droneBL = bl.GetDroneById(droneToList.ID);
            new Drone(bl, droneBL, currentUser).Show();
            //Close();
        }

        private void addADrone_Click(object sender, RoutedEventArgs e)
        {
            new Drone(bl, currentUser, droneList.Drone_Ps).Show();
            //Close();

        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            //new ManegerWindow(bl, currentUser).Show();
            Close();
        }


    }
}
