using System.Collections.Generic;
using System.Threading.Tasks;
using Logistics.Models;
using Logistics.Services.Edges.Queries;

namespace Logistics.Web.API.Tests.Mocks
{
    public class MockGetEdgesQuery : IGetEdgesQuery
    {
        private readonly MockDb db;

        public MockGetEdgesQuery(MockDb db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Edge>> ExecuteAsync()
        {
            return await Task.FromResult(db.Edges);
        }
    }
}