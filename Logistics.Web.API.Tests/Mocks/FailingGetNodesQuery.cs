using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logistics.Models;
using Logistics.Services.Nodes.Queries;

namespace Logistics.Web.API.Tests.Mocks
{
    public class FailingGetNodesQuery : IGetNodesQuery
    {
        public Task<IEnumerable<Node>> ExecuteAsync()
        {
            throw new InvalidOperationException("This exception is for testing purposes");
        }
    }
}