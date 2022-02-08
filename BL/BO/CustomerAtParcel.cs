using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    /// <summary>
    /// detatils of customer that apear on a parcel
    /// </summary>
    public class CustomerAtParcel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $" id: {Id}, " +
                $" Name: {Name} ";
        }

    }
}

