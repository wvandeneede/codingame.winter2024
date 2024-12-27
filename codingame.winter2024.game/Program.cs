using System;
using System.Collections.Generic;

class Player
{
    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        var width = int.Parse(inputs[0]); // columns in the game grid
        var height = int.Parse(inputs[1]); // rows in the game grid

        var game = new Game(width, height);
        var planner = new ActionPlanner();

        // game loop
        while (true)
        {
            int entityCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < entityCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int x = int.Parse(inputs[0]);
                int y = int.Parse(inputs[1]); // grid coordinate
                string type = inputs[2]; // WALL, ROOT, BASIC, TENTACLE, HARVESTER, SPORER, A, B, C, D
                game.Grid[x, y].Owner = int.Parse(inputs[3]); // 1 if your organ, 0 if enemy organ, -1 if neither
                int organId = int.Parse(inputs[4]); // id of this entity if it's an organ, 0 otherwise
                string organDir = inputs[5]; // N,E,S,W or X if not an organ
                int organParentId = int.Parse(inputs[6]);
                int organRootId = int.Parse(inputs[7]);

                if (type == "WALL")
                {
                    game.Grid[x, y].IsWall = true;
                }
                else if (type == "A" || type == "B" || type == "C" || type == "D")
                {
                    game.Grid[x, y].Protein = (ProteinType)Enum.Parse(typeof(ProteinType), type);
                }
                else
                {
                    var organType = (OrganType)Enum.Parse(typeof(OrganType), type);
                    var direction = (Direction)Enum.Parse(typeof(Direction), organDir);
                    var organ = new Organ(organId, new Point(x, y), organType, organRootId, direction, organParentId);
                    game.Grid[x, y].Protein = null;
                    game.Grid[x, y].Organ = organ;
                }
            }

            inputs = Console.ReadLine().Split(' ');
            game.MyProteins = new Dictionary<ProteinType, int>
            {
                { ProteinType.A, int.Parse(inputs[0]) },
                { ProteinType.B, int.Parse(inputs[1]) },
                { ProteinType.C, int.Parse(inputs[2]) },
                { ProteinType.D, int.Parse(inputs[3]) }
            };

            game.UpdateProteins();

            inputs = Console.ReadLine().Split(' ');
            int oppA = int.Parse(inputs[0]);
            int oppB = int.Parse(inputs[1]);
            int oppC = int.Parse(inputs[2]);
            int oppD = int.Parse(inputs[3]); // opponent's protein stock

            int requiredActionsCount = int.Parse(Console.ReadLine()); // your number of organisms, output an action for each one in any order
            for (int i = 0; i < requiredActionsCount; i++)
            {
                var action = planner.PlanAction(game);
                action?.Execute();
            }
        }
    }
}