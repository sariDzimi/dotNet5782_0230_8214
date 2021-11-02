using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Parcel
        {
           IDAL.DO.Parcel parcel;
            public Parcel()
            {
                parcel = new IDAL.DO.Parcel();
            }


            public override string ToString()
            {
                return $"sender {parcel.SenderId} : {parcel.Id}";
            }

        }

    }

}
