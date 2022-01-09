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
            //new ManegerWindow().Show();
            //Close();


        }
        public MainWindow(IBL bL1)
        {
            bL = bL1;
            InitializeComponent();
            new ManegerWindow().Show();

            Close();



        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    new DronesList(bL).Show();
        //    Close();
        //}

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    new ParcelsList(bL).Show();
        //    Close();
        //}
        //private void ButtonClick_OpenStationsList(object sender, RoutedEventArgs e)
        //{
        //    new StationsList(bL).Show();
        //    Close();
        //}

        //private void Button_Click_2(object sender, RoutedEventArgs e)
        //{
        //    new CustomersList(bL).Show();
        //    Close();
        //}

        //private void Button_Click_3(object sender, RoutedEventArgs e)
        //{
        //    //DronesList.Visibility = DronesList.Visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
        //}

        private void workerButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void managerButton_Click(object sender, RoutedEventArgs e)
        {
            new ManegerWindow(bL, currentUser).Show();
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
                    new ManegerWindow(bL, currentUser).Show();
                    Close();
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
        private int getId()
        {

            try
            {
                return Convert.ToInt32(IdTextBox.Text);
            }
            catch
            {
                throw new NotValidInput("Id");
            }
        }

        private string getNameSighnUp()
        {
            try
            {
                return SignUpNameTextBox.Text;
            }
            catch
            {
                throw new NotValidInput("UserName");
            }
        }

        private int getIdSighnUp()
        {

            try
            {
                return Convert.ToInt32(SignUpIdTextBox.Text);
            }
            catch
            {
                throw new NotValidInput("Id");
            }
        }

        private string getNameLogin()
        {
            try
            {
                return NameTextBox.Text;
            }
            catch
            {
                throw new NotValidInput("UserName");
            }
        }


        private double getLongitute()
        {
            try
            {
                return Convert.ToDouble($"{SignUpLongitudeTextBox.Text}");
            }
            catch
            {
                throw new NotValidInput("Longitute");
            }
        }

        private string getPhone()
        {
            try
            {
                return SignUpNameTextBox.Text;
            }
            catch
            {
                throw new NotValidInput("Phone");
            }
        }



        private double getLutitude()
        {
            try
            {
                return Convert.ToDouble($"{SignUpLatitudeTextBox.Text}");
            }
            catch
            {
                throw new NotValidInput("Lutitude");
            }
        }

        private string getNameLog()
        {
            try
            {
                return NameTextBox.Text;
            }
            catch
            {
                throw new NotValidInput("Name");
            }

        }

        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                BO.Customer customer = bL.GetCustomerById(getId());
                if ((customer.Name == getNameLog()))
                {
                    MessageBox.Show("you are in");
                    currentUser.Type = "Customer";
                    new CustomerWindow(bL, customer, currentUser).Show();
                }
                else
                {
                    MessageBox.Show("please sighn up");
                   
                }

            }
            catch (BO.NotFound)
            {
                MessageBox.Show("please sighn up");
               
            }

            catch (NotValidInput ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void SignUpSignInBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Customer customer = new BO.Customer() { Id = getIdSighnUp(), Name = getNameSighnUp(), Phone = getPhone(), Location = new BO.Location(getLutitude(), getLongitute()) };

                bL.addCustomerToDL(customer);
                currentUser.Type = "Customer";
                MessageBox.Show("you are in");
                new CustomerWindow(bL, customer, currentUser).Show();
                Close();

            }
            catch (BO.IdAlreadyExist ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

