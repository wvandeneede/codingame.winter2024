using System;
using System.Collections.Generic;
using System.Linq;

public class AStar
{
    public class PathResult
    {
        public List<Point> path;
        public int score;
    }

    class PathNode
    {
        public Point Position { get; }
        public PathNode Parent { get; }
        public int G { get; } // Cost from start to this node.
        public int H { get; } // Heuristic cost from this node to target.

        public int F => G + H; // Total cost.

        public PathNode(Point position, PathNode parent, int g, int h)
        {
            Position = position;
            Parent = parent;
            G = g;
            H = h;
        }
    }

    private readonly Game state;

    public AStar(Game state)
    {
        this.state = state;
    }

    public PathResult FindPath(Point start, Point target, Func<Point, int> getPathMovementCost = null)
    {
        var openSet = new SortedSet<PathNode>(Comparer<PathNode>.Create((a, b) =>
        {
            int compare = a.F.CompareTo(b.F);
            return compare == 0 ? a.Position.GetHashCode().CompareTo(b.Position.GetHashCode()) : compare;
        }));

        var closedSet = new HashSet<Point>();
        openSet.Add(new PathNode(start, null, 0, Heuristic(start, target)));

        while (openSet.Count > 0)
        {
            var current = openSet.First();
            openSet.Remove(current);

            if (current.Position.Equals(target))
                return ReconstructPath(current);

            closedSet.Add(current.Position);

            foreach (var neighbour in current.Position.GetNeighbours().Where(p => p.IsValid(state.Width, state.Height)))
            {
                if (closedSet.Contains(neighbour) || !state.IsValidTile(neighbour))
                    continue;

                int cost = getPathMovementCost?.Invoke(neighbour) ?? 1;
                int g = current.G + cost;
                int h = Heuristic(neighbour, target);

                var neighborNode = new PathNode(neighbour, current, g, h);

                var existingNode = openSet.FirstOrDefault(n => n.Position.Equals(neighbour));
                if (existingNode == null || neighborNode.G < existingNode.G)
                {
                    if (existingNode != null)
                        openSet.Remove(existingNode);
                    openSet.Add(neighborNode);
                }
            }
        }

        // No path found.
        return null;
    }

    private PathResult ReconstructPath(PathNode node)
    {
        var path = new List<Point>();
        int gCost = 0;
        while (node != null)
        {
            if (node.Parent != null)
            {
                path.Add(node.Position);
            }

            gCost += node.G - (node.Parent?.G ?? 0);
            node = node.Parent;
        }

        path.Reverse();
        return new PathResult
        {
            path = path,
            score = gCost,
        };
    }

    private int Heuristic(Point a, Point b)
    {
        return Math.Abs((int)a.Vector.X - (int)b.Vector.X)
            + Math.Abs((int)a.Vector.Y - (int)b.Vector.Y);
    }
}
