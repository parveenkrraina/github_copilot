using System;
using System.Collections.Generic;

namespace Lab2.StarterCode
{
    public class PathFinder
    {
        public List<int> FindOptimalPath(int[,] grid, int start, int end)
        {
            var visited = new HashSet<int>();
            var queue = new Queue<int>();
            queue.Enqueue(start);
            var parent = new Dictionary<int, int>();
            
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current == end) break;
                if (visited.Contains(current)) continue;
                
                visited.Add(current);
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    if (grid[current, i] == 1 && ! visited.Contains(i))
                    {
                        queue. Enqueue(i);
                        parent[i] = current;
                    }
                }
            }
            
            var path = new List<int>();
            var node = end;
            while (parent.ContainsKey(node))
            {
                path. Add(node);
                node = parent[node];
            }
            path.Reverse();
            return path;
        }
    }
}