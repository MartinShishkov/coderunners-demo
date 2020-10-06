using System;

namespace Logistics.Models
{
    public class Edge
    {
        public Edge(int @from, int to)
        {
            if(from <= 0)
                throw new ArgumentOutOfRangeException(nameof(from), "\'From\' value must be greater than 0");

            if(to <= 0)
                throw new ArgumentOutOfRangeException(nameof(to), "\'To\' value must be greater than 0");

            From = @from;
            To = to;
        }

        public int From { get; }
        public int To { get; }
    }
}