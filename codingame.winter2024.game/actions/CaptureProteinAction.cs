using System;
using System.Linq;

public class CaptureProteinAction : Action
{
    private Cell cell;
    private Point? target;

    public CaptureProteinAction(Game state) : base(state) { }

    public override double EvaluateScore(Cell forCell)
    {
        var adjacentProteins = State.ProteinSourcePositions
            .Where(p => p.GetNeighbours().Contains(forCell.Position))
            .ToList();

        if (!adjacentProteins.Any()) return -1;

        cell = forCell;
        target = adjacentProteins.First();

        return State.ProteinsBeingHarvested.Contains(target)
            ? 0.1
            : 0.2;
    }

    public override void Execute()
    {
        if (cell != null && target != null)
        {
            Console.WriteLine($"GROW {cell.Organ.Id} {target.Vector.X} {target.Vector.Y} BASIC");
        }
    }

    public override string ToString()
    {
        return $"{base.ToString()} {cell} > {target}";
    }
}
