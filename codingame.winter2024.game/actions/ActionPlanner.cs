using System;
using System.Collections.Generic;
using System.Linq;

public class ActionPlanner
{
    public Action PlanAction(Game state, Cell rootCell, int maxEval)
    {
        var actions = new List<Action>
        {
            new SporeAction(state),
            new GrowSporerAction(state),
            new DefendAction(state),
            new HarvestProteinAction(state),
            new CaptureProteinAction(state),
            new MoveTowardsProteinAction(state),
            new GrowRandomlyAction(state),
            new DoNothingAction(state)
        };

        Action bestAction = null;
        double bestScore = double.MinValue;

        var cellsToBeEvaluated = state.MyCells.Where(cell => cell.Organ?.RootId == rootCell.Organ?.Id)
        .OrderByDescending(cell => cell.Position.DistanceTo(rootCell.Position))
        .Take(maxEval)
        .ToList();

        foreach (var cell in cellsToBeEvaluated)
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

                if (score > 50) break;
            }
        }

        Console.Error.WriteLine($"Selected: {bestAction}");
        return bestAction;
    }
}
