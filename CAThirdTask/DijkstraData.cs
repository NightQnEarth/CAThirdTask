namespace CAThirdTask
{
    public class DijkstraData
    {
        public Node Previous;
        public double Price;

        public override string ToString() => $"Previous: {Previous} Price: {Price}";
    }
}