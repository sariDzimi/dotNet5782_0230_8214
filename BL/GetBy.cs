using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace BL
{
    public partial class BL
    { 
        public IEnumerable<DroneBL> GetDronesBy(Predicate<DroneBL> findBy)
        {
            return from drone in GetDrones()
                   where findBy(drone)
                   select drone;
        }





    }
}
