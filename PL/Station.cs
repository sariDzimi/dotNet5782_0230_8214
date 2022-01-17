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
    public class Station_p : DependencyObject
    {
        //freeChargeSlots
        public static readonly DependencyProperty freeChargeSlots =
       DependencyProperty.Register("FreeChargeSlots",
                                   typeof(object),
                                   typeof(Station_p),
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


        //ID
        public static readonly DependencyProperty IdStation =
       DependencyProperty.Register("ID",
                                   typeof(object),
                                   typeof(Station_p),
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

        //Name
        public static readonly DependencyProperty NameStation =
       DependencyProperty.Register("Name",
                                   typeof(object),
                                   typeof(Station_p),
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

        //Location
        public static readonly DependencyProperty LongitudeStation =
       DependencyProperty.Register("Longitude",
                                   typeof(object),
                                   typeof(Station_p),
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
                                  typeof(Station_p),
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

        //droneAtChargings 

        public static readonly DependencyProperty droneAtCharging =
      DependencyProperty.Register("DroneAtCharging",
                                  typeof(object),
                                  typeof(Station_p),
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




        //public int Id { get; set; }
        //public int Name { get; set; }
        //public Location Location { get; set; }

        //public int FreeChargeSlots
        //{
        //    get
        //    {
        //        return freeChargeSlots;
        //    }
        //    set
        //    {
        //        if (value < 0)
        //            throw new OutOfRange("chargeSlots");
        //        freeChargeSlots = value;
        //    }
        //}
        public override string ToString()
        {
            string droneAtCharging = " ";
            if (DroneAtCharging.Count != 0)
            {
                foreach (var d in DroneAtCharging)
                {
                    droneAtCharging += d;
                    droneAtCharging += " ";
                }
            }

            return $"station {Name} : {ID}, 'Location', 'ChargeSlots': {FreeChargeSlots}," +
                $"{droneAtCharging}  ";
        }

        //public List<DroneAtCharging> droneAtChargings;


    }
}
