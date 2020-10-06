using System.Threading.Tasks;
using Logistics.Services.Nodes.Queries;

namespace Logistics.Web.API.Tests.Mocks
{
    public class MockGetLogisticCenterIdQuery : IGetLogisticCenterIdQuery
    {
        public async Task<int> ExecuteAsync()
        {
            return await Task.FromResult(1);
        }
    }
}