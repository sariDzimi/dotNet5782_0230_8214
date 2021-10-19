using System;
using IDAL.DO;
using DalObject;

namespace ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {




            Console.WriteLine("to add enter 1");
            Console.WriteLine("to update enter 2");
            Console.WriteLine("to display enter 3");
            Console.WriteLine("to display lists enter 4");
            Console.WriteLine("to exit enter 5");
            int choices = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(choices);
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
                            DalObject.DalObject.addStation(getStationFromUser());
                            break;
                        case 2:
                            DalObject.DalObject.addDrone(getDroneFromUser());

                            break;
                        case 3:
                            DalObject.DalObject.addCustomer(getCustomerFromUser());

                            break;
                        case 4:
                            DalObject.DalObject.addParcel(getParcelFromUser());
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    Console.WriteLine("to conect a parcel to a drone enter 1");
                    Console.WriteLine("to collect a parcel by a drone enter 2");
                    Console.WriteLine("to supply a parcel to a customer enter 3");
                    Console.WriteLine("to send a drone to charge in a station enter 4");
                    choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("enter an parcels id");
                            int id = Convert.ToInt32(Console.ReadLine());
                            DalObject.DalObject.belongPacelToADrone(DalObject.DalObject.findParcel(id));
                            break;
                        case 2:
                            Console.WriteLine("enter an parcels id");
                            id = Convert.ToInt32(Console.ReadLine());
                            DalObject.DalObject.CollectAParcelByDrone(DalObject.DalObject.findParcel(id));

                            break;
                        case 3:
                            Console.WriteLine("enter an parcels id");
                            id = Convert.ToInt32(Console.ReadLine());
                            DalObject.DalObject.DeliverParcelToCustomer(DalObject.DalObject.findParcel(id));
                            break;
                        case 4:

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
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
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
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        default:
                            break;
                    }
                    break;
                case 5:
                    break;

                default:
                    Console.WriteLine("input not valid");
                    break;
            }



        }


        public static void displayStations()
        {
            //Station[]  stations = DalObject.DalObject.
        }

        public static Parcel getParcelFromUser()
        {
            Parcel parcel = new Parcel();
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
            Console.WriteLine("enter the status : 1.Free, 2.Maintenance, 3.Delivery");
            int status = Convert.ToInt32(Console.ReadLine());
            drone.Status = (DroneStatus)status;

            return drone;


        }


        public static Station getStationFromUser()
        {
            Station station = new Station();
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

    }
}
