using System.Collections.Generic;
using System.Linq;

namespace CAThirdTask
{
    public class DijkstraPathFinder
    {
        public List<Node> GetShortestPath(Graph graph, Node start, Node finish)
        {
            var notOpenedNodes = graph.Nodes.ToList();
            var track = new Dictionary<Node, DijkstraData>();
            track[start] = new DijkstraData { Price = 1, Previous = null };

            while (true)
            {
                Node toOpenNode = null;
                var bestPrice = double.PositiveInfinity;
                foreach (var node in notOpenedNodes)
                    if (track.ContainsKey(node) && track[node].Price < bestPrice)
                    {
                        bestPrice = track[node].Price;
                        toOpenNode = node;
                    }

                if (toOpenNode is null) return null;
                if (toOpenNode.Equals(finish)) break;

                foreach (var incidentEdge in toOpenNode.IncidentEdges.Where(edge => edge.From.Equals(toOpenNode)))
                {
                    var currentPrice = track[toOpenNode].Price * incidentEdge.Weight;
                    var nextNode = incidentEdge.GetOtherNode(toOpenNode);
                    if (!track.ContainsKey(nextNode) || track[nextNode].Price > currentPrice)
                        track[nextNode] = new DijkstraData { Previous = toOpenNode, Price = currentPrice };
                }

                notOpenedNodes.Remove(toOpenNode);
            }

            var result = new List<Node> { finish };

            while (track[finish].Previous != null && result.Count < graph.NodesCount)
                result.Add(finish = track[finish].Previous);

            result.Reverse();

            return result.Distinct().ToList();
        }
    }
}