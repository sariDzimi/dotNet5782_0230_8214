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
                            bl.AssignParcelToDrone(drone.Id);
                        }
                        catch (NotFound)
                        {
                            if (drone.Battery != 100)
                                try
                                {
                                    bl.SendDroneToCharge(drone.Id);
                                }
                                catch(DroneDoesNotHaveEnoughBattery){} 
                        }
                        updateDrone(drone, 1);
                        Thread.Sleep(DELAY);
                        break;
                    case DroneStatus.Delivery:
                        if (drone.ParcelInDelivery.IsWating)
                            bl.CollectParcleByDrone(drone.Id);
                        else
                            bl.SupplyParcelByDrone(drone.Id);
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
                        bl.ReleaseDroneFromCharging(drone.Id, (100 - drone.Battery) / bl.GetRateOFCharging());
                        updateDrone(drone, 1);
                        Thread.Sleep(DELAY);
                        break;
                }

            }

        }
    }
}
