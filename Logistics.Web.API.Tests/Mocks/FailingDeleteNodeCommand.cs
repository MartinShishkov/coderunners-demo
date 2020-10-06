using System;
using System.Threading.Tasks;
using Logistics.Services.Nodes.Commands;

namespace Logistics.Web.API.Tests.Mocks
{
    public class FailingDeleteNodeCommand : IDeleteNodeCommand
    {
        public Task<bool> ExecuteAsync(int nodeId)
        {
            throw new InvalidOperationException("This exception is for testing purposes");
        }
    }
}