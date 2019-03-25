using System.Collections.Generic;
using System.Linq;

namespace CAThirdTask
{
    public class Node
    {
        public readonly int Number;
        private readonly List<Node> adjacentNodes;
        private readonly List<Edge> incidentsEdges = new List<Edge>();

        public Node(int nodeNumber)
        {
            Number = nodeNumber;
            adjacentNodes = new List<Node>();
        }

        public IEnumerable<Edge> IncidentEdges => incidentsEdges.Select(edge => edge);

        public void MakeAdjacent(Node to, int edgeWeight)
        {
            adjacentNodes.Add(to);
            to.adjacentNodes.Add(this);
            incidentsEdges.Add(new Edge(this, to, edgeWeight));
            to.incidentsEdges.Add(incidentsEdges.Last());
        }

        public override bool Equals(object obj) =>
            obj is Node node && node.Number == Number && node.adjacentNodes == adjacentNodes;

        public override int GetHashCode() =>
            unchecked((Number * 397) ^ (adjacentNodes != null ? adjacentNodes.GetHashCode() : 0));

        public override string ToString() => Number.ToString();
    }
}