namespace codingame.winter2024.test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var p = new Point(0, 0);

        Assert.Equal(0, p.X);
        Assert.Equal(0, p.Y);
    }
}
