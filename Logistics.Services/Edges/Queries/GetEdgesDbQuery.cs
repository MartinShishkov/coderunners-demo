using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Logistics.Models;
using Logistics.Services.Dtos;

namespace Logistics.Services.Edges.Queries
{
    public class GetEdgesDbQuery : IGetEdgesQuery
    {
        private readonly IDbConnectionFactory connectionFactory;

        public GetEdgesDbQuery(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public async Task<IEnumerable<Edge>> ExecuteAsync()
        {
            using var conn = await this.connectionFactory.OpenAsync();

            var dtos = await conn.QueryAsync<EdgeDto>("select * from dbo.Edges");
            if(dtos == null)
                return new Edge[0];

            return dtos.Select(dto => new Edge(dto.From_Id, dto.To_Id));
        }
    }
}