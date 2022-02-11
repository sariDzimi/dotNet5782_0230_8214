using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    /// <summary>
    /// individuals that purchase services from the company
    /// </summary>
    public class Customer { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }
        public override string ToString()
        {
            string parcelSentedByCustomer = " ";
            string parcelSentedToCustomer = " ";

            if (ParcelsSentedByCustomer.Count != 0)
            {
                foreach (var p in ParcelsSentedByCustomer)
                {
                    parcelSentedByCustomer += p;
                    parcelSentedByCustomer += " ";
                }
            }

            if (ParcelsSentedToCustomer.Count != 0)
            {
                foreach (var p in ParcelsSentedToCustomer)
                {
                    parcelSentedToCustomer += p;
                    parcelSentedToCustomer += " ";
                }
            }

            return $"customer {Name} : {Id}, {Phone}, Location : {Location}," +
                $"parcelsSentedByCustomer: {parcelSentedByCustomer}, parcelsSentedToCustomer: {parcelSentedToCustomer} ";
        }

        public List<ParcelAtCustomer> ParcelsSentedByCustomer;
        public List<ParcelAtCustomer> ParcelsSentedToCustomer;
    }
}

