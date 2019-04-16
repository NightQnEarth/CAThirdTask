using System;

namespace CAThirdTask
{
    public static class Program
    {
        public static void Main()
        {
            var (graph, source, target) = DataParser.GetInputData(Console.ReadLine);
            var resultPath = DijkstraPathFinder.GetShortestPath(graph, source, target);

            Console.Write(DataParser.ResultGenerate(resultPath, graph));
        }
    }
}