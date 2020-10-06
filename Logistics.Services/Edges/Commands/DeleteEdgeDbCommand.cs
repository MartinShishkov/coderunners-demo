using System;
using System.Threading.Tasks;
using Dapper;

namespace Logistics.Services.Edges.Commands
{
    public class DeleteEdgeDbCommand : IDeleteEdgeCommand
    {
        private readonly IDbConnectionFactory connectionFactory;

        public DeleteEdgeDbCommand(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public async Task<bool> ExecuteAsync(int @from, int to)
        {
            try
            {
                using var conn = await connectionFactory.OpenAsync();

                var res = await conn.ExecuteAsync(
                    "DELETE FROM dbo.Edges WHERE (dbo.Edges.From_Id = @FromId AND dbo.Edges.To_Id = @ToId) OR (dbo.Edges.From_Id = @ToId AND dbo.Edges.To_Id = @FromId)",
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