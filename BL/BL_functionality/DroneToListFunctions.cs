using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DAL;


namespace BL
{
    partial class BL
    {
        public Drone ConvertDroneToListToDrone(DroneToList droneToList)
        {
            return GetDroneById(droneToList.Id);
        }

        public IEnumerable<DroneToList> GetDroneToLists()
        {
            return from drone in GetDrones()
                   select ConvertDroneToDroneToList(drone);

        }

        public IEnumerable<DroneToList> GetDroneToListsBy(Predicate<Drone> findBy)
        {
            return from drone in GetDrones()
                   where findBy(drone)
                   select ConvertDroneToDroneToList(drone);
        }




    }
}

