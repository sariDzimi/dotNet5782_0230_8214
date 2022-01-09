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
    class Drone_p : DependencyObject
    {

        public void Update(BO.Drone drone)
        {
            ID = drone.Id;
            Battery = drone.Battery;
            Model = drone.Model;
            DroneStatus = drone.DroneStatus;
            MaxWeight = drone.MaxWeight;
            ParcelInDelivery = drone.ParcelInDelivery;
            Location = drone.Location;
        }
        //ID
        public static readonly DependencyProperty IdDrone =
       DependencyProperty.Register("ID",
                                   typeof(object),
                                   typeof(Drone_p),
                                   new UIPropertyMetadata(0));
        public int ID
        {
            get
            {
                return (int)GetValue(IdDrone);
            }
            set
            {
                SetValue(IdDrone, value);
            }
        }

        //Battery
        public static readonly DependencyProperty BatteryDrone =
       DependencyProperty.Register("Battery",
                                   typeof(object),
                                   typeof(Drone_p),
                                   new UIPropertyMetadata(0));
        public double Battery
        {
            get
            {
                return (double)GetValue(BatteryDrone);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new OutOfRange("battery");
                SetValue(BatteryDrone, value);
            }
        }

        //Model
        public static readonly DependencyProperty ModelDrone =
       DependencyProperty.Register("Model",
                                   typeof(object),
                                   typeof(Drone_p),
                                   new UIPropertyMetadata(0));
        public string Model
        {
            get
            {
                return (string)GetValue(ModelDrone);
            }
            set
            {
                SetValue(ModelDrone, value);
            }
        }

        //DroneStatus
        public static readonly DependencyProperty DroneStatusDrone =
      DependencyProperty.Register("DroneStatus",
                                  typeof(object),
                                  typeof(Drone_p),
                                  new UIPropertyMetadata(0));
        public DroneStatus DroneStatus
        {
            get
            {
                return (DroneStatus)GetValue(DroneStatusDrone);
            }
            set
            {
                SetValue(DroneStatusDrone, value);
            }
        }

        //MaxWeight
        public static readonly DependencyProperty MaxWeightDrone =
      DependencyProperty.Register("Weight",
                                  typeof(object),
                                  typeof(Drone_p),
                                  new UIPropertyMetadata(0));
        public WeightCategories MaxWeight
        {
            get
            {
                return (WeightCategories)GetValue(MaxWeightDrone);
            }
            set
            {
                SetValue(MaxWeightDrone, value);
            }
        }

        //ParcelInDelivery
        public static readonly DependencyProperty parcelInDelivery =
     DependencyProperty.Register("ParcelInDelivery",
                                 typeof(object),
                                 typeof(Drone_p),
                                 new UIPropertyMetadata(0));
        public ParcelInDelivery ParcelInDelivery
        {
            get
            {
                return (ParcelInDelivery)GetValue(parcelInDelivery);
            }
            set
            {
                SetValue(parcelInDelivery, value);
            }
        }

        //Location
        public static readonly DependencyProperty location =
    DependencyProperty.Register("Location",
                                typeof(object),
                                typeof(Drone_p),
                                new UIPropertyMetadata(0));
        public Location Location
        {
            get
            {
                return (Location)GetValue(location);
            }
            set
            {
                SetValue(location, value);
            }
        }

        //private double battery;
        //public int Id { get; set; }
        //public string Model { get; set; }
        //public WeightCategories MaxWeight { get; set; }

        //public double Battery
        //{
        //    get
        //    {
        //        return battery;
        //    }
        //    set
        //    {
        //        if (value < 0 || value > 100)
        //            throw new OutOfRange("battery");
        //        battery = Math.Floor((double)value * 100) / 100;
        //    }
        //}
        //public DroneStatus DroneStatus { get; set; }

        //public ParcelInDelivery ParcelInDelivery { get; set; }

        //public Location Location { get; set; }

        public override string ToString()
        {
            return $"drone  : {ID}, " +
                $" battery: {Battery}%, Model: {Model}, MaxWeight: {MaxWeight}, " +
                $"DroneStatus : {DroneStatus}, ParcelAtTransfor: {ParcelInDelivery}," +
                $"Location: {Location}";

            ;
        }
    }
}
