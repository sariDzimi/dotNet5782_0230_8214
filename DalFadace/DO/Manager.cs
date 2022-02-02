using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// a type of a user who has full access control
    /// </summary>
    public class Manager
    {
        public string UserName { get; set; }
        public int Password { get; set; }

        public Manager()
        {

        }

    }
}
