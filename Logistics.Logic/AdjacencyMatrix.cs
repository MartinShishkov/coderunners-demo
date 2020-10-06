using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logistics.Models;

namespace Logistics.Logic
{
    public class AdjacencyMatrix
    {
        private readonly int[,] matrix;
        private Dictionary<int, int> elementToIndexMap;
        private Dictionary<int, int> indexToElement;

        public AdjacencyMatrix(int[] vertices, Edge[] edges)
        {
            if (vertices == null) throw new ArgumentNullException(nameof(vertices));
            if (edges == null) throw new ArgumentNullException(nameof(edges));

            matrix = ToMatrix(vertices, edges);
        }

        public int Count => this.matrix.GetLength(0);

        private int[,] ToMatrix(int[] vertices, Edge[] edges)
        {
            var adjacencyMatrix = new int[vertices.Length, vertices.Length];
            var set = new SortedSet<int>(vertices);

            this.elementToIndexMap = new Dictionary<int, int>(set.Select((el, index) => new KeyValuePair<int, int>(el, index)));
            this.indexToElement = new Dictionary<int, int>(set.Select((el, index) => new KeyValuePair<int, int>(index, el)));
            
            foreach (var edge in edges)
            {
                var row = elementToIndexMap[edge.From];
                var col = elementToIndexMap[edge.To];

                adjacencyMatrix[row, col] = 1;
                adjacencyMatrix[col, row] = 1;
            }

            return adjacencyMatrix;
        }

        public string Identity()
        {
            var sb = new StringBuilder(matrix.Length);
            var count = matrix.GetLength(1);
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    sb.Append(matrix[i, j]);
                }
            }

            return sb.ToString();
        }

        public int GetElementAt(int index)
        {
            if(this.indexToElement.ContainsKey(index))
                return this.indexToElement[index];

            return -1;
        }

        public void Set(int value, int i, int j)
        {
            if (this.matrix.GetUpperBound(0) < i ||
                this.matrix.GetUpperBound(1) < j)
                return;

            this.matrix[i, j] = value;
        }

        public int Get(int i, int j)
        {
            if (this.matrix.GetUpperBound(0) < i ||
                this.matrix.GetUpperBound(1) < j)
                return -1;

            return this.matrix[i, j];
        }
    }
}