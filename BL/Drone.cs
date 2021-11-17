using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {

        public class DroneBL
        {

            public DroneBL(int id, string model, IDAL.DO.WeightCategories weightCategories, double battery)
            {
                Id = id;
                Model = model;
                MaxWeight = weightCategories;
                Battery = battery;

            }
            public DroneBL()
            {

            }
            private double battery;
            public int Id { get; set; }
            public string Model { get; set; }
            public IDAL.DO.WeightCategories MaxWeight { get; set; }

            public double Battery
            {
                get
                {
                    return Battery;
                }
                set
                {
                    if (value < 0 || value > 100)
                        throw new OutOfRange("battery");
                    battery = value;
                }
            }
            public DroneStatus DroneStatus { get; set; }

            public ParcelAtTransfor ParcelAtTransfor { get; set; }

            public Location Location { get; set; }

            public override string ToString()
            {
                return $"drone  : {Id}";
            }
        }

    }
}