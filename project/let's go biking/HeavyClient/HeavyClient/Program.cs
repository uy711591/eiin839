using System;
using HeavyClient.RoutingWithBikesSoap;

namespace HeavyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceClient service = new ServiceClient();
            Console.WriteLine("Please enter the departure address :");
            string departure = Console.ReadLine();
            Console.WriteLine("Please enter the destination address :");
            string destination = Console.ReadLine();
            Itinirary[] itiniraries = service.getClosestStations(departure, destination);
            string[] stops = { departure, "pick up station", "drop off station", destination };
            for(int i = 0; i < itiniraries.Length; i++)
            {
                Console.WriteLine("******");
                Console.WriteLine("You are at a pin point");
                Console.Write((i % 2 == 0) ? "walking" : "riding the bike");
                Console.WriteLine(" from : " + stops[i] + "to destination\n");
                foreach (Feature feature in itiniraries[i].features)
                {
                    foreach (Segment segment in feature.properties.segments)
                    {
                        foreach (Step step in segment.steps)
                        {
                            Console.WriteLine("\t" + step.instruction);
                            Console.WriteLine("\tdistance : " + step.distance);
                            Console.WriteLine("\tduration : " + step.duration);
                            Console.WriteLine("");
                            // simulate movement
                            System.Threading.Thread.Sleep(700);
                        }
                    }
                }
                Console.WriteLine("******");
                Console.WriteLine("\n");
            }
            Console.WriteLine("******");
            Console.WriteLine("You have arrived at your destination : " + destination);
            Console.WriteLine("******");
            Console.WriteLine("\n");
            Console.WriteLine("Press on any key to quit.");
            Console.ReadLine();
        }
    }
}
