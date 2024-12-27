using System.Collections.Generic;
using System.Linq;

public class Game
{
    public int Width { get; init; }
    public int Height { get; init; }
    public Cell[,] Grid { get; init; }
    public Dictionary<ProteinType, int> MyProteins { get; set; }
    public Dictionary<ProteinType, int> OppProteins { get; set; }

    public List<Point?> WallPositions => Grid.Cast<Cell>().Where(x => x.IsWall).Select(x => x.Position).ToList();
    public List<Point?> OccupiedPositions => Grid.Cast<Cell>().Where(x => x.Organ != null).Select(x => x.Position).ToList();
    public List<Cell> MyCells => Grid.Cast<Cell>().Where(x => x.Owner == 1 && x.Organ != null).ToList();


    public List<Point> ProteinSourcePositions => Grid.Cast<Cell>().Where(x => x.Protein != null).Select(x => x.Position).ToList();

    public HashSet<Point> ProteinsBeingHarvested { get; private set; }

    public Game(int width, int height)
    {
        Width = width;
        Height = height;

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
        this.ProteinsBeingHarvested = [];
    }

    public void UpdateProteins()
    {
        ProteinsBeingHarvested.Clear();

        var myHarvesters = MyCells.Where(e => e.Organ != null && e.Organ.Type == OrganType.HARVESTER);

        foreach (var harvester in myHarvesters)
        {
            var adjacentProteins = ProteinSourcePositions
                .Where(p => p.GetNeighbours().Contains(harvester.Position));
            foreach (var protein in adjacentProteins)
            {
                ProteinsBeingHarvested.Add(protein);
            }
        }
    }

    public bool IsValidTile(Point pos)
    {
        // Proteins are valid destinations, but walls and occupied tiles are not.
        return pos.IsValid(Width, Height) &&
               !OccupiedPositions.Contains(pos) &&
               !WallPositions.Contains(pos);
    }
}
