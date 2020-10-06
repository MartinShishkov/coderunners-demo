using System;
using System.Threading.Tasks;
using Logistics.Models;
using Logistics.Services.Nodes.Commands;

namespace Logistics.Web.API.Tests.Mocks
{
    public class FailingCreateNodeCommand : ICreateNodeCommand
    {
        public Task<Node> ExecuteAsync(string name)
        {
            throw new InvalidOperationException("This exception is for testing purposes");
        }
    }
}