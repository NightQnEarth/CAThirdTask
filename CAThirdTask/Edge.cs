using System;

namespace CAThirdTask
{
    public class Edge
    {
        public readonly Node From;
        public readonly Node To;
        public readonly int Weight;

        public Edge(Node from, Node to, int weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }

        public bool IsIncident(Node node) => From.Equals(node) || To.Equals(node);

        public Node GetOtherNode(Node node)
        {
            if (!IsIncident(node)) throw new ArgumentException("The edge is not incident for given node.");
            return From.Equals(node) ? To : From;
        }
    }
}