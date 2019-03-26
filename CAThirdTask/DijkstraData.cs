namespace CAThirdTask
{
    public struct DijkstraData
    {
        public double Price { get; set; }
        public Node Previous { get; set; }

        public override string ToString() => $"Price: {Price} Previous: {Previous} ";
    }
}