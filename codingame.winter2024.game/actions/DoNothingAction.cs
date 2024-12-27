using System;

public class DoNothingAction : Action
{
    public DoNothingAction(Game state) : base(state) { }

    public override double EvaluateScore(Cell entity) => 0;

    public override void Execute()
    {
        Console.WriteLine("WAIT");
    }
}
