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

            (int startX, int startY) = map.Generate();
            var player = new Player(startX, startY);

            var gameState = GameState.PlayerTurn;

            while (true)
            {
                var allActors = new List<Actor>(map.Monsters);
                allActors.Add(player);

                renderEngine.Draw(map, allActors);

                if (gameState == GameState.PlayerTurn)
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
                }
                else if (gameState == GameState.MonsterTurn)
                {
                    // monster AI here

                    var allMonsters = new List<Actor>(map.Monsters);

                    foreach (var monster in allMonsters)
                    {
                        int dx = monster.X - player.X;
                        int dy = monster.Y - player.Y;

                        int newMonsterX;
                        int newMonsterY;

                        if (Math.Abs(dx) > Math.Abs(dy))
                        {
                            var tileToMove = Math.Sign(dx);

                            newMonsterX = monster.X - tileToMove;
                            newMonsterY = monster.Y;
                        }
                        else
                        {
                            var tileToMove = Math.Sign(dy);

                            newMonsterX = monster.X;
                            newMonsterY = monster.Y - tileToMove;
                        }

                        if (!((map.GetTile(newMonsterX, newMonsterY).Type == TileType.Wall) ||
                              (player.X == newMonsterX) && (player.Y == newMonsterY)))
                        {
                            monster.X = newMonsterX;
                            monster.Y = newMonsterY;
                        }
                    }

                    gameState = GameState.PlayerTurn;
                }
            }
        }
    }
}
