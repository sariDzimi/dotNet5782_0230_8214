using System;
using IBL.BO;
using System.Collections.Generic;



namespace ConsoleUI_BL
{
    class Program
    {


        static void Main(string[] args)
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
                        Console.WriteLine("to add a station enter 1");
                        Console.WriteLine("to add a drone enter 2");
                        Console.WriteLine("to add a customer enter 3");
                        Console.WriteLine("to add a parcel enter 4");
                        int choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:

                                Console.WriteLine("enter station number");
                                int Id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("enter station name");
                                int Name = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("enter number of free charge slots");
                                int ChargeSlots = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("enter the longitude");
                                int longitude = Convert.ToInt32(Console.ReadLine());
                                double Longitude = (double)longitude;
                                Console.WriteLine("enter the latitude");
                                int latitude = Convert.ToInt32(Console.ReadLine());
                                double Latitude = (double)latitude;
                                Location location = new Location(Longitude, Latitude);

                                try
                                {

                                bL.addStationToBL(Id, Name, location, ChargeSlots);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    
                                }

                                break;
                            case 2:
                                Console.WriteLine("enter id");
                                int id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("enter the maximal weight : 1. Light,2. Medium, 3.Heavy");
                                int weight = Convert.ToInt32(Console.ReadLine());
                                int MaxWeight = weight;
                                Console.WriteLine("enter the model");
                                string Model = Console.ReadLine();
                                Console.WriteLine("number of station for start charging");
                                int number = Convert.ToInt32(Console.ReadLine());

                                try
                                {
                                    bL.addDroneToBL(id, weight, Model, number);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);

                                }
                               

                                break;

                            case 3:
                                Console.WriteLine("enter id");
                                Id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("enter name");
                                string NameCustomer = Console.ReadLine();
                                Console.WriteLine("enter cellPhone");
                                string Phone = Console.ReadLine();
                                Console.WriteLine("enter the Longitude");
                                double longitude2 = Convert.ToDouble(Console.ReadLine());
                                Console.WriteLine("enter the Latitude");
                                double latitude2 = Convert.ToDouble(Console.ReadLine());
                                Location location1 = new Location(longitude2, latitude2);


                                try
                                {
                                    bL.addCustomerToBL(Id, NameCustomer, Phone, location1);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);

                                }
                               

                                break;
                            case 4:
                                Console.WriteLine("enter id of sender");
                                int SenderId = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("enter id of reciver");
                                Id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("enter the weight : 1. Light,2. Medium, 3.Heavy");
                                weight = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("enter the prionity : 1. Reguler,2. Fast, 3.Emergency");
                                int prionity = Convert.ToInt32(Console.ReadLine());

                                try
                                {
                                    id=bL.addParcelToBL(SenderId, Id, weight, prionity);
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
                        Console.WriteLine("to assign a  parcel to a drone enter 6");
                        Console.WriteLine("to collection of a parcel by drone enter 7");
                        Console.WriteLine("to delivery of a parcel by drone enter 8");




                        //Console.WriteLine("to Release a skimmer from charging at a base station enter 5 ");
                        choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                Console.WriteLine("enter id");
                                int id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("enter new Model");
                                string name = Console.ReadLine();
                                bL.updateDroneModel(id, name);
                                break;
                            case 2:
                                Console.WriteLine("enter id");
                                id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("enter name");
                                int nameOfStation = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("enter number of charge slots");
                                int chargeSlots = Convert.ToInt32(Console.ReadLine());
                                try
                                {
                                    bL.updateDataStation(id, nameOfStation, chargeSlots);
                                }
                                catch(Exception e){
                                    Console.WriteLine(e);
                                }
                               
                                break;
                            case 3:
                                Console.WriteLine("enter id");
                                id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("enter name");
                                name = Console.ReadLine();
                                Console.WriteLine("enter phone");
                                string phone = Console.ReadLine();
                                try
                                {
                                    bL.updateDataCustomer(id, name, phone);

                                }
                                catch(Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                               
                                break;
                            case 4:
                                Console.WriteLine("enter id");
                                id = Convert.ToInt32(Console.ReadLine());
                                try
                                {
                                    bL.sendDroneToCharge(id);
                                }
                                catch(Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                               
                                break;
                            case 5:
                                Console.WriteLine("enter id");
                                id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("enter Charging time");
                                double time = Convert.ToInt32(Console.ReadLine());
                                try
                                {
                                    bL.releaseDroneFromCharging(id, time);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                               
                                break;

                            case 6:
                                Console.WriteLine("enter id");
                                id = Convert.ToInt32(Console.ReadLine());
                                try
                                {
                                    bL.AssignAParcelToADrone(id);
                                }
                                catch(Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                               
                                break;
                            case 7:
                                Console.WriteLine("enter id");
                                id = Convert.ToInt32(Console.ReadLine());
                                try
                                {
                                    bL.collectParcleByDrone(id);
                                }
                                catch(Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                               
                                break;
                            case 8:
                                Console.WriteLine("enter id");
                                id = Convert.ToInt32(Console.ReadLine());
                                try
                                {
                                    bL.supplyParcelByDrone(id);
                                }
                                catch(Exception e)
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
                                Console.WriteLine("enter id");
                                int id = Convert.ToInt32(Console.ReadLine());
                                try
                                {
                                Console.WriteLine(bL.FindStation(id));
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                                break;
                            case 2:
                                Console.WriteLine("enter id");
                                id = Convert.ToInt32(Console.ReadLine());

                                try
                                {
                                    Console.WriteLine(bL.FindDrone(id));
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                               
                                break;
                            case 3:
                                Console.WriteLine("enter id");
                                id = Convert.ToInt32(Console.ReadLine());


                                try
                                {
                                    Console.WriteLine(bL.FindCuatomer(id));
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }

                               
                                break;
                            case 4:
                                Console.WriteLine("enter id");
                                id = Convert.ToInt32(Console.ReadLine());
                                

                                try
                                {
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
                        Console.WriteLine("to display the list of station that have free chargers enter 6");
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



        public static void DisplayList<T>(IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

        private static void addStationToBL(int id, int name, Location location, int chargeSlots)
        {
            throw new NotImplementedException();
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