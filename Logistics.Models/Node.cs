using System;

namespace Logistics.Models
{
    public class Node
    {
        public Node(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if(id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id value must be greater than 0");

            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; }
    }
}