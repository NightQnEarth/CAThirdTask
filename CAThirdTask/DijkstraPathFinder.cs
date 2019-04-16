using System.Collections.Generic;
using System.Linq;

namespace CAThirdTask
{
    public static class DijkstraPathFinder
    {
        public static List<Node> GetShortestPath(Graph graph, Node source, Node target)
        {
            var notOpenedNodes = graph.Nodes.ToList();
            var track = new Dictionary<Node, DijkstraData>
            {
                [source] = new DijkstraData { Price = 1, Previous = null }
            };

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
                if (toOpenNode.Equals(target)) break;

                foreach (var incidentArrow in toOpenNode.IncidentArrows.Where(arrow => arrow.Tail.Equals(toOpenNode)))
                {
                    var currentPrice = track[toOpenNode].Price * incidentArrow.Weight;
                    var nextNode = incidentArrow.GetOtherNode(toOpenNode);
                    if (!track.ContainsKey(nextNode) || track[nextNode].Price > currentPrice)
                        track[nextNode] = new DijkstraData { Price = currentPrice, Previous = toOpenNode };
                }

                notOpenedNodes.Remove(toOpenNode);
            }

            var result = new List<Node> { target };

            while (track[target].Previous != null && result.Count < graph.NodesCount)
                result.Add(target = track[target].Previous);

            result.Reverse();

            return result.Distinct().ToList();
        }
    }
}