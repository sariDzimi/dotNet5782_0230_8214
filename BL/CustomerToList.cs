using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace BO
    {
        class CustomerToList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public int NumberOfParcelsSendedAndProvided { get; set; }

            public int NumberOfParcelsSendedAndNotProvided { get; set; }

            public int NumberOfRecievedParcels { get; set; }

            public int NumberOfParcelsInTheWayToCutemor { get; set; }

        }

    }

