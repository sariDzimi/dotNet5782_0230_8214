﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class NotFoundException : Exception
    {
        public NotFoundException(int Id):base($"Not found {Id}")
        {

        }
    }
}
