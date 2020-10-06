using System.Collections.Generic;
using System.Threading.Tasks;
using Logistics.Models;

namespace Logistics.Services.Nodes.Queries
{
    public interface IGetNodesQuery
    {
        Task<IEnumerable<Node>> ExecuteAsync();
    }
}