using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CAThirdTask
{
    public static class DataParser
    {
        public static string ResultGenerate(List<Node> path, Graph graph)
        {
            var pathWeight = path?.Count == 1
                ? 0
                : path?.Where((node, i) => i < path.Count - 1)
                      .Select((node, i) => graph[node, path[i + 1]].Weight)
                      .Aggregate(1, (accumulate, weight) => accumulate * weight);
            return path is null
                ? "N"
                : string.Join(Environment.NewLine, "Y", string.Join(" ", path), pathWeight);
        }

        public static (Graph, Node source, Node target) GetInputData(Func<string> lineReader)
        {
            var nodesCount = int.Parse(lineReader().Trim());
            var graph = new Graph(nodesCount);

            for (int nodeNumber = 0; nodeNumber < nodesCount; nodeNumber++)
            {
                var arrowWeightsArray = ReadLineToArrowWeightsArray();
                for (int otherNodeNumber = 0; otherNodeNumber < arrowWeightsArray.Length; otherNodeNumber++)
                    if (arrowWeightsArray[otherNodeNumber] != short.MaxValue && otherNodeNumber != nodeNumber)
                        graph[nodeNumber].ConnectNodes(graph[otherNodeNumber], arrowWeightsArray[otherNodeNumber]);
            }

            var source = new Node(int.Parse(lineReader().Trim()));
            var target = new Node(int.Parse(lineReader().Trim()));

            return (graph, source, target);

            int[] ReadLineToArrowWeightsArray() => Regex.Split(lineReader(), @"\W+")
                                                        .Where(str => str != string.Empty)
                                                        .Select(int.Parse)
                                                        .ToArray();
        }
    }
}