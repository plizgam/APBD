using System;
using System.IO;

namespace Cw2
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Users\Miły\Desktop\dane.csv";

            var lines = File.ReadLines(path);

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }

            var today = DateTime.UtcNow;

            Console.WriteLine(today);
        }
    }
}
