using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Diagnostics;
using System.Windows;
using System.Collections.ObjectModel;
using BlApi;

namespace PO
{
    public class DroneList : DependencyObject
    {
        IBL BL;
        public DroneList()
        {

        }

        public ObservableCollection<Drone_p> Drone_Ps = new ObservableCollection<Drone_p>();

        public void AddDrone(DroneToList drone)
        {
            Drone_Ps.Add(new Drone_p { ID = drone.Id, Battery = drone.Battery, Location = drone.Location, Model = drone.Model, DroneStatus = drone.DroneStatus });
        }
        public ObservableCollection<Drone_p> ConvertDronelBLToPL(List<DroneToList> DronesBL)
        {
            foreach (var drone in DronesBL)
            {
                Drone_p Drone_P = new Drone_p() {ID = drone.Id, Battery = drone.Battery, Location = drone.Location, Model = drone.Model, DroneStatus= drone.DroneStatus };
                Drone_Ps.Add(Drone_P);
            }
            return Drone_Ps;
        }

        public ObservableCollection<Drone_p> ClearDrones()
        {
            Drone_Ps = new ObservableCollection<Drone_p>();
            return Drone_Ps;
        }


        public void UpdateList(Drone_p drone_P)
        {
            if (Drone_Ps.Count == 0)
            {
                this.Drone_Ps = this.ConvertDronelBLToPL(BL.GetDroneToLists().ToList());
            }                

            int index = Drone_Ps.IndexOf(Drone_Ps.Where(X => X.ID == drone_P.ID).FirstOrDefault());
            Drone_Ps[index] = drone_P;

        }
    }
}