using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI_BL
{
        public class InvalidInput : Exception{
            public InvalidInput(string msg) : base($"connot conver input to {msg}") { }
        }
}
