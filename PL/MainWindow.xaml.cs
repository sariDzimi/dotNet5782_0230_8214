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
//using PL.PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public userType currentUser;
        public IBL bl;
        public MainWindow()
        {
            bl = BLFactory.GetBl();
            InitializeComponent();
        }
        public MainWindow(IBL bl)
        {
            this.bl = bl;
            InitializeComponent();
            new ManegerWindow().Show();
            Close();
        }

        /// <summary>
        /// if user is a valid manager, opens manager window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterManeger(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Manager manager = new BO.Manager() { UserName = getUserName(), Password = getPassword() };
                bool isValid = bl.IsValidMamager(manager);
                if (isValid)
                {
                    currentUser = userType.manager;
                    MessageBox.Show("you are in");
                    new ManegerWindow(bl, currentUser).Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("you are not maneger, please login as user");
                    new MainWindow(bl).Show();
                    Close();
                }
            }
            catch (BO.NotFound)
            {
                MessageBox.Show("you are not a maneger, please login as user");
            }

            catch (NotValidInput ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///gets input of password
        /// </summary>
        /// <returns>password</returns>
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

        /// <summary>
        /// gets input of user name
        /// </summary>
        /// <returns>user name</returns>
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

        /// <summary>
        /// gets input of id
        /// </summary>
        /// <returns>id</returns>
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

        /// <summary>
        /// gets input of signed name
        /// </summary>
        /// <returns>signed name</returns>
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

        /// <summary>
        /// gets input of signed id
        /// </summary>
        /// <returns>signed id</returns>
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

        /// <summary>
        /// gets input of signed longitude
        /// </summary>
        /// <returns>signed longitude</returns>
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

        /// <summary>
        /// gets input of signed phone
        /// </summary>
        /// <returns>signed phone</returns>
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

        /// <summary>
        /// gets input of signed latitude
        /// </summary>
        /// <returns>signed latitude</returns>
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

        /// <summary>
        /// gets input of loged name
        /// </summary>
        /// <returns>loged name</returns>
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

        /// <summary>
        /// log in if the customer is valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Customer customer = bl.GetCustomerById(getId());
                if (customer.Name == getNameLog())
                {
                    MessageBox.Show("you are in");
                    currentUser = userType.cutomer;
                    new CustomerWindow(bl, customer, currentUser).Show();
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

        /// <summary>
        /// signes up the custmer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void signUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Customer customer = new BO.Customer()
                {
                    Id = getIdSighnUp(),
                    Name = getNameSighnUp(),
                    Phone = getPhone(),
                    Location = new BO.Location() { Longitude = getLongitute(), Latitude = getLutitude()}
                };

                bl.AddCustomer(customer);
                currentUser = userType.cutomer;
                MessageBox.Show("you are in");
                new CustomerWindow(bl, customer, currentUser).Show();
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

