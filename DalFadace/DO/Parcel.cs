﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DO
{
    /// <summary>
    /// a post that is being delivered by the company
    /// </summary>
    public struct Parcel
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int TargetId { get; set; }
        public WeightCategories Weight { get; set; }
        public Pritorities Pritority { get; set; }
        public DateTime? Requested { get; set; }
        public int DroneId { get; set; }
        public DateTime? Scheduled { get; set; }

        public DateTime? PickedUp { get; set; }

        public DateTime? Delivered { get; set; }

        public bool IsActive { get; set; }

        public override string ToString()
        {
            return $"sender {SenderId} : {Id}";
        }
    }

}
