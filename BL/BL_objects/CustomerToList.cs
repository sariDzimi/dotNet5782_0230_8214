using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class CustomerToList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int NumberOfParcelsSendedAndProvided { get; set; }

        public int NumberOfParcelsSendedAndNotProvided { get; set; }

        public int NumberOfRecievedParcels { get; set; }

        public int NumberOfParcelsInTheWayToCutemor { get; set; }

        public override string ToString()
        {
            return $"id:{Id}, name:{Name}, phone{Phone}, number of parcels sended and provided{NumberOfParcelsSendedAndNotProvided}," +
                $"number of parcels sended and not provide:{NumberOfParcelsSendedAndNotProvided}," +
                $"number of recivieved parcels:{NumberOfRecievedParcels}" +
                $"number of parcels in the way to the customer{NumberOfParcelsInTheWayToCutemor}";
        }

    }

}

