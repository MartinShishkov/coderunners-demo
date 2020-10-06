using System.Threading.Tasks;

namespace Logistics.Services.Edges.Commands
{
    public interface ISetEdgesCommand
    {
        Task<bool> ExecuteAsync(int fromId, int[] destinationIds);
    }
}