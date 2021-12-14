using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {

        public class Drone
        {

            public Drone(int id, string model, WeightCategories weightCategories, double battery)
            {
                Id = id;
                Model = model;
                MaxWeight = weightCategories;
                Battery = battery;
                ParcelInDelivery = new ParcelInDelivery();

            }
            public Drone()
            {
                ParcelInDelivery = new ParcelInDelivery();
                battery = 100;
            }

            private double battery;
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }

            public double Battery
            {
                get
                {
                    return battery;
                }
                set
                {
                    if (value < 0 || value > 100)
                        throw new OutOfRange("battery");
                    battery = value;
                }
            }
            public DroneStatus DroneStatus { get; set; }

            public ParcelInDelivery ParcelInDelivery { get; set; }

            public Location Location { get; set; }

            public override string ToString()
            {
                return $"drone  : {Id}, " +
                    $" battery: {battery}, Model: {Model}, MaxWeight: {MaxWeight}, " +
                    $"DroneStatus : {DroneStatus}, ParcelAtTransfor: {ParcelInDelivery}," +
                    $"Location: {Location}";

                    ;
            }
        }

    }
}