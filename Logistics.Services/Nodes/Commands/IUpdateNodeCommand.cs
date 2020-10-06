using System.Threading.Tasks;

namespace Logistics.Services.Nodes.Commands
{
    public interface IUpdateNodeCommand
    {
        Task<bool> ExecuteAsync(int id, string name);
    }
}