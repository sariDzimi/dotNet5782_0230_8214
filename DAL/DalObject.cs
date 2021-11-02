using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DalObject;
using IDAL.DO;

namespace DalObject
{
    public class DalObject : IDal.IDal
    {
        public DalObject()
        {
            DataSource.Initialize();
        }

        public void addDrone(Drone drone)
        {
            if (DataSource.drones.Any(dr => dr.Id == drone.Id))
            {
                throw new IdAlreadyExist(drone.Id);
            }

            DataSource.drones.Add(drone);

        }

        public void addCustomer(Customer customer)
        {
            if (DataSource.customers.Any(cs => cs.Id == customer.Id))
            {
                throw new IdAlreadyExist(customer.Id);
            }

            DataSource.customers.Add(customer);

        }
        public void addParcel(Parcel parcel)
        {
            if (DataSource.parcels.Any(ps => ps.Id == parcel.Id))
            {
                throw new IdAlreadyExist(parcel.Id);
            }

            DataSource.parcels.Add(parcel);

        }
        public void addStation(Station station)
        {
            if (DataSource.customers.Any(st => st.Id == station.Id))
            {
                throw new IdAlreadyExist(station.Id);
            }

            DataSource.stations.Add(station);

        }
        public void addDronCharge(DroneCharge droneCharge)
        {

            DataSource.droneCharges.Add(droneCharge);

        }
        public IEnumerable<Station> GetStations()
        {
            foreach (var station in DataSource.stations)
            {
                yield return station;
            }
        }

        public IEnumerable<Drone> GetDrones()
        {
            foreach (var drone in DataSource.drones)
            {
                yield return drone;
            }
        }

        public IEnumerable<Customer> GetCustomer()
        {
            foreach (var customer in DataSource.customers)
            {
                yield return customer;
            }
        }

        public IEnumerable<Parcel> GetParcel()
        {
            foreach (var parcel in DataSource.parcels)
            {
                yield return parcel;
            }
        }

        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            foreach (var droneCharge in DataSource.droneCharges)
            {
                yield return droneCharge;
            }
        }

        public Parcel findParcel(int id)
        {
            try
            {
                return DataSource.parcels.First(parcel => parcel.Id == id);
            }
            catch (ArgumentNullException e)
            {
                throw new NotFoundException(id);
            }

        }


        public Station findStation(int id)
        {
            try
            {
                return DataSource.stations.First(sat => sat.Id == id);
            }
            catch (ArgumentNullException e)
            {
                throw new NotFoundException(id);
            }

        }

        public Customer findCustomer(int id)
        {


            try
            {
                return DataSource.customers.First(customer => customer.Id == id);
            }
            catch (ArgumentNullException e)
            {
                throw new NotFoundException(id);
            }

        }

        public Drone findDrone(int id)
        {
            try
            {
                return DataSource.drones.First(drone => drone.Id == id);
            }
            catch (ArgumentNullException e)
            {
                throw new NotFoundException(id);
            }
        }


        public void updateDrone(Drone drone)
        {
            int index = DataSource.drones.FindIndex(d => d.Id == drone.Id);
            DataSource.drones[index] = drone;
        }


        public void updateParcel(Parcel parcel)
        {
            int index = DataSource.parcels.FindIndex(p => p.Id == parcel.Id);
            DataSource.parcels[index] = parcel;

        }
        public void updateCustomer(Customer customer)
        {
            int index = DataSource.customers.FindIndex(p => p.Id == customer.Id);
            DataSource.customers[index] = customer;

        }

        public void updateDronecharge(DroneCharge dronecharge)
        {
            int index = DataSource.droneCharges.FindIndex(p => p.DroneId == dronecharge.DroneId);
            DataSource.droneCharges[index] = dronecharge;

        }

        public void belongPacelToADrone(Parcel parcel)
        {
            throw new NotImplementedException();
        }

        public void CollectAParcelByDrone(Parcel parcel)
        {
            throw new NotImplementedException();
        }

        public void DeliverParcelToCustomer(Parcel parcel)
        {
            throw new NotImplementedException();
        }

        public void SendDroneForCharging(Drone drone, Station station)
        {
            throw new NotImplementedException();
        }

        public void ReleaseDroneFromCharging(Drone drone)
        {
            throw new NotImplementedException();

        }

        public double[] RequestElectricityUse()
        {
            double[] Electricity = { DataSource.Config.free, DataSource.Config.light, DataSource.Config.medium,DataSource.Config.heavy, DataSource.Config.rateChargePerHour };
            return Electricity;

        }

    }

}





