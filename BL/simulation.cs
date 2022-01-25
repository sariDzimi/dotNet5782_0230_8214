using BlApi;
using BO;
using System;
using System.Threading;

namespace BL
{
    class Simulation
    {
        int DELAY = 1000;
        double SPEED = 60;
        public Simulation(IBL bl, Drone drone, Action<Drone, int> updateDrone, Func<bool> needToStop)
        {
            while (!needToStop())
            {
                switch (drone.DroneStatus)
                {
                    case DroneStatus.Free:
                        try
                        {
                            bl.AssignAParcelToADrone(drone.Id);
                        }
                        catch (NotFound)
                        {
                            if (drone.Battery != 100)
                                bl.sendDroneToCharge(drone.Id);
                            //else
                               
                        }
                        updateDrone(drone, 1);
                        Thread.Sleep(DELAY);
                        break;
                    case DroneStatus.Delivery:
                        if (drone.ParcelInDelivery.IsWating)
                            bl.collectParcleByDrone(drone.Id);
                        else
                            bl.supplyParcelByDrone(drone.Id);
                        updateDrone(drone, 1);
                        Thread.Sleep(DELAY);
                        break;
                    case DroneStatus.Maintenance:
                        bl.releaseDroneFromCharging(drone.Id, (100 - drone.Battery) / bl.GetRateOFCharging());
                        updateDrone(drone, 1);
                        Thread.Sleep(DELAY);
                        break;
                }

            }

        }

        /*        public void start(Drone drone, Action<Drone> simulateDrone, Func<bool> needToStop)
                {
                    while (!needToStop())
                    {

                        Thread.Sleep(DELAY);
                    }
                }*/
    }
}
