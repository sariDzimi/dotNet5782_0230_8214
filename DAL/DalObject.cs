using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;
namespace DalObject
{
    public class DalObject
    {
        public DalObject()
        {
            DataSource.Initialize();
        }

        public void addDrone(Drone drone)
        {
            DataSource.drones[DataSource.Config.dronesIndexer] = drone;
            DataSource.Config.dronesIndexer++;
        }



    }
}
 







































