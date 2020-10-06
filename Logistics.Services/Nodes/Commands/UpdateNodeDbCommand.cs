using System;
using System.Threading.Tasks;
using Dapper;

namespace Logistics.Services.Nodes.Commands
{
    public class UpdateNodeDbCommand : IUpdateNodeCommand
    {
        private readonly IDbConnectionFactory connectionFactory;

        public UpdateNodeDbCommand(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public async Task<bool> ExecuteAsync(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            using var conn = await connectionFactory.OpenAsync();

            var affectedRows = await conn.ExecuteAsync("UPDATE dbo.Nodes SET Name=@Name WHERE id=@Id",
                new {Id = id, Name = name});

            return affectedRows > 0;
        }
    }
}