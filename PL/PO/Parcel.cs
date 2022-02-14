using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Diagnostics;
using System.Windows;
using PL;

namespace PO
{
    /// <summary>
    /// display object of Parcel
    /// </summary>
     public class Parcel : DependencyObject
    {
        public Action<BO.Parcel> ListChangedDelegate;
        
        #region Id
        public static readonly DependencyProperty IdParcel =
      DependencyProperty.Register("ID",
                                  typeof(object),
                                  typeof(Parcel),
                                  new UIPropertyMetadata(0));
        public int ID
        {
            get
            {
                return (int)GetValue(IdParcel);
            }
            set
            {
                SetValue(IdParcel, value);
            }
        }

        #endregion

        #region NameOfReciver
        public static readonly DependencyProperty nameOfReciver =
      DependencyProperty.Register("NameOfReciver",
                                  typeof(object),
                                  typeof(Parcel),
                                  new UIPropertyMetadata(0));
        public string NameOfReciver
        {
            get
            {
                return (string)GetValue(nameOfReciver);
            }
            set
            {
                SetValue(nameOfReciver, value);
            }
        }

        #endregion

        #region NameOfReciver
        public static readonly DependencyProperty parcelStatus =
       DependencyProperty.Register("ParcelStatus",
                                   typeof(object),
                                   typeof(Parcel),
                                   new UIPropertyMetadata(0));
        public ParcelStatus ParcelStatus
        {
            get
            {
                return (ParcelStatus)GetValue(parcelStatus);
            }
            set
            {
                SetValue(parcelStatus, value);
            }
        }

        #endregion

        #region NameOfSender
        public static readonly DependencyProperty nameOfSender =
       DependencyProperty.Register("NameOfSender",
                                   typeof(object),
                                   typeof(Parcel),
                                   new UIPropertyMetadata(0));
        public string NameOfSender
        {
            get
            {
                return (string)GetValue(nameOfSender);
            }
            set
            {
                SetValue(nameOfSender, value);
            }
        }
        #endregion

        #region customer At Parcel Sender
        public static readonly DependencyProperty customerAtParcelSender =
      DependencyProperty.Register("CustomerAtParcelSender",
                                  typeof(object),
                                  typeof(Parcel),
                                  new UIPropertyMetadata(0));
        public CustomerAtParcel CustomerAtParcelSender
        {
            get
            {
                return (CustomerAtParcel)GetValue(customerAtParcelSender);
            }
            set
            {
                SetValue(customerAtParcelSender, value);
            }
        }
        #endregion

        #region customer At Parcel Reciver
        public static readonly DependencyProperty customerAtParcelReciver =
        DependencyProperty.Register("CustomerAtParcelReciver",
                                  typeof(object),
                                  typeof(Parcel),
                                  new UIPropertyMetadata(0));
        public CustomerAtParcel CustomerAtParcelReciver
        {
            get
            {
                return (CustomerAtParcel)GetValue(customerAtParcelReciver);
            }
            set
            {
                SetValue(customerAtParcelReciver, value);
            }
        }
        #endregion

        #region Weight
        public static readonly DependencyProperty weightParcel =
        DependencyProperty.Register("Weight",
                                 typeof(object),
                                 typeof(Parcel),
                                 new UIPropertyMetadata(0));
        public WeightCategories Weight
        {
            get
            {
                return (WeightCategories)GetValue(weightParcel);
            }
            set
            {
                SetValue(weightParcel, value);
            }
        }
        #endregion

        #region Pritority
        public static readonly DependencyProperty pritorityParcel =
        DependencyProperty.Register("Pritority",
                                typeof(object),
                                typeof(Parcel),
                                new UIPropertyMetadata(0));
        public Pritorities Pritority
        {
            get
            {
                return (Pritorities)GetValue(pritorityParcel);
            }
            set
            {
                SetValue(pritorityParcel, value);
            }
        }
        #endregion

        #region Requested
        public static readonly DependencyProperty requestedParcel =
        DependencyProperty.Register("Requested",
                               typeof(object),
                               typeof(Parcel),
                               new UIPropertyMetadata(0));
        public DateTime? Requested
        {
            get
            {
                return (DateTime?)GetValue(requestedParcel);
            }
            set
            {
                SetValue(requestedParcel, value);
            }
        }

        #endregion

        #region IdDrone
        public static readonly DependencyProperty idDrone =
        DependencyProperty.Register("IdDrone",
                              typeof(object),
                              typeof(Parcel),
                              new UIPropertyMetadata(0));
        public int IdDrone
        {
            get
            {
                return (int)GetValue(idDrone);
            }
            set
            {
                SetValue(idDrone, value);
            }
        }

        #endregion

        #region Scheduled
        public static readonly DependencyProperty ScheduledParcel =
        DependencyProperty.Register("Scheduled",
                              typeof(object),
                              typeof(Parcel),
                              new UIPropertyMetadata(0));
        public DateTime? Scheduled
        {
            get
            {
                return (DateTime?)GetValue(ScheduledParcel);
            }
            set
            {
                SetValue(ScheduledParcel, value);
            }
        }
        #endregion

        #region PickedUp
        public static readonly DependencyProperty PickedUpParcel =
        DependencyProperty.Register("PickedUp",
                             typeof(object),
                             typeof(Parcel),
                             new UIPropertyMetadata(0));
        public DateTime? PickedUp
        {
            get
            {
                return (DateTime?)GetValue(PickedUpParcel);
            }
            set
            {
                SetValue(PickedUpParcel, value);
            }
        }

        #endregion

        #region Delivered
        public static readonly DependencyProperty DeliveredParcel =
        DependencyProperty.Register("Delivered",
                            typeof(object),
                            typeof(Parcel),
                            new UIPropertyMetadata(0));
        public DateTime? Delivered
        {
            get
            {
                return (DateTime?)GetValue(DeliveredParcel);
            }
            set
            {
                SetValue(DeliveredParcel, value);
            }
        }
        #endregion
    }
}
