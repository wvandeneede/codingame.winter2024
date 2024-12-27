public abstract class Action
{
    protected Game State { get; }
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
}