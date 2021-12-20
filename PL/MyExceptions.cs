using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    class NotValidInput : Exception
    {
        public NotValidInput(string  e) : base($"{e}  Not valid")
        {
            
        }
    }

}
