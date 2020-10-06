using System.Threading.Tasks;

namespace Logistics.Services.Edges.Commands
{
    public interface IDeleteEdgeCommand
    {
        Task<bool> ExecuteAsync(int from, int to);
    }
}