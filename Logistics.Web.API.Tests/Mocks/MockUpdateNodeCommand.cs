using System.Threading.Tasks;
using Logistics.Services.Nodes.Commands;

namespace Logistics.Web.API.Tests.Mocks
{
    public class MockUpdateNodeCommand : IUpdateNodeCommand
    {
        public async Task<bool> ExecuteAsync(int id, string name)
        {
            return await Task.FromResult(true);
        }
    }
}