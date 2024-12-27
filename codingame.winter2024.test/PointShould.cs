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
    public void ReturnDistanceTo(int p1x, int p1y, int p2x, int p2y, int distance)
    {
        var p = new Point(p1x, p1y);
        var p2 = new Point(p2x, p2y);

        Assert.Equal(distance, p.DistanceTo(p2));
    }

    [Fact]
    public void BeValidWithinGridBounds()
    {
        var p = new Point(0, 0);

        Assert.True(p.IsValid(10, 10));
    }

    [Fact]
    public void BeInvalidOutsideGridBounds()
    {
        var p = new Point(11, 11);

        Assert.False(p.IsValid(10, 10));
    }
}
