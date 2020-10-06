using System.Threading.Tasks;
using Logistics.Models;
using Logistics.Services.Nodes.Queries;

namespace Logistics.Web.API.Tests.Mocks
{
    public class FailingGetNodeByIdQuery : IGetNodeByIdQuery
    {
        public Task<Node> ExecuteAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}