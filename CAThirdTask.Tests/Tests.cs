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
        public void SampleGraphFromReadme_ReturnPath()
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

        [Test]
        public void TwoNodesConnectedGraph_ReturnPath()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "2",
                "32767 4",
                "32767 32767",
                "1",
                "2");

            var expectedResult = string.Join(
                Environment.NewLine,
                "Y",
                "1 2",
                "4");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void TwoNodesDisconnectedGraph_ReturnN()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "2",
                "32767 32767",
                "32767 32767",
                "1",
                "2");

            Assert.AreEqual(ExpectedNegativeResult, GetActualResult(inputLines));
        }

        [Test]
        public void ThreeNodesGraph_ReturnPath()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "3",
                "32767 4 32767",
                "32767 32767 5",
                "32767 32767 32767",
                "1",
                "3");

            var expectedResult = string.Join(
                Environment.NewLine,
                "Y",
                "1 2 3",
                "20");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void ThreeNodesCyclicGraph_ReturnPath()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "3",
                "32767 4 32767",
                "32767 32767 5",
                "6 32767 32767",
                "1",
                "3");

            var expectedResult = string.Join(
                Environment.NewLine,
                "Y",
                "1 2 3",
                "20");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void EmptyGraph_ReturnN()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "6",
                "32767 32767 32767 32767 32767 32767",
                "32767 32767 32767 32767 32767 32767",
                "32767 32767 32767 32767 32767 32767",
                "32767 32767 32767 32767 32767 32767",
                "32767 32767 32767 32767 32767 32767",
                "32767 32767 32767 32767 32767 32767",
                "2",
                "5");

            Assert.AreEqual(ExpectedNegativeResult, GetActualResult(inputLines));
        }

        [Test]
        public void FiveNodesGraphWithUnreachableTarget_ReturnN()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "5",
                "32767 25 4 32767 32767",
                "32767 32767 32767 32767 32767",
                "32767 0 32767 7 32767",
                "32767 32767 32767 32767 32767",
                "32767 32767 32767 1 6",
                "1",
                "5");

            Assert.AreEqual(ExpectedNegativeResult, GetActualResult(inputLines));
        }

        [Test]
        public void FiveNodesCompleteGraphWithLoops_ReturnPath()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "5",
                "1 2 9 4 5",
                "0 34 23 2 1",
                "0 0 32767 7 32767",
                "55 1618 1488 9 7",
                "12 3 7 1 32766",
                "2",
                "3");

            var expectedResult = string.Join(
                Environment.NewLine,
                "Y",
                "2 1 3",
                "0");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void SixNodesDisconnectedGraph_ReturnPath()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "6",
                "32767 32767 32767 32767 32767 32767",
                "32767 32767 66 32767 32767 32767",
                "32767 32767 32767 32767 32767 1",
                "32767 32767 32767 32767 32767 32767",
                "32767 32767 32767 32767 32767 32767",
                "32767 32767 32767 32767 32767 32767",
                "2",
                "6");

            var expectedResult = string.Join(
                Environment.NewLine,
                "Y",
                "2 3 6",
                "66");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void OneNodeGraph_ReturnPath()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "1",
                "32767",
                "1",
                "1");

            var expectedResult = string.Join(
                Environment.NewLine,
                "Y",
                "1",
                "0");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void FourNodesGraphWhereStartAndFinishCoincide_ReturnPath()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "4",
                "32767 25 4 32767",
                "32767 32767 32767 32767",
                "32767 0 32767 7",
                "32767 32767 32767 32767",
                "3",
                "3");

            var expectedResult = string.Join(
                Environment.NewLine,
                "Y",
                "3",
                "0");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void FourNodesGraphWithDeceptiveEquilibriumPaths_ReturnPath()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "4",
                "32767 2 1 32767",
                "32767 32767 32767 2",
                "32767 32767 32767 3",
                "32767 32767 32767 32767",
                "1",
                "4");

            var expectedResult = string.Join(
                Environment.NewLine,
                "Y",
                "1 3 4",
                "3");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }

        [Test]
        public void FourNodesGraphWithEqualsPaths_ReturnPath()
        {
            var inputLines = string.Join(
                Environment.NewLine,
                "4",
                "32767 2 2 32767",
                "32767 32767 32767 2",
                "32767 32767 32767 2",
                "32767 32767 32767 32767",
                "1",
                "4");

            var expectedResult = string.Join(
                Environment.NewLine,
                "Y",
                "1 2 4",
                "4");

            Assert.AreEqual(expectedResult, GetActualResult(inputLines));
        }
    }
}