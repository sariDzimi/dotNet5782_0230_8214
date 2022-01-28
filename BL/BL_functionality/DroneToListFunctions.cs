using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace BL
{
    partial class BL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone ConvertDroneToListToDrone(DroneToList droneToList)
        {
            return GetDroneById(droneToList.Id);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetDroneToLists()
        {
            return from drone in GetDrones()
                   select ConvertDroneToDroneToList(drone);

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetDroneToListsBy(Predicate<Drone> findBy)
        {
            return from drone in GetDrones()
                   where findBy(drone)
                   select ConvertDroneToDroneToList(drone);
        }




    }
}

