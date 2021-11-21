using System;
using IBL.BO;
using System.Collections.Generic;



namespace ConsoleUI_BL
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                BL.BL bL = new BL.BL();
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
                            int id;
                            Console.WriteLine("to add a station enter 1");
                            Console.WriteLine("to add a drone enter 2");
                            Console.WriteLine("to add a customer enter 3");
                            Console.WriteLine("to add a parcel enter 4");
                            int choice = Convert.ToInt32(Console.ReadLine());
                            switch (choice)
                            {
                                case 1:
                                    try
                                    {
                                        id = getInt("enter station number");
                                        int nameStation = getInt("enter station name");
                                        int chargeSlots = getInt("enter number of free charge slots");
                                        double longitude = getDouble("enter the longitude");
                                        double Latitude = getDouble("enter the latitude");
                                        Location location = new Location(longitude, Latitude);
                                        bL.addStationToBL(id, nameStation, location, chargeSlots);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);

                                    }
                                    break;
                                case 2:
                                    try
                                    {
                                        id = getId();
                                        int MaxWeight = getInt("enter the maximal weight : 1. Light,2. Medium, 3.Heavy");
                                        string Model = getString("enter the model");
                                        int number = getInt("number of station for start charging");
                                        bL.addDroneToBL(id, MaxWeight, Model, number);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                    }
                                    break;

                                case 3:
                                    try
                                    {
                                        id = getId();
                                        string NameCustomer = getString("enter name");
                                        string Phone = getString("enter cellPhone");
                                        double longitude2 = getDouble("enter the Longitude");
                                        double latitude2 = getDouble("enter the Latitude");
                                        Location location1 = new Location(longitude2, latitude2);

                                        bL.addCustomerToBL(id, NameCustomer, Phone, location1);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                    }
                                    break;

                                case 4:
                                    try
                                    {
                                        int weight;
                                        int SenderId = getInt("enter id of sender");
                                        id = getInt("enter id of reciver");
                                        weight = getInt("enter the weight : 1. Light,2. Medium, 3.Heavy");
                                        int prionity = getInt("enter the prionity : 1. Reguler,2. Fast, 3.Emergency");

                                        id = bL.addParcelToBL(SenderId, id, weight, prionity);
                                        Console.WriteLine($"your id's parcel is {id}");
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);

                                    }



                                    break;
                                default:
                                    Console.WriteLine("please enter a number btween 1-4");
                                    break;
                            }
                            break;
                        case 2:
                            Console.WriteLine("to udate Model of drone enter 1");
                            Console.WriteLine("to update data of station enter 2");
                            Console.WriteLine("to update data of customer enter 3");
                            Console.WriteLine("to send a drone to charge in a station enter 4");
                            Console.WriteLine("to release drone from charging enter 5");
                            Console.WriteLine("to assign a parcel to a drone enter 6");
                            Console.WriteLine("to collect a parcel by drone enter 7");
                            Console.WriteLine("to deliver a parcel by drone enter 8");

                            choice = getInt();
                            string name;
                            switch (choice)
                            {

                                case 1:
                                    try
                                    {
                                        id = getId();
                                        name = getString("enter new Model");
                                        bL.updateDroneModel(id, name);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                    }
                                    break;
                                case 2:
                                    try
                                    {
                                        id = getId();
                                        int nameOfStation = getInt("enter name");
                                        int chargeSlots = getInt("enter number of charge slots");

                                        bL.updateDataStation(id, nameOfStation, chargeSlots);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                    }

                                    break;
                                case 3:
                                    try
                                    {
                                        id = getId();
                                        name = getString("enter name");
                                        string phone = getString("enter phone");

                                        bL.updateDataCustomer(id, name, phone);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                    }

                                    break;
                                case 4:
                                    try
                                    {
                                        id = getId();
                                        bL.sendDroneToCharge(id);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                    }

                                    break;
                                case 5:

                                    try
                                    {
                                        id = getId();
                                        double time = getDouble("enter Charging time");
                                        bL.releaseDroneFromCharging(id, time);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                    }

                                    break;

                                case 6:
                                    id = getId();
                                    try
                                    {
                                        bL.AssignAParcelToADrone(id);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                    }

                                    break;
                                case 7:
                                    id = getId();
                                    try
                                    {
                                        bL.collectParcleByDrone(id);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                    }

                                    break;
                                case 8:
                                    id = getId();
                                    try
                                    {
                                        bL.supplyParcelByDrone(id);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                    }

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
                                    try
                                    {
                                        id = getId();
                                        Console.WriteLine(bL.FindStation(id));
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                    }
                                    break;
                                case 2:

                                    try
                                    {
                                        id = getId();
                                        Console.WriteLine(bL.FindDrone(id));
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                    }

                                    break;
                                case 3:
                                    try
                                    {
                                        id = getId();
                                        Console.WriteLine(bL.FindCuatomer(id));
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                    }


                                    break;
                                case 4:
                                    try
                                    {
                                        id = getId();
                                        Console.WriteLine(bL.FindParcel(id));
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                    }
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
                            Console.WriteLine("to display the list of not asigned parcels enter 5");
                            Console.WriteLine("to display the list of stations that have free chargers enter 6");
                            choice = Convert.ToInt32(Console.ReadLine());
                            switch (choice)
                            {
                                case 1:
                                    DisplayList(bL.GetStations());
                                    break;
                                case 2:
                                    DisplayList(bL.GetDrones());
                                    break;
                                case 3:
                                    DisplayList(bL.GetCustomers());
                                    break;
                                case 4:
                                    DisplayList(bL.GetParcels());
                                    break;
                                case 5:
                                    DisplayList(bL.GetNotAsignedParcels());
                                    break;
                                case 6:
                                    DisplayList(bL.GetStationsWithEmptyChargeSlots());
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 5:
                            Console.WriteLine("exit");
                            break;
                        default:
                            Console.WriteLine("input not valid");
                            break;
                    }

                } while (choices != 5);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// displays a list of objects to the console
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void DisplayList<T>(IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// propts the user for an id 
        /// </summary>
        /// <returns>input from user</returns>
        public static int getId()
        {
            return getInt("enter id");
        }

        /// <summary>
        /// propts the user for an int
        /// </summary>
        /// <param name="message"></param>
        /// <returns>input from user</returns>
        public static int getInt(string message = "")
        {
            int num;
            Console.WriteLine(message);
            try
            {
                num = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                throw new InvalidInput("int");
            }
            return num;

        }

        /// <summary>
        /// propts the user for a double
        /// </summary>
        /// <param name="message"></param>
        /// <returns>input from user</returns>
        public static double getDouble(string message)
        {
            double num;
            Console.WriteLine(message);
            try
            {
                num = Convert.ToDouble(Console.ReadLine());
            }
            catch (Exception e)
            {
                throw new InvalidInput("int");
            }
            return num;

        }

        /// <summary>
        /// propts the user for a string
        /// </summary>
        /// <param name="message"></param>
        /// <returns>input from user</returns>
        public static string getString(string message)
        {
            Console.WriteLine(message);
            string str = Console.ReadLine();
            return str;
        }

    }
}

