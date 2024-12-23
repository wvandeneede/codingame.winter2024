public class Cell
{
    public Point? Position { get; set; }
    public bool isWall { get; set; }
    public ProteinType Protein { get; set; }
    public Organ? Organ { get; set; }
}
