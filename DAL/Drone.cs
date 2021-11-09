using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct DroneDL
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }

            double battery { get; set; }
            public override string ToString()
            {
                return $"drone {Id} : {Model}";
            }
        }
    }
}