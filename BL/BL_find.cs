using BlApi.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
     partial class BL
    {
        /// <summary>
        /// Find Station
        /// </summary>
        /// <param Predicate="id"></param>
        /// <returns></returns>
        private Station FindStationBy(Predicate <Station> findBy)
        {

            Station stationBL = new Station();

            try
            {
                stationBL = (from station in GetStations()
                             where findBy(station)
                             select station).First();
            }
            catch (Exception ex)
            {
                throw new NotFound($"{ex}");
            }
            return stationBL;
        }


        private Station FindStation(int id)
        {
            return FindStationBy(s => s.Id == id);
        }
        /// <summary>
        /// Find Drone
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Drone FindDrone(int id)
        {
            Drone droneBL = new Drone();
            droneBL = dronesBL.Find(d => d.Id == id);
            if (droneBL==null)
            {
                throw new NotFound($"drone number {id}");
            }
            return droneBL;
        }






        private Drone FindDroneBy(Predicate<Drone> findBy)
        {

            Drone droneBL = new Drone();

            try
            {
                droneBL = (from drone in GetDrones()
                             where findBy(drone)
                             select drone).First();
            }
            catch (Exception ex)
            {
                throw new NotFound($"{ex}");
            }
            return droneBL;
        }





        /// <summary>
        /// Find Cuatomer
        /// </summary>
        /// <param Predicate></param>
        /// <returns></returns>


        private Customer FindCustomerBy(Predicate<Customer> findBy)
        {

            Customer customerBL = new Customer();

            try
            {
                customerBL = (from customer in GetCustomers().ToList()
                              where findBy(customer)
                              select customer).First();
            }
            catch (Exception ex)
            {
                throw new NotFound($"{ex}");
            }
            return customerBL;
        }



        /// <summary>
        /// Find Parcel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public Parcel FindParcelBy(Predicate<Parcel> findBy)
        {

            Parcel parcelBL = new Parcel();

            try
            {
                parcelBL = (from parcel in GetParcels().ToList()
                            where findBy(parcel)
                            select parcel).First();
            }
            catch (Exception ex)
            {
                throw new NotFound($"{ex}");
            }
            return parcelBL;
        }

        public Parcel FindParcel(int id)
        {
            return FindParcelBy(p => p.Id == id);
        }

        public DroneCharge FindDroneCharge(int droneId)
        {
            try
            {
                return (from droneCharge in GetDronesCharges()
                        where droneCharge.DroneId == droneId
                        select droneCharge).First();
            }
            catch (Exception)
            {
                throw new NotFound($"drone charge");
            }
        }
    }

    

    
}