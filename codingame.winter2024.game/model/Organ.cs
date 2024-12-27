public class Organ
{
    public int Id { get; set; }
    public Point? Position { get; set; }
    public OrganType Type { get; set; }
    public Direction Direction { get; set; }
    public int RootId { get; set; }
    public int ParentId { get; set; }

    public Organ(int id, Point position, OrganType type, int rootId, Direction direction, int parentId)
    {
        Id = id;
        Position = position;
        Type = type;
        RootId = rootId;
        Direction = direction;
        ParentId = parentId;
    }

    public override string ToString()
    {
        return $"organ_{Type}_{Id}_{Position}";
    }
}
