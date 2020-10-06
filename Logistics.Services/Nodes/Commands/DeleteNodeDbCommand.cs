using System;
using System.Threading.Tasks;
using Dapper;

namespace Logistics.Services.Nodes.Commands
{
    public class DeleteNodeDbCommand : IDeleteNodeCommand
    {
        private readonly IDbConnectionFactory connectionFactory;

        public DeleteNodeDbCommand(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public async Task<bool> ExecuteAsync(int nodeId)
        {
            using var conn = await connectionFactory.OpenAsync();
            using var tran = conn.BeginTransaction();

            var deletedEdges = await conn.ExecuteAsync(
                "DELETE FROM dbo.Edges WHERE From_Id = @id OR To_Id = @id",
                new {id = nodeId}, tran);

            var res = await conn.ExecuteAsync(
                "DELETE FROM dbo.Nodes WHERE Id = @id",
                new {id = nodeId}, tran);

            tran.Commit();

            return res > 0;
        }
    }
}