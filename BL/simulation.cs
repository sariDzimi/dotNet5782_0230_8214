using BlApi;
using BO;
using System;
using System.Threading;

namespace BL
{
    class Simulation
    {
        int DELAY = 1000;
        int SPEED = 100;
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
                        //Check if it works.........
                        if (drone.ParcelInDelivery.IsWating)
                            bl.collectParcleByDrone(drone.Id);
                        else
                            bl.supplyParcelByDrone(drone.Id);
                        updateDrone(drone, 1);
                        Thread.Sleep(DELAY);
                        break;
                    case DroneStatus.Maintenance:
                        double NeedToFuul = (100 - drone.Battery) / bl.GetRateOFCharging();
                        while ((drone.Battery + NeedToFuul / SPEED) < 100)
                        {
                            drone.Battery += NeedToFuul / SPEED;
                            updateDrone(drone, 1);
                            Thread.Sleep(SPEED);  
                        }
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
