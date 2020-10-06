using System.Collections.Generic;
using System.Linq;
using Logistics.Models;

namespace Logistics.Web.API.Tests.Mocks
{
    public class MockDb
    {
        private readonly IEnumerable<Edge> edges;
        private readonly IEnumerable<Node> nodes;

        public MockDb(IEnumerable<Node> nodes, IEnumerable<Edge> edges)
        {
            this.nodes = nodes;
            this.edges = edges;
        }

        public Edge[] Edges => edges.ToArray();
        public Node[] Nodes => nodes.ToArray();
    }
}