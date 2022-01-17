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

        public StationList()
        {

        }

        public ObservableCollection<Station_p> Stations = new ObservableCollection<Station_p>();

        public void AddStation(StationToList station)
        {
            Stations.Add(new Station_p { ID = station.ID, Name = station.Name  });
        }
        public ObservableCollection<Station_p> ConvertStationBLToPL(List<StationToList> StationsBL)
        {

            foreach (var station in StationsBL)
            {
                Station_p station_P = new Station_p() { ID = station.ID };
                Stations.Add(station_P);
            }
            return Stations;
        }

    }
}

