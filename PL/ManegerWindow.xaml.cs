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
using BL;

namespace PL
{
    /// <summary>
    /// Interaction logic for ManegerWindow.xaml
    /// </summary>
    public partial class ManegerWindow : Window
    {
        public CurrentUser currentUser = new CurrentUser();
        public IBL bL1;

        public ManegerWindow()
        {
            InitializeComponent();
        }

        public ManegerWindow(IBL bL, CurrentUser CurrentUser1)
        {
            bL1 = bL;
            currentUser = CurrentUser1;
            InitializeComponent();
            CurrentUser.Text = currentUser.Type.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DronesList(bL1, currentUser).Show();
            //this.Content = new DroneListPage(bL1, currentUser);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //this.Content = new ParcelListPage(bL1, currentUser);
            new ParcelsList(bL1, currentUser).Show();
        }
        private void ButtonClick_OpenStationsList(object sender, RoutedEventArgs e)
        {
            new StationsList(bL1, currentUser).Show();
            //this.Content = new StationListPage(bL1, currentUser);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //this.Content = new CustomerListPage(bL1, currentUser);
            new CustomersList(bL1, currentUser).Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
           DronesList.Visibility = DronesList.Visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
        }


        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //this.Content = new StationListPage(bL1, currentUser);
            new StationsList(bL1, currentUser).Show();
        }

/*        private void openWindowList(Window window)
        {
            Hide();
            window.ShowDialog();
            Show();
        }*/

        //private void Enter(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        BO.Manager manager = new BO.Manager() { UserName = getUserName(), Password = getPassword() };
        //        bool flag;
        //        flag = bL1.CheckWorkerIfExixst(manager);
        //        if (flag == true)
        //        {
        //            currentUser.Type = "Maneger";
        //            MessageBox.Show("you are in");
        //        }



        //        else
        //        {
        //            MessageBox.Show("you are not maneger, please login like user");
        //            new MainWindow(bL1).Show();
        //            Close();
        //        }
        //    }
        //    catch (BO.NotFound)
        //    {
        //        MessageBox.Show("you are not maneger, please login like user");
        //        new MainWindow(bL1).Show();
        //        Close();
        //    }

        //    catch (NotValidInput ex)
        //    {
        //        MessageBox.Show(ex.Message);

        //    }


        //}


        //private int getPassword()
        //{

        //    try
        //    {
        //        return Convert.ToInt32(PassWordText.Text);
        //    }
        //    catch
        //    {
        //        throw new NotValidInput("Password");
        //    }
        //}

        //private string getUserName()
        //{
        //    try
        //    {
        //        return UserNameText.Text;
        //    }
        //    catch
        //    {
        //        throw new NotValidInput("UserName");
        //    }
        //}


    }
}

