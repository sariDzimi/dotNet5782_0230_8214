using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Diagnostics;
using System.Windows;

namespace PO
{
    class Parcel_p : DependencyObject
    {

        //ID
        public static readonly DependencyProperty IdParcel =
      DependencyProperty.Register("ID",
                                  typeof(object),
                                  typeof(Parcel_p),
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



        //customer At Parcel Sender
        public static readonly DependencyProperty customerAtParcelSender =
      DependencyProperty.Register("CustomerAtParcelSender",
                                  typeof(object),
                                  typeof(Parcel_p),
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

        //customer At Parcel Reciver
        public static readonly DependencyProperty customerAtParcelReciver =
        DependencyProperty.Register("CustomerAtParcelReciver",
                                  typeof(object),
                                  typeof(Parcel_p),
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

        //Weight 
        public static readonly DependencyProperty weightParcel =
        DependencyProperty.Register("Weight",
                                 typeof(object),
                                 typeof(Parcel_p),
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


        //Pritority
        public static readonly DependencyProperty pritorityParcel =
        DependencyProperty.Register("Pritority",
                                typeof(object),
                                typeof(Parcel_p),
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

        //Requested
        public static readonly DependencyProperty requestedParcel =
        DependencyProperty.Register("Requested",
                               typeof(object),
                               typeof(Parcel_p),
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

        //IdDrone
        public static readonly DependencyProperty idDrone =
        DependencyProperty.Register("IdDrone",
                              typeof(object),
                              typeof(Parcel_p),
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

        //Scheduled
        public static readonly DependencyProperty ScheduledParcel =
        DependencyProperty.Register("Scheduled",
                              typeof(object),
                              typeof(Parcel_p),
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

        //PickedUp 
        public static readonly DependencyProperty PickedUpParcel =
        DependencyProperty.Register("PickedUp",
                             typeof(object),
                             typeof(Parcel_p),
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

        //Delivered
        public static readonly DependencyProperty DeliveredParcel =
        DependencyProperty.Register("Delivered",
                            typeof(object),
                            typeof(Parcel_p),
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

    }
}
