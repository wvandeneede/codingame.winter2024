public class Game
{
    public Cell[,] Grid { get; init; }
    public Dictionary<ProteinType, int> MyProteins { get; set; }
    public Dictionary<ProteinType, int> OppProteins { get; set; }

    public Game(int width, int height)
    {
        this.Grid = new Cell[width, height];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                this.Grid[x, y] = new Cell(new Point(x, y), false, null, null);
            }
        }

        this.MyProteins = [];
        this.OppProteins = [];
    }
}
