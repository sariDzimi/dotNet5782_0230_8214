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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using BO;
using PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelListPage.xaml
    /// </summary>
    public partial class ParcelListPage : Page
    {
        public ParcelListPage()
        {
            InitializeComponent();
        }
        public CurrentUser currentUser = new CurrentUser();

        private IBL bl;
        CollectionView view;
        List<ParcelToList> items;

        public ParcelListPage(IBL bL1, CurrentUser currentUser1)
        {
            currentUser = currentUser1;
            InitializeComponent();
            bl = bL1;
            items = bl.GetParcelToLists().ToList();
            ParcelsListView.ItemsSource = items;
            view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelsListView.ItemsSource);

            PrioritySelector.ItemsSource = Enum.GetValues(typeof(BO.Pritorities));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));

        }

        private void MouseDoubleClick_ParcelChoosen(object sender, MouseButtonEventArgs e)
        {
            ParcelToList parcelToList = (sender as ListView).SelectedValue as ParcelToList;
            BO.Parcel parcelBL = bl.FindParcel(parcelToList.ID);
            Window.GetWindow(this).Content = new ParcelPage(bl, parcelBL, currentUser, this);
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
            items = bl.GetParcelToLists().ToList();
            ParcelsListView.ItemsSource = items;
            view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelsListView.ItemsSource);
        }

        private void AddParcelButton(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content =  new ParcelPage(bl, currentUser, this);

        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            new ManegerWindow(bl, currentUser).Show();
            Window.GetWindow(this).Close();
        }
    }
}
