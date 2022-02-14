using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    class NotValidInput : Exception
    {
        /// <summary>
        /// Exception of not valid input
        /// </summary>
        /// <param name="e"></param>
        public NotValidInput(string  e) : base($"{e} not valid")
        {
            
        }
    }
}
