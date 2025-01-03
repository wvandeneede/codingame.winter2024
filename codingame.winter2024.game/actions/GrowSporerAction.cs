
using System;
using System.Collections.Generic;
using System.Linq;

class GrowSporerAction : PlaceAdjacentAction
{
    public GrowSporerAction(Game state) : base(state) { }

    public override void Execute()
    {
        if (cell != null && position != null && direction != null)
        {
            Console.WriteLine($"GROW {cell.Organ.Id} {position.Vector.X} {position.Vector.Y} SPORER {direction}");
        }
    }

    protected override IEnumerable<TargetData> GetTargets(Cell forCell)
    {
        var allCellsForRoot = State.MyCells.Where(cell => cell.Organ?.RootId == forCell.Organ?.RootId);
        var sporers = allCellsForRoot.Where(cell => cell.Organ?.Type == OrganType.SPORER);

        if (sporers.Count() > 0) return [];

        return State.ProteinSourcePositions
            .Where(protein => protein.GetNeighbours().Any(n => State.IsValidTile(n)))
            .SelectMany(protein => protein.GetNeighbours()
                .Where(neighbour => State.IsValidTile(neighbour))
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

    protected override double GetScore(TargetData data)
    {
        return 61;
    }

    public override Dictionary<ProteinType, int> Cost()
    {
        return new Dictionary<ProteinType, int>() {
            {ProteinType.A, 0},
            {ProteinType.B, 1},
            {ProteinType.C, 0},
            {ProteinType.D, 1}
        };
    }
}
