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
        List<Parcel> items;
        public ParcelsList()
        {
            InitializeComponent();
        }

        public ParcelsList(IBL bL1)
        {
            WindowStyle = WindowStyle.None;

            InitializeComponent();
            bl = bL1;
/*            items = new List<Parcel>();
            foreach(Parcel parcel in bl.GetParcels())
            {
                items.Add(new Parcel() {Id = parcel.Id, Weight = parcel.Weight, PickedUp = parcel.PickedUp, customerAtParcelReciver = parcel.customerAtParcelReciver, customerAtParcelSender= parcel.customerAtParcelSender, Delivered = parcel.Delivered, Pritority= parcel.Pritority, Requested= parcel.Requested, Scheduled= parcel.Scheduled  });
            }*/

            ParcelsListView.ItemsSource = items;
            view = (CollectionView)CollectionViewSource.GetDefaultView(ParcelsListView.ItemsSource);
            
            PrioritySelector.ItemsSource = Enum.GetValues(typeof(BO.Pritorities));
            MaxWeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));

        }
        private void MaxWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (BO.WeightCategories)MaxWeightSelector.SelectedItem;
            ParcelsListView.ItemsSource = bl.GetParcelsBy((d) => d.Weight == selected);
        }

        private void PrioritySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (BO.Pritorities)PrioritySelector.SelectedItem;
            ParcelsListView.ItemsSource = bl.GetParcelsBy((d) => d.Pritority== selected);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ParcelsListView.ItemsSource = items;
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("customerAtParcelSender.Id");
            view.GroupDescriptions.Add(groupDescription);
        }
    }
}
