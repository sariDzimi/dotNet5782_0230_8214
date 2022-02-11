using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// details of station when apears in list
    /// </summary>
    public class StationToList
    {
        public int Id { get; set; }

        public int Name { get; set; }

        public int NumberOfFreeChargeSlots { get; set; }

        public int NumberOfUsedChargeSlots { get; set; }

        public override string ToString()
        {
            return $"id:{Id} name:{Name} numbercharge slots- free:{NumberOfFreeChargeSlots}, used{NumberOfUsedChargeSlots}";
        }

    }
}

