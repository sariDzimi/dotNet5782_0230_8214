using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    /// <summary>
    /// a post that is being delivered by the company
    /// </summary>
    public class Parcel
    {
        public int Id { get; set; }
        public CustomerAtParcel customerAtParcelSender { get; set; }
        public CustomerAtParcel customerAtParcelReciver { get; set; }
        public WeightCategories Weight { get; set; }
        public Pritorities Pritority { get; set; }
        public DateTime? Requested { get; set; }
        public DroneAtParcel droneAtParcel { get; set; }
        public DateTime? Scheduled { get; set; }
        public DateTime? PickedUp { get; set; }
        public DateTime? Delivered { get; set; }

        public override string ToString()
        {
            return $"ParcelBL  : {Id}, " +
                $" customerAtParcelSender: {customerAtParcelSender}, customerAtParcelReciver: {customerAtParcelReciver}," +
                $" Weight: {Weight}, " +
                $"Pritority : {Pritority}, Requested: {Requested}, " +
                $"droneAtParcel: {droneAtParcel}, Scheduled : {Scheduled},PickedUp: {PickedUp}," +
                $" Delivered: {Delivered} ";
        }
    }
}


