using System.Collections.Generic;
using System.Linq;
using System.Numerics;

abstract class PlaceAdjacentAction : Action
{
    protected Cell cell;
    protected Point? target;
    protected Point? position;
    protected Direction direction;

    public static Dictionary<Vector2, Direction> directionVectorsToKeys;
    public static Dictionary<Direction, Vector2> directionKeysToVectors = new Dictionary<Direction, Vector2>
    {
        { Direction.N, new Vector2(0, -1) }, // Up
        { Direction.S, new Vector2(0, 1) },  // Down
        { Direction.W, new Vector2(-1, 0) }, // Left
        { Direction.E, new Vector2(1, 0) },  // Right
    };

    public PlaceAdjacentAction(Game state) : base(state)
    {
        directionVectorsToKeys = directionKeysToVectors.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
    }

    public override double EvaluateScore(Cell forCell)
    {
        if (!EvaluateCost()) return -1;

        var targets = GetTargets(forCell);

        if (!targets.Any())
        {
            return -1;
        }

        var bestTarget = targets.First();
        cell = forCell;
        position = bestTarget.Position;
        target = bestTarget.Target;
        direction = bestTarget.Direction;

        return GetScore(bestTarget);
    }

    protected abstract IEnumerable<TargetData> GetTargets(Cell cell);

    protected virtual double GetScore(TargetData data)
    {
        return 100;
    }

    public static Direction GetDirection(Vector2 dir)
    {
        return directionKeysToVectors
            .FirstOrDefault(x => x.Value == dir)
            .Key;
    }

    public static List<Direction> GetClosestDirections(Vector2 tentacleDir)
    {
        // List to hold the closest directions
        var closestDirections = new List<Direction>();
        float maxDot = -1f;

        // Check each direction in your mapping
        foreach (var direction in directionVectorsToKeys)
        {
            float dotProduct = Vector2.Dot(tentacleDir, direction.Key);

            // Check for the closest direction(s)
            if (dotProduct > maxDot)
            {
                maxDot = dotProduct;
                closestDirections.Clear();
                closestDirections.Add(direction.Value);
            }
            else if (dotProduct == maxDot)
            {
                closestDirections.Add(direction.Value);
            }
        }

        return closestDirections;
    }

    public override string ToString()
    {
        return $"{base.ToString()} {cell} > {target} ({position} {direction})";
    }
}
