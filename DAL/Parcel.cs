using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        class Parcel
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int Target { get; set; }
            public weightCategories Weight { get; set; }
            public pritorities Pritority { get; set; }
            public DateTime Requested { get; set; }
            public int DroneId { get; set; }

            public DateTime Scheduled { get; set; }

            public DateTime PickedUp { get; set; }

            public DateTime Delivered { get; set; }


        }

    }
   
}
