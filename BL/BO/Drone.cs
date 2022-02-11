using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    /// <summary>
    /// unmanned aerial vehicle that delivers parcels
    /// </summary>
    public class Drone
    {
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
                    throw new OutOfRange("battery out of range");
                battery = Math.Floor((double)value * 100) / 100;
            }
        }
        public DroneStatus DroneStatus { get; set; }

        public ParcelInDelivery ParcelInDelivery { get; set; }

        public Location Location { get; set; }

        public override string ToString()
        {
            return $"drone  : {Id}, " +
                $" battery: {battery}%, Model: {Model}, MaxWeight: {MaxWeight}, " +
                $"DroneStatus : {DroneStatus}, ParcelAtTransfor: {ParcelInDelivery}," +
                $"Location: {Location}";

            ;
        }
    }

}
