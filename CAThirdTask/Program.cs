using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CAThirdTask
{
    public static class Program
    {
        public static void Main()
        {
            var pathfinder = new DijkstraPathFinder();
            var (graph, start, finish) = GetInputData(Console.ReadLine);
            var resultPath = pathfinder.GetShortestPath(graph, start, finish);

            Console.Write(ResultGenerate(resultPath, graph));
        }

        public static string ResultGenerate(List<Node> path, Graph graph)
        {
            var pathWeight = path?.Where((node, i) => i < path.Count - 1)
                                 .Select((node, i) => graph[node, path[i + 1]].Weight)
                                 .Aggregate(1, (accumulate, weight) => accumulate * weight);
            return path is null
                ? "N"
                : string.Join(Environment.NewLine, "Y", string.Join(" ", path), pathWeight);
        }

        public static (Graph, Node start, Node finish) GetInputData(Func<string> lineReader)
        {
            var nodesCount = int.Parse(lineReader().Trim());
            var graph = new Graph(nodesCount);

            for (int nodeNumber = 0; nodeNumber < nodesCount; nodeNumber++)
            {
                var edgeWeightsArray = ReadLineToEdgeWeightsArray();
                for (int otherNodeNumber = 0; otherNodeNumber < edgeWeightsArray.Length; otherNodeNumber++)
                    if (edgeWeightsArray[otherNodeNumber] != short.MaxValue && otherNodeNumber != nodeNumber)
                        graph[nodeNumber].MakeAdjacent(graph[otherNodeNumber], edgeWeightsArray[otherNodeNumber]);
            }

            var start = new Node(int.Parse(lineReader().Trim()));
            var finish = new Node(int.Parse(lineReader().Trim()));

            return (graph, start, finish);

            int[] ReadLineToEdgeWeightsArray() => Regex.Split(lineReader(), @"\W+")
                                                       .Where(str => str != string.Empty)
                                                       .Select(int.Parse)
                                                       .ToArray();
        }
    }
}