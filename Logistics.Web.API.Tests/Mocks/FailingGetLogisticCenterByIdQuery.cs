using System;
using System.Threading.Tasks;
using Logistics.Services.Nodes.Queries;

namespace Logistics.Web.API.Tests.Mocks
{
    public class FailingGetLogisticCenterByIdQuery : IGetLogisticCenterIdQuery
    {
        public Task<int> ExecuteAsync()
        {
            throw new InvalidOperationException("This exception is for testing purposes");
        }
    }
}