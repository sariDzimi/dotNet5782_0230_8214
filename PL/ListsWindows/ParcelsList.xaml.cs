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
        ObservableCollection<ParcelToList> parcels = new ObservableCollection<ParcelToList>();
        CollectionView view;
        public ParcelsList()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
        }

        public ParcelsList(IBL bL1) : this()
        {
            bl = bL1;
            parcels = Convert(bl.GetParcelToLists());
            PrioritySelector.ItemsSource = Enum.GetValues(typeof(Pritorities));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            DataContext = parcels;
        }

        /// <summary>
        /// converts collection of type IEnumerable to observable collection
        /// <typeparam name="T"></typeparam>
        /// <param name="original">collection of type IEnumerable</param>
        /// <returns>collection of type ObservableCollection</returns>
        public ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }

        /// <summary>
        /// opens parcel window of the choosen parcel
        /// </summary>
        /// <param name="sender">parcel</param>
        /// <param name="e"></param>
        private void parcelChoosen_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ParcelToList parcelToList = (sender as ListView).SelectedValue as ParcelToList;
            BO.Parcel parcel = bl.GetParcelById(parcelToList.Id);
            OpenWindow = new ParcelWindow(bl, parcel, userType.manager);
            OpenWindow.ChangedParcelDelegate += updateInList;
            if (parcelToList.parcelStatus == ParcelStatus.Requested)
            {
                OpenWindow.ChangedParcelDelegate += DeleteParcel;

            }
            OpenWindow.Show();
        }

        /// <summary>
        /// updates parcel in the observable collection
        /// </summary>
        /// <param name="parcel">updated parcel</param>
        public void updateInList(BO.Parcel parcel)
        {
            ParcelToList parcelToList = parcels.First((d) => d.Id == parcel.Id);
            int index = parcels.IndexOf(parcelToList);
            parcels[index] = bl.convertParcelToTypeOfParcelToList(parcel);

        }

        /// <summary>
        /// adda parcel to the observable collection
        /// </summary>
        /// <param name="parcel">parcel</param>
        public void addParcelToList(BO.Parcel parcel)
        {
            parcels.Add(bl.convertParcelToTypeOfParcelToList(parcel));
        }

        /// <summary>
        /// shows only parcels with the maxWeight
        /// </summary>
        /// <param name="sender">maxWeight</param>
        /// <param name="e"></param>
        private void maxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (BO.WeightCategories)MaxWeightSelector.SelectedItem;
            parcels = Convert<ParcelToList>(bl.GetParcelsToListBy((d) => d.weightCategories == selected));
            DataContext = parcels;
        }

        /// <summary>
        /// shows only paecels with the priority
        /// </summary>
        /// <param name="sender">priority</param>
        /// <param name="e"></param>
        private void prioritySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (BO.Pritorities)PrioritySelector.SelectedItem;
            parcels = Convert<ParcelToList>(bl.GetParcelsToListBy((d) => d.pritorities == selected));
            DataContext = parcels;
        }

        /// <summary>
        /// groups parcels by senders name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupBySender_Click(object sender, RoutedEventArgs e)
        {
            clearListView();
            if (view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("NameOfCustomerSended");
                view.GroupDescriptions.Add(groupDescription);
            }
        }

        /// <summary>
        /// show all parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showAllList_Click(object sender, RoutedEventArgs e)
        {
            clearListView();
        }

        /// <summary>
        /// groups parcel by recivers name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupByReciver_Click(object sender, RoutedEventArgs e)
        {
            clearListView();
            if (view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();

                PropertyGroupDescription groupDescription = new PropertyGroupDescription("NameOfCustomerReciver");
                view.GroupDescriptions.Add(groupDescription);
            }
        }

        /// <summary>
        /// clears grouped of selcted view to default view
        /// </summary>
        private void clearListView()
        {
            parcels = new ObservableCollection<ParcelToList>();
            parcels = Convert<ParcelToList>(bl.GetParcelToLists());
            DataContext = parcels;
            view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelsListView.ItemsSource);

        }


        /// <summary>
        /// adds parcel
        /// </summary>
        /// <param name="sender">parcel</param>
        /// <param name="e"></param>
        private void addParcel_Click(object sender, RoutedEventArgs e)
        {

            OpenWindow = new ParcelWindow(bl, userType.manager);
            OpenWindow.ChangedParcelDelegate += addParcelToList;
            OpenWindow.Show();
        }

        /// <summary>
        /// deletes parcel
        /// </summary>
        /// <param name="parcel"></param>

        private void dpick_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            parcels = new ObservableCollection<ParcelToList>();
            foreach (var parcel in bl.GetParcelsBy((p) => p.Scheduled > dp1.SelectedDate.Value.Date))
            {
                parcels.Add(bl.convertParcelToTypeOfParcelToList(parcel));
            }
            DataContext = parcels;
    
        }

        private void DeleteParcel(BO.Parcel parcel)
        {
            ParcelToList parcelToList = parcels.First((d) => d.Id == parcel.Id);
            parcels.Remove(parcelToList);
            OpenWindow.ChangedParcelDelegate -= updateInList;
        }

        /// <summary>
        /// closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
