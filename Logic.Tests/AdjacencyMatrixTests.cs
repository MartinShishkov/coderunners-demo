using Logistics.Logic;
using Logistics.Models;
using NUnit.Framework;

namespace Logic.Tests
{
    [TestFixture]
    public class AdjacencyMatrixTests
    {
        [Test]
        public void Test01()
        {
            var m = new AdjacencyMatrix(new int[0], new Edge[0]);

            Assert.AreEqual("", m.Identity());
        }

        [Test]
        public void Test02()
        {
            var m = new AdjacencyMatrix(new int[]{1}, new Edge[0]);

            Assert.AreEqual("0", m.Identity());
        }

        [Test]
        public void Test03()
        {
            var m = new AdjacencyMatrix(new int[]{1, 2}, new Edge[0]);
            Assert.AreEqual("0000", m.Identity());
        }

        [Test]
        public void Test04()
        {
            var m = new AdjacencyMatrix(new int[]{1, 2}, new Edge[]
            {
                new Edge(1, 2), 
            });
            Assert.AreEqual("0110", m.Identity());
        }

        [Test]
        public void Test05()
        {
            var m = new AdjacencyMatrix(new int[]{1, 2}, new Edge[]
            {
                new Edge(1, 2), 
                new Edge(2, 1), 
            });
            Assert.AreEqual("0110", m.Identity());
        }

        [Test]
        public void Test06()
        {
            var m = new AdjacencyMatrix(new int[]{1, 2}, new Edge[]
            {
                new Edge(2, 1),
                new Edge(1, 2), 
            });
            Assert.AreEqual("0110", m.Identity());
        }

        [Test]
        public void Test07()
        {
            var m = new AdjacencyMatrix(new int[]{1, 2, 3}, new Edge[]
            {
                new Edge(2, 1),
                new Edge(1, 2), 
            });
            Assert.AreEqual("010100000", m.Identity());
        }

        [Test]
        public void Test08()
        {
            var m = new AdjacencyMatrix(new int[]{1, 2, 3}, new Edge[]
            {
                new Edge(2, 1),
                new Edge(1, 2), 
                new Edge(3, 3), 
            });
            Assert.AreEqual("010100001", m.Identity());
        }

        [Test]
        public void Test09()
        {
            var m = new AdjacencyMatrix(new int[]{1, 2, 3, 4}, new Edge[]
            {
                new Edge(1, 1),
                new Edge(1, 2),
                new Edge(1, 3),
                new Edge(1, 4),
                new Edge(2, 1), 
                new Edge(2, 2), 
                new Edge(2, 3), 
                new Edge(2, 4), 
                new Edge(3, 1), 
                new Edge(3, 2), 
                new Edge(3, 3), 
                new Edge(3, 4), 
                new Edge(4, 1), 
                new Edge(4, 2), 
                new Edge(4, 3), 
                new Edge(4, 4), 
            });
            Assert.AreEqual("1111111111111111", m.Identity());
        }

        [Test]
        public void Test10()
        {
            int a = 1, b = 2, c = 3, d = 4;

            var m1 = new AdjacencyMatrix(new[]{a, b, c, d}, new[]
            {
                new Edge(a, b), 
                new Edge(b, c), 
                new Edge(c, d), 
                new Edge(d, a), 
            });

            var m2 = new AdjacencyMatrix(new[]{a, b, c, d}, new[]
            {
                new Edge(a, b), 
                new Edge(b, a), 
                new Edge(b, c), 
                new Edge(c, b), 
                new Edge(c, d), 
                new Edge(d, c), 
                new Edge(d, a), 
                new Edge(a, d), 
            });

            Assert.AreEqual(m1.Identity(), m2.Identity());
        }
    }
}