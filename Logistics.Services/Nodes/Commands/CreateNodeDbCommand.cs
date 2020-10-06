using System;
using System.Threading.Tasks;
using Dapper;
using Logistics.Models;

namespace Logistics.Services.Nodes.Commands
{
    public class CreateNodeDbCommand : ICreateNodeCommand
    {
        private readonly IDbConnectionFactory connectionFactory;

        public CreateNodeDbCommand(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public async Task<Node> ExecuteAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            
            using var conn = await connectionFactory.OpenAsync();

            var id = await conn.ExecuteScalarAsync<int>("INSERT INTO dbo.Nodes (Name) VALUES (@Name); SELECT CAST(SCOPE_IDENTITY() as int)",
                new {Name = name});

            return new Node(id, name);
        }
    }
}