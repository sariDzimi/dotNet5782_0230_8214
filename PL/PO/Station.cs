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
    /// display object of Station
    /// </summary>
    public class Station : DependencyObject
    {
        public Action<BO.Station> ListChangedDelegate;

        #region freeChargeSlots
        public static readonly DependencyProperty freeChargeSlots =
       DependencyProperty.Register("numberOfFreeChargeSlots",
                                   typeof(object),
                                   typeof(Station),
                                   new UIPropertyMetadata(0));
        public int FreeChargeSlots
        {
            get
            {
                return (int)GetValue(freeChargeSlots);
            }
            set
            {
                if (value < 0)
                    throw new OutOfRange("chargeSlots");

                SetValue(freeChargeSlots, value);
            }
        }

        #endregion

        #region Id
        public static readonly DependencyProperty IdStation =
       DependencyProperty.Register("ID",
                                   typeof(object),
                                   typeof(Station),
                                   new UIPropertyMetadata(0));
        public int ID
        {
            get
            {
                return (int)GetValue(IdStation);
            }
            set
            {
                SetValue(IdStation, value);
            }
        }

        #endregion

        #region Name
        public static readonly DependencyProperty NameStation =
       DependencyProperty.Register("Name",
                                   typeof(object),
                                   typeof(Station),
                                   new UIPropertyMetadata(0));
        public int Name
        {
            get
            {
                return (int)GetValue(NameStation);
            }
            set
            {
                SetValue(NameStation, value);
            }
        }

        #endregion

        #region Location
        public static readonly DependencyProperty LongitudeStation =
       DependencyProperty.Register("Longitude",
                                   typeof(object),
                                   typeof(Station),
                                   new UIPropertyMetadata(0));
        public double Longitude
        {
            get
            {
                return (double)GetValue(LongitudeStation);
            }
            set
            {
                SetValue(LongitudeStation, value);
            }
        }

        public static readonly DependencyProperty LatitudeStation =
      DependencyProperty.Register("Latitude",
                                  typeof(object),
                                  typeof(Station),
                                  new UIPropertyMetadata(0));
        public double Latitude
        {
            get
            {
                return (double)GetValue(LatitudeStation);
            }
            set
            {
                SetValue(LatitudeStation, value);
            }
        }



        #endregion

        #region droneAtChargings

        public static readonly DependencyProperty droneAtCharging =
      DependencyProperty.Register("DroneAtCharging",
                                  typeof(object),
                                  typeof(Station),
                                  new UIPropertyMetadata(0));
        public List<DroneAtCharging> DroneAtCharging
        {
            get
            {
                return (List<DroneAtCharging>)GetValue(droneAtCharging);
            }
            set
            {
                SetValue(droneAtCharging, value);
            }
        }

        #endregion

    }
}
