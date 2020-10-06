using System.Data;
using System.Threading.Tasks;

namespace Logistics.Services
{
    public interface IDbConnectionFactory {
        Task<IDbConnection> OpenAsync();
    }
}