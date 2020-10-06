using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Logistics.Logic;
using Logistics.Services.Edges.Queries;

namespace Logistics.Services.Nodes.Queries
{
    public class GetLogisticCenterIdDbQuery : IGetLogisticCenterIdQuery
    {
        private readonly IDbConnectionFactory connectionFactory;
        private readonly IGetEdgesQuery getEdges;
        private readonly IGetNodesQuery _getNodes;

        public GetLogisticCenterIdDbQuery(
            IGetEdgesQuery getEdges,
            IGetNodesQuery getNodes,
            IDbConnectionFactory connectionFactory)
        {
            this.getEdges = getEdges 
                ?? throw new ArgumentNullException(nameof(getEdges));
            this.connectionFactory = connectionFactory 
                ?? throw new ArgumentNullException(nameof(connectionFactory));
            this._getNodes = getNodes 
                ?? throw new ArgumentNullException(nameof(getNodes));
        }

        public async Task<int> ExecuteAsync()
        {
            var edges = await getEdges.ExecuteAsync();
            var nodes = await _getNodes.ExecuteAsync();

            var adjMatrix = new AdjacencyMatrix(nodes.Select(s => s.Id).ToArray(), edges.ToArray());

            var identity = adjMatrix.Identity();
            var identitiesEqual = await IsEqualToLastIdentityAsync(identity);
            if (identitiesEqual)
                return await GetCenterIdAsync();

            var service = new LogisticCenterService(adjMatrix);
            var nodeId = service.CalculateLogisticCenter();
            await this.UpsertIdAsync(nodeId);
            await this.UpsertIdentityAsync(identity);

            return nodeId;
        }

        private async Task UpsertIdAsync(int id)
        {
            using var conn = await connectionFactory.OpenAsync();
            var count = await conn.ExecuteScalarAsync<int>("SELECT COUNT(1) FROM dbo.LogisticCenter");
            if (count == 0)
            {
                await conn.ExecuteAsync("INSERT INTO dbo.LogisticCenter (Id) VALUES (@Id)", new {Id = id});
            }
            else
            {
                await conn.ExecuteAsync("UPDATE dbo.LogisticCenter SET Id = @Id", new {Id = id});
            }
        }

        private async Task UpsertIdentityAsync(string identity)
        {
            using var conn = await connectionFactory.OpenAsync();
            var count = await conn.ExecuteScalarAsync<int>("SELECT COUNT(1) FROM dbo.LastGraphIdentity");
            if (count == 0)
            {
                await conn.ExecuteAsync("INSERT INTO dbo.LastGraphIdentity ([Identity]) VALUES (@Identity)", new {Identity = identity});
            }
            else
            {
                await conn.ExecuteAsync("UPDATE dbo.LastGraphIdentity SET [Identity] = @Identity", new {Identity = identity});
            }
        }

        private async Task<int> GetCenterIdAsync()
        {
            using var conn = await this.connectionFactory.OpenAsync();

            var id = await conn.QueryFirstOrDefaultAsync<int>("select Id from dbo.LogisticCenter");

            return id;
        }

        private async Task<bool> IsEqualToLastIdentityAsync(string identity)
        {
            if (string.IsNullOrWhiteSpace(identity))
                return false;

            var lastIdentity = await GetLastGraphIdentityAsync();

            return identity == lastIdentity;
        }

        private async Task<string> GetLastGraphIdentityAsync()
        {
            using var conn = await this.connectionFactory.OpenAsync();

            var identity = await conn.QueryFirstOrDefaultAsync<string>("select [Identity] from dbo.LastGraphIdentity");

            return identity;
        }
    }
}