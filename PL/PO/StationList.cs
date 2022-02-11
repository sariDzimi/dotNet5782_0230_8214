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
    public class StationList : DependencyObject
    {
        IBL BL;

        public StationList()
        {

        }

        public ObservableCollection<Station_p> Stations = new ObservableCollection<Station_p>();

        public void AddStation(StationToList station)
        {
            //Station station1 = BL.GetStationById(station.ID);
            Stations.Add(new Station_p { ID = station.Id, Name = station.Name, FreeChargeSlots= station.NumberOfFreeChargeSlots, /*Latitude  =station1.Location.Latitude, Longitude = station1.Location.Longitude*/  });
        }
        public ObservableCollection<Station_p> ConvertStationBLToPL(List<StationToList> StationsBL)
        {

            foreach (var station in StationsBL)
            {
                Station_p station_P = new Station_p() { ID = station.Id, FreeChargeSlots = station.NumberOfFreeChargeSlots, Name = station.Name };
                Stations.Add(station_P);
            }
            return Stations;
        }
        public void UpdateListStations(Station_p station_P)
        {
            if (Stations.Count == 0)
            {
                this.Stations = this.ConvertStationBLToPL(BL.GetStationToLists().ToList());
            }

            int index = Stations.IndexOf(Stations.Where(X => X.ID == station_P.ID).FirstOrDefault());
            Stations[index] = station_P;

        }
        public ObservableCollection<Station_p> ClearStations()
        {
            //foreach (var parcel in Parcels)
            //{
            //    Parcels.Remove(parcel);
            //}
            Stations = new ObservableCollection<Station_p>();
            return Stations;
        }
    }
}

