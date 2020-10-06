using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.Services.Edges.Queries
{
    public interface IGetEdgesQuery
    {
        Task<IEnumerable<Models.Edge>> ExecuteAsync();
    }
}