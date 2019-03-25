using System.Collections.Generic;
using System.Linq;

namespace CAThirdTask
{
    public class Graph
    {
        private readonly Node[] nodes;

        public Graph(int nodesCount) =>
            nodes = Enumerable.Range(1, nodesCount)
                              .Select(nodeNumber => new Node(nodeNumber))
                              .ToArray();

        public IEnumerable<Node> Nodes => nodes.Select(node => node);

        public Node this[int nodeNumber] => nodes[nodeNumber];

        public Edge this[Node from, Node to] =>
            nodes.First(node => node.Equals(from)).IncidentEdges.First(edge => edge.IsIncident(to));
    }
}