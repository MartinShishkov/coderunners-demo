using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Logistics.Models;
using Logistics.Services.Dtos;

namespace Logistics.Services.Nodes.Queries
{
    public class GetNodesDbQuery : IGetNodesQuery
    {
        private readonly IDbConnectionFactory connectionFactory;

        public GetNodesDbQuery(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public async Task<IEnumerable<Node>> ExecuteAsync()
        {
            using var conn = await this.connectionFactory.OpenAsync();

            var dtos = await conn.QueryAsync<NodeDto>("SELECT * FROM dbo.Nodes");
            if(dtos == null)
                return new Node[0];

            return dtos.Select(dto => new Node(dto.Id, dto.Name));
        }
    }
}