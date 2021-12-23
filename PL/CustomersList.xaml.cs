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
    /// Interaction logic for CustomerList.xaml
    /// </summary>
    public partial class CustomersList : Window
    {
        private IBL bl;
        public CustomersList()
        {
            InitializeComponent();
        }

        public CustomersList(IBL blArg)
        {
            InitializeComponent();
            bl = blArg;
            WindowStyle = WindowStyle.None;
            customersListView.ItemsSource = bl.GetCustomerToLists();

        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow(bl).Show();
            Close();
        }
    }
}
