namespace codingame.winter2024.test;

public class PointShould
{
    [Fact]
    public void ConstructSuccesfully()
    {
        var p = new Point(0, 0);

        Assert.Equal(0, p.X);
        Assert.Equal(0, p.Y);
    }
}
