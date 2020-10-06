using System;
using System.Threading.Tasks;
using Dapper;

namespace Logistics.Services.Edges.Commands
{
    public class CreateEdgeDbCommand : ICreateEdgeCommand
    {
        private readonly IDbConnectionFactory connectionFactory;

        public CreateEdgeDbCommand(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public async Task<bool> ExecuteAsync(int @from, int to)
        {
            try
            {
                using var conn = await connectionFactory.OpenAsync();
                var res = await conn.ExecuteAsync("INSERT INTO dbo.Edges (From_Id, To_Id) VALUES (@FromId, @ToId)",
                    new {FromId = from, ToId = to});

                return res > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}