using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            Console.WriteLine(r);
            double s = r.Next();
            Console.WriteLine(s);
            Console.ReadKey();
        }
    }
}
