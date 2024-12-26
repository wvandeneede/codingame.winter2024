namespace codingame.winter2024.test;

public class GameShould
{
    [Fact]
    public void ConstructSuccesfully()
    {
        var game = new Game(10, 10);

        for (var y = 0; y < 10; y++)
        {
            for (var x = 0; x < 10; x++)
            {
                Assert.Equal(x, game.Grid[x, y].Position.X);
                Assert.Equal(y, game.Grid[x, y].Position.Y);
            }
        }
    }
}
