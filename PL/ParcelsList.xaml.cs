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


namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelsList.xaml
    /// </summary>
    public partial class ParcelsList : Window
    {
        private IBL bl;
        CollectionView view;
        List<ParcelToList> items;
        public ParcelsList()
        {
            InitializeComponent();
        }

        public ParcelsList(IBL bL1)
        {
            WindowStyle = WindowStyle.None;

            InitializeComponent();
            bl = bL1;
            items = new List<ParcelToList>();
            foreach (var parcel in bl.GetParcelToLists())
            {
                items.Add(new ParcelToList() { ID = parcel.ID, weightCategories = parcel.weightCategories, NameOfCustomerReciver = parcel.NameOfCustomerReciver, NameOfCustomerSended = parcel.NameOfCustomerSended, parcelStatus = parcel.parcelStatus, pritorities = parcel.pritorities});
            }

            ParcelsListView.ItemsSource = items;
            view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelsListView.ItemsSource);

            PrioritySelector.ItemsSource = Enum.GetValues(typeof(BO.Pritorities));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));

        }
        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (BO.WeightCategories)MaxWeightSelector.SelectedItem;
            ParcelsListView.ItemsSource = bl.GetParcelsToListBy((d) => d.weightCategories == selected);
        }

        private void PrioritySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 

            var selected = (BO.Pritorities)PrioritySelector.SelectedItem;
            ParcelsListView.ItemsSource = bl.GetParcelsToListBy((d) => d.pritorities == selected);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            clearListView();
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("NameOfCustomerSended");
            view.GroupDescriptions.Add(groupDescription);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            clearListView();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            clearListView();
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("NameOfCustomerReciver");
            view.GroupDescriptions.Add(groupDescription);

        }
        private void clearListView()
        {
            items.Clear();
            items = new List<ParcelToList>();
            foreach (var parcel in bl.GetParcelToLists())
            {
                items.Add(new ParcelToList() { ID = parcel.ID, weightCategories = parcel.weightCategories, NameOfCustomerReciver = parcel.NameOfCustomerReciver, NameOfCustomerSended = parcel.NameOfCustomerSended, parcelStatus = parcel.parcelStatus, pritorities = parcel.pritorities });
            }

            ParcelsListView.ItemsSource = items;
            view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelsListView.ItemsSource);
        }
    }
}
