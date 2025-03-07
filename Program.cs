using System;

namespace TeacherComputerRetrieval
{
    class Program
    {
        static void Main(string[] args)
        {
            //string input = "AB5,BC4,CD8,DC8,DE6,AD5,CE2,EB3,AE7";
            Console.WriteLine("Enter routes (e.g., AB5, BC4, CD8):");
            string input = Console.ReadLine();
            var graph = new Graph(input);

            // Test queries
            Console.WriteLine(graph.GetRouteDistance("ABC"));           // 1. 9
            Console.WriteLine(graph.GetRouteDistance("AEBCD"));         // 2. 22
            Console.WriteLine(graph.GetRouteDistance("AED"));           // 3. NO SUCH ROUTE
            Console.WriteLine(graph.CountTripsWithMaxStops('C', 'C', 3)); // 4. 2
            Console.WriteLine(graph.CountTripsWithExactStops('A', 'C', 4)); // 5. 3
            Console.WriteLine(graph.GetShortestRouteDistance('A', 'C')); // 6. 9
            Console.WriteLine(graph.GetShortestRouteDistance('B', 'B')); // 7. 9
            Console.WriteLine(graph.CountTripsWithDistanceLimit('C', 'C', 30)); // 8. 7
        }
    }
}