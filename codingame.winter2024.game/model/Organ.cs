public class Organ
{
    public int Id { get; set; }
    public Point? Position { get; set; }
    public OrganType Type { get; set; }
    public Direction Direction { get; set; }
    public int RootId { get; set; }
    public int parentId { get; set; }
    public int Owner { get; set; }
}
