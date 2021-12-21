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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using BL;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IBL bL;
        public MainWindow()
        {

            bL = BLFactory.GetBl();
            InitializeComponent();
            

        }
        public MainWindow( IBL bL1)
        {
            bL = bL1;
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DronesList(bL).Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new ParcelsList(bL).Show();
            Close();
        }
        private void ButtonClick_OpenStationsList(object sender, RoutedEventArgs e)
        {
            new StationsList(bL).Show();
            Close();
        }
    }
}
