
namespace TeacherComputerRetrieval
{
    public class Graph
    {
        private readonly Dictionary<char, List<(char destination, int distance)>> _adjacencyList;

        public Graph(string input)
        {
            _adjacencyList = new Dictionary<char, List<(char, int)>>();
            ParseInput(input);

            // Debug: Print adjacency list
            Console.WriteLine("Adjacency List:");
            foreach (var key in _adjacencyList.Keys)
            {
                Console.WriteLine($"{key}: {string.Join(", ", _adjacencyList[key].Select(e => $"{e.destination}({e.distance})"))}");
            }
        }


        private void ParseInput(string input)
        {
            var routes = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var route in routes)
            {
                if (route.Length < 3 || !char.IsLetter(route[0]) || !char.IsLetter(route[1]))
                    throw new FormatException($"Invalid route format: {route}");

                char start = route[0];
                char end = route[1];

                if (!int.TryParse(route.Substring(2), out int distance))
                    throw new FormatException($"Invalid distance in route: {route}");

                if (!_adjacencyList.ContainsKey(start))
                    _adjacencyList[start] = new List<(char, int)>();

                _adjacencyList[start].Add((end, distance));
            }
        }



        public string GetRouteDistance(string route)
        {
            if (route.Length < 2) return "NO SUCH ROUTE";  // Handle single-node case

            int totalDistance = 0;
            for (int i = 0; i < route.Length - 1; i++)
            {
                char current = route[i];
                char next = route[i + 1];
                if (!_adjacencyList.ContainsKey(current))
                    return "NO SUCH ROUTE";

                var neighbors = _adjacencyList[current];
                var edge = neighbors.Find(n => n.Item1 == next);
                if (edge == default((char, int)))  // Correct way to check if no valid edge exists
                    return "NO SUCH ROUTE";

                totalDistance += edge.Item2;

            }
            return totalDistance.ToString();
        }


        public (int count, List<string> paths) CountTripsWithMaxStops(char start, char end, int maxStops)
        {
            var paths = new List<string>();
            int count = CountTrips(start, end, 0, maxStops, true, start.ToString(), paths);
            return (count, paths);
        }


        public (int count, List<string> paths) CountTripsWithExactStops(char start, char end, int exactStops)
        {
            var paths = new List<string>();
            int count = CountTrips(start, end, 0, exactStops, false, start.ToString(), paths);
            return (count, paths);
        }


        private int CountTrips(char current, char end, int stops, int limit, bool isMax, string path, List<string> paths)
        {
            if (stops > limit) return 0;

            // If we reach the end node with valid stops, count the path
            if (current == end && stops > 0)
            {
                if (isMax || stops == limit)
                {
                    paths.Add(path);
                    return 1;
                }
            }

            if (!_adjacencyList.ContainsKey(current)) return 0;

            int count = 0;
            foreach (var (next, _) in _adjacencyList[current])
            {
                count += CountTrips(next, end, stops + 1, limit, isMax, path + "-" + next, paths);
            }
            return count;
        }


        //private int CountTrips(char current, char end, int stops, int limit, bool isMax, List<char> currentPath, List<string> paths)
        //{
        //    if (stops > limit) return 0;
        //    if (current == end && stops > 0)
        //    {
        //        if (isMax || stops == limit)
        //        {
        //            paths.Add(string.Join("-", currentPath));
        //            return 1;
        //        }
        //        return 0;
        //    }

        //    if (!_adjacencyList.ContainsKey(current)) return 0;

        //    int count = 0;
        //    foreach (var (next, _) in _adjacencyList[current])
        //    {
        //        currentPath.Add(next);
        //        count += CountTrips(next, end, stops + 1, limit, isMax, currentPath, paths);
        //        currentPath.RemoveAt(currentPath.Count - 1);
        //    }
        //    return count;
        //}

