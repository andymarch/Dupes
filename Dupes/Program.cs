using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dupes.Core;

namespace Dupes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("___ Dupes ___");

            string sourceLine = "";
            while (string.IsNullOrEmpty(sourceLine) || !Directory.Exists(sourceLine))
            {
                Console.WriteLine("Enter source directory");
                sourceLine = Console.ReadLine();
            }

            Console.WriteLine("Source line of: " + sourceLine + " accepted");
            string destinationLine = "";
            while (string.IsNullOrEmpty(destinationLine) || !Directory.Exists(destinationLine))
            {
                Console.WriteLine("Enter destination directory");
                destinationLine = Console.ReadLine();
            }
            Console.WriteLine("Destination line of: " + destinationLine + " accepted");

            DupeAction action = new DupeAction(
                                                new Pattern()
                                                {
                                                    Source = sourceLine,
                                                    Destination=destinationLine
                                                });
            Thread DAThread = new Thread(new ThreadStart(action.Begin));
            DAThread.Start();
            string commandLine = "";
            while (commandLine != "exit")
            {
                Console.WriteLine("Type 'exit' to quit...");
                commandLine = Console.ReadLine();
            }
            action.End();
            Console.WriteLine("Shut down successful");
        }
    }
}
