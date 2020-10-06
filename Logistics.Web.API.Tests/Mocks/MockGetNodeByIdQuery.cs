using System.Linq;
using System.Threading.Tasks;
using Logistics.Models;
using Logistics.Services.Nodes.Queries;

namespace Logistics.Web.API.Tests.Mocks
{
    public class MockGetNodeByIdQuery : IGetNodeByIdQuery
    {
        private readonly MockDb db;

        public MockGetNodeByIdQuery(MockDb db)
        {
            this.db = db;
        }

        public async Task<Node> ExecuteAsync(int id)
        {
            return await Task.FromResult(db.Nodes.FirstOrDefault(s => s.Id == id));
        }
    }
}