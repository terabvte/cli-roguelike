namespace cli_roguelike
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Roguelike Capstone";
            Console.CursorVisible = false;

            var map = new Map(width: 140, height: 32);
            var renderEngine = new RenderEngine(map.Width, map.Height);
            var random = new Random();

            (int startX, int startY) = map.Generate();
            var player = new Player(startX, startY);

            var gameState = GameState.PlayerTurn;

            while (true)
            {
                var allActors = new List<Actor>(map.Monsters);
                allActors.Add(player);

                renderEngine.Draw(map, allActors);

                switch (gameState)
                {
                    case GameState.PlayerTurn:
                    {
                        var keyInfo = Console.ReadKey(true);
                        int newPlayerX = player.X;
                        int newPlayerY = player.Y;

                        switch (keyInfo.Key)
                        {
                            case ConsoleKey.UpArrow:
                            case ConsoleKey.W: newPlayerY--; break;
                            case ConsoleKey.DownArrow:
                            case ConsoleKey.S: newPlayerY++; break;
                            case ConsoleKey.LeftArrow:
                            case ConsoleKey.A: newPlayerX--; break;
                            case ConsoleKey.RightArrow:
                            case ConsoleKey.D: newPlayerX++; break;
                            case ConsoleKey.Escape: return;
                        }

                        var targetTile = map.GetTile(newPlayerX, newPlayerY);
                        if (targetTile.Type == TileType.Floor)
                        {
                            player.X = newPlayerX;
                            player.Y = newPlayerY;
                            gameState = GameState.MonsterTurn;
                        }

                        break;
                    }
                    case GameState.MonsterTurn:
                    {
                        // thanks gippity
                        // --- MONSTER AI LOGIC ---
                        foreach (var monster in map.Monsters)
                        {
                            if (map.IsInLineOfSight(monster.X, monster.Y, player.X, player.Y))
                            {
                                int newMonsterX = monster.X;
                                int newMonsterY = monster.Y;

                                // --- NEW "STUPIDITY CHECK" LOGIC ---
                                // On a 1-in-5 chance...
                                if (random.Next(1, 6) == 5)
                                {
                                    // ...make a dumb, random move.
                                    int randomDirection = random.Next(0, 4);
                                    switch (randomDirection)
                                    {
                                        case 0: newMonsterY--; break; // Up
                                        case 1: newMonsterY++; break; // Down
                                        case 2: newMonsterX--; break; // Left
                                        case 3: newMonsterX++; break; // Right
                                    }
                                }
                                else
                                {
                                    // ...otherwise, make the smart move towards the player.
                                    int dx = player.X - monster.X;
                                    int dy = player.Y - monster.Y;

                                    if (Math.Abs(dx) > Math.Abs(dy))
                                    {
                                        newMonsterX += Math.Sign(dx);
                                    }
                                    else
                                    {
                                        newMonsterY += Math.Sign(dy);
                                    }
                                }

                                // --- COLLISION CHECK (runs for both smart and dumb moves) ---
                                var targetTile = map.GetTile(newMonsterX, newMonsterY);
                                if (targetTile.Type == TileType.Floor &&
                                    !(newMonsterX == player.X && newMonsterY == player.Y))
                                {
                                    monster.X = newMonsterX;
                                    monster.Y = newMonsterY;
                                }
                            }
                        }

                        gameState = GameState.PlayerTurn;
                        break;
                    }
                }
            }
        }
    }
}
