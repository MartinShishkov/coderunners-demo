using System.Threading.Tasks;
using Logistics.Models;

namespace Logistics.Services.Nodes.Commands
{
    public interface ICreateNodeCommand
    {
        Task<Node> ExecuteAsync(string name);
    }
}