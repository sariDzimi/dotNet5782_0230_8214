using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class Customer
    {
        public Customer(int id, string name, string phone, double Longitude, double Latitude)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Location = new Location(Longitude, Latitude);
            parcelsSentedByCustomer = new List<ParcelAtCustomer>();
            parcelsSentedToCustomer = new List<ParcelAtCustomer>();



        }

        public Customer()
        {
            parcelsSentedByCustomer = new List<ParcelAtCustomer>();

            parcelsSentedToCustomer = new List<ParcelAtCustomer>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public Location Location { get; set; }
        public override string ToString()
        {
            string parcelSentedByCustomer = " ";
            string parcelSentedToCustomer = " ";

            if (parcelsSentedByCustomer.Count != 0)
            {
                foreach (var p in parcelsSentedByCustomer)
                {
                    parcelSentedByCustomer += p;
                    parcelSentedByCustomer += " ";
                }
            }

            if (parcelsSentedToCustomer.Count != 0)
            {
                foreach (var p in parcelsSentedToCustomer)
                {
                    parcelSentedToCustomer += p;
                    parcelSentedToCustomer += " ";
                }
            }




            return $"customer {Name} : {Id}, {Phone}, Location : {Location}," +
                $"parcelsSentedByCustomer: {parcelSentedByCustomer}, parcelsSentedToCustomer: {parcelSentedToCustomer} ";
        }


        public List<ParcelAtCustomer> parcelsSentedByCustomer;
        public List<ParcelAtCustomer> parcelsSentedToCustomer;






    }
}

