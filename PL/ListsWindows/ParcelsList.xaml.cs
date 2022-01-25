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
using System.Collections.ObjectModel;
using PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelsList.xaml
    /// </summary>
    public partial class ParcelsList : Window
    {
        public CurrentUser currentUser = new CurrentUser();

        private IBL bl;
        CollectionView view;
        List<ParcelToList> items;
        ParcelList parcelsList = new ParcelList();
        //DroneList droneList = new DroneList();

        public ParcelsList()
        {
            InitializeComponent();
        }

        public ParcelsList(IBL bL1, CurrentUser currentUser1)
        {
            currentUser = currentUser1;
            WindowStyle = WindowStyle.None;
            InitializeComponent();
            bl = bL1;
            items = bl.GetParcelToLists().ToList();
            PrioritySelector.ItemsSource = Enum.GetValues(typeof(BO.Pritorities));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            CurrentUser.Text = currentUser.Type;
            parcelsList.Parcels = parcelsList.ConvertParcelBLToPL(items);
            DataContext = parcelsList.Parcels;
            ParcelsListView.ItemsSource = parcelsList.Parcels;
            view = (CollectionView)CollectionViewSource.GetDefaultView(DataContext);
        }



        private void MouseDoubleClick_ParcelChoosen(object sender, MouseButtonEventArgs e)
        {
            Parcel_p parcel = (sender as ListView).SelectedValue as Parcel_p;
            BO.Parcel parcelBL = bl.GetParcelById(parcel.ID);
            new ParcelWindow(bl, parcelBL, currentUser).Show();
            Close();
        }
        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var selected = (BO.WeightCategories)MaxWeightSelector.SelectedItem;
           // ParcelsListView.ItemsSource = bl.GetParcelsToListBy((d) => d.weightCategories == selected);

            var selected = (BO.WeightCategories)MaxWeightSelector.SelectedItem;
            parcelsList.ClearParcels();
            parcelsList.ConvertParcelBLToPL(bl.GetParcelsToListBy((d) => d.weightCategories == selected).ToList());
            ParcelsListView.ItemsSource = parcelsList.Parcels;
        }

        private void PrioritySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (BO.Pritorities)PrioritySelector.SelectedItem;
          //  ParcelsListView.ItemsSource = bl.GetParcelsToListBy((d) => d.pritorities == selected);
            parcelsList.ClearParcels();
            parcelsList.ConvertParcelBLToPL(bl.GetParcelsToListBy((d) => d.pritorities == selected).ToList());
            ParcelsListView.ItemsSource = parcelsList.Parcels;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            clearListView();
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("NameOfSender");
            view.GroupDescriptions.Add(groupDescription);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            clearListView();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            clearListView();
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("NameOfReciver");
            view.GroupDescriptions.Add(groupDescription);

        }
        private void clearListView()
        {
            parcelsList.Parcels = parcelsList.ClearParcels();
            parcelsList.Parcels = parcelsList.ConvertParcelBLToPL(bl.GetParcelToLists().ToList());
            ParcelsListView.ItemsSource = parcelsList.Parcels;
            view = (CollectionView)CollectionViewSource.GetDefaultView(parcelsList.Parcels);
        }

        private void AddParcelButton(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(bl, currentUser, parcelsList.Parcels).Show();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
