using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class OutOfRange : Exception
        {
            public OutOfRange(string message) : base(message + " out of range"){}
        }
    }
}
