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
        public IEnumerable<Drone> GetDronesBy(Predicate<Drone> findBy)
        {
            return from drone in GetDrones()
                   where findBy(drone)
                   select drone;
        }

        public IEnumerable<DroneToList> GetDroneToListsBy(Predicate<Drone> findBy)
        {
            return from drone in GetDrones()
                   where findBy(drone)
                   select ConvertDroneToDroneToList(drone);
        }




    }
}
