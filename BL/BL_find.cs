﻿using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    public partial class BL
    {
        /// <summary>
        /// Find Station
        /// </summary>
        /// <param Predicate="id"></param>
        /// <returns></returns>
        public Station FindStationBy(Predicate <Station> findBy)
        {

            Station stationBL = new Station();

            try
            {
                 stationBL= (from station in GetStations().ToList()
                        where findBy(station)
                        select station).First();
            }
            catch (Exception ex)
            {
                throw new NotFound($"{ex}");
            }
            return stationBL;
        }
         
        /// <summary>
        /// Find Drone
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Drone FindDrone(int id)
        {
            Drone droneBL = new Drone();
            droneBL = dronesBL.Find(d => d.Id == id);
            if (droneBL==null)
            {
                throw new NotFound($"drone number {id}");
            }
            return droneBL;
        }



        public Drone FindDroneBy(Predicate<Drone> findBy)
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


        public Customer FindCuatomerBy(Predicate<Customer> findBy)
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
    }

    
}