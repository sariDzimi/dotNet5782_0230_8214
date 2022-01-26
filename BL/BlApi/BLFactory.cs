using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BL
{
     public static class BLFactory
    {
        public static IBL GetBl()
        {
            return BL.GetInstance;

        }
    }
}
