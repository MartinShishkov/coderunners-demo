using System.Collections.Generic;
using System.Threading.Tasks;
using Logistics.Models;
using Logistics.Services.Nodes.Queries;

namespace Logistics.Web.API.Tests.Mocks
{
    public class MockGetNodesQuery : IGetNodesQuery
    {
        private readonly MockDb db;

        public MockGetNodesQuery(MockDb db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Node>> ExecuteAsync()
        {
            return await Task.FromResult(db.Nodes);
        }
    }
}