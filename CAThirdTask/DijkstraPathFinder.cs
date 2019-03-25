using System.Collections.Generic;
using System.Linq;

namespace CAThirdTask
{
    public class DijkstraPathFinder
    {
        public List<Node> GetShortestPath(Graph graph, Node start, Node finish)
        {
            var notVisited = graph.Nodes.ToList();
            var track = new Dictionary<Node, DijkstraData>();
            track[start] = new DijkstraData { Price = 0, Previous = null };

            while (true)
            {
                Node toOpen = null;
                var bestPrice = double.PositiveInfinity;
                foreach (var e in notVisited)
                    if (track.ContainsKey(e) && track[e].Price < bestPrice)
                    {
                        bestPrice = track[e].Price;
                        toOpen = e;
                    }

                if (toOpen is null) return null;
                if (toOpen.Equals(finish)) break;

                foreach (var incidentEdge in toOpen.IncidentEdges.Where(z => z.From.Equals(toOpen)))
                {
                    var currentPrice = track[toOpen].Price + incidentEdge.Weight;
                    var nextNode = incidentEdge.GetOtherNode(toOpen);
                    if (!track.ContainsKey(nextNode) || track[nextNode].Price > currentPrice)
                        track[nextNode] = new DijkstraData { Previous = toOpen, Price = currentPrice };
                }

                notVisited.Remove(toOpen);
            }

            var result = new List<Node> { finish };

            while (finish != null) result.Add(finish = track[finish].Previous);

            result.Reverse();

            return result;
        }
    }
}