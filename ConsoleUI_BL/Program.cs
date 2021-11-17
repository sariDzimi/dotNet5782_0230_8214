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

                                bL.addStationToBL(Id, Name, location, ChargeSlots);

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

                                bL.addDroneToBL(id, weight, Model, number);

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

                                bL.addCustomerToBL(Id, NameCustomer, Phone, location1);

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
                                bL.updateDataStation(id, nameOfStation, chargeSlots);
                                break;
                            case 3:
                                Console.WriteLine("enter id");
                                id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("enter name");
                                name = Console.ReadLine();
                                Console.WriteLine("enter phone");
                                string phone = Console.ReadLine();
                                bL.updateDataCustomer(id, name, phone);
                                break;
                            default:
                                break;
                        }
                        break;
                        /*                    case 3:
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
                        */
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
        /*
                private static void displayStationsWithEmptyChargingSlots(IEnumerable<Station> enumerable1, IEnumerable<DroneCharge> enumerable2)
                {
                    throw new NotImplementedException();
                }

                private static void DisplayNotBelongedParcels(IEnumerable<Parcel> enumerable)
                {
                    throw new NotImplementedException();
                }
        */


        public void DisplayList<T>(IEnumerable<T> list)
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

/*        public static void DisplayNotBelongedParcels(List<Parcel> parcels)
        {
            foreach (var parcel in parcels)
            {
                if (parcel.DroneId == 0)
                    Console.WriteLine(parcel);
            }
        }*/

/*        public static void displayStationsWithEmptyChargingSlots(IEnumerable<Station> stations, List<DroneCharge> droneCharges)
        {
            for (int i = 0; i < stations.Count; i++)
            {
                int ChargeSlots = 0;
                for (int j = 0; j < droneCharges.Count; j++)
                {
                    if (droneCharges[j].stationId == stations[i].Id)
                        ChargeSlots++;

                }
                if (ChargeSlots < stations[i].ChargeSlots)
                {
                    Console.WriteLine(stations[i]);
                    break;
                }
            }
        }*/
    }
}



