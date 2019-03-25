using System.Collections.Generic;
using System.Linq;

namespace CAThirdTask
{
    public class Node
    {
        public readonly int Number;
        private readonly List<Edge> incidentsEdges = new List<Edge>();

        public Node(int nodeNumber) => Number = nodeNumber;

        public IEnumerable<Edge> IncidentEdges => incidentsEdges.Select(edge => edge);

        public void MakeAdjacent(Node to, int edgeWeight)
        {
            incidentsEdges.Add(new Edge(this, to, edgeWeight));
            to.incidentsEdges.Add(incidentsEdges.Last());
        }

        public override bool Equals(object obj) => obj is Node node && node.Number == Number;

        public override int GetHashCode() => Number;

        public override string ToString() => Number.ToString();
    }
}