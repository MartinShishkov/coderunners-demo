using System.Threading.Tasks;
using Logistics.Services.Edges.Commands;

namespace Logistics.Web.API.Tests.Mocks
{
    public class MockCreateEdgeCommand : ICreateEdgeCommand
    {
        private readonly MockDb db;

        public MockCreateEdgeCommand(MockDb db)
        {
            this.db = db;
        }

        public async Task<bool> ExecuteAsync(int @from, int to)
        {
            return await Task.FromResult(true);
        }
    }
}