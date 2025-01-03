using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

public class MoveTowardsProteinAction : Action
{
    private Cell cell;
    private Point? target;
    private List<Point> path;

    public MoveTowardsProteinAction(Game state) : base(state) { }

    public override double EvaluateScore(Cell forCell)
    {
        if (forCell.Organ == null) return -1;
        if (!EvaluateCost()) return -1;

        var targets = State.ProteinSourcePositions
            .Where(p => !State.OccupiedPositions.Contains(p)
                && !State.ProteinsBeingHarvested.Contains(p)
                && forCell.Position.DistanceTo(p) > 1)
            .OrderBy(p => forCell.Position.DistanceTo(p))
            .Take(3)
            .Select(p => new
            {
                Location = p,
                Distance = forCell.Position.DistanceTo(p),
                Path = GetPath(forCell.Position, p),
            })
            .Where(x => x.Path != null);

        if (!targets.Any()) return -1;

        var closest = targets
            .OrderBy(t => t.Path.score)
            .First();

        cell = forCell;
        target = closest.Location;
        path = closest.Path.path;

        return 10.0 / (closest.Path.score + 1);
    }

    public override void Execute()
    {
        if (cell != null && target != null)
        {
            Point? nextStep = path.First();
            if (nextStep != null)
            {
                Console.WriteLine($"GROW {cell.Organ.Id} {nextStep.Vector.X} {nextStep.Vector.Y} BASIC");
            }
        }
    }

    private AStar.PathResult GetPath(Point start, Point target)
    {
        var pathfinder = new AStar(State);
        return pathfinder.FindPath(
            start,
            target,
            pos => State.ProteinsBeingHarvested.Contains(pos) ? 7 : 1
        );
    }

    public override string ToString()
    {
        return $"{base.ToString()} {cell} > {target} | [{string.Join(", ", path)}]";
    }

    public override Dictionary<ProteinType, int> Cost()
    {
        return new Dictionary<ProteinType, int>() {
            {ProteinType.A, 1},
            {ProteinType.B, 0},
            {ProteinType.C, 0},
            {ProteinType.D, 0}
        };
    }
}
