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

    [Fact]
    public void ReturnDistanceTo()
    {
        var p = new Point(0, 0);
        var p2 = new Point(4, 4);

        var distance = p.DistanceTo(p2);

        Assert.Equal(8, distance);
    }
}
