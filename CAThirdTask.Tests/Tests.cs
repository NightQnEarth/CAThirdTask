using System;
using System.IO;
using NUnit.Framework;

namespace CAThirdTask.Tests
{
    [TestFixture]
    public class Tests
    {
        private const string ExpectedNegativeResult = "N";

        private readonly DijkstraPathFinder pathFinder = new DijkstraPathFinder();

        private string GetActualResult(string inputLines)
        {
            var tempFileName = Path.GetTempFileName();

            using (var writer = new StreamWriter(tempFileName))
                writer.Write(inputLines);

            var actualResult = GetActualResult();

            try
            {
                File.Delete(tempFileName);
            }
            catch (IOException) { }

            return actualResult;

            string GetActualResult()
            {
                using (var reader = new StreamReader(tempFileName))
                {
                    var (graph, start, finish) = Program.GetInputData(reader.ReadLine);
                    var resultPath = pathFinder.GetShortestPath(graph, start, finish);
                    return Program.ResultGenerate(resultPath, graph);
                }
            }
        }

        [Test]
        public void SampleGraphFromReadme_ReturnRoute()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "4",
                "32767 25 4 32767",
                "32767 32767 32767 32767",
                "32767 0 32767 7",
                "32767 32767 32767 32767",
                "1",
                "4");

            var expectedResult = string.Join(
                Environment.NewLine,
                "Y",
                "1 3 4",
                "28");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }
    }
}