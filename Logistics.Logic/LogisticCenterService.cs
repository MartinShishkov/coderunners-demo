using System;

namespace Logistics.Logic
{
    public class LogisticCenterService
    {
        private readonly AdjacencyMatrix adjacencyMatrix;

        public LogisticCenterService(AdjacencyMatrix adjacencyMatrix)
        {
            this.adjacencyMatrix = adjacencyMatrix 
                ?? throw new ArgumentNullException(nameof(adjacencyMatrix));
        }

        public int CalculateLogisticCenter()
        {
            this.Floyd(adjacencyMatrix);

            return FindCenter(adjacencyMatrix);
        }

        private void Floyd(AdjacencyMatrix matrix)
        {
            var count = matrix.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (matrix.Get(i, j) == 0)
                        matrix.Set(100000, i, j);
                }
            }

            for (int k = 0; k < count; k++)
            {
                for (int i = 0; i < count; i++)
                {
                    for (int j = 0; j < count; j++)
                    {
                        var sum = matrix.Get(i, k) + matrix.Get(k, j);

                        if (matrix.Get(i, j) > sum)
                        {
                            matrix.Set(sum, i, j);
                        }
                    }
                }
            }

            for (int i = 0; i < count; i++)
            {
                matrix.Set(0, i, i);
            }
        }

        private int FindCenter(AdjacencyMatrix matrix)
        {
            int max, center = -1, min = 100000;
            var count = matrix.Count;

            for (int i = 0; i < count; i++)
            {
                max = matrix.Get(i, 0) + matrix.Get(0, i);
                for (int j = 0; j < count; j++)
                {
                    var sum = matrix.Get(i, j) + matrix.Get(j, i);
                    if ((i != j) && (sum > max))
                    {
                        max = sum;
                    }
                }

                if (max <= min)
                {
                    min = max;
                    center = i;
                }
            }

            if (center == -1)
                return center;

            return matrix.GetElementAt(center);
        }
    }
}