using System;
using System.Collections.Generic;

public class DoNothingAction : Action
{
    public DoNothingAction(Game state) : base(state) { }

    public override Dictionary<ProteinType, int> Cost()
    {
        return new Dictionary<ProteinType, int>() {
            {ProteinType.A, 0},
            {ProteinType.B, 0},
            {ProteinType.C, 0},
            {ProteinType.D, 0}
        };
    }

    public override double EvaluateScore(Cell entity) => 0;

    public override void Execute()
    {
        Console.WriteLine("WAIT");
    }
}
