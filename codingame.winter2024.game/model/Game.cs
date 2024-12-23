namespace codingame.winter2024.game.model;

public class Game
{
    public Cell[][] Grid { get; set; }
    public Dictionary<ProteinType, int> MyProteins { get; set; }
    public Dictionary<ProteinType, int> OppProteins { get; set; }
    public Organ[] MyOrgans { get; set; }
    public Organ[] OppOrgans { get; set; }
    public Dictionary<int, Organ> OrganMap { get; set; }
}
