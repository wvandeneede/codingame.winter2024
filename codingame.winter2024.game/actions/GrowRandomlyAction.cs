using System;
using System.Linq;

public class GrowRandomlyAction : Action
{
    private Cell cell;
    private Point? target;

    public GrowRandomlyAction(Game state) : base(state) { }

    public override double EvaluateScore(Cell forCell)
    {
        var neighbours = forCell.Position.GetNeighbours().Where(State.IsValidTile).ToList();
        if (!neighbours.Any()) return -1;

        var random = new Random();
        cell = forCell;
        target = neighbours[random.Next(neighbours.Count())];

        return 0.3; // Low priority
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
