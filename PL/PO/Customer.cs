using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using BO;
using PL;

namespace PO
{
    /// <summary>
    /// display object of Customer
    /// </summary>
    public class Customer : DependencyObject
    {
        public Action<BO.Customer> ListChangedDelegate;

        #region Name
        public static readonly DependencyProperty NameCustomer =
       DependencyProperty.Register("Name",
                                   typeof(object),
                                   typeof(Customer),
                                   new UIPropertyMetadata(0));
        public string Name
        {
            get
            {
                return (string)GetValue(NameCustomer);
            }
            set
            {
                SetValue(NameCustomer, value);
            }
        }
        #endregion

        #region Id
        public static readonly DependencyProperty IdCustomer =
        DependencyProperty.Register("ID",
                                    typeof(object),
                                    typeof(Customer),
                                    new UIPropertyMetadata(0));

        public int Id
        {
            get
            {
                return (int)GetValue(IdCustomer);
            }
            set
            {
                SetValue(IdCustomer, value);
            }
        }
        #endregion

        #region Phone
        public static readonly DependencyProperty PhoneCustomer =
        DependencyProperty.Register("Phone",
                                typeof(object),
                                typeof(Customer),
                                new UIPropertyMetadata(0));

        public string Phone
        {
            get
            {
                return (string)GetValue(PhoneCustomer);
            }
            set
            {
                SetValue(PhoneCustomer, value);
            }
        }

        #endregion

        #region Longitude
        public static readonly DependencyProperty LongitudeCustomer =
        DependencyProperty.Register("Longitude",
                                  typeof(object),
                                  typeof(Station),
                                  new UIPropertyMetadata(0));
        public double Longitude
        {
            get
            {
                return (double)GetValue(LongitudeCustomer);
            }
            set
            {
                SetValue(LongitudeCustomer, value);
            }
        }

        #endregion

        #region Latitude
        public static readonly DependencyProperty LatitudeCustomer =
        DependencyProperty.Register("Latitude",
                                  typeof(object),
                                  typeof(Station),
                                  new UIPropertyMetadata(0));
        public double Latitude
        {
            get
            {
                return (double)GetValue(LatitudeCustomer);
            }
            set
            {
                SetValue(LatitudeCustomer, value);
            }
        }

        #endregion

        #region parcels Sented By Customer
        public static readonly DependencyProperty parcelsSentedByCustomer =
        DependencyProperty.Register("parcelsSentedByCustomer",
                               typeof(object),
                               typeof(Customer),
                               new UIPropertyMetadata(0));

        public List<ParcelAtCustomer> ParcelsSentedByCustomer
        {
            get
            {
                return (List<ParcelAtCustomer>)GetValue(parcelsSentedByCustomer);
            }
            set
            {
                SetValue(parcelsSentedByCustomer, value);
            }
        }

        #endregion

        #region parcels Sented To Customer
        public static readonly DependencyProperty parcelsSentedToCustomer =
        DependencyProperty.Register("parcelsSentedToCustomer",
                                typeof(object),
                                typeof(Customer),
                                new UIPropertyMetadata(0));

        public List<ParcelAtCustomer> ParcelsSentedToCustomer
        {
            get
            {
                return (List<ParcelAtCustomer>)GetValue(parcelsSentedToCustomer);
            }
            set
            {
                SetValue(parcelsSentedToCustomer, value);
            }
        }
        #endregion

        #region NumberOfParcelsInTheWayToCutemor

        public static readonly DependencyProperty numberOfParcelsInTheWayToCutemor =
        DependencyProperty.Register("NumberOfParcelsInTheWayToCutemor",
                               typeof(object),
                               typeof(Customer),
                               new UIPropertyMetadata(0));

        public int NumberOfParcelsInTheWayToCutemor
        {
            get
            {
                return (int)GetValue(numberOfParcelsInTheWayToCutemor);
            }
            set
            {
                SetValue(numberOfParcelsInTheWayToCutemor, value);
            }
        }

        #endregion

        #region NumberOfRecievedParcels
        public static readonly DependencyProperty numberOfRecievedParcels =
      DependencyProperty.Register("NumberOfRecievedParcels",
                              typeof(object),
                              typeof(Customer),
                              new UIPropertyMetadata(0));

        public int NumberOfRecievedParcels
        {
            get
            {
                return (int)GetValue(numberOfRecievedParcels);
            }
            set
            {
                SetValue(numberOfRecievedParcels, value);
            }
        }

        #endregion

        #region NumberOfParcelsSendedAndNotProvided

        public static readonly DependencyProperty numberOfParcelsSendedAndNotProvided =
        DependencyProperty.Register("NumberOfParcelsSendedAndNotProvided",
                             typeof(object),
                             typeof(Customer),
                             new UIPropertyMetadata(0));

        public int NumberOfParcelsSendedAndNotProvided
        {
            get
            {
                return (int)GetValue(numberOfParcelsSendedAndNotProvided);
            }
            set
            {
                SetValue(numberOfParcelsSendedAndNotProvided, value);
            }
        }

        #endregion

        #region NumberOfParcelsSendedAndProvided
        public static readonly DependencyProperty numberOfParcelsSendedAndProvided =
        DependencyProperty.Register("NumberOfParcelsSendedAndProvided",
                            typeof(object),
                            typeof(Customer),
                            new UIPropertyMetadata(0));

        public int NumberOfParcelsSendedAndProvided
        {
            get
            {
                return (int)GetValue(numberOfParcelsSendedAndProvided);
            }
            set
            {
                SetValue(numberOfParcelsSendedAndProvided, value);
            }
        }

        #endregion

        #region update

        /// <summary>
        /// updates Customer to be as the BL object
        /// </summary>
        /// <param name="customer">updated customer</param>
        public void updateDisplayObject(BO.Customer customer)
        {
            Latitude = customer.Location.Latitude;
            Longitude = customer.Location.Longitude;
            Id = customer.Id;
            Name = customer.Name;
        }

        /// <summary>
        /// updates Customer to be as CustomerToList
        /// </summary>
        /// <param name="customer">CustomerToList</param>
        public void updateFromCustomerToList(CustomerToList customer)
        {
            NumberOfParcelsInTheWayToCutemor = customer.NumberOfParcelsInTheWayToCutemor;
            NumberOfParcelsSendedAndNotProvided = customer.NumberOfParcelsSendedAndNotProvided;
            NumberOfParcelsSendedAndProvided = customer.NumberOfParcelsSendedAndProvided;
            NumberOfRecievedParcels = customer.NumberOfRecievedParcels;
        }
        #endregion
    }

}
