using System.Threading.Tasks;

namespace Logistics.Services.Edges.Commands
{
    public interface ICreateEdgeCommand
    {
        Task<bool> ExecuteAsync(int from, int to);
    }
}