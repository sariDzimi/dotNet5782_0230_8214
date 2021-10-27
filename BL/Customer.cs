using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Customer
        {

            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }

            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public override string ToString()
            {
                return $"customer {Name} : {Id}";
            }

        }


    }
}
