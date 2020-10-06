using Logistics.Logic;
using Logistics.Models;
using NUnit.Framework;

namespace Logic.Tests
{
    [TestFixture]
    public class LogisticCenterServiceTests
    {
        [Test]
        public void SingleVertexIsTheCenterOfTheGraph2()
        {
            var m = new AdjacencyMatrix(new int[]{11}, new Edge[0]);
            var service = new LogisticCenterService(m);

            var center = service.CalculateLogisticCenter();

            Assert.AreEqual(11, center);
        }

        [Test]
        public void SingleVertexIsTheCenterOfTheGraph()
        {
            var m = new AdjacencyMatrix(new[]{11}, new[] { new Edge(11, 11), });
            var service = new LogisticCenterService(m);

            var center = service.CalculateLogisticCenter();

            Assert.AreEqual(11, center);
        }

        [Test]
        public void GraphWithDisconnectedVerticesDoesNotHaveCenter()
        {
            var m = new AdjacencyMatrix(new[]{11, 12}, 
                new[] { new Edge(11, 11), new Edge(12, 12) });

            var service = new LogisticCenterService(m);

            var center = service.CalculateLogisticCenter();

            Assert.AreEqual(-1, center);
        }

        [Test]
        public void TwoConnectedVerticesShouldReturnSecondVertexAsCenter()
        {
            var m = new AdjacencyMatrix(new int[]{11, 12}, 
                new[] { new Edge(11, 12), new Edge(12, 11) });

            var service = new LogisticCenterService(m);

            var center = service.CalculateLogisticCenter();

            Assert.AreEqual(12, center);
        }

        [Test]
        public void ThreeConnectedVerticesShouldReturnMiddleVertexAsCenter()
        {
            var m = new AdjacencyMatrix(new[]{11, 12, 13}, 
                new[] {
                    new Edge(11, 12),
                    new Edge(12, 11),
                    new Edge(12, 13),
                    new Edge(13, 12),
            });

            var service = new LogisticCenterService(m);

            var center = service.CalculateLogisticCenter();

            Assert.AreEqual(12, center);
        }

        [Test]
        public void RectangleGraphCenterShouldBeLastVertex()
        {
            var m = new AdjacencyMatrix(new[]{1, 2, 3, 4}, new[]
            {
                new Edge(1, 2),
                new Edge(1, 4),
                new Edge(2, 1),
                new Edge(2, 3),
                new Edge(3, 2),
                new Edge(3, 4),
                new Edge(4, 1),
                new Edge(4, 3),
            });

            var service = new LogisticCenterService(m);

            var center = service.CalculateLogisticCenter();

            Assert.AreEqual(4, center);
        }

        [Test]
        public void Test5()
        {
            int a = 1, b = 2, c = 3, d = 4, e = 5;
            var m = new AdjacencyMatrix(new[]{a, b, c, d, e}, new[]
            {
                new Edge(a, b),
                new Edge(a, d),
                new Edge(b, a),
                new Edge(b, c),
                new Edge(c, b),
                new Edge(c, d),
                new Edge(d, a),
                new Edge(d, c),
                new Edge(d, e),
                new Edge(e, d),
            });

            var service = new LogisticCenterService(m);

            var center = service.CalculateLogisticCenter();

            Assert.AreEqual(d, center);
        }

        [Test]
        public void Test6()
        {
            int a = 1, b = 2, c = 3, d = 4, e = 5;
            var m = new AdjacencyMatrix(new[]{a, b, c, d, e}, new[]
            {
                new Edge(a, b),
                new Edge(a, e),
                new Edge(a, d),
                new Edge(b, a),
                new Edge(b, e),
                new Edge(b, c),
                new Edge(c, b),
                new Edge(c, e),
                new Edge(c, d),
                new Edge(d, a),
                new Edge(d, e),
                new Edge(d, c),
                new Edge(e, a),
                new Edge(e, b),
                new Edge(e, c),
                new Edge(e, d),
            });

            var service = new LogisticCenterService(m);

            var center = service.CalculateLogisticCenter();

            Assert.AreEqual(e, center);
        }

        [Test]
        public void GraphWithDisconnectedVertexShouldHaveNoCenter()
        {
            int a = 1, b = 2, c = 3, d = 4, e = 5, f = 6;

            var m = new AdjacencyMatrix(new[] {a, b, c, d, e, f}, new[]
            {
                new Edge(a, b),
                new Edge(a, e),
                new Edge(a, d),
                new Edge(b, a),
                new Edge(b, e),
                new Edge(b, c),
                new Edge(c, b),
                new Edge(c, e),
                new Edge(c, d),
                new Edge(d, a),
                new Edge(d, e),
                new Edge(d, c),
                new Edge(e, a),
                new Edge(e, b),
                new Edge(e, c),
                new Edge(e, d),
                new Edge(f, f),
            });

            var service = new LogisticCenterService(m);

            var center = service.CalculateLogisticCenter();

            Assert.AreEqual(-1, center);
        }
    }
}
