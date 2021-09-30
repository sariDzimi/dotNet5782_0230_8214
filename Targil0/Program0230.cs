using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome8214();
            Welcome0230();
            Console.ReadKey();
        }

        static partial void Welcome8214();

        private static void Welcome0230()
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine($"{name} welcome to my first console application");
        }
    }
}
