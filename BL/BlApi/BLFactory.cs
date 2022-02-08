using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BL
{
    /// <summary>
    /// defines object of BL
    /// </summary>
    public static class BLFactory
    {
        /// <summary>
        /// gets an instance of BL
        /// </summary>
        /// <returns>instance of BL</returns>
        public static IBL GetBl()
        {
            return BL.GetInstance;
        }
    }
}
