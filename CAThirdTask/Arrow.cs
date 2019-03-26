using System;

namespace CAThirdTask
{
    public class Arrow
    {
        public readonly Node Tail;
        public readonly Node Head;
        public readonly int Weight;

        public Arrow(Node tail, Node head, int weight)
        {
            Tail = tail;
            Head = head;
            Weight = weight;
        }

        public override string ToString() => $"({Tail}, {Head})";

        public Node GetOtherNode(Node node)
        {
            if (!Tail.Equals(node) && !Head.Equals(node))
                throw new ArgumentException("The arrow is not incident for given node.");

            return Tail.Equals(node) ? Head : Tail;
        }
    }
}