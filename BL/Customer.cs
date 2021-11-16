using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class CustomerBL
        {
            public CustomerBL(int id, string name, string phone, double Longitude, double Latitude)
            {
                Id = id;
                Name = name;
                Phone = phone;
                Location = new Location(Longitude, Latitude);
                parcelsSentedByCustomer = new List<ParcelAtCustomer>();
                parcelsSentedToCustomer = new List<ParcelAtCustomer>();



            }
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }

            public Location Location { get; set; }
            public override string ToString()
            {
                return $"customer {Name} : {Id}";
            }


            List<ParcelAtCustomer> parcelsSentedByCustomer;
            List<ParcelAtCustomer> parcelsSentedToCustomer;

          


           
            
        }
    }
}
