using System.Threading.Tasks;

namespace Logistics.Services.Nodes.Commands
{
    public interface IDeleteNodeCommand
    {
        Task<bool> ExecuteAsync(int nodeId);
    }
}