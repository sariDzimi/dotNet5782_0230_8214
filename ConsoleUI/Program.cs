using System;

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
                case 2:
                    Console.WriteLine("to conect a parcel to a drone enter 1");
                    Console.WriteLine("to collect a parcel by a drone enter 2");
                    Console.WriteLine("to supply a parcel to a customer enter 3");
                    Console.WriteLine("to send a drone to charge in a station enter 4");
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
    }
}
