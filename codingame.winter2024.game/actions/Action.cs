using System.Collections.Generic;

public abstract class Action
{
    protected Game State { get; }

    public abstract Dictionary<ProteinType, int> Cost();
    public abstract double EvaluateScore(Cell cell);
    public abstract void Execute();

    protected Action(Game state)
    {
        State = state;
    }

    public override string ToString()
    {
        return $"{GetType()}";
    }

    public Action Clone()
    {
        return (Action)MemberwiseClone();
    }

    public bool EvaluateCost()
    {
        var stock = State.MyProteins;
        var cost = Cost();

        return stock[ProteinType.A] >= cost[ProteinType.A] && stock[ProteinType.B] >= cost[ProteinType.B] && stock[ProteinType.C] >= cost[ProteinType.C] && stock[ProteinType.D] >= cost[ProteinType.D];
    }
}