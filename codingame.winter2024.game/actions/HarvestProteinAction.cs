using System;
using System.Collections.Generic;
using System.Linq;

class HarvestProteinAction : PlaceAdjacentAction
{
    public HarvestProteinAction(Game state) : base(state) { }

    public override void Execute()
    {
        if (cell != null && position != null && direction != null)
        {
            Console.WriteLine($"GROW {cell.Organ.Id} {position.Vector.X} {position.Vector.Y} HARVESTER {direction}");
        }
    }

    protected override IEnumerable<TargetData> GetTargets(Cell forCell)
    {
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
                .Where(t => t.Distance == 1 && !State.ProteinsBeingHarvested.Contains(t.Target));
    }

    protected override double GetScore(TargetData data)
    {
        return 101;
    }
}
