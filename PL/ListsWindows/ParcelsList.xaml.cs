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
using PL.PO;

namespace PL
{
    public delegate void Changed<T>(T change);
    /// <summary>
    /// Interaction logic for ParcelsList.xaml
    /// </summary>
    public partial class ParcelsList : Window
    {
        public CurrentUser currentUser = new CurrentUser();

        private IBL bl;

        ObservableCollection<ParcelToList> Parcels = new ObservableCollection<ParcelToList>();

        CollectionView view;
        //List<ParcelToList> items;
        ParcelList parcelsList = new ParcelList();
        //CustomerList Customer = new CustomerList();
        //DroneList droneList = new DroneList();

        public ParcelsList()
        {
            InitializeComponent();
        }

        public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }

        public ParcelsList(IBL bL1, CurrentUser currentUser1)
        {
            currentUser = currentUser1;
            WindowStyle = WindowStyle.None;
            InitializeComponent();
            bl = bL1;
            Parcels = Convert<ParcelToList>(bl.GetParcelToLists());
            PrioritySelector.ItemsSource = Enum.GetValues(typeof(BO.Pritorities));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            CurrentUser.Text = currentUser.Type.ToString();
            DataContext = Parcels;

        }

        private void MouseDoubleClick_ParcelChoosen(object sender, MouseButtonEventArgs e)
        {
            ParcelToList parcelToList = (sender as ListView).SelectedValue as ParcelToList;
            BO.Parcel parcel = bl.GetParcelById(parcelToList.Id);
            ParcelWindow OpenWindow = new ParcelWindow(bl, parcel, currentUser);
            OpenWindow.ChangedParcelDelegate += UpdateInList;
            OpenWindow.Show();
        }


        public void UpdateInList(BO.Parcel parcel)
        {
            ParcelToList parcelToList = Parcels.First((d) => d.Id == parcel.Id);
            int index = Parcels.IndexOf(parcelToList);
            Parcels[index] = new ParcelToList() { Id = parcel.Id, NameOfCustomerReciver = parcel.customerAtParcelReciver.Name, NameOfCustomerSended = parcel.customerAtParcelSender.Name, pritorities = parcel.Pritority, weightCategories = parcel.Weight };

        }
        public void AddParcelToLst(BO.Parcel parcel)
        {
            Parcels.Add(new ParcelToList() { Id = parcel.Id, NameOfCustomerReciver = parcel.customerAtParcelReciver.Name, NameOfCustomerSended = parcel.customerAtParcelSender.Name, pritorities = parcel.Pritority, weightCategories = parcel.Weight });
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
            ParcelsListView.DataContext = parcelsList.Parcels;
            view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelsListView.ItemsSource);

        }

        private void AddParcelButton(object sender, RoutedEventArgs e)
        {

            ParcelWindow OpenWindow = new ParcelWindow(bl, currentUser);
            OpenWindow.ChangedParcelDelegate += AddParcelToLst;
            OpenWindow.Show();
            //Close();
            //new ParcelWindow(bl, currentUser).Show();
        }


        private void DeleteParcel(BO.Parcel parcel)
        {
            ParcelToList parcelToList = Parcels.First((d) => d.Id == parcel.Id);
            Parcels.Remove(parcelToList);
        }

           /* ParcelWindow OpenWindow = new ParcelWindow(bl, currentUser);
            OpenWindow.ChangedParcelDelegate += AddParcelToLst;
            OpenWindow.Show();*/
                //Close();
                //new ParcelWindow(bl, currentUser).Show();
            
        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
