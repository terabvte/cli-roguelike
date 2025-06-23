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
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
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

                    Tile targetTile = map.GetTile(newPlayerX, newPlayerY);
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

                    gameState = GameState.PlayerTurn; 
                }
            }
        }
    }
}
