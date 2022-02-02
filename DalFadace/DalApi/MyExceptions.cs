using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string obj, int id) : base($"{obj} : {id} is not found")
        {
        }

    }
    public class IdAlreadyExistException : Exception
    {
        public IdAlreadyExistException(string obj, int Id) : base($"{obj} with id:{Id} already exsist") { }
        
    }

    public class ListIsEmptyException : Exception
    {
        public ListIsEmptyException(string list) : base(list + " is empty") { }
    }
}
