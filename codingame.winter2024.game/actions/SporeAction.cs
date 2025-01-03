using System;
using System.Collections.Generic;
using System.Linq;

class SporeAction : PlaceAdjacentAction
{
    public SporeAction(Game state) : base(state) { }

    public override Dictionary<ProteinType, int> Cost()
    {
        return new Dictionary<ProteinType, int>() {
            {ProteinType.A, 1},
            {ProteinType.B, 1},
            {ProteinType.C, 1},
            {ProteinType.D, 1}
        };
    }

    public override void Execute()
    {
        if (cell != null && position != null)
        {
            Console.WriteLine($"SPORE {cell.Organ.Id} {position.Vector.X} {position.Vector.Y}");
        }
    }

    protected override double GetScore(TargetData data)
    {
        return 71;
    }

    protected override IEnumerable<TargetData> GetTargets(Cell forCell)
    {
        if (forCell.Organ?.Type != OrganType.SPORER) return [];

        return State.ProteinSourcePositions
            .Where(protein => protein.GetNeighbours().Any(n => State.IsValidTile(n)))
            .SelectMany(protein => protein.GetNeighbours())
            .SelectMany(protein => protein.GetNeighbours()
                .Where(neighbour => State.IsValidTile(neighbour))
                .Where(target => State.IsValidTile(target) && (target.Vector.X == forCell.Position.Vector.X || target.Vector.Y == forCell.Position.Vector.Y))
                .Select(neighbour => new TargetData
                {
                    Origin = forCell.Position,
                    Target = protein,
                    Position = neighbour,
                    Distance = forCell.Position.DistanceTo(neighbour),
                    Direction = GetDirection(protein.Vector - neighbour.Vector),
                }))
                .Where(t => t.Distance > 3 && t.Distance < State.Width && !State.ProteinsBeingHarvested.Contains(t.Target));
    }
}
