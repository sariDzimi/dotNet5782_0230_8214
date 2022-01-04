using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string  e) : base($"{e}  Not found")
        {
        }

    }
    public class IdAlreadyExist : Exception
    {
        public IdAlreadyExist(int Id) : base($"id:{Id} already exsist") { }
        
    }

   public class NoSuchInstance: Exception
    {
        public NoSuchInstance() : base("no such instance") { }
    }


    public class ListEmpty : Exception
    {
        public ListEmpty(string list) : base(list + " is empty") { }
    }
}
