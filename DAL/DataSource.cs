using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    class DataSource
    {
        internal Drone[] drones = new Drone[10];
        internal Station[] stations = new Station[5];
        internal Customer[] customers = new Customer[100];
        internal Parcel[] parcels = new Parcel[100];

        internal class Config
        {
            static internal int dronesIndexer = 0;
            static internal int stationsIndexer = 0;
            static internal int customersIndexer = 0;
            static internal int parcelsIndexer = 0;
            static internal int parcelRecognizer = 0;

        }
    } 
}
