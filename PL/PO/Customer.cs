using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using BO;
namespace PO
{
    class Customer_p : DependencyObject
    {
        
        public override string ToString()
        {
            string parcelSentedByCustomer = " ";
            string parcelSentedToCustomer = " ";

            if (ParcelsSentedByCustomer.Count != 0)
            {
                foreach (var p in ParcelsSentedByCustomer)
                {
                    parcelSentedByCustomer += p;
                    parcelSentedByCustomer += " ";
                }
            }

            if (ParcelsSentedToCustomer.Count != 0)
            {
                foreach (var p in ParcelsSentedToCustomer)
                {
                    parcelSentedToCustomer += p;
                    parcelSentedToCustomer += " ";
                }
            }




            return $"customer {Name} :  {Phone}, Location :," +
             $"parcelsSentedByCustomer: {parcelSentedByCustomer}, parcelsSentedToCustomer: {parcelSentedToCustomer} ";
        }

        //Name
        public static readonly DependencyProperty NameCustomer =
       DependencyProperty.Register("Name",
                                   typeof(object),
                                   typeof(Customer_p),
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

        //ID
        public static readonly DependencyProperty IdCustomer =
        DependencyProperty.Register("ID",
                                    typeof(object),
                                    typeof(Customer_p),
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

        //Phone
        public static readonly DependencyProperty PhoneCustomer =
        DependencyProperty.Register("Phone",
                                typeof(object),
                                typeof(Customer_p),
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


        //Location
        public static readonly DependencyProperty LongitudeCustomer =
      DependencyProperty.Register("Longitude",
                                  typeof(object),
                                  typeof(Station_p),
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

        public static readonly DependencyProperty LatitudeCustomer=
      DependencyProperty.Register("Latitude",
                                  typeof(object),
                                  typeof(Station_p),
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
        //parcels Sented By Customer
        public static readonly DependencyProperty parcelsSentedByCustomer =
       DependencyProperty.Register("parcelsSentedByCustomer",
                               typeof(object),
                               typeof(Customer_p),
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

        //
        //parcels Sented To Customer
        public static readonly DependencyProperty parcelsSentedToCustomer =
        DependencyProperty.Register("parcelsSentedToCustomer",
                                typeof(object),
                                typeof(Customer_p),
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


    }

}
