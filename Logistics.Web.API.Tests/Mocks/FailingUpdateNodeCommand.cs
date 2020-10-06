using System;
using System.Threading.Tasks;
using Logistics.Services.Nodes.Commands;

namespace Logistics.Web.API.Tests.Mocks
{
    public class FailingUpdateNodeCommand : IUpdateNodeCommand
    {
        public Task<bool> ExecuteAsync(int id, string name)
        {
            throw new InvalidOperationException("This exception is for testing purposes");
        }
    }
}