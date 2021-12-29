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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CurrentUser currentUser = new CurrentUser();
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new CustomersList(bL).Show();
            Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //DronesList.Visibility = DronesList.Visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
        }

        private void workerButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void managerButton_Click(object sender, RoutedEventArgs e)
        {
            new ManegerWindow(bL, currentUser).Show();
            Close();
        }

        private void customerButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void EnterManeger(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Manager manager = new BO.Manager() { UserName = getUserName(), Password = getPassword() };
                bool flag;
                flag = bL.CheckWorkerIfExixst(manager);
                if (flag == true)
                {
                    currentUser.Type = "Maneger";
                    MessageBox.Show("you are in");
                }



                else
                {
                    MessageBox.Show("you are not maneger, please login like user");
                    new MainWindow(bL).Show();
                    Close();
                }
            }
            catch (BO.NotFound)
            {
                MessageBox.Show("you are not maneger, please login like user");
                new MainWindow(bL).Show();
                Close();
            }

            catch (NotValidInput ex)
            {
                MessageBox.Show(ex.Message);

            }


        }


        private int getPassword()
        {

            try
            {
                return Convert.ToInt32(PassWordText.Text);
            }
            catch
            {
                throw new NotValidInput("Password");
            }
        }

        private string getUserName()
        {
            try
            {
                return UserNameText.Text;
            }
            catch
            {
                throw new NotValidInput("UserName");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            new Window1().Show();
        }
    }
}

