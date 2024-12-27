using System.Numerics;

public class Point
{
    public Vector2 Vector { get; }

    public Point(int x, int y)
    {
        Vector = new Vector2(x, y);
    }

    public Point(float x, float y)
    {
        Vector = new Vector2(x, y);
    }

    public int DistanceTo(Point other)
    {
        return Math.Abs((int)Vector.X - (int)other.Vector.X)
            + Math.Abs((int)Vector.Y - (int)other.Vector.Y);
    }

    public IEnumerable<Point> GetNeighbours()
    {
        yield return new Point(Vector.X - 1, Vector.Y);
        yield return new Point(Vector.X + 1, Vector.Y);
        yield return new Point(Vector.X, Vector.Y - 1);
        yield return new Point(Vector.X, Vector.Y + 1);
    }

    public bool IsValid(int width, int height)
    {
        return Vector.X >= 0
            && Vector.X < width
            && Vector.Y >= 0
            && Vector.Y < height;
    }

    public override string ToString()
    {
        return $"({Vector.X},{Vector.Y})";
    }
}
