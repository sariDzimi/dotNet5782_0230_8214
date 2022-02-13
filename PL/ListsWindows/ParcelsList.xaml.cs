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
using System.Collections.ObjectModel;
using PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelsList.xaml
    /// </summary>
    /// 
    public partial class ParcelsList : Window
    {
        ParcelWindow OpenWindow;
        private IBL bl;
        ObservableCollection<ParcelToList> Parcels = new ObservableCollection<ParcelToList>();
        CollectionView view;
        public ParcelsList()
        {
            InitializeComponent();
        }

        public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }

        public ParcelsList(IBL bL1)
        {
            WindowStyle = WindowStyle.None;
            InitializeComponent();
            bl = bL1;
            Parcels = Convert<ParcelToList>(bl.GetParcelToLists());
            PrioritySelector.ItemsSource = Enum.GetValues(typeof(BO.Pritorities));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            DataContext = Parcels;

        }

        private void MouseDoubleClick_ParcelChoosen(object sender, MouseButtonEventArgs e)
        {
            ParcelToList parcelToList = (sender as ListView).SelectedValue as ParcelToList;
            BO.Parcel parcel = bl.GetParcelById(parcelToList.Id);
            OpenWindow = new ParcelWindow(bl, parcel, userType.manager);
            OpenWindow.ChangedParcelDelegate += UpdateInList;
            if (parcelToList.parcelStatus == ParcelStatus.Requested)
            {
                OpenWindow.ChangedParcelDelegate += DeleteParcel;

            }
            OpenWindow.Show();
        }

        public void UpdateInList(BO.Parcel parcel)
        {
            ParcelToList parcelToList = Parcels.First((d) => d.Id == parcel.Id);
            int index = Parcels.IndexOf(parcelToList);
            Parcels[index] = bl.convertParcelToTypeOfParcelToList(parcel);

        }
        public void AddParcelToLst(BO.Parcel parcel)
        {
            Parcels.Add(bl.convertParcelToTypeOfParcelToList(parcel));
        }
        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (BO.WeightCategories)MaxWeightSelector.SelectedItem;
            Parcels = Convert<ParcelToList>(bl.GetParcelsToListBy((d) => d.weightCategories == selected));
            DataContext = Parcels;
        }

        private void PrioritySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (BO.Pritorities)PrioritySelector.SelectedItem;
            Parcels = Convert<ParcelToList>(bl.GetParcelsToListBy((d) => d.pritorities == selected));
            DataContext = Parcels;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            clearListView();
            if(view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("NameOfCustomerSended");
                view.GroupDescriptions.Add(groupDescription);
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            clearListView();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            clearListView();
            if (view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();

                PropertyGroupDescription groupDescription = new PropertyGroupDescription("NameOfCustomerReciver");
                view.GroupDescriptions.Add(groupDescription);
            }
        }
        private void clearListView()
        {
            Parcels = new ObservableCollection<ParcelToList>();
            Parcels = Convert<ParcelToList>(bl.GetParcelToLists());
            DataContext = Parcels;
            view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelsListView.ItemsSource);

        }

        private void AddParcelButton(object sender, RoutedEventArgs e)
        {

            OpenWindow = new ParcelWindow(bl, userType.manager);
            OpenWindow.ChangedParcelDelegate += AddParcelToLst;
            OpenWindow.Show();
        }


        private void DeleteParcel(BO.Parcel parcel)
        {
            ParcelToList parcelToList = Parcels.First((d) => d.Id == parcel.Id);
            Parcels.Remove(parcelToList);
            OpenWindow.ChangedParcelDelegate -= UpdateInList;
        }
        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
