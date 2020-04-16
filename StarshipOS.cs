using System;
using System.Collections.Generic;

namespace DesignPatterns.GangOfFour.Creational.Singleton
{
    /// <summary>
    /// Singleton Design Pattern.
    /// 
    /// Definition: The Singleton Design Pattern ensures a class has only one instance 
    ///             and provides a global point of access to it.
    /// 
    /// The Singleton defines an 'instance' operation that allows clients to access
    /// the unique instance.
    /// 
    /// The Singleton is also responsible for creating and maintaining its own 
    /// unique instance.
    /// 
    /// The example below uses the Singleton Pattern to implement a primary computer/operating
    /// system (OS) for Star Trek starships.  Each ship's computer should be a unique instance
    /// (i.e. no ship should run two operating systems simultaneously)
    /// 
    /// Each request to the different clients on the ship (engineering, medical, bridge, etc) 
    /// all must go through the primary computer; however, there is only one, unique instance of  
    /// the primary computer/operating system per ship.
    /// </summary>
    public class Program
    {
     
   
        static void Main()
        {
            var b1 = PrimaryShipOS.GetShipOS();
            var b2 = PrimaryShipOS.GetShipOS();
            var b3 = PrimaryShipOS.GetShipOS();
            var b4 = PrimaryShipOS.GetShipOS();

            // Confirm these are the same instance
            if (b1 == b2 && b2 == b3 && b3 == b4)
            {
                Console.WriteLine("Same instance\n");
            }

            // Next, manage 15 requests for a particular client on a Starship
            var shipOS = PrimaryShipOS.GetShipOS();
            for (int i = 0; i < 15; i++)
            {
                string subRoutineName = shipOS.NextStarshipSubroutine.Name;
                Console.WriteLine("Dispatch request to: " + subRoutineName);
            }

            // Wait for user
            Console.ReadKey();
        }
    }

    /// <summary>
    /// The 'Singleton' class
    /// 
    /// We use the 'sealed' modifier to prevent classes from
    /// inheriting from the PrimaryShipOS...
    /// </summary>
    sealed class PrimaryShipOS
    {
        // Static memebers are initialized instantly the first
        // time that the class is loaded.
       
        // .NET guarantees thread safety for static initialization
        static readonly PrimaryShipOS instance = new PrimaryShipOS();

        // Type-safe generic list of starshipSubroutines
        List<StarshipSubroutine> starshipSubroutines;
        Random random = new Random();

        // Note: Singletons use private constructors...
        private PrimaryShipOS()
        {
            // Create a list of subRoutines and load the list...
            starshipSubroutines = new List<StarshipSubroutine>
                {
                  new StarshipSubroutine{ Name = "Warp Drive Diagnostics", Location = "Engineering" },
                  new StarshipSubroutine{ Name = "SIF Generator Calibriation", Location = "Engineering" },
                  new StarshipSubroutine{ Name = "Shield Modulators", Location = "Bridge" },
                  new StarshipSubroutine{ Name = "Terraformer", Location = "Holodeck Server" },
                  new StarshipSubroutine{ Name = "Heartrate Analyzer", Location = "Medical" },
                };
        }

        public static PrimaryShipOS GetShipOS()
        {
            return instance;
        }

        // Manages subroutine requests
        public StarshipSubroutine NextStarshipSubroutine
        {
            get
            {
                int r = random.Next(starshipSubroutines.Count);
                return starshipSubroutines[r];
            }
        }
    }

    /// <summary>
    /// Represents subroutine generated from a location on a Starship...
    /// </summary>
    class StarshipSubroutine
    {
        // Gets or sets StarshipSubroutine name
        public string Name { get; set; }

        // Gets or sets StarshipSubroutine location
        public string Location { get; set; }
    }
}
