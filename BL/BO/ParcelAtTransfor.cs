using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    /// <summary>
    /// details of parcel when in transfor
    /// </summary>
    public class ParcelAtTransfor
    {
        public int Id { get; set; }
        public DO.Pritorities Pritorities { get; set; }
        public CustomerAtParcel CustomerAtDeliverySender { get; set; }
        public CustomerAtParcel CustomerAtDeliveryReciver { get; set; }

        public override string ToString()
        {
            return $"ID  : {Id}, " +
                $" pritorities: {Pritorities}, customerAtDeliverySender: {CustomerAtDeliverySender}," +
                $" customerAtDeliveryReciver: {CustomerAtDeliveryReciver}, ";
            }

    }
}
