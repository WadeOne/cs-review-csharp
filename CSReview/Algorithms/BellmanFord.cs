using System.Collections.Generic;
using Xunit;

namespace CSReview.Algorithms
{
    public class BellmanFord
    {
        public (int?[] distances, int[] path) FindShortestPaths(int[] vertices, (int u, int v, int weight)[] edges, int source)
        {
            var distances = new int?[vertices.Length];
            distances[source - 1] = 0;
            distances[source - 1] = 0;
            var path = new int[vertices.Length];

            for (int i = 0; i < vertices.Length - 1; i++)
            {
                foreach (var (u, v, weight) in edges)
                {
                    Relax(u, v, weight);
                }
            }

            return (distances, path);

            void Relax(int u, int v, int weight)
            {
                var newDistance = weight + (distances[u - 1] ?? 0); 
                if (distances[v - 1] == null || distances[v - 1] > newDistance)
                {
                    distances[v - 1] = newDistance;
                    path[v - 1] = u;
                }
            }
        }

        [Fact]
        public void Test()
        {
            var vertices = new int[] {1, 2, 3, 4, 5, 6, 7};
            var edges = new (int u, int v, int weight)[]
            {
                (u: 1, v: 2, weight: 5),
                (u: 1, v: 3, weight: 2),
                (u: 2, v: 3, weight: 7),
                (u: 2, v: 4, weight: 12),
                (u: 3, v: 5, weight: 10),
                (u: 3, v: 7, weight: 6),
                (u: 4, v: 5, weight: 4),
                (u: 4, v: 6, weight: 2),
                (u: 5, v: 6, weight: 3),
            };

            var res = FindShortestPaths(vertices, MakeUndirectedEdges(edges), 1);
        }

        private (int u, int v, int weight)[] MakeUndirectedEdges((int u, int v, int weight)[] directedEdges)
        {
            var list = new List<(int, int, int)>();
            foreach (var directedEdge in directedEdges)
            {
                list.Add(directedEdge);
                list.Add((directedEdge.v, directedEdge.u, directedEdge.weight));
            }

            return list.ToArray();
        }
    }
}