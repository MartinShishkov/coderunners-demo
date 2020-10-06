using System.Threading.Tasks;
using Logistics.Services.Nodes.Commands;

namespace Logistics.Web.API.Tests.Mocks
{
    public class MockDeleteNodeCommand : IDeleteNodeCommand
    {
        public async Task<bool> ExecuteAsync(int nodeId)
        {
            return await Task.FromResult(true);
        }
    }
}