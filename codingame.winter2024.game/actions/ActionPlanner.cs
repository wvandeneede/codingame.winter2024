using System;
using System.Collections.Generic;
using System.Linq;

public class ActionPlanner
{
    public Action PlanAction(Game state, Cell rootCell)
    {
        var actions = new List<Action>
        {
            new DefendAction(state),
            new HarvestProteinAction(state),
            new CaptureProteinAction(state),
            new MoveTowardsProteinAction(state),
            new GrowRandomlyAction(state),
            new DoNothingAction(state)
        };

        Action bestAction = null;
        double bestScore = double.MinValue;

        foreach (var cell in state.MyCells.Where(cell => cell.Organ?.RootId == rootCell.Organ?.Id))
        {
            foreach (var a in actions)
            {
                var action = a.Clone();
                double score = action.EvaluateScore(cell);

                if (score > -1)
                {
                    Console.Error.WriteLine($"Evaluating {score}: {action}");
                }

                if (score > bestScore)
                {
                    bestAction = action;
                    bestScore = score;
                }
            }
        }

        Console.Error.WriteLine($"Selected: {bestAction}");
        return bestAction;
    }
}
