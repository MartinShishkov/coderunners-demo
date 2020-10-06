using System.Threading.Tasks;

namespace Logistics.Services.Nodes.Queries
{
    public interface IGetLogisticCenterIdQuery
    {
        Task<int> ExecuteAsync();
    }
}