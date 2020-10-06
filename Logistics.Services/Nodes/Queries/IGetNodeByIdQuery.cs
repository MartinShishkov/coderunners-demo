using System.Threading.Tasks;
using Logistics.Models;

namespace Logistics.Services.Nodes.Queries
{
    public interface IGetNodeByIdQuery
    {
        Task<Node> ExecuteAsync(int id);
    }
}