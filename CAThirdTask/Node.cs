using System.Collections.Generic;
using System.Linq;

namespace CAThirdTask
{
    public class Node
    {
        private readonly int number;
        private readonly List<Arrow> incidentsArrows = new List<Arrow>();

        public Node(int nodeNumber) => number = nodeNumber;

        public IEnumerable<Arrow> IncidentArrows => incidentsArrows.Select(arrow => arrow);

        public void ConnectNodes(Node head, int arrowWeight)
        {
            incidentsArrows.Add(new Arrow(this, head, arrowWeight));
            head.incidentsArrows.Add(incidentsArrows.Last());
        }

        public override bool Equals(object obj) => obj is Node node && node.number == number;

        public override int GetHashCode() => number;

        public override string ToString() => number.ToString();
    }
}