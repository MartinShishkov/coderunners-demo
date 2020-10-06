using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Logistics.Services
{
    public sealed class DbConnectionFactory : IDbConnectionFactory {
        private readonly string connectionString;

        public DbConnectionFactory(string connectionString) {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(connectionString));
            this.connectionString = connectionString;
        }

        public async Task<IDbConnection> OpenAsync() {
            var sqlConnection = new SqlConnection(this.connectionString);
            await sqlConnection.OpenAsync();
            return sqlConnection;
        }
    }
}