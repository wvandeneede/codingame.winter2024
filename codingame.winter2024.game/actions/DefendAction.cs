using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class DefendAction : PlaceAdjacentAction
{
    public DefendAction(Game state) : base(state) { }

    public override void Execute()
    {
        if (cell != null && position != null && direction != null)
        {
            Console.WriteLine($"GROW {cell.Organ.Id} {position.Vector.X} {position.Vector.Y} TENTACLE {direction}");
        }
    }

    protected override IEnumerable<TargetData> GetTargets(Cell forCell)
    {
        if (State.myTentaclePositions.Count > 0) return [];

        return State.OpponentCells
            .Where(organs => organs.Position.GetNeighbours().Any(n => State.IsValidTile(n)))
            .SelectMany(organs => organs.Position.GetNeighbours()
                .Where(neighbour => State.IsValidTile(neighbour))
                .Select(neighbour => new TargetData
                {
                    Origin = forCell.Position,
                    Target = organs.Position,
                    Position = neighbour,
                    Distance = forCell.Position.DistanceTo(neighbour),
                    Direction = GetClosestDirections(organs.Position.Vector - neighbour.Vector)
                        .FirstOrDefault(dir => IsFacingEmpty(neighbour, dir)),
                }))
                .Where(t => t.Distance == 1)
                .OrderBy(t => t.Distance);
    }

    protected bool IsFacingEmpty(Point position, Direction direction)
    {
        if (!directionKeysToVectors.ContainsKey(direction))
        {
            return false;
        }

        Vector2 offset = directionKeysToVectors[direction];
        Vector2 targetPos = position.Vector + offset;

        int targetX = (int)targetPos.X;
        int targetY = (int)targetPos.Y;

        if (targetX >= 0
            && targetX < State.Width
            && targetY >= 0
            && targetY < State.Height)
        {
            return State.OccupiedPositions.Contains(new Point(targetX, targetY));
        }

        return false;
    }

    protected override double GetScore(TargetData data)
    {
        return data.Distance <= 2
            ? 150
            : 0.1;
    }

    public override Dictionary<ProteinType, int> Cost()
    {
        return new Dictionary<ProteinType, int>() {
            {ProteinType.A, 0},
            {ProteinType.B, 1},
            {ProteinType.C, 1},
            {ProteinType.D, 0}
        };
    }
}
