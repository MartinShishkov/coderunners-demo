using System.Threading.Tasks;
using Logistics.Models;
using Logistics.Services.Nodes.Commands;

namespace Logistics.Web.API.Tests.Mocks
{
    public class MockCreateNodeCommand : ICreateNodeCommand
    {
        public async Task<Node> ExecuteAsync(string name)
        {
            return await Task.FromResult(new Node(1, name));
        }
    }
}