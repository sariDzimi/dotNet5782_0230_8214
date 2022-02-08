using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace Dal
{

    /// <summary>
    /// source of the data.
    /// data is stored in lists.
    /// </summary>
    public class DataSource
    {
        #region DataSource Lists

        static internal List<Drone> drones = new List<Drone>();
        static internal List<Station> stations = new List<Station>();
        static internal List<Customer> customers = new List<Customer>();
        static internal List<Parcel> parcels = new List<Parcel>();
        static internal List<DroneCharge> droneCharges = new List<DroneCharge>();
        static internal List<Manager> Managers = new List<Manager>();

        #endregion

        /// <summary>
        /// config details.
        /// cantains data of how much electricity a drone uses for parcels in diffrents weights
        /// </summary>
        internal class Config
        {
            public static double free = .1;
            public static double light = .4;
            public static double medium = .5;
            public static double heavy = .7;
            public static double rateChargePerHour = .5;
        }

        #region Initialize

        /// <summary>
        /// intilizes the lists of the database with randomng data
        /// </summary>
        static public void Initialize()
        {
            int maxRand = 10;
            Random rand = new Random();
            for (int i = 0; i < 2; i++)
            {
                Station station = new Station();
                station.Id = stations.Count + 1;
                station.Name = stations.Count + 1;
                station.Latitude = rand.Next() % maxRand + 1;
                station.Longitude = rand.Next() % maxRand + 1;
                station.ChargeSlots = rand.Next(100);
                stations.Add(station);
            }

            for (int i = 0; i < 5; i++)
            {

                Drone drone = new Drone();
                drone.Id = (drones.Count) + 1;
                drone.Model = "MarvicAir2";
                drone.MaxWeight = (WeightCategories)(rand.Next() % 3) + 1;
                drones.Add(drone);
            }

            for (int i = 0; i < 10; i++)
            {
                Customer customer = new Customer();
                customer.Id = (customers.Count) + 1;
                customer.Name = $"customer{i}";
                customer.Phone = $"{rand.Next(111111111, 999999999)}";
                customer.Latitude = rand.Next() % maxRand + 1;
                customer.Longitude = rand.Next() % maxRand + 1;
                customers.Add(customer);
            }

            for (int i = 0; i < 10; i++)
            {
                Parcel parcel = new Parcel();
                parcel.Id = parcels.Count + 1;
                parcel.SenderId = customers[rand.Next() % (customers.Count - 1)].Id;
                parcel.TargetId = customers[rand.Next() % (customers.Count - 1)].Id;
                parcel.Weight = (WeightCategories)(rand.Next() % 3) + 1;
                parcel.Pritority = (Pritorities)(rand.Next() % 3);
                parcel.Requested = randomDate();
                parcel.IsActive = true;
                List<Drone> notAssignDrones = getNotAssignedDrones();
                parcel.Scheduled = (notAssignDrones.Count == 0) ? null : randomDateOrNull(parcel.Requested);
                parcel.PickedUp = randomDateOrNull(parcel.Scheduled);
                parcel.Delivered = randomDateOrNull(parcel.PickedUp);
                parcel.DroneId = parcel.Scheduled == null ? 0 : notAssignDrones[rand.Next(0, notAssignDrones.Count)].Id;
                parcels.Add(parcel);
            }

            Managers.Add(new Manager() { UserName = "sariDzimi", Password = 123456789 });
            Managers.Add(new Manager() { UserName = "MiryamSH", Password = 987654321 });
            Managers.Add(new Manager() { UserName = "", Password = 0 });
        }

        #endregion

        #region Random functions

        /// <summary>
        /// randoms a date
        /// </summary>
        /// <returns>random date</returns>
        private static DateTime randomDate()
        {
            Random rand = new Random();
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rand.Next(range));
        }


        /// <summary>
        /// randoms a date statring from the given date
        /// </summary>
        /// <param name="startDate">starting date</param>
        /// <returns>rabdom date</returns>
        private static DateTime? randomDate(DateTime? startDate)
        {
            if (startDate == null)
                return null;
            Random rand = new Random();
            DateTime? start = startDate;
            int range = (DateTime.Today - startDate).Value.Days;
            return start.Value.AddDays(rand.Next(range));
        }


        /// <summary>
        /// random an answer between null of a random date
        /// </summary>
        /// <param name="startDate">starting date</param>
        /// <returns>null or random date</returns>
        public static DateTime? randomDateOrNull(DateTime? startDate)
        {
            Random rand = new Random();
            int x = (int)(rand.Next(1, 4));
            if (x < 2)
                return null;
            else
                return randomDate(startDate);
        }

        #endregion

        #region help functions
        private static List<Drone> getNotAssignedDrones()
        {
            List<Drone> notAssignDrones = new List<Drone>();
            foreach (var drone in drones)
            {
                bool assigned = false;
                foreach (var parcel in parcels)
                {
                    if (parcel.DroneId == drone.Id)
                    {
                        assigned = true;
                        break;
                    }

                }
                if (!assigned)
                    notAssignDrones.Add(drone);
            }
            return notAssignDrones;
        }

        #endregion

    }

}
