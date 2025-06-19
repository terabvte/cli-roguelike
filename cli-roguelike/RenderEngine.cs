namespace cli_roguelike;

public class RenderEngine
{
    public void Draw(Map map, Player player)
    {
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                var currentTile = map.GetTile(x, y);

                switch (currentTile.Type)
                {
                    case TileType.Wall:
                        Console.Write("#");
                        break;

                    case TileType.Floor:
                        Console.Write(".");
                        break;

                    default:
                        return;
                }
            }

            Console.WriteLine();
        }

        Console.SetCursorPosition(player.X, player.Y);
        Console.Write(player.Marker);
    }
}
