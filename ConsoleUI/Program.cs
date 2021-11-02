using System;
using IDAL.DO;
using DalObject;
using System.Collections.Generic;

namespace ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            DalObject.DalObject dalObject = new DalObject.DalObject();
            int choices;
            do
            {
                Console.WriteLine("to add enter 1");
                Console.WriteLine("to update enter 2");
                Console.WriteLine("to display enter 3");
                Console.WriteLine("to display lists enter 4");
                Console.WriteLine("to exit enter 5");
                choices = Convert.ToInt32(Console.ReadLine());
                switch (choices)
                {
                    case 1:
                        Console.WriteLine("to add a station enter 1");
                        Console.WriteLine("to add a drone enter 2");
                        Console.WriteLine("to add a customer enter 3");
                        Console.WriteLine("to add a parcel enter 4");
                        int choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                dalObject.addStation(getStationFromUser());
                                break;
                            case 2:
                                dalObject.addDrone(getDroneFromUser());

                                break;
                            case 3:
                                dalObject.addCustomer(getCustomerFromUser());

                                break;
                            case 4:
                                dalObject.addParcel(getParcelFromUser());
                                break;
                            default:
                                Console.WriteLine("please enter a number btween 1-4");
                                break;
                        }
                        break;
                    case 2:
                        Console.WriteLine("to conect a parcel to a drone enter 1");
                        Console.WriteLine("to collect a parcel by a drone enter 2");
                        Console.WriteLine("to supply a parcel to a customer enter 3");
                        Console.WriteLine("to send a drone to charge in a station enter 4");
                        Console.WriteLine("to Release a skimmer from charging at a base station enter 5 ");
                        choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:

                                dalObject.belongPacelToADrone(dalObject.findParcel(getParcleId()));
                                break;
                            case 2:
                                dalObject.CollectAParcelByDrone(dalObject.findParcel(getParcleId()));

                                break;
                            case 3:
                                dalObject.DeliverParcelToCustomer(dalObject.findParcel(getParcleId()));
                                break;
                            case 4:
                                dalObject.SendDroneForCharging(dalObject.findDrone(getDroeId()), dalObject.findStation(getStaionId()));
                                break;
                            case 5:
                                dalObject.ReleaseDroneFromCharging(dalObject.findDrone(getDroeId()));
                                break;

                            default:
                                break;
                        }


                        break;
                    case 3:
                        Console.WriteLine("to display a station enter 1");
                        Console.WriteLine("to display a drone enter 2");
                        Console.WriteLine("to display a customer enter 3");
                        Console.WriteLine("to display a parcel enter 4");
                        choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                DisplayObj<Station>(dalObject.findStation(getStaionId()));
                                break;
                            case 2:
                                DisplayObj<Drone>(dalObject.findDrone(getDroeId()));
                                break;
                            case 3:
                                DisplayObj<Customer>(dalObject.findCustomer(getCustomerId()));
                                break;
                            case 4:
                                DisplayObj<Parcel>(dalObject.findParcel(getParcleId()));
                                break;
                            default:
                                break;
                        }

                        break;
                    case 4:
                        Console.WriteLine("to display the stations list enter 1");
                        Console.WriteLine("to display the drones list enter 2");
                        Console.WriteLine("to display the customers list enter 3");
                        Console.WriteLine("to display the parcels list enter 4");
                        Console.WriteLine("to display the list of parcels that are free enter 5");
                        Console.WriteLine("to display the list of station that have  free chargers enter 6");
                        choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                DisplayList<Station>(dalObject.GetStations());
                                break;
                            case 2:
                                DisplayList<Drone>(dalObject.GetDrones());
                                break;
                            case 3:
                                DisplayList<Customer>(dalObject.GetCustomer());
                                break;
                            case 4:
                                DisplayList<Parcel>(dalObject.GetParcel());
                                break;
                            case 5:
                                DisplayNotBelongedParcels(dalObject.GetParcel());
                                break;
                            case 6:
                                displayStationsWithEmptyChargingSlots(dalObject.GetStations(), dalObject.GetDroneCharges());
                                break;
                            default:
                                break;
                        }
                        break;
                    case 5:
                        break;
                    case 6:
                        break;

                    default:
                        Console.WriteLine("input not valid");
                        break;
                }




            } while (choices != 5);
        }

        private static void displayStationsWithEmptyChargingSlots(IEnumerable<Station> enumerable1, IEnumerable<DroneCharge> enumerable2)
        {
            throw new NotImplementedException();
        }

        private static void DisplayNotBelongedParcels(IEnumerable<Parcel> enumerable)
        {
            throw new NotImplementedException();
        }

        public static Parcel getParcelFromUser()
        {
            Parcel parcel = new Parcel();
            Console.WriteLine("enter the id");
            parcel.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter a senderId");
            parcel.SenderId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter a targetetId");
            parcel.TargetId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter the weight : 1. Light,2. Medium, 3.Heavy");
            int weight = Convert.ToInt32(Console.ReadLine());
            parcel.Weight = (WeightCategories)weight;
            Console.WriteLine("enter the prionity : 1. Reguler,2. Fast, 3.Emergency");
            int prionity = Convert.ToInt32(Console.ReadLine());
            parcel.Pritority = (Pritorities)prionity;
            return parcel;


        }

        public static Drone getDroneFromUser()
        {
            Drone drone = new Drone();
            Console.WriteLine("enter an id");
            drone.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter a modedl");
            drone.Model = Console.ReadLine();
            Console.WriteLine("enter the weight : 1. Light,2. Medium, 3.Heavy");
            int weight = Convert.ToInt32(Console.ReadLine());
            drone.MaxWeight = (WeightCategories)weight;


            return drone;


        }


        public static Station getStationFromUser()
        {
            Station station = new Station();
            Console.WriteLine("enter the id");
            station.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter the name");
            station.Name = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter number of charge slots");
            station.ChargeSlots = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter the longitude");
            int longitude = Convert.ToInt32(Console.ReadLine());
            station.Longitude = (double)longitude;
            Console.WriteLine("enter the latitude");
            int latitude = Convert.ToInt32(Console.ReadLine());
            station.Latitude = (double)latitude;

            return station;


        }


        public static Customer getCustomerFromUser()
        {

            Customer customer = new Customer();
            Console.WriteLine("enter the id");
            customer.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter the name");
            customer.Name = Console.ReadLine();
            Console.WriteLine("enter the celPhone");
            customer.Phone = Console.ReadLine();
            Console.WriteLine("enter the longitude");
            int longitude = Convert.ToInt32(Console.ReadLine());
            customer.Longitude = (double)longitude;
            Console.WriteLine("enter the latitude");
            int latitude = Convert.ToInt32(Console.ReadLine());
            customer.Latitude = (double)latitude;

            return customer;


        }

        public static int getParcleId()
        {
            Console.WriteLine("enter an parcels id");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }

        public static int getDroeId()
        {
            Console.WriteLine("enter an drone id");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }

        public static int getStaionId()
        {
            Console.WriteLine("enter an station id");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }

        public static int getCustomerId()
        {
            Console.WriteLine("enter an cusomer id");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }

        public static void DisplayObj<T>(T obj)
        {
            Console.WriteLine(obj);
        }
        public static void DisplayList<T>( IEnumerable<T> arr)//List<T> arr)
        {
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
        }
        public static void DisplayNotBelongedParcels(List<Parcel> parcels) 
        {
            foreach(var parcel in parcels)
            {
                if(parcel.DroneId == 0)
                    Console.WriteLine(parcel);
            }
        }

       public static void displayStationsWithEmptyChargingSlots(IEnumerable<Station> stations, List<DroneCharge> droneCharges)
        {

        }
    }
}