        //public (int distance, string path) GetShortestRouteDistance(char start, char end)
        //{
        //    if (start == end)
        //    {
        //        // Special case: Find the shortest cycle (B -> B)
        //        int minCycleDistance = int.MaxValue;
        //        string shortestCyclePath = "NO PATH";

        //        if (_adjacencyList.ContainsKey(start))
        //        {
        //            foreach (var (neighbor, dist) in _adjacencyList[start])
        //            {
        //                var (cycleDist, cyclePath) = Dijkstra(neighbor, start);
        //                if (cycleDist != -1)
        //                {
        //                    int totalCycleDist = dist + cycleDist;
        //                    if (totalCycleDist < minCycleDistance)
        //                    {
        //                        minCycleDistance = totalCycleDist;
        //                        shortestCyclePath = start + "-" + cyclePath;
        //                    }
        //                }
        //            }
        //        }
        //        return (minCycleDistance == int.MaxValue ? -1 : minCycleDistance, shortestCyclePath);
        //    }
        //    return Dijkstra(start, end);
        //}

        public (int, string) GetShortestRouteDistance(char start, char end)
        {
            if (!_adjacencyList.ContainsKey(start) || !_adjacencyList.ContainsKey(end))
                return (-1, "NO SUCH ROUTE");

            var (distance, path) = Dijkstra(start, end);
            return distance == int.MaxValue ? (-1, "NO SUCH ROUTE") : (distance, path);
        }




        private (int, string) Dijkstra(char start, char end)
        {
            if (!_adjacencyList.ContainsKey(start) || !_adjacencyList.ContainsKey(end))
                return (-1, "NO SUCH ROUTE");

            var distances = new Dictionary<char, int>();
            var previousNodes = new Dictionary<char, char?>(); // Track the shortest path
            var unvisited = new HashSet<char>();

            foreach (var node in _adjacencyList.Keys)
            {
                distances[node] = int.MaxValue;
                previousNodes[node] = null;
                unvisited.Add(node);
            }
            distances[start] = 0;

            while (unvisited.Count > 0)
            {
                // Get the node with the smallest known distance
                char current = unvisited.OrderBy(n => distances[n]).First();
                unvisited.Remove(current);

                // If we've reached the destination, reconstruct and return the path
                if (current == end && distances[end] != int.MaxValue)
                {
                    string path = ReconstructPath(previousNodes, start, end);
                    return (distances[end], path);
                }

                if (!_adjacencyList.ContainsKey(current)) continue;

                foreach (var (neighbor, distance) in _adjacencyList[current])
                {
                    if (!unvisited.Contains(neighbor)) continue;

                    int newDist = distances[current] + distance;
                    if (newDist < distances[neighbor])
                    {
                        distances[neighbor] = newDist;
                        previousNodes[neighbor] = current;
                    }
                }
            }

            return (-1, "NO SUCH ROUTE"); // No valid route found
        }


        private string ReconstructPath(Dictionary<char, char?> previousNodes, char start, char end)
        {
            var path = new Stack<char>();
            char? current = end;
            while (current != null)
            {
                path.Push(current.Value);
                current = previousNodes[current.Value];
            }
            return string.Join("-", path);
        }



        public (int count, List<string> paths) CountTripsWithDistanceLimit(char start, char end, int maxDistance)
        {
            var paths = new List<string>();
            int count = CountTripsWithDistance(start, end, 0, maxDistance, start.ToString(), paths);
            return (count, paths);
        }

        private int CountTripsWithDistance(char current, char end, int currentDistance, int maxDistance, string path, List<string> paths)
        {
            if (currentDistance >= maxDistance) return 0;
            int tripCount = 0;

            if (current == end && currentDistance > 0)
            {
                paths.Add(path);  // Store valid path
                tripCount++;
            }

            if (!_adjacencyList.ContainsKey(current)) return tripCount;

            foreach (var (next, distance) in _adjacencyList[current])
            {
                tripCount += CountTripsWithDistance(next, end, currentDistance + distance, maxDistance, path + "-" + next, paths);
            }

            return tripCount;
        }

    }
}