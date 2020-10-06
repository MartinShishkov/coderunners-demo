using System;
using System.Threading.Tasks;
using Dapper;
using Logistics.Models;
using Logistics.Services.Dtos;

namespace Logistics.Services.Nodes.Queries
{
    public class GetNodeByIdDbQuery : IGetNodeByIdQuery
    {
        private readonly IDbConnectionFactory connectionFactory;

        public GetNodeByIdDbQuery(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory 
                ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public async Task<Node> ExecuteAsync(int id)
        {
            using var conn = await this.connectionFactory.OpenAsync();

            var dto = await conn.QueryFirstOrDefaultAsync<NodeDto>("SELECT * FROM dbo.Nodes WHERE dbo.Nodes.Id = @Id", new {Id = id});
            return new Node(dto.Id, dto.Name);
        }
    }
}