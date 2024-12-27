namespace codingame.winter2024.test;

public class PointShould
{
    [Fact]
    public void ConstructSuccesfully()
    {
        var p = new Point(0, 0);

        Assert.Equal(0, p.Vector.X);
        Assert.Equal(0, p.Vector.Y);
    }

    [Theory]
    [InlineData(0, 0, 0, 0, 0)]
    [InlineData(0, 0, 1, 1, 2)]
    [InlineData(0, 0, 1, 0, 1)]
    [InlineData(0, 0, 0, 1, 1)]
    [InlineData(0, 0, 4, 4, 8)]
    [InlineData(1, 1, 0, 0, 2)]
    [InlineData(1, 0, 0, 0, 1)]
    [InlineData(0, 1, 0, 0, 1)]
    [InlineData(4, 4, 0, 0, 8)]
    public void ReturnDistanceTo(int p1x, int p1y, int p2x, int p2y, int distance)
    {
        var p = new Point(p1x, p1y);
        var p2 = new Point(p2x, p2y);

        Assert.Equal(distance, p.DistanceTo(p2));
    }

    [Theory]
    [InlineData(0, 0)]  // Lower bounds
    [InlineData(9, 9)]  // Upper bounds
    [InlineData(5, 5)]  // Middle of grid
    public void BeValidWithinGridBounds(int x, int y)
    {
        var p = new Point(x, y);

        Assert.True(p.IsValid(10, 10));
    }

    [Theory]
    [InlineData(10, 9)]     // X out of bounds
    [InlineData(9, 10)]     // Y out of bounds
    [InlineData(-1, 5)]     // Negative X
    [InlineData(5, -1)]     // Negative Y
    [InlineData(-1, -1)]    // Both negative
    [InlineData(10, 10)]    // Both out of bounds
    public void BeInvalidOutsideGridBounds(int x, int y)
    {
        var p = new Point(x, y);

        Assert.False(p.IsValid(10, 10));
    }
}
