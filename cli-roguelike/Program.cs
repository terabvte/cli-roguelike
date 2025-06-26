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
                var allActors = new List<Actor>(map.Monsters) { player };
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

                        // Player makes a decision: Attack or Move
                        var monsterAtTarget = map.Monsters.FirstOrDefault(m => m.X == newPlayerX && m.Y == newPlayerY);

                        if (monsterAtTarget != null)
                        {
                            // --- ATTACK LOGIC (Phase 3) ---
                            Console.WriteLine("Player attacks the Goblin!"); // Placeholder
                            gameState = GameState.MonsterTurn;
                        }
                        else if (map.GetTile(newPlayerX, newPlayerY).Type == TileType.Floor)
                        {
                            // --- MOVE LOGIC ---
                            player.X = newPlayerX;
                            player.Y = newPlayerY;
                            gameState = GameState.MonsterTurn;
                        }

                        break;
                    }

                    case GameState.MonsterTurn:
                    {
                        // --- MONSTER AI LOGIC (REFACTORED AND COMPLETE) ---
                        foreach (var monster in map.Monsters)
                        {
                            // 1. PERCEIVE: The monster only acts if it can see the player.
                            if (map.IsInLineOfSight(monster.X, monster.Y, player.X, player.Y))
                            {
                                // 2. DECIDE: Calculate the intended move.
                                int newMonsterX = monster.X;
                                int newMonsterY = monster.Y;

                                // On a 1-in-5 chance, make a random move.
                                if (random.Next(1, 6) == 5)
                                {
                                    int randomDirection = random.Next(0, 4);
                                    switch (randomDirection)
                                    {
                                        case 0: newMonsterY--; break;
                                        case 1: newMonsterY++; break;
                                        case 2: newMonsterX--; break;
                                        case 3: newMonsterX++; break;
                                    }
                                }
                                else // Otherwise, make the smart move towards the player.
                                {
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

                                // 3. ACT: Validate the chosen move and commit to it.
                                // First, check if the destination is the player's tile.
                                if (newMonsterX == player.X && newMonsterY == player.Y)
                                {
                                    // --- ATTACK LOGIC (Phase 3) ---
                                    Console.WriteLine("Goblin attacks the Player!"); // Placeholder
                                }
                                // Else, check if the destination is an empty floor tile.
                                else if (map.GetTile(newMonsterX, newMonsterY).Type == TileType.Floor)
                                {
                                    // Check that no other monster is already on that tile.
                                    if (!map.Monsters.Any(m => m.X == newMonsterX && m.Y == newMonsterY))
                                    {
                                        monster.X = newMonsterX;
                                        monster.Y = newMonsterY;
                                    }
                                }
                            }
                        }

                        // After all monsters have taken their turn, give control back to the player.
                        gameState = GameState.PlayerTurn;
                        break;
                    }
                }
            }
        }
    }
}
