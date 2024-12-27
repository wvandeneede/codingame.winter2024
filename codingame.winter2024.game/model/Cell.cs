public class Cell
{
    public Point? Position { get; set; }
    public bool IsWall { get; set; }
    public ProteinType? Protein { get; set; }
    public Organ? Organ { get; set; }
    public int Owner { get; set; }
    public bool Harvesting { get; set; }

    public Cell(Point? position, bool isWall, ProteinType? protein, Organ? organ)
    {
        Position = position;
        IsWall = isWall;
        Protein = protein;
        Organ = organ;
        Harvesting = false;
    }

    public override string ToString()
    {
        return $"cell_{Position}_{IsWall}_{Protein}_{Organ}";
    }
}
