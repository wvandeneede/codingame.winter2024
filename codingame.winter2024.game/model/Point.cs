using System;
using System.Collections.Generic;
using System.Numerics;

public class Point : IComparable<Point>, IComparable
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

    public int CompareTo(Point? other)
    {
        if (other is null) return 1;

        // First compare X values
        int xComparison = Vector.X.CompareTo(other.Vector.X);
        if (xComparison != 0) return xComparison;

        // If X values are equal, compare Y values
        return Vector.Y.CompareTo(other.Vector.Y);
    }

    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;

        if (obj is not Point point)
            throw new ArgumentException("Object is not a Point", nameof(obj));

        return CompareTo(point);
    }

    public static bool operator <(Point left, Point right)
    {
        if (right is null) return false;
        if (left is null) return true;
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(Point left, Point right)
    {
        if (left is null) return false;
        if (right is null) return true;
        return left.CompareTo(right) > 0;
    }

    public static bool operator <=(Point left, Point right)
    {
        if (left is null) return right is null;
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >=(Point left, Point right)
    {
        if (right is null) return left is null;
        return left.CompareTo(right) >= 0;
    }

    // It's also good practice to override Equals and GetHashCode
    public override bool Equals(object? obj)
    {
        if (obj is not Point other) return false;
        return Vector.Equals(other.Vector);
    }

    public override int GetHashCode()
    {
        return Vector.GetHashCode();
    }

    // Optional: Implement == and != operators for consistency
    public static bool operator ==(Point left, Point right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(Point left, Point right)
    {
        return !(left == right);
    }
}
