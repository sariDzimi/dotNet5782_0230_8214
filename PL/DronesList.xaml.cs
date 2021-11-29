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
using System.Collections.Generic;
using System.Windows;

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
            InitializeComponent();
            
        }

        public DronesList(BL.BL bL1)
        {
            InitializeComponent();
            bl = bL1;
            DronesListView.ItemsSource = bl.GetDrones();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatus));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
        }

        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            var selected = (IBL.BO.WeightCategories)comboBox.SelectedItem;
            DronesListView.ItemsSource = bl.GetDronesBy((d) => d.MaxWeight == selected);
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            var selected = (IBL.BO.DroneStatus)comboBox.SelectedItem;
            DronesListView.ItemsSource = bl.GetDronesBy((d) => d.DroneStatus == selected);
        }
    }
}