//דוגמאת הרצה:
//    IBL.BO.DroneIsNotInCorrectStatus: drone is not in Maintenance
//   at BL.BL.releaseDroneFromCharging(Int32 idDrone, Double timeInCharging) in C:\סמסטר א- חומר\C#\C#\BL\BL.cs:line 112
//   at ConsoleUI_BL.Program.Main(String[] args) in C:\סמסטר א- חומר\C#\C#\ConsoleUI_BL\Program.cs:line 221
//to add enter 1
//to update enter 2
//to display enter 3
//to display lists enter 4
//to exit enter 5
//2
//to udate Model of drone enter 1
//to update data of station enter 2
//to update data of customer enter 3
//to send a drone to charge in a station enter 4
//to release drone from charging enter 5
//to assign a  parcel to a drone enter 6
//to collection of a parcel by drone enter 7
//to delivery of a parcel by drone enter 8
//6
//enter id
//5
//IBL.BO.DroneIsNotInCorrectStatus: drone is not free
//   at BL.BL.AssignAParcelToADrone(Int32 id) in C:\סמסטר א- חומר\C#\C#\BL\BL.cs:line 246
//   at ConsoleUI_BL.Program.Main(String[] args) in C:\סמסטר א- חומר\C#\C#\ConsoleUI_BL\Program.cs:line 235
//to add enter 1
//to update enter 2
//to display enter 3
//to display lists enter 4
//to exit enter 5
//2
//to udate Model of drone enter 1
//to update data of station enter 2
//to update data of customer enter 3
//to send a drone to charge in a station enter 4
//to release drone from charging enter 5
//to assign a  parcel to a drone enter 6
//to collection of a parcel by drone enter 7
//to delivery of a parcel by drone enter 8
//7
//enter id
//3
//IBL.BO.DroneIsNotInCorrectStatus: drone is not in Delivery
//   at BL.BL.collectParcleByDrone(Int32 idDrone) in C:\סמסטר א- חומר\C#\C#\BL\BL.cs:line 131
//   at ConsoleUI_BL.Program.Main(String[] args) in C:\סמסטר א- חומר\C#\C#\ConsoleUI_BL\Program.cs:line 248
//to add enter 1
//to update enter 2
//to display enter 3
//to display lists enter 4
//to exit enter 5
//8
//input not valid
//to add enter 1
//to update enter 2
//to display enter 3
//to display lists enter 4
//to exit enter 5
//2
//to udate Model of drone enter 1
//to update data of station enter 2
//to update data of customer enter 3
//to send a drone to charge in a station enter 4
//to release drone from charging enter 5
//to assign a  parcel to a drone enter 6
//to collection of a parcel by drone enter 7
//to delivery of a parcel by drone enter 8
//8
//enter id
//4
//IBL.BO.DroneIsNotInCorrectStatus: drone is not in delivery
//   at BL.BL.supplyParcelByDrone(Int32 DroneID) in C:\סמסטר א- חומר\C#\C#\BL\BL.cs:line 318
//   at ConsoleUI_BL.Program.Main(String[] args) in C:\סמסטר א- חומר\C#\C#\ConsoleUI_BL\Program.cs:line 261
//to add enter 1
//to update enter 2
//to display enter 3
//to display lists enter 4
//to exit enter 5
//5
//input not valid